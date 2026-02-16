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
            lblTexto.Location = new Point(935, 16);
            lblTexto.Name = "lblTexto";
            lblTexto.Size = new Size(615, 822);
            lblTexto.TabIndex = 1;
            lblTexto.Text = "label1";
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSiguiente
            // 
            btnSiguiente.FlatStyle = FlatStyle.Flat;
            btnSiguiente.Font = new Font("Impact", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSiguiente.ForeColor = Color.White;
            btnSiguiente.Image = Properties.Resources.boton_de_siguiente;
            btnSiguiente.Location = new Point(1096, 854);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(301, 81);
            btnSiguiente.TabIndex = 2;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click_1;
            // 
            // pbImagen
            // 
            pbImagen.Image = Properties.Resources.primera_imagen_de_la_intro;
            pbImagen.Location = new Point(198, 168);
            pbImagen.Name = "pbImagen";
            pbImagen.Size = new Size(547, 499);
            pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            pbImagen.TabIndex = 0;
            pbImagen.TabStop = false;
            pbImagen.Click += pbImagen_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.fondo_tipo_televisor_para_intro;
            pictureBox1.Location = new Point(-101, 16);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1144, 1017);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // FormIntro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1563, 959);
            Controls.Add(pbImagen);
            Controls.Add(btnSiguiente);
            Controls.Add(lblTexto);
            Controls.Add(pictureBox1);
            Name = "FormIntro";
            Text = "FormIntro";
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