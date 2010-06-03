//Kamil Hawdziejuk
//16-05-2010

using System;
using Microsoft.DirectX;

namespace InteractingMeshes
{
    /// <summary>
    /// Prostopadloscian zorientowany zgodnie z osiami ukladu.
    /// Przydatny jako struktura ograniczajaca przestrzen 3D.
    /// </summary>
    public struct AABox : IEquatable<AABox>
    {
        #region --- Metody statyczne ---

        /// <summary>
        /// Zwraca box o wymiarach 1 x 1 x 1 i środku w punkcie (0, 0, 0)
        /// </summary>
        public static AABox UnitBox
        {
            get { return new AABox(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f)); }
        }

        /// <summary>
        /// Zwraca tablice wierzchołków, opisujących podany prostopadłościan. Współrzędne
        /// wierzchołków są w układzie lokalnym.
        /// </summary>
        /// <param name="_box">Prostopadłościan do opisania</param>
        /// <returns>Tablica zawierająca współrzędne prostopadłościanu</returns>
        public static Vector3[] GetBoxVertices(AABox _box)
        {
            // zwracane wierzcholki
            var _vertices = new Vector3[8];

            // plaszczyzna dalsza, mniejszy Z.
            _vertices[0] = _box.min;
            _vertices[1] = _box.min + new Vector3(_box.Size.X, 0, 0);
            _vertices[2] = _box.min + new Vector3(_box.Size.X, _box.Size.Y, 0);
            _vertices[3] = _box.min + new Vector3(0, _box.Size.Y, 0);

            // plaszczyzna blizcza, wiekszy Z.
            _vertices[4] = _box.min + new Vector3(0, 0, _box.Size.Z);
            _vertices[5] = _box.min + new Vector3(_box.Size.X, 0, _box.Size.Z);
            _vertices[6] = _box.min + new Vector3(_box.Size.X, _box.Size.Y, _box.Size.Z);
            _vertices[7] = _box.min + new Vector3(0, _box.Size.Y, _box.Size.Z);

            return _vertices;
        }

        #endregion

        #region --- Tworzenie i usuwanie obiektów ---

        /// <summary>
        /// Konstruktor z dwoch punktow
        /// </summary>
        /// <param name="_Min">minimum</param>
        /// <param name="_Max">maksimum</param>
        public AABox(Vector3 _Min, Vector3 _Max)
        {
            _min = _Min;
            _max = _Max;
            isInitialized = true;
        }

        #endregion

        #region --- Metody publiczne ---

        /// <summary>
        /// Zwraca narozniki prostopadloscianu
        /// </summary>
        /// <returns></returns>
        public Vector3[] GetCorners()
        {
            var vecs = new Vector3[8];
            vecs[0] = _min;
            vecs[1] = new Vector3(_min.X, _min.Y, _max.Z);
            vecs[2] = new Vector3(_min.X, _max.Y, _min.Z);
            vecs[3] = new Vector3(_min.X, _max.Y, _max.Z);
            vecs[4] = new Vector3(_max.X, _min.Y, _min.Z);
            vecs[5] = new Vector3(_max.X, _min.Y, _max.Z);
            vecs[6] = new Vector3(_max.X, _max.Y, _min.Z);
            vecs[7] = _max;
            return vecs;
        }

        /// <summary>
        /// Okresla czy prostopadloscian jest poprawnie okreslony
        /// </summary>
        /// <returns></returns>
        public bool IsInitialized()
        {
            return isInitialized;
        }

