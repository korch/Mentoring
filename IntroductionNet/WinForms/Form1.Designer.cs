namespace WinForms
{
    partial class Form1
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
            this.lbl1 = new System.Windows.Forms.Label();
            this.countryCb = new System.Windows.Forms.ComboBox();
            this.btnGreeting = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Namelbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(9, 44);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(84, 13);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Choose country:";
            // 
            // countryCb
            // 
            this.countryCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.countryCb.FormattingEnabled = true;
            this.countryCb.Location = new System.Drawing.Point(102, 41);
            this.countryCb.Name = "countryCb";
            this.countryCb.Size = new System.Drawing.Size(181, 21);
            this.countryCb.TabIndex = 1;
            // 
            // btnGreeting
            // 
            this.btnGreeting.Location = new System.Drawing.Point(12, 68);
            this.btnGreeting.Name = "btnGreeting";
            this.btnGreeting.Size = new System.Drawing.Size(271, 69);
            this.btnGreeting.TabIndex = 2;
            this.btnGreeting.Text = "Greeting";
            this.btnGreeting.UseVisualStyleBackColor = true;
            this.btnGreeting.Click += new System.EventHandler(this.btnGreeting_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(53, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(230, 20);
            this.textBox1.TabIndex = 3;
            // 
            // Namelbl
            // 
            this.Namelbl.AutoSize = true;
            this.Namelbl.Location = new System.Drawing.Point(9, 15);
            this.Namelbl.Name = "Namelbl";
            this.Namelbl.Size = new System.Drawing.Size(38, 13);
            this.Namelbl.TabIndex = 4;
            this.Namelbl.Text = "Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 141);
            this.Controls.Add(this.Namelbl);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGreeting);
            this.Controls.Add(this.countryCb);
            this.Controls.Add(this.lbl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Greeting";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.ComboBox countryCb;
        private System.Windows.Forms.Button btnGreeting;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Namelbl;
    }
}

