namespace JUEGO_INGENIERIA.Vistas
{
    partial class ElegirPersonaje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElegirPersonaje));
            gris = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ElegirGris = new Button();
            ElegirEliezer = new Button();
            ElegirRoand = new Button();
            ((System.ComponentModel.ISupportInitialize)gris).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // gris
            // 
            gris.BackColor = Color.Transparent;
            gris.Image = Properties.Resources.gris_frente1;
            gris.Location = new Point(254, 124);
            gris.Name = "gris";
            gris.Size = new Size(248, 311);
            gris.SizeMode = PictureBoxSizeMode.StretchImage;
            gris.TabIndex = 0;
            gris.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(600, 124);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(248, 311);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(944, 124);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(248, 311);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // ElegirGris
            // 
            ElegirGris.BackColor = Color.RoyalBlue;
            ElegirGris.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ElegirGris.ForeColor = SystemColors.ButtonFace;
            ElegirGris.Location = new Point(254, 478);
            ElegirGris.Name = "ElegirGris";
            ElegirGris.Size = new Size(248, 82);
            ElegirGris.TabIndex = 3;
            ElegirGris.Text = "ELEGIR";
            ElegirGris.UseVisualStyleBackColor = false;
            ElegirGris.Click += ElegirGris_Click;
            // 
            // ElegirEliezer
            // 
            ElegirEliezer.BackColor = Color.RoyalBlue;
            ElegirEliezer.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ElegirEliezer.ForeColor = SystemColors.ButtonFace;
            ElegirEliezer.Location = new Point(600, 478);
            ElegirEliezer.Name = "ElegirEliezer";
            ElegirEliezer.Size = new Size(248, 82);
            ElegirEliezer.TabIndex = 4;
            ElegirEliezer.Text = "ELEGIR";
            ElegirEliezer.UseVisualStyleBackColor = false;
            ElegirEliezer.Click += ElegirEliezer_Click;
            // 
            // ElegirRoand
            // 
            ElegirRoand.BackColor = Color.RoyalBlue;
            ElegirRoand.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ElegirRoand.ForeColor = SystemColors.ButtonFace;
            ElegirRoand.Location = new Point(944, 478);
            ElegirRoand.Name = "ElegirRoand";
            ElegirRoand.Size = new Size(248, 82);
            ElegirRoand.TabIndex = 5;
            ElegirRoand.Text = "ELEGIR";
            ElegirRoand.UseVisualStyleBackColor = false;
            ElegirRoand.Click += ElegirRoand_Click;
            // 
            // ElegirPersonaje
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1480, 757);
            Controls.Add(ElegirRoand);
            Controls.Add(ElegirEliezer);
            Controls.Add(ElegirGris);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(gris);
            Name = "ElegirPersonaje";
            Text = "ElegirPersonaje";
            ((System.ComponentModel.ISupportInitialize)gris).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox gris;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button ElegirGris;
        private Button ElegirEliezer;
        private Button ElegirRoand;
    }
}