namespace Ajedrez
{
    partial class Menu2
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
            this.uI_LOGIN2 = new Ajedrez.UI_LOGIN();
            this.uI_LOGIN1 = new Ajedrez.UI_LOGIN();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uI_LOGIN2
            // 
            this.uI_LOGIN2.Location = new System.Drawing.Point(481, 40);
            this.uI_LOGIN2.Name = "uI_LOGIN2";
            this.uI_LOGIN2.Size = new System.Drawing.Size(226, 374);
            this.uI_LOGIN2.TabIndex = 1;
            // 
            // uI_LOGIN1
            // 
            this.uI_LOGIN1.Location = new System.Drawing.Point(25, 40);
            this.uI_LOGIN1.Name = "uI_LOGIN1";
            this.uI_LOGIN1.Size = new System.Drawing.Size(252, 381);
            this.uI_LOGIN1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 297);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "Jugar!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Menu2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uI_LOGIN2);
            this.Controls.Add(this.uI_LOGIN1);
            this.Name = "Menu2";
            this.Text = "Menu2";
            this.Load += new System.EventHandler(this.Menu2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UI_LOGIN uI_LOGIN1;
        private UI_LOGIN uI_LOGIN2;
        private System.Windows.Forms.Button button1;
    }
}