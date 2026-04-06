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
            pbMarcel = new PictureBox();
            pnlIntro = new Panel();
            lblMarcelText = new Label();
            pbFondoNarrativa = new PictureBox();
            tmrGameLoop = new System.Windows.Forms.Timer(components);
            btnSkipDialogo = new Button();
            timerEscritura = new System.Windows.Forms.Timer(components);
            pnlEscenario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbMarcel).BeginInit();
            pnlIntro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbFondoNarrativa).BeginInit();
            SuspendLayout();
            // 
            // pnlEscenario
            // 
            pnlEscenario.BackColor = Color.Black;
            pnlEscenario.BackgroundImageLayout = ImageLayout.None;
            pnlEscenario.Controls.Add(btnSkipDialogo);
            pnlEscenario.Controls.Add(pbMarcel);
            pnlEscenario.Controls.Add(pnlIntro);
            pnlEscenario.Location = new Point(1, -3);
            pnlEscenario.Margin = new Padding(0);
            pnlEscenario.Name = "pnlEscenario";
            pnlEscenario.Size = new Size(1290, 725);
            pnlEscenario.TabIndex = 0;
            // 
            // pbMarcel
            // 
            pbMarcel.BackColor = Color.DimGray;
            pbMarcel.Location = new Point(262, 448);
            pbMarcel.Name = "pbMarcel";
            pbMarcel.Size = new Size(158, 198);
            pbMarcel.TabIndex = 1;
            pbMarcel.TabStop = false;
            // 
            // pnlIntro
            // 
            pnlIntro.BackColor = Color.Gray;
            pnlIntro.Controls.Add(lblMarcelText);
            pnlIntro.Controls.Add(pbFondoNarrativa);
            pnlIntro.Location = new Point(386, 524);
            pnlIntro.Name = "pnlIntro";
            pnlIntro.Size = new Size(200, 100);
            pnlIntro.TabIndex = 0;
            // 
            // lblMarcelText
            // 
            lblMarcelText.AutoSize = true;
            lblMarcelText.Location = new Point(68, 31);
            lblMarcelText.Name = "lblMarcelText";
            lblMarcelText.Size = new Size(38, 15);
            lblMarcelText.TabIndex = 1;
            lblMarcelText.Text = "label1";
            // 
            // pbFondoNarrativa
            // 
            pbFondoNarrativa.Location = new Point(57, 21);
            pbFondoNarrativa.Name = "pbFondoNarrativa";
            pbFondoNarrativa.Size = new Size(100, 50);
            pbFondoNarrativa.TabIndex = 0;
            pbFondoNarrativa.TabStop = false;
            // 
            // tmrGameLoop
            // 
            tmrGameLoop.Interval = 20;
            tmrGameLoop.Tick += tmrGameLoop_Tick;
            // 
            // btnSkipDialogo
            // 
            btnSkipDialogo.Location = new Point(551, 632);
            btnSkipDialogo.Name = "btnSkipDialogo";
            btnSkipDialogo.Size = new Size(75, 23);
            btnSkipDialogo.TabIndex = 2;
            btnSkipDialogo.Text = "button1";
            btnSkipDialogo.UseVisualStyleBackColor = true;
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
            ((System.ComponentModel.ISupportInitialize)pbMarcel).EndInit();
            pnlIntro.ResumeLayout(false);
            pnlIntro.PerformLayout();
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