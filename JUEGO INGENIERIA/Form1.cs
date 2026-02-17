using JUEGO_INGENIERIA.Vistas;
using JUEGO_INGENIERIA.Properties; // NECESARIO para las imágenes
using System;
using System.Collections.Generic; // NECESARIO para las Listas
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
        // --- VARIABLES DE MOVIMIENTO (Tuyas) ---
        bool goArriba, goAbajo, goIzquierda, goDerecha;
        int velocidad = 5;

        // --- VARIABLES DE DATOS (Tuyas) ---
        public static Jugador? JugadorActual;

        // --- VARIABLES NUEVAS PARA ANIMACIÓN (Agregadas) ---
        List<Image> animAbajo = new List<Image>();
        List<Image> animArriba = new List<Image>();
        List<Image> animIzquierda = new List<Image>();
        List<Image> animDerecha = new List<Image>();
        int frameActual = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // --- TU CÓDIGO IMPORTANTE: INTRO Y REGISTRO ---
        // Esto NO se ha tocado, sigue abriendo tus ventanas al inicio
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
            FormIntro intro = new FormIntro();
            intro.ShowDialog();

            FormAdmision registro = new FormAdmision();
            registro.ShowDialog();

            this.Show();

            // Truco: Aseguramos que el foco vuelva al mapa para poder movernos
            this.Focus();
        }

        // --- TU CÓDIGO IMPORTANTE: ACTUALIZAR ETIQUETAS ---
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (JugadorActual != null)
            {
                lblNombreJugador.Text = "Jugador: " + JugadorActual.Nombre;
                lblNivel.Text = "Nivel: " + JugadorActual.Nivel.ToString();
            }
        }

        // --- NUEVO: CARGA DE IMÁGENES ---
        // Necesitamos esto para llenar los álbumes.
        // SI ESTO NO SE EJECUTA, dale doble clic al fondo del Formulario en diseño.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Abajo (Frente)
            animAbajo.Add(Resources.gris_frente1);
            animAbajo.Add(Resources.gris_frente2);

            // Arriba (Espalda)
            animArriba.Add(Resources.gris_espalda1);
            animArriba.Add(Resources.gris_espalda2);
            animArriba.Add(Resources.gris_espalda3);

            // Derecha
            animDerecha.Add(Resources.gris_ladoderecho1);
            animDerecha.Add(Resources.gris_ladoderecho2);
            animDerecha.Add(Resources.gris_ladoderecho3);

            // Izquierda
            animIzquierda.Add(Resources.gris_ladoizquiedo1);
            animIzquierda.Add(Resources.gris_ladoizquiedo2);
            animIzquierda.Add(Resources.gris_ladoizquiedo3);

            pbPersonaje.Image = Resources.gris_frente2; // Imagen inicial
        }

        // --- TECLAS (Igual que antes) ---
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goArriba = true;
            if (e.KeyCode == Keys.Down) goAbajo = true;
            if (e.KeyCode == Keys.Left) goIzquierda = true;
            if (e.KeyCode == Keys.Right) goDerecha = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) goArriba = false;
            if (e.KeyCode == Keys.Down) goAbajo = false;
            if (e.KeyCode == Keys.Left) goIzquierda = false;
            if (e.KeyCode == Keys.Right) goDerecha = false;
        }

        // --- MOVIMIENTO CON ANIMACIÓN (Mejorado) ---
        private void tmrMovimiento_Tick(object sender, EventArgs e)
        {
            bool seMueve = false;

            // Movimiento Vertical
            if (goArriba == true && pbPersonaje.Top > 0)
            {
                pbPersonaje.Top -= velocidad;
                Animar(animArriba); // <--- AGREGADO
                seMueve = true;
            }
            if (goAbajo == true && pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height)
            {
                pbPersonaje.Top += velocidad;
                Animar(animAbajo); // <--- AGREGADO
                seMueve = true;
            }

            // Movimiento Horizontal
            if (goIzquierda == true && pbPersonaje.Left > 0)
            {
                pbPersonaje.Left -= velocidad;
                Animar(animIzquierda); // <--- AGREGADO
                seMueve = true;
            }
            if (goDerecha == true && pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
            {
                pbPersonaje.Left += velocidad;
                Animar(animDerecha); // <--- AGREGADO
                seMueve = true;
            }

            // Si se detiene, reseteamos la animación
            if (seMueve == false)
            {
                frameActual = 0;
            }
        }

        int contadorLentitud = 0;

        // --- FUNCIÓN DE ANIMAR (Nueva) ---
        private void Animar(List<Image> animacion)
        {
            if (animacion.Count > 0)
            {
                contadorLentitud++;

                // Solo cambiamos la imagen cada 4 o 5 ciclos del reloj (ajusta este número)
                if (contadorLentitud > 4)
                {
                    frameActual++;
                    if (frameActual >= animacion.Count) frameActual = 0;
                    pbPersonaje.Image = animacion[frameActual];

                    contadorLentitud = 0; // Reiniciamos el contador
                }
            }
        }
    }
}