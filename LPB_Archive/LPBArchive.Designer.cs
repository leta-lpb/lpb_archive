namespace LPB_Archive
{
    partial class LPBArchive
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LPBArchive));
            this.txt_output_path = new System.Windows.Forms.TextBox();
            this.txt_input_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_tools_path = new System.Windows.Forms.TextBox();
            this.btn_verify = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.lbl_status = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // txt_output_path
            // 
            this.txt_output_path.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_output_path.Location = new System.Drawing.Point(12, 71);
            this.txt_output_path.Name = "txt_output_path";
            this.txt_output_path.Size = new System.Drawing.Size(203, 20);
            this.txt_output_path.TabIndex = 1;
            this.txt_output_path.TextChanged += new System.EventHandler(this.txt_output_path_TextChanged);
            // 
            // txt_input_path
            // 
            this.txt_input_path.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_input_path.Location = new System.Drawing.Point(12, 26);
            this.txt_input_path.Name = "txt_input_path";
            this.txt_input_path.Size = new System.Drawing.Size(203, 20);
            this.txt_input_path.TabIndex = 2;
            this.txt_input_path.TextChanged += new System.EventHandler(this.txt_input_path_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Input Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tools Path";
            // 
            // txt_tools_path
            // 
            this.txt_tools_path.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_tools_path.Location = new System.Drawing.Point(12, 119);
            this.txt_tools_path.Name = "txt_tools_path";
            this.txt_tools_path.Size = new System.Drawing.Size(203, 20);
            this.txt_tools_path.TabIndex = 5;
            this.txt_tools_path.TextChanged += new System.EventHandler(this.txt_tools_path_TextChanged);
            // 
            // btn_verify
            // 
            this.btn_verify.Location = new System.Drawing.Point(12, 210);
            this.btn_verify.Name = "btn_verify";
            this.btn_verify.Size = new System.Drawing.Size(141, 23);
            this.btn_verify.TabIndex = 8;
            this.btn_verify.Text = "Archive and Verify Tape";
            this.btn_verify.UseVisualStyleBackColor = true;
            this.btn_verify.Click += new System.EventHandler(this.btn_verify_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(221, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(630, 466);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(12, 409);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(74, 13);
            this.lbl_status.TabIndex = 10;
            this.lbl_status.Text = "No Active Job";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 396);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Status:";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(12, 181);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(85, 23);
            this.btn_save.TabIndex = 12;
            this.btn_save.Text = "Save Settings";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Example: c:\\captures\\";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(94, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Example: c:\\clouddrive\\";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(94, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Example: c:\\archive\\";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 146);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(145, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Delete unused master file";
            this.toolTip1.SetToolTip(this.checkBox1, resources.GetString("checkBox1.ToolTip"));
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // LPBArchive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 505);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btn_verify);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_tools_path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_input_path);
            this.Controls.Add(this.txt_output_path);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LPBArchive";
            this.Text = "LPB Archive";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_output_path;
        private System.Windows.Forms.TextBox txt_input_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_tools_path;
        private System.Windows.Forms.Button btn_verify;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

