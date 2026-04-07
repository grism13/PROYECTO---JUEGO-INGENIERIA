namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormNivel3
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
            pnlIntro = new Panel();
            btnSkipDialogo = new Button();
            lblMarcelText = new Label();
            pbMarcel = new PictureBox();
            pbFondoNarrativa = new PictureBox();
            tmrGameLoop = new System.Windows.Forms.Timer(components);
            timerEscritura = new System.Windows.Forms.Timer(components);
            pnlEscenario.SuspendLayout();
            pnlIntro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbMarcel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbFondoNarrativa).BeginInit();
            SuspendLayout();
            // 
            // pnlEscenario
            // 
            pnlEscenario.BackColor = Color.Black;
            pnlEscenario.BackgroundImageLayout = ImageLayout.None;
            pnlEscenario.Controls.Add(pnlIntro);
            pnlEscenario.Location = new Point(1, -3);
            pnlEscenario.Margin = new Padding(0);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(1290, 725);
            pnlEscenario.TabIndex = 0;
            // 
            // pnlIntro
            // 
            pnlIntro.BackColor = Color.Transparent;
            pnlIntro.Controls.Add(btnSkipDialogo);
            pnlIntro.Controls.Add(lblMarcelText);
            pnlIntro.Controls.Add(pbMarcel);
            pnlIntro.Controls.Add(pbFondoNarrativa);
            pnlIntro.Location = new Point(158, 396);
            pnlIntro.Name = "pnlIntro";
            pnlIntro.Size = new Size(943, 266);
            pnlIntro.TabIndex = 0;
            // 
            // btnSkipDialogo
            // 
            btnSkipDialogo.Cursor = Cursors.Hand;
            btnSkipDialogo.FlatAppearance.BorderSize = 0;
            btnSkipDialogo.FlatStyle = FlatStyle.Flat;
            btnSkipDialogo.ForeColor = Color.White;
            btnSkipDialogo.Image = Properties.Resources.botonazul;
            btnSkipDialogo.Location = new Point(672, 213);
            btnSkipDialogo.Name = "btnSkipDialogo";
            btnSkipDialogo.Size = new Size(160, 46);
            btnSkipDialogo.TabIndex = 2;
            btnSkipDialogo.Text = "SKIP";
            btnSkipDialogo.UseVisualStyleBackColor = true;
            // 
            // lblMarcelText
            // 
            lblMarcelText.BackColor = Color.SandyBrown;
            lblMarcelText.Location = new Point(294, 45);
            lblMarcelText.Name = "lblMarcelText";
            lblMarcelText.Size = new Size(616, 144);
            lblMarcelText.TabIndex = 1;
            lblMarcelText.Text = "label1";
            lblMarcelText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbMarcel
            // 
            pbMarcel.BackColor = Color.Transparent;
            pbMarcel.Location = new Point(44, 3);
            pbMarcel.Name = "pbMarcel";
            pbMarcel.Size = new Size(215, 256);
            pbMarcel.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMarcel.TabIndex = 1;
            pbMarcel.TabStop = false;
            // 
            // pbFondoNarrativa
            // 
            pbFondoNarrativa.Image = Properties.Resources.narrativa;
            pbFondoNarrativa.Location = new Point(265, 9);
            pbFondoNarrativa.Name = "pbFondoNarrativa";
            pbFondoNarrativa.Size = new Size(675, 209);
            pbFondoNarrativa.SizeMode = PictureBoxSizeMode.StretchImage;
            pbFondoNarrativa.TabIndex = 0;
            pbFondoNarrativa.TabStop = false;
            // 
            // tmrGameLoop
            // 
            tmrGameLoop.Interval = 20;
            tmrGameLoop.Tick += tmrGameLoop_Tick;
            // 
            // FormNivel3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlEscenario);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "FormNivel3";
            Text = "FormNivel2";
            WindowState = FormWindowState.Maximized;
            Load += FormNivel2_Load;
            KeyDown += FormNivel2_KeyDown;
            KeyUp += FormNivel2_KeyUp;
            pnlEscenario.ResumeLayout(false);
            pnlIntro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbMarcel).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbFondoNarrativa).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEscenario;
        private System.Windows.Forms.Timer tmrGameLoop;
        private Panel pnlIntro;
        private PictureBox pbFondoNarrativa;
        private PictureBox pbMarcel;
        private Label lblMarcelText;
        private Button btnSkipDialogo;
        private System.Windows.Forms.Timer timerEscritura;
    }
}