using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace JUEGO_INGENIERIA.Vistas
{

    public partial class FormOno : Form
    {

        private Random generadorAleatorio = new Random();
        public int miNumero = 0;
        public FormOno()
        {
            InitializeComponent();

        }

        private int ObtenerNumeroAleatorio(int minimo, int maximo)
        {
            return generadorAleatorio.Next(minimo, maximo + 1);
        }



        private void btnEnviar_Click(object sender, EventArgs e)
        {

            int miNumeroDeLaSuerte = ObtenerNumeroAleatorio(1, 5);
            

        }

        private void FormOno_Load(object sender, EventArgs e)
        {

            cmbOpcionesNPC.Items.Clear();

            // Agregamos las preguntas predeterminadas
            cmbOpcionesNPC.Items.Add("¿Crees que logré pasar Matemática este semestre sin ir a reparación?");
            cmbOpcionesNPC.Items.Add("¿Será que por fin saco un 20 en el proyecto de Programación?");
            cmbOpcionesNPC.Items.Add("¿Sobreviviré a Circuitos sin quemar la protoboard?");
            cmbOpcionesNPC.Items.Add("¿Cuántas tazas de café necesito para entender esta guía antes del parcial?");
            cmbOpcionesNPC.Items.Add("¿Crees que el profesor me acepte el código?");



            cmbOpcionesNPC.SelectedIndex = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        
}
}
