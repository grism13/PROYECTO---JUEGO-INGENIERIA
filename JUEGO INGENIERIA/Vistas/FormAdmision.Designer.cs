namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormAdmision
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmision));
            registratetxt = new Label();
            label2 = new Label();
            dvJugadores = new DataGridView();
            button1 = new Button();
            btnEliminar = new Button();
            txtNombre = new TextBox();
            btnCrear = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            pictureBox4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dvJugadores).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // registratetxt
            // 
            registratetxt.AutoSize = true;
            registratetxt.BackColor = Color.Transparent;
            registratetxt.FlatStyle = FlatStyle.Flat;
            registratetxt.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            registratetxt.Image = Properties.Resources.fondo_imagen_para_registro__1_;
            registratetxt.Location = new Point(838, 425);
            registratetxt.Name = "registratetxt";
            registratetxt.Size = new Size(270, 27);
            registratetxt.TabIndex = 0;
            registratetxt.Text = "REGISTRA TU USUARIO";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(255, 224, 192);
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(176, 63);
            label2.Name = "label2";
            label2.Size = new Size(264, 27);
            label2.TabIndex = 1;
            label2.Text = "MUNDOS GUARDADOS";
            // 
            // dvJugadores
            // 
            dvJugadores.AllowUserToAddRows = false;
            dvJugadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvJugadores.BackgroundColor = Color.White;
            dvJugadores.BorderStyle = BorderStyle.None;
            dvJugadores.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dvJugadores.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dvJugadores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dvJugadores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dvJugadores.DefaultCellStyle = dataGridViewCellStyle2;
            dvJugadores.EnableHeadersVisualStyles = false;
            dvJugadores.Location = new Point(124, 116);
            dvJugadores.Margin = new Padding(3, 2, 3, 2);
            dvJugadores.Name = "dvJugadores";
            dvJugadores.RowHeadersVisible = false;
            dvJugadores.RowHeadersWidth = 51;
            dvJugadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dvJugadores.Size = new Size(389, 308);
            dvJugadores.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = Color.Teal;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Trebuchet MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(93, 468);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(170, 63);
            button1.TabIndex = 3;
            button1.Text = "JUGAR";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Maroon;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Trebuchet MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(93, 536);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(165, 62);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.White;
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.ForeColor = Color.Black;
            txtNombre.Location = new Point(865, 454);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(223, 33);
            txtNombre.TabIndex = 5;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.MidnightBlue;
            btnCrear.FlatStyle = FlatStyle.Flat;
            btnCrear.Font = new Font("Trebuchet MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCrear.ForeColor = Color.White;
            btnCrear.Location = new Point(892, 521);
            btnCrear.Margin = new Padding(3, 2, 3, 2);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(181, 41);
            btnCrear.TabIndex = 6;
            btnCrear.Text = "CREAR";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.fondo_tipo_televisor_para_intro;
            pictureBox1.Location = new Point(12, 29);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(618, 618);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.LOGO_DEL_JUEGO;
            pictureBox2.Location = new Point(685, 12);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(194, 230);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.titulo_DEL_JUEGO;
            pictureBox3.Location = new Point(838, -105);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(465, 495);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 10;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(981, 12);
            label1.Name = "label1";
            label1.Size = new Size(192, 27);
            label1.TabIndex = 12;
            label1.Text = "BIENVENIDOS A";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.fondo_imagen_para_registro__1_;
            pictureBox4.Location = new Point(742, 265);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(458, 439);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 13;
            pictureBox4.TabStop = false;
            // 
            // FormAdmision
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1367, 686);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(btnCrear);
            Controls.Add(txtNombre);
            Controls.Add(btnEliminar);
            Controls.Add(button1);
            Controls.Add(dvJugadores);
            Controls.Add(label2);
            Controls.Add(registratetxt);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            ForeColor = Color.Black;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormAdmision";
            Text = "REGISTRO";
            ((System.ComponentModel.ISupportInitialize)dvJugadores).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label registratetxt;
        private Label label2;
        private DataGridView dvJugadores;
        private Button button1;
        private Button btnEliminar;
        private TextBox txtNombre;
        private Button btnCrear;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label1;
        private PictureBox pictureBox4;
    }
}