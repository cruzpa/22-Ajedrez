namespace Ajedrez
{
    partial class FinPartida
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
            this.labelTitulo = new System.Windows.Forms.Label();
            this.labelGanador = new System.Windows.Forms.Label();
            this.labelPerdedor = new System.Windows.Forms.Label();
            this.labelTiempo = new System.Windows.Forms.Label();
            this.buttonRevancha = new System.Windows.Forms.Button();
            this.buttonMenu = new System.Windows.Forms.Button();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitulo
            // 
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(0, 30);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(400, 26);
            this.labelTitulo.TabIndex = 0;
            this.labelTitulo.Text = "¡JAQUE MATE!";
            this.labelTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelGanador
            // 
            this.labelGanador.AutoSize = true;
            this.labelGanador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGanador.Location = new System.Drawing.Point(50, 80);
            this.labelGanador.Name = "labelGanador";
            this.labelGanador.Size = new System.Drawing.Size(120, 20);
            this.labelGanador.TabIndex = 1;
            this.labelGanador.Text = "Ganador: Nombre";
            // 
            // labelPerdedor
            // 
            this.labelPerdedor.AutoSize = true;
            this.labelPerdedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPerdedor.Location = new System.Drawing.Point(50, 110);
            this.labelPerdedor.Name = "labelPerdedor";
            this.labelPerdedor.Size = new System.Drawing.Size(130, 20);
            this.labelPerdedor.TabIndex = 2;
            this.labelPerdedor.Text = "Perdedor: Nombre";
            // 
            // labelTiempo
            // 
            this.labelTiempo.AutoSize = true;
            this.labelTiempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTiempo.Location = new System.Drawing.Point(50, 140);
            this.labelTiempo.Name = "labelTiempo";
            this.labelTiempo.Size = new System.Drawing.Size(150, 20);
            this.labelTiempo.TabIndex = 3;
            this.labelTiempo.Text = "Tiempo Jugado: 00:00:00";
            // 
            // buttonRevancha
            // 
            this.buttonRevancha.Location = new System.Drawing.Point(30, 200);
            this.buttonRevancha.Name = "buttonRevancha";
            this.buttonRevancha.Size = new System.Drawing.Size(100, 40);
            this.buttonRevancha.TabIndex = 4;
            this.buttonRevancha.Text = "Revancha";
            this.buttonRevancha.UseVisualStyleBackColor = true;
            this.buttonRevancha.Click += new System.EventHandler(this.buttonRevancha_Click);
            // 
            // buttonMenu
            // 
            this.buttonMenu.Location = new System.Drawing.Point(150, 200);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(100, 40);
            this.buttonMenu.TabIndex = 5;
            this.buttonMenu.Text = "Menu";
            this.buttonMenu.UseVisualStyleBackColor = true;
            this.buttonMenu.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonSalir
            // 
            this.buttonSalir.Location = new System.Drawing.Point(270, 200);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(100, 40);
            this.buttonSalir.TabIndex = 6;
            this.buttonSalir.Text = "Salir";
            this.buttonSalir.UseVisualStyleBackColor = true;
            this.buttonSalir.Click += new System.EventHandler(this.buttonSalir_Click);
            // 
            // FinPartida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 280);
            this.Controls.Add(this.buttonSalir);
            this.Controls.Add(this.buttonMenu);
            this.Controls.Add(this.buttonRevancha);
            this.Controls.Add(this.labelTiempo);
            this.Controls.Add(this.labelPerdedor);
            this.Controls.Add(this.labelGanador);
            this.Controls.Add(this.labelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FinPartida";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fin de Partida";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label labelGanador;
        private System.Windows.Forms.Label labelPerdedor;
        private System.Windows.Forms.Label labelTiempo;
        private System.Windows.Forms.Button buttonRevancha;
        private System.Windows.Forms.Button buttonMenu;
        private System.Windows.Forms.Button buttonSalir;
    }
}
