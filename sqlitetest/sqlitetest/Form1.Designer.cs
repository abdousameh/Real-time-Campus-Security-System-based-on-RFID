namespace sqlitetest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.studentnum = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.Button();
            this.studentname = new System.Windows.Forms.TextBox();
            this.output_information = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // studentnum
            // 
            this.studentnum.Font = new System.Drawing.Font("宋体", 15F);
            this.studentnum.Location = new System.Drawing.Point(67, 53);
            this.studentnum.Multiline = true;
            this.studentnum.Name = "studentnum";
            this.studentnum.Size = new System.Drawing.Size(349, 56);
            this.studentnum.TabIndex = 0;
            this.studentnum.TextChanged += new System.EventHandler(this.studentnum_Textchanged);
            this.studentnum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.studentnum_Keypress);
            // 
            // output
            // 
            this.output.Location = new System.Drawing.Point(490, 298);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(110, 43);
            this.output.TabIndex = 1;
            this.output.Text = "输出数据";
            this.output.UseVisualStyleBackColor = true;
            this.output.Click += new System.EventHandler(this.output_Click);
            // 
            // studentname
            // 
            this.studentname.Font = new System.Drawing.Font("宋体", 15F);
            this.studentname.Location = new System.Drawing.Point(490, 53);
            this.studentname.Multiline = true;
            this.studentname.Name = "studentname";
            this.studentname.Size = new System.Drawing.Size(264, 56);
            this.studentname.TabIndex = 2;
            // 
            // output_information
            // 
            this.output_information.Location = new System.Drawing.Point(67, 153);
            this.output_information.Multiline = true;
            this.output_information.Name = "output_information";
            this.output_information.Size = new System.Drawing.Size(349, 188);
            this.output_information.TabIndex = 3;
            this.output_information.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.output_Keypress);
            // 
            // input
            // 
            this.input.Location = new System.Drawing.Point(646, 153);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(108, 41);
            this.input.TabIndex = 4;
            this.input.Text = "录入信息";
            this.input.UseVisualStyleBackColor = true;
            this.input.Click += new System.EventHandler(this.input_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(490, 153);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(110, 41);
            this.clear.TabIndex = 5;
            this.clear.Text = "清除数据库";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "学号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "姓名";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 414);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.input);
            this.Controls.Add(this.output_information);
            this.Controls.Add(this.studentname);
            this.Controls.Add(this.output);
            this.Controls.Add(this.studentnum);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox studentnum;
        private System.Windows.Forms.Button output;
        private System.Windows.Forms.TextBox studentname;
        private System.Windows.Forms.TextBox output_information;
        private System.Windows.Forms.Button input;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

