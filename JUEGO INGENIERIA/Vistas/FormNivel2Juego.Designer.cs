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
            lblCuentaRegresiva = new Label();
            btnEmpezar = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // lblCuentaRegresiva
            // 
            lblCuentaRegresiva.AutoSize = true;
            lblCuentaRegresiva.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCuentaRegresiva.Location = new Point(322, 18);
            lblCuentaRegresiva.Name = "lblCuentaRegresiva";
            lblCuentaRegresiva.Size = new Size(147, 60);
            lblCuentaRegresiva.TabIndex = 0;
            lblCuentaRegresiva.Text = "Listo...";
            // 
            // btnEmpezar
            // 
            btnEmpezar.Location = new Point(501, 32);
            btnEmpezar.Name = "btnEmpezar";
            btnEmpezar.Size = new Size(94, 29);
            btnEmpezar.TabIndex = 1;
            btnEmpezar.Text = "Empezar";
            btnEmpezar.UseVisualStyleBackColor = true;
            btnEmpezar.Click += btnEmpezar_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(42, 368);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(65, 75);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(113, 368);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(62, 75);
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.Location = new Point(181, 368);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(58, 75);
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;
            pictureBox4.Location = new Point(245, 368);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(64, 75);
            pictureBox4.TabIndex = 5;
            pictureBox4.TabStop = false;
            // 
            // FormNivel2Juego
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 480);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btnEmpezar);
            Controls.Add(lblCuentaRegresiva);
            Name = "FormNivel2Juego";
            Text = "FormNivel2Juego";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCuentaRegresiva;
        private Button btnEmpezar;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
    }
}