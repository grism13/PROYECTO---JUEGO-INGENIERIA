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
        public FormDecanato()
        {
            InitializeComponent();
            timer1.Start();
        }

        // Variables para animacion de telefono

        private int pasoAnimacion = 0;  

        private void timer1_Tick(object sender, EventArgs e)
        {
            pasoAnimacion++;

            if (pasoAnimacion > 2)
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
            }
        }
    }
}