        /// <summary>
        /// Zwraca przetransformowany AABox
        /// </summary>
        /// <param name="_Position">Przesuniecie</param>
        /// <param name="_Orientation">Obrot</param>
        /// <param name="_Scale">skala</param>
        /// <returns></returns>
        public AABox Transform(Vector3 _Position, Matrix _Orientation, Vector3 _Scale)
        {
            if (!IsInitialized())
            {
                return new AABox();
            }

            Vector3[] vecs = GetCorners();
            for (int i = 0; i < 8; ++i)
            {
                Vector3 vect = GeometricUtils.MultiplyVectors(_Scale, vecs[i]);
                vecs[i] = GeometricUtils.MultiplyMatrixVector(_Orientation, vect);
                vecs[i] += _Position;
            }

            var b = new AABox();
            b.isInitialized = true;
            b._min = vecs[0];
            b._max = vecs[0];
            for (int i = 1; i < 8; ++i)
            {
                b._min = GeometricUtils.FloorVector(b._min, vecs[i]);
                b._max = GeometricUtils.CeilVector(b._max, vecs[i]);
            }

            return b;
        }

        /// <summary>
        /// Indicates weather this box intersects other box or is inside
        /// </summary>
        public bool Intersects(AABox box)
        {
            if (!IsInitialized() || !box.IsInitialized())
                return false;

            // Check by planes
            if (_max.X < box._min.X)
                return false;
            if (_max.Y < box._min.Y)
                return false;
            if (_max.Z < box._min.Z)
                return false;

            if (_min.X > box._max.X)
                return false;
            if (_min.Y > box._max.Y)
                return false;
            if (_min.Z > box._max.Z)
                return false;

            // Intersecting
            return true;
        }

        /// <summary>
        /// Indicates weather this box totally encloses other box
        /// </summary>
        public bool Contains(AABox box)
        {
            if (!IsInitialized() || !box.IsInitialized())
                return false;

            // Check by planes
            if (_max.X < box._max.X)
                return false;
            if (_max.Y < box._max.Y)
                return false;
            if (_max.Z < box._max.Z)
                return false;

            if (_min.X > box._min.X)
                return false;
            if (_min.Y > box._min.Y)
                return false;
            if (_min.Z > box._min.Z)
                return false;

            // Intersecting
            return true;
        }

        /// <summary>
        /// Indicates weather a point is inside this box
        /// </summary>
        public bool Contains(Vector3 _point)
        {
            if (!IsInitialized())
                return false;

            // Check by planes
            if (_max.X < _point.X)
                return false;
            if (_max.Y < _point.Y)
                return false;
            if (_max.Z < _point.Z)
                return false;

            if (_min.X > _point.X)
                return false;
            if (_min.Y > _point.Y)
                return false;
            if (_min.Z > _point.Z)
                return false;

            // Inside
            return true;
        }

        /// <summary>
        /// Indicates weather a point is inside this box, with given epsilon
        /// </summary>
        public bool Contains(Vector3 _point, Vector3 _epsilon)
        {
            if (!IsInitialized())
                return false;

            // Check by planes
            if (_max.X + _epsilon.X < _point.X)
                return false;
            if (_max.Y + _epsilon.Y < _point.Y)
                return false;
            if (_max.Z + _epsilon.Z < _point.Z)
                return false;

            if (_min.X > _point.X + _epsilon.X)
                return false;
            if (_min.Y > _point.Y + _epsilon.Y)
                return false;
            if (_min.Z > _point.Z + _epsilon.Z)
                return false;

            // Inside
            return true;
        }

        #endregion

        #region --- Właściwości publiczne ---

        /// <summary>
        /// minimum AABoxa
        /// </summary>
        public Vector3 min
        {
            get { return _min; }
            set
            {
                _min = value;
                isInitialized = true;
            }
        }

        /// <summary>
        /// maksimum AABoxa
        /// </summary>
        public Vector3 max
        {
            get { return _max; }
            set
            {
                _max = value;
                isInitialized = true;
            }
        }

        /// <summary>
        /// Rozmiar prostopadloscianu
        /// </summary>
        public Vector3 Size
        {
            get { return _max - _min; }
        }

        /// <summary>
        /// Srodek prostopadloscianu
        /// </summary>
        public Vector3 Center
        {
            get { return 0.5f*(_max + _min); }
        }

        #endregion

