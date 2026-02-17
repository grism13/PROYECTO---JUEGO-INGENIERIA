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
        // Variable para detectar los movimientos de teclas
        bool goArriba, goAbajo, goIzquierda, goDerecha;

        //Veocidad del jugador
        int velocidad = 5;


        // Se crea una variable estatica 
        public static Jugador? JugadorActual;
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
                lblNombreJugador.Text = "Jugador: " + JugadorActual.Nombre;
                lblNivel.Text = "Nivel: " + JugadorActual.Nivel.ToString();
            }
        }

        // Aqui se viene la cojida de codigo :( 


        // Esto detecta cuando el jugador preciona las teclas
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goArriba = true;
            if (e.KeyCode == Keys.Down) goAbajo = true;
            if (e.KeyCode == Keys.Left) goIzquierda = true;
            if (e.KeyCode == Keys.Right) goDerecha = true;
        }

        // Y esto cuando las suelta
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goArriba = false;
            if (e.KeyCode == Keys.Down) goAbajo = false;
            if (e.KeyCode == Keys.Left) goIzquierda = false;
            if (e.KeyCode == Keys.Right) goDerecha = false;
        }

        // Aqui si se viene lo bueno, el movimiento del jugador
        private void tmrMovimiento_Tick(object sender, EventArgs e)
        {
            // Movimiento Vertical
            if (goArriba == true && pbPersonaje.Top > 0)
            {
                pbPersonaje.Top -= velocidad; // Subir es restar Y
            }
            if (goAbajo == true && pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height)
            {
                pbPersonaje.Top += velocidad; // Bajar es sumar Y
            }

            // Movimiento Horizontal
            if (goIzquierda == true && pbPersonaje.Left > 0)
            {
                pbPersonaje.Left -= velocidad; // Izquierda es restar X
            }
            if (goDerecha == true && pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
            {
                pbPersonaje.Left += velocidad; // Derecha es sumar X
            }
        }
    }
}
