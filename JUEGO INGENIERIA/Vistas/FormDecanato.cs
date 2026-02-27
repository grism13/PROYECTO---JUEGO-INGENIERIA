using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormDecanato : Form
    {
        private FormMovimiento motorMovimiento;

        public FormDecanato()
        {
            InitializeComponent();
            timer1.Start();
            DoubleBuffered = true;
            motorMovimiento = new FormMovimiento(this, pbPersonaje);
            motorMovimiento.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (motorMovimiento != null)
            {
                motorMovimiento.DibujarPersonaje(e.Graphics);
            }

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox x)
                {
                    if ((string)x.Tag != "muro" && x.Name.StartsWith("pictureBox"))
                    {
                        if (x.Image != null && pbPersonaje.Bounds.IntersectsWith(x.Bounds))
                        {
                            e.Graphics.DrawImage(x.Image, x.Left, x.Top, x.Width, x.Height);
                        }
                    }
                }
            }
        }


        // Variables para animacion de telefono

        private int pasoAnimacion = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            pasoAnimacion++;

            if (pasoAnimacion > 3)
            {
                pasoAnimacion = 0;
            }

            switch (pasoAnimacion)
            {
                case 0:
                    pictureBox1.Image = Properties.Resources.flavioHablando1;
                    break;
                case 1:
                    pictureBox1.Image = Properties.Resources.flavioHablando2;
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.flavioHablando3;
                    break;
                case 3:
                    pictureBox1.Image = Properties.Resources.flavioTranquilo;
                    break;
            }
        }


        private void tmrRevisarZonas_Tick(object sender, EventArgs e)
        {
            // Verificamos si TU personaje pisa la zona
            if (pbPersonaje.Bounds.IntersectsWith(pbZonaActiva.Bounds))
            {
                // Solo activamos esto si el panel estaba oculto (acaba de entrar a la zona)
                if (panelInfo.Visible == false)
                {
                    panelInfo.Visible = true;

                    // 1. Pausamos el Timer que hace que Flavio se mueva
                    timer1.Stop();

                    // 2. Le ponemos la imagen estática a Flavio
                    // IMPORTANTE: Cambia "flavio_estatico" por el nombre exacto de la imagen de tu proyecto
                    pictureBox1.Image = Properties.Resources.flavioTranquilo;
                }
            }
            else
            {
                // Si sales de la zona y el panel estaba visible...
                if (panelInfo.Visible == true)
                {
                    panelInfo.Visible = false;

                    // 3. Volvemos a iniciar el Timer de Flavio para que continúe su animación normal
                    timer1.Start();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hola");
            this.ActiveControl = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("toma");
            this.ActiveControl = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("toma");
            this.ActiveControl = null;
        }
    }
}