        #region --- Operatory ---

        /// <summary>
        /// Dodawanie dwóch boxów
        /// </summary>
        /// <param name="b1">Box 1</param>
        /// <param name="b2">Box 2</param>
        /// <returns>Box będący sumą podanych boxów (czyli taki, który zawiera podane boxy)</returns>
        public static AABox operator +(AABox b1, AABox b2)
        {
            if (!b1.IsInitialized())
                return b2;
            if (!b2.IsInitialized())
                return b1;

            var b = new AABox();
            b.isInitialized = true;
            b._min = b1._min;
            b._min = GeometricUtils.FloorVector(b._min, b2._min);

            b._max = b1._max;
            b._max = GeometricUtils.CeilVector(b._max, b2._max);

            return b;
        }

        /// <summary>
        /// Dodaje punkt do boxa - box jest rozciągany w taki sposób, aby zawierał podany punkt.
        /// </summary>
        /// <param name="b1">Box</param>
        /// <param name="v">Punkt</param>
        /// <returns>Zwraca box zawierający podany punkt</returns>
        public static AABox operator +(AABox b1, Vector3 v)
        {
            if (!b1.IsInitialized())
            {
                return new AABox(v, v);
            }
            var b = new AABox();
            b.isInitialized = true;
            b._min = b1._min;
            b._min = GeometricUtils.FloorVector(b._min, v);

            b._max = b1._max;
            b._max = GeometricUtils.CeilVector(b._max, v);

            return b;
        }

        /// <summary>
        /// Operator porównania 2 boxów z domyślną dokładnością <see cref="Math3D.Epsilon"/>
        /// </summary>
        /// <param name="_b1">Box 1</param>
        /// <param name="_b2">Box 2</param>
        /// <returns>Zwraca true, jeżeli boxy są równe, czyli mają ten sam środek i wymiary</returns>
        public static bool operator ==(AABox _b1, AABox _b2)
        {
            return _b1.Equals(_b2);
        }

        /// <summary>
        /// Operator porównania 2 boxów z domyślną dokładnością <see cref="Math3D.Epsilon"/>
        /// </summary>
        /// <param name="_b1">Box 1</param>
        /// <param name="_b2">Box 2</param>
        /// <returns>Zwraca true, jeżeli boxy są różne, czyli nie mają tego samego środka i wymiarów</returns>
        public static bool operator !=(AABox _b1, AABox _b2)
        {
            return !_b1.Equals(_b2);
        }

        #endregion

        #region --- Pola ---

        /// <summary>
        /// maksimum AABoxa
        /// </summary>
        private Vector3 _max;

        /// <summary>
        /// minimum AABoxa
        /// </summary>
        private Vector3 _min;

        /// <summary>
        /// Domyslnie jest false!
        /// </summary>
        private bool isInitialized;

        #endregion

        #region --- Implementacja interfejsu IEquatable<AABox> ---

        /// <summary>
        /// Sprawdza, czy podany box jest równy danemu. Boxy są równe, jeżeli mają ten sam środek i wymiary.
        /// </summary>
        /// <param name="_other">Porównywany box</param>
        /// <returns>Zwraca true, jeżeli boxy są równe</returns>
        public bool Equals(AABox _other)
        {
            return Equals(_other, 0.01);
        }

        /// <summary>
        /// Sprawdza, czy podany box jest równy danemu, z zadaną dokładnością. Boxy są równe, 
        /// jeżeli mają ten sam środek i wymiary.
        /// </summary>
        /// <param name="_other">Porównywany box</param>
        /// <param name="_epsilon">Dokładność obliczeń</param>
        /// <returns>Zwraca true, jeżeli boxy są równe</returns>
        public bool Equals(AABox _other, double _epsilon)
        {
            return GeometricUtils.EqualsVectors(Center, _other.Center) &&
                   GeometricUtils.EqualsVectors(Size, _other.Size);
        }

        #endregion
    } ;
}