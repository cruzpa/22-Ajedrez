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
            this.SuspendLayout();
            // 
            // uI_LOGIN2
            // 
            this.uI_LOGIN2.Location = new System.Drawing.Point(480, 40);
            this.uI_LOGIN2.Name = "uI_LOGIN2";
            this.uI_LOGIN2.Size = new System.Drawing.Size(226, 374);
            this.uI_LOGIN2.TabIndex = 1;
            // 
            // uI_LOGIN1
            // 
            this.uI_LOGIN1.Location = new System.Drawing.Point(27, 34);
            this.uI_LOGIN1.Name = "uI_LOGIN1";
            this.uI_LOGIN1.Size = new System.Drawing.Size(252, 381);
            this.uI_LOGIN1.TabIndex = 0;
            // 
            // Menu2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}