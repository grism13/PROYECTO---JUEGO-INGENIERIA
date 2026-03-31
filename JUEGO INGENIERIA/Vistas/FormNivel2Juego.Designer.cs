namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel2Juego
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNivel2Juego));
            lblCuentaRegresiva = new Label();
            btnEmpezar = new Button();
            pbMetaIzq = new PictureBox();
            pbMetaAbajo = new PictureBox();
            pbMetaDer = new PictureBox();
            pbMetaArriba = new PictureBox();
            pnlPistaBaile = new Panel();
            pictureBox1 = new PictureBox();
            lblPuntuacion = new Label();
            lblFaltas = new Label();
            pbJoseJesus = new PictureBox();
            pnlNarrativaIntro = new Panel();
            lblTextoNarrativa = new Label();
            pbJoseJesusIntro = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbMetaIzq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaAbajo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaDer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaArriba).BeginInit();
            pnlPistaBaile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbJoseJesus).BeginInit();
            pnlNarrativaIntro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbJoseJesusIntro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // lblCuentaRegresiva
            // 
            resources.ApplyResources(lblCuentaRegresiva, "lblCuentaRegresiva");
            lblCuentaRegresiva.BackColor = Color.Transparent;
            lblCuentaRegresiva.ForeColor = Color.FromArgb(64, 0, 64);
            lblCuentaRegresiva.Name = "lblCuentaRegresiva";
            // 
            // btnEmpezar
            // 
            btnEmpezar.BackColor = Color.Transparent;
            btnEmpezar.BackgroundImage = Properties.Resources.botonazul;
            resources.ApplyResources(btnEmpezar, "btnEmpezar");
            btnEmpezar.Cursor = Cursors.Hand;
            btnEmpezar.FlatAppearance.BorderSize = 0;
            btnEmpezar.ForeColor = Color.White;
            btnEmpezar.Name = "btnEmpezar";
            btnEmpezar.UseVisualStyleBackColor = false;
            btnEmpezar.Click += btnEmpezar_Click_1;
            // 
            // pbMetaIzq
            // 
            pbMetaIzq.BackColor = Color.White;
            resources.ApplyResources(pbMetaIzq, "pbMetaIzq");
            pbMetaIzq.Image = Properties.Resources.rosa;
            pbMetaIzq.Name = "pbMetaIzq";
            pbMetaIzq.TabStop = false;
            // 
            // pbMetaAbajo
            // 
            pbMetaAbajo.BackColor = Color.White;
            pbMetaAbajo.Image = Properties.Resources.azul;
            resources.ApplyResources(pbMetaAbajo, "pbMetaAbajo");
            pbMetaAbajo.Name = "pbMetaAbajo";
            pbMetaAbajo.TabStop = false;
            // 
            // pbMetaDer
            // 
            pbMetaDer.BackColor = Color.White;
            pbMetaDer.Image = Properties.Resources.naranja;
            resources.ApplyResources(pbMetaDer, "pbMetaDer");
            pbMetaDer.Name = "pbMetaDer";
            pbMetaDer.TabStop = false;
            // 
            // pbMetaArriba
            // 
            pbMetaArriba.BackColor = Color.White;
            pbMetaArriba.Image = Properties.Resources.verde;
            resources.ApplyResources(pbMetaArriba, "pbMetaArriba");
            pbMetaArriba.Name = "pbMetaArriba";
            pbMetaArriba.TabStop = false;
            // 
            // pnlPistaBaile
            // 
            pnlPistaBaile.BackColor = Color.Black;
            pnlPistaBaile.Controls.Add(pbMetaIzq);
            pnlPistaBaile.Controls.Add(pbMetaArriba);
            pnlPistaBaile.Controls.Add(pbMetaAbajo);
            pnlPistaBaile.Controls.Add(pbMetaDer);
            pnlPistaBaile.Controls.Add(pictureBox1);
            pnlPistaBaile.ForeColor = Color.Black;
            resources.ApplyResources(pnlPistaBaile, "pnlPistaBaile");
            pnlPistaBaile.Name = "pnlPistaBaile";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.marconivel2;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // lblPuntuacion
            // 
            lblPuntuacion.BackColor = Color.Transparent;
            resources.ApplyResources(lblPuntuacion, "lblPuntuacion");
            lblPuntuacion.Name = "lblPuntuacion";
            // 
            // lblFaltas
            // 
            lblFaltas.BackColor = Color.Transparent;
            resources.ApplyResources(lblFaltas, "lblFaltas");
            lblFaltas.Name = "lblFaltas";
            // 
            // pbJoseJesus
            // 
            pbJoseJesus.BackColor = Color.Transparent;
            resources.ApplyResources(pbJoseJesus, "pbJoseJesus");
            pbJoseJesus.Name = "pbJoseJesus";
            pbJoseJesus.TabStop = false;
            // 
            // pnlNarrativaIntro
            // 
            pnlNarrativaIntro.BackColor = Color.Transparent;
            pnlNarrativaIntro.Controls.Add(lblTextoNarrativa);
            pnlNarrativaIntro.Controls.Add(pbJoseJesusIntro);
            pnlNarrativaIntro.Controls.Add(pictureBox2);
            resources.ApplyResources(pnlNarrativaIntro, "pnlNarrativaIntro");
            pnlNarrativaIntro.Name = "pnlNarrativaIntro";
            // 
            // lblTextoNarrativa
            // 
            lblTextoNarrativa.BackColor = Color.SandyBrown;
            resources.ApplyResources(lblTextoNarrativa, "lblTextoNarrativa");
            lblTextoNarrativa.Name = "lblTextoNarrativa";
            lblTextoNarrativa.Click += lblTextoNarrativa_Click;
            // 
            // pbJoseJesusIntro
            // 
            pbJoseJesusIntro.BackColor = Color.DarkGray;
            resources.ApplyResources(pbJoseJesusIntro, "pbJoseJesusIntro");
            pbJoseJesusIntro.Name = "pbJoseJesusIntro";
            pbJoseJesusIntro.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.narrativa;
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            // 
            // FormNivel2Juego
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.fondoNivel2;
            Controls.Add(btnEmpezar);
            Controls.Add(pnlNarrativaIntro);
            Controls.Add(pbJoseJesus);
            Controls.Add(lblFaltas);
            Controls.Add(lblPuntuacion);
            Controls.Add(pnlPistaBaile);
            Controls.Add(lblCuentaRegresiva);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormNivel2Juego";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pbMetaIzq).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaAbajo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaDer).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaArriba).EndInit();
            pnlPistaBaile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbJoseJesus).EndInit();
            pnlNarrativaIntro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbJoseJesusIntro).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCuentaRegresiva;
        private Button btnEmpezar;
        private PictureBox pbMetaIzq;
        private PictureBox pbMetaAbajo;
        private PictureBox pbMetaDer;
        private PictureBox pbMetaArriba;
        private Panel pnlPistaBaile;
        private Label lblPuntuacion;
        private Label lblFaltas;
        private PictureBox pbJoseJesus;
        private PictureBox pictureBox1;
        private Panel pnlNarrativaIntro;
        private PictureBox pbJoseJesusIntro;
        private PictureBox pictureBox2;
        private Label lblTextoNarrativa;
    }
}