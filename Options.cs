//Kamil Hawdziejuk
//03-06-2010

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InteractingMeshes
{
    public partial class Options : Form
    {
        [Flags]
        public enum CollisionStage
        {
            /// <summary>
            /// Test on boxes
            /// </summary>
            Box = 1,
            /// <summary>
            /// Test on GJK algorithm
            /// </summary>
            GJK = 2,
            /// <summary>
            /// Test on BSP structure
            /// </summary>
            BSP = 4,
            /// <summary>
            /// No tested performed
            /// </summary>
            None = 0
        }


        #region --- Creating and destroying objects ---

        /// <summary>
        /// Constructor
        /// </summary>
        public Options()
        {
            InitializeComponent();
        }

        #endregion

        #region --- Fields ---

        /// <summary>
        /// Collision level
        /// </summary>
        public static CollisionStage CollisionLevel = CollisionStage.None;

        #endregion

        #region --- Public properties ---

        public System.Windows.Forms.ListBox ListObjects
        {
            get
            {
                return this.list;
            }
        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == this.checkBoxTest)
            {
                if (this.checkBoxTest.Checked)
                {
                    CollisionLevel |= CollisionStage.Box;
                }
                else
                {
                    CollisionLevel -= CollisionStage.Box;
                }
            }
            if (sender == this.checkGJKTest)
            {
                if (this.checkGJKTest.Checked)
                {
                    CollisionLevel |= CollisionStage.GJK;
                }
                else
                {
                    CollisionLevel -= CollisionStage.GJK;
                }
            }
            if (sender == this.checkBSPTest)
            {
                if (this.checkBSPTest.Checked)
                {
                    CollisionLevel |= CollisionStage.BSP;
                    this.autopartitioningBtn.Checked = true;
                }
                else
                {
                    CollisionLevel -= CollisionStage.BSP;
                }
            }

            SpaceApplication.Instance.Focus();
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }

        private void deleteObject_Click(object sender, EventArgs e)
        {
            SpaceApplication.Manager.RemoveActiveObject();
            SpaceApplication.Instance.Focus();
        }

        private void boxAdd_Click(object sender, EventArgs e)
        {
            SpaceApplication.Manager.Add("box");
            SpaceApplication.Instance.Focus();
        }

        private void torusAdd_Click(object sender, EventArgs e)
        {
            SpaceApplication.Manager.Add("torus");
            SpaceApplication.Instance.Focus();
        }

        private void sphereAdd_Click(object sender, EventArgs e)
        {
            SpaceApplication.Manager.AddSphere(Double.Parse(radiusBox.Text),
                Int32.Parse(slicesBox.Text), Int32.Parse(stacksBox.Text));
            SpaceApplication.Instance.Focus();
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListControl).SelectedIndex != -1)
            {
                SpaceApplication.Manager.ActiveObject =
                    SpaceApplication.Manager.Objects[(sender as ListControl).SelectedIndex];
            }
            SpaceApplication.Instance.Focus();
        }

        private void list_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<GeometricObject> selected = new List<GeometricObject>();

            foreach (GeometricObject sel in this.ListObjects.SelectedItems)
            {
                SpaceApplication.Manager.RemoveObject(sel);
                selected.Add(sel);
            }

            foreach (var sel in selected)
            {
                this.ListObjects.Items.Remove(sel);
            }
            SpaceApplication.Instance.Focus();
        }

        private void autopartitioningBtn_CheckedChanged(object sender, EventArgs e)
        {
            BSPNode.Autopartitioning = true;
        }

        private void parallelPlaneBtn_CheckedChanged(object sender, EventArgs e)
        {
            BSPNode.Autopartitioning = false;
        }
    }
}
