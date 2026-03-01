using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormOno : Form
    {
        private Random generadorAleatorio = new Random();

        // --- VARIABLES PARA LA ANIMACIÓN ---
        private Dictionary<PictureBox, Point> posicionesOriginales = new Dictionary<PictureBox, Point>();
        private List<PictureBox> listaCartas = new List<PictureBox>();
        private bool moviendoAlCentro = true;

        // Ajusta estas coordenadas (X, Y) al centro de tu formulario
        private Point puntoCentro = new Point(340, 200);

        public FormOno()
        {
            InitializeComponent();
        }

        private int ObtenerNumeroAleatorio(int minimo, int maximo)
        {
            return generadorAleatorio.Next(minimo, maximo + 1);
        }

        private void FormOno_Load(object sender, EventArgs e)
        {
            cmbOpcionesNPC.Items.Clear();
            cmbOpcionesNPC.Items.Add("¿Crees que logré pasar Matemática este semestre sin ir a reparación?");
            cmbOpcionesNPC.Items.Add("¿Será que por fin saco un 20 en el proyecto de Programación?");
            cmbOpcionesNPC.Items.Add("¿Sobreviviré a Circuitos sin quemar la protoboard?");
            cmbOpcionesNPC.Items.Add("¿Cuántas tazas de café necesito para entender esta guía antes del parcial?");
            cmbOpcionesNPC.Items.Add("¿Crees que el profesor me acepte el código?");
            cmbOpcionesNPC.SelectedIndex = 0;

            // 1. Guardamos las cartas en la lista
            listaCartas = new List<PictureBox> { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };

            // 2. Registramos la posición inicial de cada una
            foreach (var carta in listaCartas)
            {
                posicionesOriginales[carta] = carta.Location;
            }
        }

        private void accionDeBtn()
        {
            // Si la animación ya está corriendo, no hacemos nada
            if (timerAnimacion.Enabled) return;

            // Iniciamos la animación hacia el centro
            moviendoAlCentro = true;
            timerAnimacion.Start();

            // Lógica del juego
            int numeroGenerado = ObtenerNumeroAleatorio(1, 5);
            string preguntaSeleccionada = cmbOpcionesNPC.SelectedItem.ToString();
            string expresionRespuesta = "";

            switch (numeroGenerado)
            {
                case 1: expresionRespuesta = "Uy..."; break;
                case 2: expresionRespuesta = "Mmm..."; break;
                case 3: expresionRespuesta = "Bueno, no está mal"; break;
                case 4: expresionRespuesta = "¡Eso suena prometedor!"; break;
                case 5: expresionRespuesta = "¡Wow, eso es increíble!"; break;
            }

            lblTexto.Text = $"{expresionRespuesta} Tienes un {numeroGenerado} de 5 a tu pregunta:\n{preguntaSeleccionada}";
            return;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            accionDeBtn();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            accionDeBtn();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            accionDeBtn();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            accionDeBtn();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            accionDeBtn();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            accionDeBtn();
        }

        private void timerAnimacion_Tick(object sender, EventArgs e)
        {
            bool todasLlegaron = true;
            int velocidad = 14; 
            foreach (var carta in listaCartas)
            {
                
                Point destino = moviendoAlCentro ? puntoCentro : posicionesOriginales[carta];

                if (Math.Abs(carta.Left - destino.X) > velocidad)
                {
                    carta.Left += (carta.Left < destino.X) ? velocidad : -velocidad;
                    todasLlegaron = false;
                }
                else
                {
                    carta.Left = destino.X;
                }

                
                if (Math.Abs(carta.Top - destino.Y) > velocidad)
                {
                    carta.Top += (carta.Top < destino.Y) ? velocidad : -velocidad;
                    todasLlegaron = false;
                }
                else
                {
                    carta.Top = destino.Y;
                }
            }

            // Control de flujo de la animación
            if (todasLlegaron)
            {
                if (moviendoAlCentro)
                {
                    moviendoAlCentro = false;
                }
                else
                {
                    timerAnimacion.Stop();
                }
            }
        }


    }
}