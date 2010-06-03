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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.list = new System.Windows.Forms.ListBox();
            this.autopartitioningBtn = new System.Windows.Forms.RadioButton();
            this.parallelPlaneBtn = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radiusBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.slicesBox = new System.Windows.Forms.TextBox();
            this.stacksBox = new System.Windows.Forms.TextBox();
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
            this.checkBoxTest.Location = new System.Drawing.Point(13, 33);
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
            this.checkGJKTest.Location = new System.Drawing.Point(13, 56);
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
            this.checkBSPTest.Location = new System.Drawing.Point(13, 79);
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
            this.tabControl.Size = new System.Drawing.Size(209, 152);
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
            this.boxAdd.Location = new System.Drawing.Point(64, 6);
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
            this.spherePage.Controls.Add(this.stacksBox);
            this.spherePage.Controls.Add(this.slicesBox);
            this.spherePage.Controls.Add(this.label5);
            this.spherePage.Controls.Add(this.label4);
            this.spherePage.Controls.Add(this.radiusBox);
            this.spherePage.Controls.Add(this.label3);
            this.spherePage.Controls.Add(this.sphereAdd);
            this.spherePage.Location = new System.Drawing.Point(4, 22);
            this.spherePage.Name = "spherePage";
            this.spherePage.Size = new System.Drawing.Size(201, 126);
            this.spherePage.TabIndex = 2;
            this.spherePage.Text = "Sphere";
            this.spherePage.UseVisualStyleBackColor = true;
            // 
            // sphereAdd
            // 
            this.sphereAdd.Location = new System.Drawing.Point(64, 13);
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
            this.groupBox1.Controls.Add(this.parallelPlaneBtn);
            this.groupBox1.Controls.Add(this.autopartitioningBtn);
            this.groupBox1.Controls.Add(this.checkGJKTest);
            this.groupBox1.Controls.Add(this.checkBoxTest);
            this.groupBox1.Controls.Add(this.checkBSPTest);
            this.groupBox1.Location = new System.Drawing.Point(27, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 177);
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
            this.groupBox2.Location = new System.Drawing.Point(27, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 360);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Objects";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 29);
            this.button1.TabIndex = 9;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.label1.Location = new System.Drawing.Point(17, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Existing:";
            // 
            // list
            // 
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(20, 266);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(136, 69);
            this.list.TabIndex = 6;
            this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
            this.list.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.list_KeyPress);
            // 
            // autopartitioningBtn
            // 
            this.autopartitioningBtn.AutoSize = true;
            this.autopartitioningBtn.Checked = true;
            this.autopartitioningBtn.Location = new System.Drawing.Point(30, 102);
            this.autopartitioningBtn.Name = "autopartitioningBtn";
            this.autopartitioningBtn.Size = new System.Drawing.Size(98, 17);
            this.autopartitioningBtn.TabIndex = 4;
            this.autopartitioningBtn.TabStop = true;
            this.autopartitioningBtn.Text = "Autopartitioning";
            this.autopartitioningBtn.UseVisualStyleBackColor = true;
            this.autopartitioningBtn.CheckedChanged += new System.EventHandler(this.autopartitioningBtn_CheckedChanged);
            // 
            // parallelPlaneBtn
            // 
            this.parallelPlaneBtn.AutoSize = true;
            this.parallelPlaneBtn.Location = new System.Drawing.Point(30, 125);
            this.parallelPlaneBtn.Name = "parallelPlaneBtn";
            this.parallelPlaneBtn.Size = new System.Drawing.Size(152, 17);
            this.parallelPlaneBtn.TabIndex = 5;
            this.parallelPlaneBtn.Text = "Split plane parallel to X,Y,Z";
            this.parallelPlaneBtn.UseVisualStyleBackColor = true;
            this.parallelPlaneBtn.CheckedChanged += new System.EventHandler(this.parallelPlaneBtn_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Radius:";
            // 
            // radiusBox
            // 
            this.radiusBox.Location = new System.Drawing.Point(60, 45);
            this.radiusBox.Name = "radiusBox";
            this.radiusBox.Size = new System.Drawing.Size(48, 20);
            this.radiusBox.TabIndex = 2;
            this.radiusBox.Text = "5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Slices:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Stacks:";
            // 
            // slicesBox
            // 
            this.slicesBox.Location = new System.Drawing.Point(60, 71);
            this.slicesBox.Name = "slicesBox";
            this.slicesBox.Size = new System.Drawing.Size(49, 20);
            this.slicesBox.TabIndex = 5;
            this.slicesBox.Text = "8";
            // 
            // stacksBox
            // 
            this.stacksBox.Location = new System.Drawing.Point(60, 95);
            this.stacksBox.Name = "stacksBox";
            this.stacksBox.Size = new System.Drawing.Size(49, 20);
            this.stacksBox.TabIndex = 6;
            this.stacksBox.Text = "8";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(293, 589);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.tabControl.ResumeLayout(false);
            this.boxPage.ResumeLayout(false);
            this.torusPage.ResumeLayout(false);
            this.spherePage.ResumeLayout(false);
            this.spherePage.PerformLayout();
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
        private System.Windows.Forms.RadioButton parallelPlaneBtn;
        private System.Windows.Forms.RadioButton autopartitioningBtn;
        private System.Windows.Forms.TextBox stacksBox;
        private System.Windows.Forms.TextBox slicesBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox radiusBox;
        private System.Windows.Forms.Label label3;

    }
}