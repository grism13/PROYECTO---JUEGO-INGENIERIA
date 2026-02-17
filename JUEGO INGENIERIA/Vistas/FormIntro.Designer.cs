namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormIntro
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
            lblTexto = new Label();
            btnSiguiente = new Button();
            pbImagen = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbImagen).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblTexto
            // 
            lblTexto.BackColor = Color.Transparent;
            lblTexto.Font = new Font("Times New Roman", 10F);
            lblTexto.ForeColor = Color.Black;
            lblTexto.Location = new Point(800, 151);
            lblTexto.Name = "lblTexto";
            lblTexto.Size = new Size(538, 233);
            lblTexto.TabIndex = 1;
            lblTexto.Text = "label1";
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Cursor = Cursors.Hand;
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.Font = new Font("Impact", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.White;
            btnSiguiente.Image = Properties.Resources.boton_de_siguiente;
            btnSiguiente.Location = new Point(948, 534);
            btnSiguiente.Margin = new Padding(3, 2, 3, 2);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(263, 61);
            btnSiguiente.TabIndex = 2;
            btnSiguiente.Text = "SIGUIENTE";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click_1;
            // 
            // pbImagen
            // 
            pbImagen.Image = Properties.Resources.primera_imagen_de_la_intro;
            pbImagen.Location = new Point(173, 126);
            pbImagen.Margin = new Padding(3, 2, 3, 2);
            pbImagen.Name = "pbImagen";
            pbImagen.Size = new Size(479, 374);
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            pbImagen.TabIndex = 0;
            pbImagen.TabStop = false;
            pbImagen.Click += pbImagen_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.fondo_tipo_televisor_para_intro;
            pictureBox1.Location = new Point(-88, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1001, 763);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // FormIntro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1383, 680);
            Controls.Add(pbImagen);
            Controls.Add(btnSiguiente);
            Controls.Add(lblTexto);
            Controls.Add(pictureBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormIntro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HISTORIA";
            FormClosing += FormIntro_FormClosing;
            Load += FormIntro_Load;
            ((System.ComponentModel.ISupportInitialize)pbImagen).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblTexto;
        private Button btnSiguiente;
        private PictureBox pbImagen;
        private PictureBox pictureBox1;
    }
}