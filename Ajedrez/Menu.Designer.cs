namespace Ajedrez
{
    partial class Menu
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.uI_LOGIN2 = new Ajedrez.UI_LOGIN();
            this.uI_LOGIN1 = new Ajedrez.UI_LOGIN();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(276, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "Jugar!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(314, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ajedrez";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Jugador Blancas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(569, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Jugador Negras";
            // 
            // uI_LOGIN2
            // 
            this.uI_LOGIN2.jugador = null;
            this.uI_LOGIN2.Location = new System.Drawing.Point(525, 92);
            this.uI_LOGIN2.Name = "uI_LOGIN2";
            this.uI_LOGIN2.Size = new System.Drawing.Size(250, 150);
            this.uI_LOGIN2.TabIndex = 1;
            // 
            // uI_LOGIN1
            // 
            this.uI_LOGIN1.jugador = null;
            this.uI_LOGIN1.Location = new System.Drawing.Point(18, 92);
            this.uI_LOGIN1.Name = "uI_LOGIN1";
            this.uI_LOGIN1.Size = new System.Drawing.Size(250, 150);
            this.uI_LOGIN1.TabIndex = 0;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 304);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uI_LOGIN2);
            this.Controls.Add(this.uI_LOGIN1);
            this.Name = "Menu";
            this.Text = "Menu2";
            this.Load += new System.EventHandler(this.Menu2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI_LOGIN uI_LOGIN1;
        private UI_LOGIN uI_LOGIN2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}