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
            pbPuerta = new PictureBox();
            pbMalo = new PictureBox();
            pbPersonaje = new PictureBox();
            pbPersonaje2 = new PictureBox();
            pbPuerta2 = new PictureBox();
            pbLaverinto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbPuerta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMalo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPuerta2).BeginInit();
            SuspendLayout();
            // 
            // pbLaverinto
            // 
            pbLaverinto.BackColor = SystemColors.ButtonFace;
            pbLaverinto.Controls.Add(pbPuerta);
            pbLaverinto.Controls.Add(pbMalo);
            pbLaverinto.Controls.Add(pbPersonaje);
            pbLaverinto.Location = new Point(38, 89);
            pbLaverinto.Name = "pbLaverinto";
            pbLaverinto.Size = new Size(458, 413);
            pbLaverinto.TabIndex = 0;
            // 
            // pbPuerta
            // 
            pbPuerta.BackColor = Color.PaleGreen;
            pbPuerta.Location = new Point(433, 332);
            pbPuerta.Name = "pbPuerta";
            pbPuerta.Size = new Size(25, 25);
            pbPuerta.TabIndex = 2;
            pbPuerta.TabStop = false;
            // 
            // pbMalo
            // 
            pbMalo.BackColor = Color.Red;
            pbMalo.Location = new Point(384, 31);
            pbMalo.Name = "pbMalo";
            pbMalo.Size = new Size(38, 45);
            pbMalo.TabIndex = 1;
            pbMalo.TabStop = false;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Location = new Point(21, 31);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(43, 45);
            pbPersonaje.TabIndex = 0;
            pbPersonaje.TabStop = false;
            // 
            // pbPersonaje2
            // 
            pbPersonaje2.BackColor = Color.Transparent;
            pbPersonaje2.Location = new Point(528, 190);
            pbPersonaje2.Name = "pbPersonaje2";
            pbPersonaje2.Size = new Size(106, 133);
            pbPersonaje2.TabIndex = 1;
            pbPersonaje2.TabStop = false;
            pbPersonaje2.Visible = false;
            // 
            // pbPuerta2
            // 
            pbPuerta2.BackColor = Color.PaleGreen;
            pbPuerta2.Location = new Point(874, 2);
            pbPuerta2.Name = "pbPuerta2";
            pbPuerta2.Size = new Size(92, 13);
            pbPuerta2.TabIndex = 3;
            pbPuerta2.TabStop = false;
            // 
            // FormNivel4Inicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1112, 572);
            Controls.Add(pbPuerta2);
            Controls.Add(pbPersonaje2);
            Controls.Add(pbLaverinto);
            Name = "FormNivel4Inicio";
            Text = "FormNivel4Inicio";
            pbLaverinto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbPuerta).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMalo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPuerta2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pbLaverinto;
        private PictureBox pbPuerta;
        private PictureBox pbMalo;
        private PictureBox pbPersonaje;
        private PictureBox pbPersonaje2;
        private PictureBox pbPuerta2;
    }
}