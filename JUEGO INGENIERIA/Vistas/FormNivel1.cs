using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Vistas;
using System.Drawing.Text;
using System.IO;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel1 : Form
    {
        // Variable para guardar al estudiante
        Jugador jugadorActual;

        // --- CONFIGURACIÓN ---
        int velocidadCaida = 18;
        int tiempoLimite = 30;

        // --- VARIABLES INTERNAS ---
        int xIzquierda, xCentro, xDerecha;
        int carrilActual = 1;
        List<PictureBox> objetosCayendo = new List<PictureBox>();
        Random rnd = new Random();

        int puntos = 0;
        int vidas = 3;
        int tiempoRestante;

        // --- NUEVAS VARIABLES PARA EL DIÁLOGO DE OSWALD ---
        int indiceFrase = 0;
        int indiceLetra = 0;
        string[] discursoOswald = {
            "Oswald: Error 404: Talento no encontrado. Soy Oswald v1.0.",
            "Bienvenido a tu primer 'Hola Mundo' del dolor.",
            "He llenado la sala de excepciones y bugs. Usa tus flechas para esquivarlos.",
            "Si tocas uno... CRASH. El sistema se cae. ¿Listo para compilar o vas a dar error?"
        };

        // CONSTRUCTOR: Recibimos al jugador
        public FormNivel1(Jugador jugadorRecibido)
        {
            InitializeComponent();
            this.jugadorActual = jugadorRecibido;
        }

        private void FormNivel1_Load(object sender, EventArgs e)
        {
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");

            
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(rutaFuente);            
            Font fuentePixel = new Font(pfc.Families[0], 10f);
            lblOswaldText.Font = fuentePixel;
            lblTiempo.Font = fuentePixel;
            lblVidas.Font = fuentePixel;
            lblPuntos.Font = fuentePixel;

            // 1. VALIDACIÓN DE DINERO
            if (jugadorActual.Billetera < 100)
            {
                MessageBox.Show("No tienes los $100 necesarios para esta clase.", "Sin Fondos");
                this.Close();
                return;
            }

            // 2. CONFIGURACIÓN VISUAL INICIAL
            tiempoRestante = tiempoLimite;
            lblTiempo.Text = "TIEMPO: " + tiempoRestante;
            lblVidas.Text = "VIDAS: " + vidas;
            lblPuntos.Text = "PUNTOS: " + puntos;

            // 3. MOTORES (Configurados pero NO iniciados)
            tmrGameLoop.Interval = 20;
            tmrGenerador.Interval = 700;
            tmrReloj.Interval = 1000;
            timerEscritura.Interval = 50; // Velocidad de las letras

            // 4. CARRILES
            int anchoPista = pnlEscenario.Width;
            int anchoJugador = pbxJugador.Width;
            xIzquierda = (anchoPista / 6) - (anchoJugador / 2);
            xCentro = (anchoPista / 2) - (anchoJugador / 2);
            xDerecha = (anchoPista * 5 / 6) - (anchoJugador / 2);

            carrilActual = 1;
            ActualizarPosicion();

            // 5. ACTIVAR INTRO DE OSWALD
            pnlIntro.Visible = true;
            pnlIntro.BringToFront();
            lblOswaldText.Text = "";
            timerEscritura.Start(); // Empieza a escribir la primera frase
        }

        // --- SISTEMA DE DIÁLOGO (MÁQUINA DE ESCRIBIR) ---
        private void timerEscritura_Tick(object sender, EventArgs e)
        {
            string fraseCompleta = discursoOswald[indiceFrase];
            if (indiceLetra < fraseCompleta.Length)
            {
                lblOswaldText.Text += fraseCompleta[indiceLetra];
                indiceLetra++;
            }
            else
            {
                timerEscritura.Stop();
            }
        }

        // Evento Click para avanzar el texto (Conectarlo a lblOswaldText y pnlIntro)
        private void lblOswaldText_Click(object sender, EventArgs e)
        {
            if (timerEscritura.Enabled)
            {
                // Si aún escribe, mostrar frase completa de golpe
                timerEscritura.Stop();
                lblOswaldText.Text = discursoOswald[indiceFrase];
            }
            else
            {
                // Si terminó, pasar a la siguiente frase
                indiceFrase++;
                if (indiceFrase < discursoOswald.Length)
                {
                    lblOswaldText.Text = "";
                    indiceLetra = 0;
                    timerEscritura.Start();
                }
                else
                {
                    // FIN DEL DISCURSO: Iniciar el juego real
                    EmpezarJuegoReal();
                }
            }
        }

        private void EmpezarJuegoReal()
        {
            // 1. Apagamos el timer de escritura por seguridad
            timerEscritura.Stop();

            // 2. Ocultamos TODO el panel de intro
            pnlIntro.Visible = false;

            // 3. (EXTRA DE SEGURIDAD) Ocultamos el texto y la imagen manualmente 
            // por si acaso no estuvieran dentro del panel en el diseñador.
            lblOswaldText.Visible = false;
            pictureBox1.Visible = false;

            // 4. Mandamos el panel al fondo para que no estorbe los clics del juego
            pnlIntro.SendToBack();

            // 5. Iniciamos los motores del juego
            tmrGameLoop.Start();
            tmrGenerador.Start();
            tmrReloj.Start();

            // 6. Ponemos el foco en el formulario para que las teclas funcionen al instante
            this.Focus();
        }

        // --- MOVIMIENTO ---
        private void FormNivel1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && carrilActual > 0)
            {
                carrilActual--;
                ActualizarPosicion();
            }
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && carrilActual < 2)
            {
                carrilActual++;
                ActualizarPosicion();
            }
        }

        private void ActualizarPosicion()
        {
            int nuevaX = 0;
            if (carrilActual == 0) nuevaX = xIzquierda;
            else if (carrilActual == 1) nuevaX = xCentro;
            else if (carrilActual == 2) nuevaX = xDerecha;
            pbxJugador.Location = new Point(nuevaX, pbxJugador.Location.Y);
        }

        // --- RELOJ ---
        private void tmrReloj_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            lblTiempo.Text = "TIEMPO: " + tiempoRestante;
            if (tiempoRestante <= 0)
            {
                DetenerJuego();
                if (puntos >= 10) GanarNivel();
                else PerderNivel("Tiempo agotado");
            }
        }

        // --- GENERADOR DE CUBOS ---
        private void tmrGenerador_Tick(object sender, EventArgs e)
        {
            PictureBox nuevoObjeto = new PictureBox();
            nuevoObjeto.Size = new Size(50, 50);
            nuevoObjeto.SizeMode = PictureBoxSizeMode.Zoom;

            int r = rnd.Next(0, 3);
            int posX = (r == 0) ? xIzquierda : (r == 1) ? xCentro : xDerecha;
            nuevoObjeto.Location = new Point(posX, -70);

            if (rnd.Next(0, 100) < 60) // 60% BUENO
            {
                nuevoObjeto.BackColor = Color.Lime;
                nuevoObjeto.Tag = "bueno";
            }
            else // 40% MALO
            {
                nuevoObjeto.BackColor = Color.Red;
                nuevoObjeto.Tag = "malo";
            }

            pnlEscenario.Controls.Add(nuevoObjeto);
            nuevoObjeto.BringToFront();
            objetosCayendo.Add(nuevoObjeto);
        }

        // --- FÍSICA Y EFECTO ABSORBER ---
        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            for (int i = objetosCayendo.Count - 1; i >= 0; i--)
            {
                PictureBox obj = objetosCayendo[i];
                obj.Top += velocidadCaida;

                if (obj.Bounds.IntersectsWith(pbxJugador.Bounds))
                {
                    string tipo = (string)obj.Tag;

                    if (tipo == "bueno")
                    {
                        if (puntos < 20) puntos++; // LÍMITE DE 20 PUNTOS
                    }
                    else if (tipo == "malo")
                    {
                        vidas--;
                        if (vidas <= 0) { DetenerJuego(); PerderNivel("Sin vidas"); return; }
                    }

                    lblPuntos.Text = "PUNTOS: " + puntos;
                    lblVidas.Text = "VIDAS: " + vidas;

                    EliminarObjeto(obj, i);
                    continue;
                }

                if (obj.Top > pnlEscenario.Height) EliminarObjeto(obj, i);
            }
        }

        private void EliminarObjeto(PictureBox obj, int index)
        {
            pnlEscenario.Controls.Remove(obj);
            objetosCayendo.RemoveAt(index);
            obj.Dispose();
        }

        private void DetenerJuego()
        {
            tmrGameLoop.Stop();
            tmrGenerador.Stop();
            tmrReloj.Stop();
        }

        private void GanarNivel()
        {
            if (jugadorActual.Nivel == 1)
            {
                jugadorActual.Nivel = 2;
                MessageBox.Show($"¡FELICIDADES!\nHas aprobado la materia.\n\nNota: {puntos}/20\n¡Subes al Nivel 2!",
                                "Nivel Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"¡Bien hecho!\nHas completado el repaso correctamente.\n(No ganas experiencia extra porque ya eres Nivel {jugadorActual.Nivel}).",
                                "Repaso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void PerderNivel(string motivo)
        {
            jugadorActual.Billetera -= 100;
            MessageBox.Show($"REPROBADO ({motivo}).\n\nMulta: $100\nSaldo restante: ${jugadorActual.Billetera}",
                            "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        
    }
}