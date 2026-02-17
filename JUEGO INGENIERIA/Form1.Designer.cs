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
            nivelPersonaje = new Label();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
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
            // tmrMovimiento
            // 
            tmrMovimiento.Enabled = true;
            tmrMovimiento.Interval = 20;
            tmrMovimiento.Tick += tmrMovimiento_Tick;
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Location = new Point(623, 222);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(160, 200);
            pbPersonaje.SizeMode = PictureBoxSizeMode.AutoSize;
            pbPersonaje.TabIndex = 2;
            pbPersonaje.TabStop = false;
            // 
            // nivelPersonaje
            // 
            nivelPersonaje.AutoSize = true;
            nivelPersonaje.Location = new Point(1258, 50);
            nivelPersonaje.Name = "nivelPersonaje";
            nivelPersonaje.Size = new Size(50, 20);
            nivelPersonaje.TabIndex = 3;
            nivelPersonaje.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1477, 750);
            Controls.Add(nivelPersonaje);
            Controls.Add(pbPersonaje);
            Controls.Add(lblNivel);
            Controls.Add(lblNombreJugador);
            DoubleBuffered = true;
            KeyPreview = true;
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
        private Label nivelPersonaje;
    }
}