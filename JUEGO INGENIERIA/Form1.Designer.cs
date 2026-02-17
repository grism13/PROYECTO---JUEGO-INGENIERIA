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
            components = new System.ComponentModel.Container();
            lblNombreJugador = new Label();
            lblNivel = new Label();
            tmrMovimiento = new System.Windows.Forms.Timer(components);
            pbPersonaje = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            SuspendLayout();
            // 
            // lblNombreJugador
            // 
            lblNombreJugador.AutoSize = true;
            lblNombreJugador.Location = new Point(32, 66);
            lblNombreJugador.Name = "lblNombreJugador";
            lblNombreJugador.Size = new Size(38, 15);
            lblNombreJugador.TabIndex = 0;
            lblNombreJugador.Text = "label1";
            // 
            // lblNivel
            // 
            lblNivel.AutoSize = true;
            lblNivel.Location = new Point(32, 32);
            lblNivel.Name = "lblNivel";
            lblNivel.Size = new Size(38, 15);
            lblNivel.TabIndex = 1;
            lblNivel.Text = "label1";
            // 
            // tmrMovimiento
            // 
            tmrMovimiento.Enabled = true;
            tmrMovimiento.Interval = 20;
            tmrMovimiento.Tick += tmrMovimiento_Tick;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources._7;
            pbPersonaje.Location = new Point(469, 156);
            pbPersonaje.Margin = new Padding(3, 2, 3, 2);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(160, 200);
            pbPersonaje.SizeMode = PictureBoxSizeMode.AutoSize;
            pbPersonaje.TabIndex = 2;
            pbPersonaje.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1264, 562);
            Controls.Add(pbPersonaje);
            Controls.Add(lblNivel);
            Controls.Add(lblNombreJugador);
            DoubleBuffered = true;
            KeyPreview = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Activated += Form1_Activated;
            Shown += Form1_Shown;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNombreJugador;
        private Label lblNivel;
        private System.Windows.Forms.Timer tmrMovimiento;
        private PictureBox pbPersonaje;
    }
}