namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel4_Final
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
            pnlEscenario = new Panel();
            pnlDialogo = new Panel();
            pbRetratoJefe = new PictureBox();
            btnContinuar = new Button();
            lblTextoDialogo = new Label();
            pictureBox1 = new PictureBox();
            tmrGameLoop = new System.Windows.Forms.Timer(components);
            pnlEscenario.SuspendLayout();
            pnlDialogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbRetratoJefe).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlEscenario
            // 
            pnlEscenario.Controls.Add(pnlDialogo);
            pnlEscenario.Dock = DockStyle.Fill;
            pnlEscenario.Location = new Point(0, 0);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(1280, 720);
            pnlEscenario.TabIndex = 0;
            // 
            // pnlDialogo
            // 
            pnlDialogo.BackColor = Color.Transparent;
            pnlDialogo.Controls.Add(pbRetratoJefe);
            pnlDialogo.Controls.Add(btnContinuar);
            pnlDialogo.Controls.Add(lblTextoDialogo);
            pnlDialogo.Controls.Add(pictureBox1);
            pnlDialogo.Location = new Point(84, 305);
            pnlDialogo.Name = "pnlDialogo";
            pnlDialogo.Size = new Size(1030, 276);
            pnlDialogo.TabIndex = 0;
            pnlDialogo.Visible = false;
            pnlDialogo.Paint += pnlDialogo_Paint;
            // 
            // pbRetratoJefe
            // 
            pbRetratoJefe.BackColor = Color.Transparent;
            pbRetratoJefe.Location = new Point(22, 3);
            pbRetratoJefe.Name = "pbRetratoJefe";
            pbRetratoJefe.Size = new Size(321, 259);
            pbRetratoJefe.SizeMode = PictureBoxSizeMode.StretchImage;
            pbRetratoJefe.TabIndex = 1;
            pbRetratoJefe.TabStop = false;
            // 
            // btnContinuar
            // 
            btnContinuar.BackColor = Color.Transparent;
            btnContinuar.BackgroundImageLayout = ImageLayout.Stretch;
            btnContinuar.FlatStyle = FlatStyle.Flat;
            btnContinuar.ForeColor = Color.White;
            btnContinuar.Image = Properties.Resources.botonazul;
            btnContinuar.Location = new Point(716, 227);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(158, 35);
            btnContinuar.TabIndex = 1;
            btnContinuar.Text = "Continuar...";
            btnContinuar.UseVisualStyleBackColor = false;
            // 
            // lblTextoDialogo
            // 
            lblTextoDialogo.BackColor = Color.SandyBrown;
            lblTextoDialogo.FlatStyle = FlatStyle.Flat;
            lblTextoDialogo.Location = new Point(363, 45);
            lblTextoDialogo.Name = "lblTextoDialogo";
            lblTextoDialogo.Size = new Size(619, 142);
            lblTextoDialogo.TabIndex = 3;
            lblTextoDialogo.Text = "label1";
            lblTextoDialogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.narrativa;
            pictureBox1.Location = new Point(333, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(679, 196);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // FormNivel4_Final
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlEscenario);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormNivel4_Final";
            Text = "FormNivel4_Final";
            pnlEscenario.ResumeLayout(false);
            pnlDialogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbRetratoJefe).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEscenario;
        private System.Windows.Forms.Timer tmrGameLoop;
        private Panel pnlDialogo;
        private PictureBox pbRetratoJefe;
        private PictureBox pictureBox1;
        private Label lblTextoDialogo;
        private Button btnContinuar;
    }
}