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
        // --- VARIABLES DE MOVIMIENTO ---
        bool goArriba, goAbajo, goIzquierda, goDerecha;
        int velocidad = 5;

        // --- VARIABLES DE DATOS ---
        public static Jugador? JugadorActual;

        // --- VARIABLES PARA ANIMACIÓN ---
        List<Image> animAbajo = new List<Image>();
        List<Image> animArriba = new List<Image>();
        List<Image> animIzquierda = new List<Image>();
        List<Image> animDerecha = new List<Image>();

        // --- ÁLBUMES DE DIAGONALES ---
        List<Image> animArribaDerecha = new List<Image>();
        List<Image> animArribaIzquierda = new List<Image>();
        List<Image> animAbajoDerecha = new List<Image>();
        List<Image> animAbajoIzquierda = new List<Image>();

        int frameActual = 0;
        int contadorLentitud = 0; // Lo moví aquí arriba para que sea global

        public Form1()
        {
            InitializeComponent();
        }

        // --- TU CÓDIGO IMPORTANTE: INTRO Y REGISTRO ---
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
            FormIntro intro = new FormIntro();
            intro.ShowDialog();

            FormAdmision registro = new FormAdmision();
            registro.ShowDialog();

            this.Show();
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

        // --- CARGA DE IMÁGENES ---
        private void Form1_Load(object sender, EventArgs e)
        {
            // Abajo (Frente)
            animAbajo.Add(Resources.gris_frente1);
            animAbajo.Add(Resources.gris_frente2);
            animAbajo.Add(Resources.gris_frente3);

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

            // DIAGONALES (Tus nuevas cargas)
            // Arriba + Derecha
            animArribaDerecha.Add(Resources.gris_inclinadaderechaespalda1);
            animArribaDerecha.Add(Resources.gris_inclinadaderechaespalda2);
            animArribaDerecha.Add(Resources.gris_inclinadaderechaespalda3);

            // Arriba + Izquierda
            animArribaIzquierda.Add(Resources.gris_inclinadaizquiedaespalda1);
            animArribaIzquierda.Add(Resources.gris_inclinadaizquiedaespalda2);
            animArribaIzquierda.Add(Resources.gris_inclinadaizquiedaespalda3);

            // Abajo + Derecha
            animAbajoDerecha.Add(Resources.gris_inclinadaderechafrente1);
            animAbajoDerecha.Add(Resources.gris_inclinadaderechafrente2);
            animAbajoDerecha.Add(Resources.gris_inclinadaderechafrente3);

            // Abajo + Izquierda
            animAbajoIzquierda.Add(Resources.gris_inclinadaizquiedafrente1);
            animAbajoIzquierda.Add(Resources.gris_inclinadaizquiedafrente2);
            animAbajoIzquierda.Add(Resources.gris_inclinadaizquiedafrente3);

            pbPersonaje.Image = Resources.gris_frente2; // Imagen inicial
        }

        // --- TECLAS ---
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

        // --- EL CAMBIO MAGISTRAL AQUÍ (Lógica corregida) ---
        private void tmrMovimiento_Tick(object sender, EventArgs e)
        {
            // Definimos dos velocidades locales
            int vNormal = 5;
            int vDiag = 3; // Más lento para diagonales (evita el efecto turbo)

            bool seMueve = false;

            // --- 1. PRIORIDAD: DIAGONALES ---
            // Revisamos primero si hay DOS teclas presionadas

            // ARRIBA + DERECHA
            if (goArriba && goDerecha)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Top -= vDiag;
                    pbPersonaje.Left += vDiag;
                    Animar(animArribaDerecha); // ¡Usa tu lista diagonal!
                    seMueve = true;
                }
            }
            // ARRIBA + IZQUIERDA
            else if (goArriba && goIzquierda)
            {
                if (pbPersonaje.Top > 0 && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top -= vDiag;
                    pbPersonaje.Left -= vDiag;
                    Animar(animArribaIzquierda);
                    seMueve = true;
                }
            }
            // ABAJO + DERECHA
            else if (goAbajo && goDerecha)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height &&
                    pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Top += vDiag;
                    pbPersonaje.Left += vDiag;
                    Animar(animAbajoDerecha);
                    seMueve = true;
                }
            }
            // ABAJO + IZQUIERDA
            else if (goAbajo && goIzquierda)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height && pbPersonaje.Left > 0)
                {
                    pbPersonaje.Top += vDiag;
                    pbPersonaje.Left -= vDiag;
                    Animar(animAbajoIzquierda);
                    seMueve = true;
                }
            }

            // --- 2. SECUNDARIO: CARDINALES (Una sola tecla) ---

            else if (goArriba)
            {
                if (pbPersonaje.Top > 0)
                {
                    pbPersonaje.Top -= vNormal;
                    Animar(animArriba);
                    seMueve = true;
                }
            }
            else if (goAbajo)
            {
                if (pbPersonaje.Top + pbPersonaje.Height < this.ClientSize.Height)
                {
                    pbPersonaje.Top += vNormal;
                    Animar(animAbajo);
                    seMueve = true;
                }
            }
            else if (goIzquierda)
            {
                if (pbPersonaje.Left > 0)
                {
                    pbPersonaje.Left -= vNormal;
                    Animar(animIzquierda);
                    seMueve = true;
                }
            }
            else if (goDerecha)
            {
                if (pbPersonaje.Left + pbPersonaje.Width < this.ClientSize.Width)
                {
                    pbPersonaje.Left += vNormal;
                    Animar(animDerecha);
                    seMueve = true;
                }
            }

            // --- 3. RESETEO ---
            if (seMueve == false)
            {
                frameActual = 0;
                contadorLentitud = 10; // Truco: esto hace que al arrancar no tenga retraso la primera vez
            }
        }

        // --- FUNCIÓN DE ANIMAR ---
        private void Animar(List<Image> animacion)
        {
            if (animacion.Count > 0)
            {
                contadorLentitud++;

                // Ajusta este > 4 si quieres que vaya más lento o más rápido
                if (contadorLentitud > 3)
                {
                    frameActual++;
                    if (frameActual >= animacion.Count) frameActual = 0;
                    pbPersonaje.Image = animacion[frameActual];
                    contadorLentitud = 0;
                }
            }
        }
    }
}