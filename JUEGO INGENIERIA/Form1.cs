using JUEGO_INGENIERIA.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
        // Se crea una variable estatica 
        public static Jugador JugadorActual;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Aqui se oculta inicio y abre FormNarrativa.cs
            this.Hide();
            FormIntro intro = new FormIntro();
            intro.ShowDialog();

            //Aqui deberia comenzar el registro del usuario :( Tercera evaluacion de c# y sigo odiandolo
            FormAdmision registro = new FormAdmision();
            registro.ShowDialog();

            this.Show();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (JugadorActual != null)
            {
                lblNombreJugador.Text = "Jugador; " + JugadorActual.Nombre;
                lblNivel.Text = "Nivel: " + JugadorActual.Nivel.ToString();
            }
        }
    }
}
