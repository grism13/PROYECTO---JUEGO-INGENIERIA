namespace JUEGO_INGENIERIA.Vistas
{
    partial class formNivel2
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
            lblCuentaRegresiva = new Label();
            panelIzq = new PictureBox();
            panelArriba = new PictureBox();
            panelAbajo = new PictureBox();
            panelDer = new PictureBox();
            btnEmpezar = new PictureBox();
            timerCuenta = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)panelIzq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelArriba).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelAbajo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelDer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEmpezar).BeginInit();
            SuspendLayout();
            // 
            // lblCuentaRegresiva
            // 
            lblCuentaRegresiva.AutoSize = true;
            lblCuentaRegresiva.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCuentaRegresiva.Location = new Point(142, 32);
            lblCuentaRegresiva.Name = "lblCuentaRegresiva";
            lblCuentaRegresiva.Size = new Size(197, 81);
            lblCuentaRegresiva.TabIndex = 0;
            lblCuentaRegresiva.Text = "Listo...";
            // 
            // panelIzq
            // 
            panelIzq.BackColor = Color.DarkOrange;
            panelIzq.Location = new Point(95, 305);
            panelIzq.Name = "panelIzq";
            panelIzq.Size = new Size(87, 62);
            panelIzq.TabIndex = 1;
            panelIzq.TabStop = false;
            // 
            // panelArriba
            // 
            panelArriba.BackColor = Color.Firebrick;
            panelArriba.Location = new Point(191, 223);
            panelArriba.Name = "panelArriba";
            panelArriba.Size = new Size(87, 62);
            panelArriba.TabIndex = 2;
            panelArriba.TabStop = false;
            // 
            // panelAbajo
            // 
            panelAbajo.BackColor = Color.ForestGreen;
            panelAbajo.Location = new Point(202, 305);
            panelAbajo.Name = "panelAbajo";
            panelAbajo.Size = new Size(87, 62);
            panelAbajo.TabIndex = 3;
            panelAbajo.TabStop = false;
            // 
            // panelDer
            // 
            panelDer.BackColor = Color.SteelBlue;
            panelDer.Location = new Point(314, 305);
            panelDer.Name = "panelDer";
            panelDer.Size = new Size(87, 62);
            panelDer.TabIndex = 4;
            panelDer.TabStop = false;
            // 
            // btnEmpezar
            // 
            btnEmpezar.BackColor = Color.DeepPink;
            btnEmpezar.Location = new Point(399, 61);
            btnEmpezar.Name = "btnEmpezar";
            btnEmpezar.Size = new Size(43, 39);
            btnEmpezar.TabIndex = 5;
            btnEmpezar.TabStop = false;
            btnEmpezar.Click += btnEmpezar_Click;
            // 
            // timerCuenta
            // 
            timerCuenta.Interval = 1000;
            timerCuenta.Tick += timerCuenta_Tick_1;
            // 
            // formNivel2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 450);
            Controls.Add(btnEmpezar);
            Controls.Add(panelDer);
            Controls.Add(panelAbajo);
            Controls.Add(panelArriba);
            Controls.Add(panelIzq);
            Controls.Add(lblCuentaRegresiva);
            Name = "formNivel2";
            Text = "FormNivel2";
            ((System.ComponentModel.ISupportInitialize)panelIzq).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelArriba).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelAbajo).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelDer).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEmpezar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCuentaRegresiva;
        private PictureBox panelIzq;
        private PictureBox panelArriba;
        private PictureBox panelAbajo;
        private PictureBox panelDer;
        private PictureBox btnEmpezar;
        private System.Windows.Forms.Timer timerCuenta;
    }
}