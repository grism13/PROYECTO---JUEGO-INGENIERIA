namespace JUEGO_INGENIERIA.Vistas
{
    partial class FormDerrota
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
            btnAceptar = new Button();
            lblMensaje = new Label();
            SuspendLayout();
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(664, 605);
            btnAceptar.Margin = new Padding(3, 4, 3, 4);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(86, 31);
            btnAceptar.TabIndex = 0;
            btnAceptar.Text = "button1";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // lblMensaje
            // 
            lblMensaje.AutoSize = true;
            lblMensaje.Location = new Point(685, 542);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(50, 20);
            lblMensaje.TabIndex = 1;
            lblMensaje.Text = "label1";
            // 
            // FormDerrota
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1473, 926);
            Controls.Add(lblMensaje);
            Controls.Add(btnAceptar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormDerrota";
            Text = "FormDerrota";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAceptar;
        private Label lblMensaje;
    }
}