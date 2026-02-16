namespace JUEGO_INGENIERIA
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
            lblNombreJugador = new Label();
            lblNivel = new Label();
            SuspendLayout();
            // 
            // lblNombreJugador
            // 
            lblNombreJugador.AutoSize = true;
            lblNombreJugador.Location = new Point(36, 88);
            lblNombreJugador.Name = "lblNombreJugador";
            lblNombreJugador.Size = new Size(50, 20);
            lblNombreJugador.TabIndex = 0;
            lblNombreJugador.Text = "label1";
            // 
            // lblNivel
            // 
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(36, 43);
            lblNivel.Name = "lblNivel";
            lblNivel.Size = new Size(50, 20);
            lblNivel.TabIndex = 1;
            lblNivel.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1477, 750);
            Controls.Add(lblNivel);
            Controls.Add(lblNombreJugador);
            Name = "Form1";
            Text = "Form1";
            Activated += Form1_Activated;
            Shown += Form1_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreJugador;
        private Label lblNivel;
    }
}