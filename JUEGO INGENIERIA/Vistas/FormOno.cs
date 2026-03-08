using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
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
        private int numeroPendiente = 0;
        private string preguntaPendiente = "";
        int altoOriginalCarta;
        int topOriginalCarta;
        // Ajusta estas coordenadas (X, Y) al centro de tu formulario
        private Point puntoCentro = new Point(340, 200);
        // La 'lista' guarda todas las preguntas como si fuera un inventario
        private List<string> listaPreguntas = new List<string>();

        // El 'índice' funciona como un contador matemático que señala en qué número de pregunta se está
        private int indicePregunta = 0;

        public FormOno()
        {
            InitializeComponent();
            AplicarFuente();
            this.DoubleBuffered = true;
        }

        private int ObtenerNumeroAleatorio(int minimo, int maximo)
        {
            return generadorAleatorio.Next(minimo, maximo + 1);
        }

        private void FormOno_Load(object sender, EventArgs e)
        {
            listaPreguntas.Add("¿Crees que logré pasar Matemática este semestre sin ir a reparación?");
            listaPreguntas.Add("¿Será que por fin saco un 20 en el proyecto de Programación?");
            listaPreguntas.Add("¿Sobreviviré a Circuitos sin quemar la protoboard?");
            listaPreguntas.Add("¿Cuántas tazas de café necesito para entender esta guía antes del parcial?");
            listaPreguntas.Add("¿Crees que el profesor me acepte el código?");

            lblPreguntaActual.Text = listaPreguntas[0];

            // 1. Guardamos las cartas en la lista
            listaCartas = new List<PictureBox> { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };
            altoOriginalCarta = pbCartaRevelada.Height;
            topOriginalCarta = pbCartaRevelada.Top;
            pbCartaRevelada.Height = 0; // La escondemos al principio

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

            // 1. Ocultamos la carta revelada y el texto por si es la segunda vez que se juega
            pbCartaRevelada.Image = null;
            lblTexto.Text = "";

            // 2. Iniciamos la animación hacia el centro
            moviendoAlCentro = true;
            timerAnimacion.Start();

            // 3. Calculamos la respuesta y la guardamos en la memoria temporal (SIN MOSTRARLA)
            numeroPendiente = ObtenerNumeroAleatorio(1, 5);
            preguntaPendiente = lblPreguntaActual.Text;
        }

        // Según el número, asignamos la frase y la imagen real de la carta

        // IMPORTANTE: Cambiar "Properties.Resources.Carta1" por los nombres reales de las imágenes en el proyecto

        private void RevelarResultadoFinal()
        {
            string expresionRespuesta = "";

            switch (numeroPendiente)
            {
                case 1:
                    expresionRespuesta = "Uy...";
                    pbCartaRevelada.Image = Properties.Resources.Carta1;
                    break;
                case 2:
                    expresionRespuesta = "Mmm...";
                    pbCartaRevelada.Image = Properties.Resources.Carta2;
                    break;
                case 3:
                    expresionRespuesta = "Bueno, no está mal";
                    pbCartaRevelada.Image = Properties.Resources.Carta3;
                    break;
                case 4:
                    expresionRespuesta = "¡Eso suena prometedor!";
                    pbCartaRevelada.Image = Properties.Resources.Carta4;
                    break;
                case 5:
                    expresionRespuesta = "¡Wow, eso es increíble!";
                    pbCartaRevelada.Image = Properties.Resources.Carta5;
                    break;
            }
            // Se aplasta la carta y se empuja hacia abajo
            pbCartaRevelada.Height = 0;
            pbCartaRevelada.Top = topOriginalCarta + altoOriginalCarta;

            // ¡Se enciende la animación!
            timerRevelarCarta.Start();

            lblTexto.Text = $"{expresionRespuesta} Tienes un {numeroPendiente} de 5 a tu pregunta:\n{preguntaPendiente}";
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
                    RevelarResultadoFinal();
                }
            }
        }

        private void timerRevelarCarta_Tick(object sender, EventArgs e)
        {
            int velocidadAparicion = 15;

            if (pbCartaRevelada.Height < altoOriginalCarta)
            {
                pbCartaRevelada.Height += velocidadAparicion;
                pbCartaRevelada.Top -= velocidadAparicion;
            }
            else
            {
                // Llegó a su tamaño máximo, se ajusta y se apaga el motor
                pbCartaRevelada.Height = altoOriginalCarta;
                pbCartaRevelada.Top = topOriginalCarta;
                timerRevelarCarta.Stop();
            }
        }

        private void pbFlechaDer_Click(object sender, EventArgs e)
        {
            indicePregunta++; // Avanza una posición hacia adelante

            // Si se llegó al final de la lista, el contador vuelve al principio
            if (indicePregunta >= listaPreguntas.Count)
            {
                indicePregunta = 0;
            }

            // Finalmente, se actualiza el texto visible en la pantalla
            lblPreguntaActual.Text = listaPreguntas[indicePregunta];
        }

        private void pbFlechaIzq_Click(object sender, EventArgs e)
        {
            indicePregunta--; // Retrocede una posición

            // Si bajó de cero, salta a la última pregunta disponible
            if (indicePregunta < 0)
            {
                indicePregunta = listaPreguntas.Count - 1;
            }

            // Se actualiza el texto visible
            lblPreguntaActual.Text = listaPreguntas[indicePregunta];
        }
        private void AplicarFuente()
        {
            try
            {
                string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(rutaFuente);

                Font fuenteRetro = new Font(pfc.Families[0], 9f);

                lblTexto.Font = fuenteRetro;
                lblPreguntaActual.Font = fuenteRetro; // El nuevo Label del carrusel
            }
            catch { }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();    
        }
    }
}