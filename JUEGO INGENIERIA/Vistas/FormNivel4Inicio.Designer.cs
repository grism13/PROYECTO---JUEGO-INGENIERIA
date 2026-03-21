namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel4Inicio
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
            pbLaverinto = new Panel();
            pbPersonaje = new PictureBox();
            pbMalo = new PictureBox();
            pbPuerta = new PictureBox();
            pbLaverinto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMalo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPuerta).BeginInit();
            SuspendLayout();
            // 
            // pbLaverinto
            // 
            pbLaverinto.BackColor = SystemColors.ButtonFace;
            pbLaverinto.Controls.Add(pbPuerta);
            pbLaverinto.Controls.Add(pbMalo);
            pbLaverinto.Controls.Add(pbPersonaje);
            pbLaverinto.Location = new Point(94, 71);
            pbLaverinto.Name = "pbLaverinto";
            pbLaverinto.Size = new Size(458, 316);
            pbLaverinto.TabIndex = 0;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Green;
            pbPersonaje.Location = new Point(21, 31);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(25, 25);
            pbPersonaje.TabIndex = 0;
            pbPersonaje.TabStop = false;
            // 
            // pbMalo
            // 
            pbMalo.BackColor = Color.Red;
            pbMalo.Location = new Point(397, 31);
            pbMalo.Name = "pbMalo";
            pbMalo.Size = new Size(25, 25);
            pbMalo.TabIndex = 1;
            pbMalo.TabStop = false;
            // 
            // pbPuerta
            // 
            pbPuerta.BackColor = Color.PaleGreen;
            pbPuerta.Location = new Point(433, 249);
            pbPuerta.Name = "pbPuerta";
            pbPuerta.Size = new Size(25, 25);
            pbPuerta.TabIndex = 2;
            pbPuerta.TabStop = false;
            // 
            // FormNivel4Inicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(800, 450);
            Controls.Add(pbLaverinto);
            Name = "FormNivel4Inicio";
            Text = "FormNivel4Inicio";
            pbLaverinto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMalo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPuerta).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pbLaverinto;
        private PictureBox pbPuerta;
        private PictureBox pbMalo;
        private PictureBox pbPersonaje;
    }
}