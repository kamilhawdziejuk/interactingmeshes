namespace InteractingMeshes
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxTest = new System.Windows.Forms.CheckBox();
            this.checkGJKTest = new System.Windows.Forms.CheckBox();
            this.checkBSPTest = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.boxPage = new System.Windows.Forms.TabPage();
            this.boxAdd = new System.Windows.Forms.Button();
            this.torusPage = new System.Windows.Forms.TabPage();
            this.torusAdd = new System.Windows.Forms.Button();
            this.spherePage = new System.Windows.Forms.TabPage();
            this.sphereAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.list = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.boxPage.SuspendLayout();
            this.torusPage.SuspendLayout();
            this.spherePage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxTest
            // 
            this.checkBoxTest.AutoSize = true;
            this.checkBoxTest.Location = new System.Drawing.Point(16, 33);
            this.checkBoxTest.Name = "checkBoxTest";
            this.checkBoxTest.Size = new System.Drawing.Size(79, 17);
            this.checkBoxTest.TabIndex = 1;
            this.checkBoxTest.Text = "Test Boxes";
            this.checkBoxTest.UseVisualStyleBackColor = true;
            this.checkBoxTest.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkGJKTest
            // 
            this.checkGJKTest.AutoSize = true;
            this.checkGJKTest.Location = new System.Drawing.Point(16, 56);
            this.checkGJKTest.Name = "checkGJKTest";
            this.checkGJKTest.Size = new System.Drawing.Size(188, 17);
            this.checkGJKTest.TabIndex = 2;
            this.checkGJKTest.Text = "Test GJK (Gilbert Johnson Keerthi)";
            this.checkGJKTest.UseVisualStyleBackColor = true;
            this.checkGJKTest.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBSPTest
            // 
            this.checkBSPTest.AutoSize = true;
            this.checkBSPTest.Location = new System.Drawing.Point(16, 79);
            this.checkBSPTest.Name = "checkBSPTest";
            this.checkBSPTest.Size = new System.Drawing.Size(71, 17);
            this.checkBSPTest.TabIndex = 3;
            this.checkBSPTest.Text = "Test BSP";
            this.checkBSPTest.UseVisualStyleBackColor = true;
            this.checkBSPTest.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.boxPage);
            this.tabControl.Controls.Add(this.torusPage);
            this.tabControl.Controls.Add(this.spherePage);
            this.tabControl.Location = new System.Drawing.Point(16, 46);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(209, 110);
            this.tabControl.TabIndex = 4;
            // 
            // boxPage
            // 
            this.boxPage.Controls.Add(this.boxAdd);
            this.boxPage.Location = new System.Drawing.Point(4, 22);
            this.boxPage.Name = "boxPage";
            this.boxPage.Padding = new System.Windows.Forms.Padding(3);
            this.boxPage.Size = new System.Drawing.Size(201, 84);
            this.boxPage.TabIndex = 0;
            this.boxPage.Text = "Box";
            this.boxPage.UseVisualStyleBackColor = true;
            // 
            // boxAdd
            // 
            this.boxAdd.Location = new System.Drawing.Point(63, 16);
            this.boxAdd.Name = "boxAdd";
            this.boxAdd.Size = new System.Drawing.Size(75, 23);
            this.boxAdd.TabIndex = 0;
            this.boxAdd.Text = "Insert";
            this.boxAdd.UseVisualStyleBackColor = true;
            this.boxAdd.Click += new System.EventHandler(this.boxAdd_Click);
            // 
            // torusPage
            // 
            this.torusPage.Controls.Add(this.torusAdd);
            this.torusPage.Location = new System.Drawing.Point(4, 22);
            this.torusPage.Name = "torusPage";
            this.torusPage.Padding = new System.Windows.Forms.Padding(3);
            this.torusPage.Size = new System.Drawing.Size(201, 84);
            this.torusPage.TabIndex = 1;
            this.torusPage.Text = "Torus";
            this.torusPage.UseVisualStyleBackColor = true;
            // 
            // torusAdd
            // 
            this.torusAdd.Location = new System.Drawing.Point(56, 18);
            this.torusAdd.Name = "torusAdd";
            this.torusAdd.Size = new System.Drawing.Size(78, 24);
            this.torusAdd.TabIndex = 1;
            this.torusAdd.Text = "Insert";
            this.torusAdd.UseVisualStyleBackColor = true;
            this.torusAdd.Click += new System.EventHandler(this.torusAdd_Click);
            // 
            // spherePage
            // 
            this.spherePage.Controls.Add(this.sphereAdd);
            this.spherePage.Location = new System.Drawing.Point(4, 22);
            this.spherePage.Name = "spherePage";
            this.spherePage.Size = new System.Drawing.Size(201, 84);
            this.spherePage.TabIndex = 2;
            this.spherePage.Text = "Sphere";
            this.spherePage.UseVisualStyleBackColor = true;
            // 
            // sphereAdd
            // 
            this.sphereAdd.Location = new System.Drawing.Point(64, 19);
            this.sphereAdd.Name = "sphereAdd";
            this.sphereAdd.Size = new System.Drawing.Size(75, 23);
            this.sphereAdd.TabIndex = 0;
            this.sphereAdd.Text = "Insert";
            this.sphereAdd.UseVisualStyleBackColor = true;
            this.sphereAdd.Click += new System.EventHandler(this.sphereAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.checkGJKTest);
            this.groupBox1.Controls.Add(this.checkBoxTest);
            this.groupBox1.Controls.Add(this.checkBSPTest);
            this.groupBox1.Location = new System.Drawing.Point(27, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 109);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collision tests:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.list);
            this.groupBox2.Controls.Add(this.tabControl);
            this.groupBox2.Location = new System.Drawing.Point(27, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 264);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Objects";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Adding:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Existing:";
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(23, 175);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(136, 69);
            this.list.TabIndex = 6;
            this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
            this.list.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.list_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 29);
            this.button1.TabIndex = 9;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(281, 435);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.tabControl.ResumeLayout(false);
            this.boxPage.ResumeLayout(false);
            this.torusPage.ResumeLayout(false);
            this.spherePage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxTest;
        private System.Windows.Forms.CheckBox checkGJKTest;
        private System.Windows.Forms.CheckBox checkBSPTest;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage boxPage;
        private System.Windows.Forms.TabPage torusPage;
        private System.Windows.Forms.Button torusAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button boxAdd;
        private System.Windows.Forms.TabPage spherePage;
        private System.Windows.Forms.Button sphereAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Button button1;

    }
}