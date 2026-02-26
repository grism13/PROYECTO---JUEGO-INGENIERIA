using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Vistas;


namespace JUEGO_INGENIERIA.Vistas
{
    public partial class ElegirPersonaje : Form
    {
        public ElegirPersonaje()
        {
            InitializeComponent();
        }


        public static class DatosJuego
        {
            public static string PersonajeElegido = "Gris";
        }

        // Para elegir el personaje de Eliezer
        private void ElegirEliezer_Click(object sender, EventArgs e)
        {
            Vistas.DatosJuego.PersonajeElegido = "Eliezer";
            this.Close();
        }

        // Para elegir a Roand
        private void ElegirRoand_Click(object sender, EventArgs e)
        {
            Vistas.DatosJuego.PersonajeElegido = "Roand";
            this.Close();
        }

        // Para elegir a Gris
        private void ElegirGris_Click(object sender, EventArgs e)
        {
            Vistas.DatosJuego.PersonajeElegido = "Gris";
            this.Close();
        }

       



    }
}
