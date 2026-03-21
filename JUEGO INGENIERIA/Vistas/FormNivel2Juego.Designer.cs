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
            lblPuntuacion = new Label();
            lblFaltas = new Label();
            ((System.ComponentModel.ISupportInitialize)pbMetaIzq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaAbajo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaDer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaArriba).BeginInit();
            pnlPistaBaile.SuspendLayout();
            SuspendLayout();
            // 
            // lblCuentaRegresiva
            // 
            resources.ApplyResources(lblCuentaRegresiva, "lblCuentaRegresiva");
            lblCuentaRegresiva.BackColor = Color.Transparent;
            lblCuentaRegresiva.Name = "lblCuentaRegresiva";
            // 
            // btnEmpezar
            // 
            resources.ApplyResources(btnEmpezar, "btnEmpezar");
            btnEmpezar.Name = "btnEmpezar";
            btnEmpezar.UseVisualStyleBackColor = true;
            btnEmpezar.Click += btnEmpezar_Click_1;
            // 
            // pbMetaIzq
            // 
            pbMetaIzq.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(pbMetaIzq, "pbMetaIzq");
            pbMetaIzq.Name = "pbMetaIzq";
            pbMetaIzq.TabStop = false;
            // 
            // pbMetaAbajo
            // 
            pbMetaAbajo.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(pbMetaAbajo, "pbMetaAbajo");
            pbMetaAbajo.Name = "pbMetaAbajo";
            pbMetaAbajo.TabStop = false;
            // 
            // pbMetaDer
            // 
            pbMetaDer.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(pbMetaDer, "pbMetaDer");
            pbMetaDer.Name = "pbMetaDer";
            pbMetaDer.TabStop = false;
            // 
            // pbMetaArriba
            // 
            pbMetaArriba.BorderStyle = BorderStyle.FixedSingle;
            resources.ApplyResources(pbMetaArriba, "pbMetaArriba");
            pbMetaArriba.Name = "pbMetaArriba";
            pbMetaArriba.TabStop = false;
            // 
            // pnlPistaBaile
            // 
            pnlPistaBaile.BackColor = Color.White;
            pnlPistaBaile.Controls.Add(pbMetaIzq);
            pnlPistaBaile.Controls.Add(pbMetaArriba);
            pnlPistaBaile.Controls.Add(pbMetaAbajo);
            pnlPistaBaile.Controls.Add(pbMetaDer);
            resources.ApplyResources(pnlPistaBaile, "pnlPistaBaile");
            pnlPistaBaile.Name = "pnlPistaBaile";
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
            // FormNivel2Juego
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = Properties.Resources.fondoNivel2;
            Controls.Add(lblFaltas);
            Controls.Add(lblPuntuacion);
            Controls.Add(pnlPistaBaile);
            Controls.Add(btnEmpezar);
            Controls.Add(lblCuentaRegresiva);
            DoubleBuffered = true;
            Name = "FormNivel2Juego";
            ((System.ComponentModel.ISupportInitialize)pbMetaIzq).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaAbajo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaDer).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMetaArriba).EndInit();
            pnlPistaBaile.ResumeLayout(false);
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
    }
}