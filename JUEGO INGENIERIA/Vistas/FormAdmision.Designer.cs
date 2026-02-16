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
            label1 = new Label();
            label2 = new Label();
            dvJugadores = new DataGridView();
            button1 = new Button();
            btnEliminar = new Button();
            txtNombre = new TextBox();
            btnCrear = new Button();
            ((System.ComponentModel.ISupportInitialize)dvJugadores).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(953, 181);
            label1.Name = "label1";
            label1.Size = new Size(252, 34);
            label1.TabIndex = 0;
            label1.Text = "Registrate Jugador!!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(257, 56);
            label2.Name = "label2";
            label2.Size = new Size(84, 34);
            label2.TabIndex = 1;
            label2.Text = "Datos";
            // 
            // dvJugadores
            // 
            dvJugadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dvJugadores.BackgroundColor = Color.White;
            dvJugadores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dvJugadores.Location = new Point(67, 129);
            dvJugadores.Name = "dvJugadores";
            dvJugadores.RowHeadersWidth = 51;
            dvJugadores.Size = new Size(557, 437);
            dvJugadores.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = Color.SeaGreen;
            button1.Font = new Font("Trebuchet MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(67, 603);
            button1.Name = "button1";
            button1.Size = new Size(249, 100);
            button1.TabIndex = 3;
            button1.Text = "JUGAR";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.Firebrick;
            btnEliminar.Font = new Font("Trebuchet MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(375, 603);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(249, 100);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(953, 258);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(249, 27);
            txtNombre.TabIndex = 5;
            // 
            // btnCrear
            // 
            btnCrear.BackColor = Color.Navy;
            btnCrear.Font = new Font("Trebuchet MS", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCrear.ForeColor = Color.White;
            btnCrear.Location = new Point(953, 343);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(249, 100);
            btnCrear.TabIndex = 6;
            btnCrear.Text = "CREAR";
            btnCrear.UseVisualStyleBackColor = false;
            btnCrear.Click += btnCrear_Click;
            // 
            // FormAdmision
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1450, 738);
            Controls.Add(btnCrear);
            Controls.Add(txtNombre);
            Controls.Add(btnEliminar);
            Controls.Add(button1);
            Controls.Add(dvJugadores);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormAdmision";
            Text = "FormAdmision";
            ((System.ComponentModel.ISupportInitialize)dvJugadores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DataGridView dvJugadores;
        private Button button1;
        private Button btnEliminar;
        private TextBox txtNombre;
        private Button btnCrear;
    }
}