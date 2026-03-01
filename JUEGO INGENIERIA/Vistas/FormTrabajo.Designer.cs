namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormTrabajo
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
            pbPersonaje = new PictureBox();
            pbMesa1 = new PictureBox();
            pbMesa2 = new PictureBox();
            pbMesa3 = new PictureBox();
            pbMesa4 = new PictureBox();
            panel1 = new Panel();
            lblEntregados = new Label();
            lblDocumentos = new Label();
            lblTiempo = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            pbGenerador = new PictureBox();
            panel2 = new Panel();
            lblMesaDestino = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa4).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbGenerador).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // pbPersonaje
            // 
            pbPersonaje.BackColor = Color.Transparent;
            pbPersonaje.Image = Properties.Resources.gris_frente3;
            pbPersonaje.Location = new Point(394, 208);
            pbPersonaje.Name = "pbPersonaje";
            pbPersonaje.Size = new Size(88, 119);
            pbPersonaje.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPersonaje.TabIndex = 4;
            pbPersonaje.TabStop = false;
            // 
            // pbMesa1
            // 
            pbMesa1.BackColor = Color.Green;
            pbMesa1.Location = new Point(31, 208);
            pbMesa1.Name = "pbMesa1";
            pbMesa1.Size = new Size(125, 62);
            pbMesa1.TabIndex = 6;
            pbMesa1.TabStop = false;
            // 
            // pbMesa2
            // 
            pbMesa2.BackColor = Color.Green;
            pbMesa2.Location = new Point(31, 470);
            pbMesa2.Name = "pbMesa2";
            pbMesa2.Size = new Size(125, 62);
            pbMesa2.TabIndex = 7;
            pbMesa2.TabStop = false;
            // 
            // pbMesa3
            // 
            pbMesa3.BackColor = Color.Green;
            pbMesa3.Location = new Point(700, 208);
            pbMesa3.Name = "pbMesa3";
            pbMesa3.Size = new Size(125, 62);
            pbMesa3.TabIndex = 8;
            pbMesa3.TabStop = false;
            // 
            // pbMesa4
            // 
            pbMesa4.BackColor = Color.Green;
            pbMesa4.Location = new Point(700, 470);
            pbMesa4.Name = "pbMesa4";
            pbMesa4.Size = new Size(125, 62);
            pbMesa4.TabIndex = 9;
            pbMesa4.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblEntregados);
            panel1.Controls.Add(lblDocumentos);
            panel1.Controls.Add(lblTiempo);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(143, 125);
            panel1.TabIndex = 10;
            // 
            // lblEntregados
            // 
            lblEntregados.AutoSize = true;
            lblEntregados.Location = new Point(15, 84);
            lblEntregados.Name = "lblEntregados";
            lblEntregados.Size = new Size(50, 20);
            lblEntregados.TabIndex = 2;
            lblEntregados.Text = "label3";
            // 
            // lblDocumentos
            // 
            lblDocumentos.AutoSize = true;
            lblDocumentos.Location = new Point(15, 48);
            lblDocumentos.Name = "lblDocumentos";
            lblDocumentos.Size = new Size(50, 20);
            lblDocumentos.TabIndex = 1;
            lblDocumentos.Text = "label2";
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.Location = new Point(15, 15);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(50, 20);
            lblTiempo.TabIndex = 0;
            lblTiempo.Text = "label1";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // pbGenerador
            // 
            pbGenerador.BackColor = Color.Green;
            pbGenerador.Location = new Point(373, 48);
            pbGenerador.Name = "pbGenerador";
            pbGenerador.Size = new Size(125, 63);
            pbGenerador.TabIndex = 11;
            pbGenerador.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblMesaDestino);
            panel2.Location = new Point(578, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(291, 81);
            panel2.TabIndex = 12;
            // 
            // lblMesaDestino
            // 
            lblMesaDestino.AutoSize = true;
            lblMesaDestino.Location = new Point(14, 19);
            lblMesaDestino.Name = "lblMesaDestino";
            lblMesaDestino.Size = new Size(50, 20);
            lblMesaDestino.TabIndex = 0;
            lblMesaDestino.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 228);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 1;
            label1.Text = "Mesa1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(65, 491);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 13;
            label2.Text = "Mesa2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(734, 228);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 14;
            label3.Text = "Mesa3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(734, 491);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 15;
            label4.Text = "Mesa4";
            // 
            // FormTrabajo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 567);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(panel2);
            Controls.Add(pbGenerador);
            Controls.Add(panel1);
            Controls.Add(pbMesa4);
            Controls.Add(pbMesa3);
            Controls.Add(pbMesa2);
            Controls.Add(pbMesa1);
            Controls.Add(pbPersonaje);
            Name = "FormTrabajo";
            Text = "FormTrabajo";
            ((System.ComponentModel.ISupportInitialize)pbPersonaje).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbMesa4).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbGenerador).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbPersonaje;
        private PictureBox pbMesa1;
        private PictureBox pbMesa2;
        private PictureBox pbMesa3;
        private PictureBox pbMesa4;
        private Panel panel1;
        private Label lblEntregados;
        private Label lblDocumentos;
        private Label lblTiempo;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pbGenerador;
        private Panel panel2;
        private Label lblMesaDestino;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}