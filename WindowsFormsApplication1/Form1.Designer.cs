namespace WindowsFormsApplication1
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
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.выбратьфайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакIPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox2.Location = new System.Drawing.Point(0, 24);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(928, 528);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            this.richTextBox2.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьфайлToolStripMenuItem,
            this.сохранитьКакIPSToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(928, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // выбратьфайлToolStripMenuItem
            // 
            this.выбратьфайлToolStripMenuItem.Name = "выбратьфайлToolStripMenuItem";
            this.выбратьфайлToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.выбратьфайлToolStripMenuItem.Text = "Выбрать файл";
            this.выбратьфайлToolStripMenuItem.Click += new System.EventHandler(this.выбратьфайлToolStripMenuItem_Click);
            // 
            // сохранитьКакIPSToolStripMenuItem
            // 
            this.сохранитьКакIPSToolStripMenuItem.Name = "сохранитьКакIPSToolStripMenuItem";
            this.сохранитьКакIPSToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.сохранитьКакIPSToolStripMenuItem.Text = "Сохранить как IPS";
            this.сохранитьКакIPSToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакIPSToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "Выберете, пожалуйста файл";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(928, 552);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GSE Handler";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem выбратьфайлToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакIPSToolStripMenuItem;
    }
}

