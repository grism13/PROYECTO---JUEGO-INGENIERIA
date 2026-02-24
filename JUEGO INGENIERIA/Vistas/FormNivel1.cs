using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Vistas;
using System.IO;
using System.Drawing.Text;
using System.Text.Json; // Para usar el archivo .json de los jugadores


using JUEGO_INGENIERIA.Modelos;
using WMPLib;
using System.Media; // <-- NUEVO: Librería para los efectos de sonido rápidos (.wav)

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel1 : Form
    {
        Jugador jugadorActual;

        // --- CONFIGURACIÓN ---
        int velocidadCaida = 25;
        int tiempoLimite = 30;

        // --- IMÁGENES Y AUDIO ---
        Image lobueno;
        Image lomalo;

        WindowsMediaPlayer reproductorMusica = new WindowsMediaPlayer();
        SoundPlayer sfxBueno = new SoundPlayer(); // <-- REPRODUCTOR EFECTO BUENO
        SoundPlayer sfxMalo = new SoundPlayer();  // <-- REPRODUCTOR EFECTO MALO

        // --- VARIABLES INTERNAS ---
        int xIzquierda, xCentro, xDerecha;
        int carrilActual = 1;

        List<ObjetoJuego> objetosLogicos = new List<ObjetoJuego>();
        Random rnd = new Random();
        int puntos = 0;
        int vidas = 3;
        int tiempoRestante;

        // --- VARIABLES DIÁLOGO ---
        int indiceFrase = 0;
        int indiceLetra = 0;
        string[] discursoOswald = {
            "Oswald: Error 404: Talento no encontrado. Soy Oswald v1.0.",
            "Bienvenido a tu primer 'Hola Mundo' del dolor.",
            "He llenado la sala de excepciones y bugs. Usa tus flechas para esquivarlos.",
            "Si tocas uno... CRASH. El sistema se cae. ¿Listo para compilar o vas a dar error?"
        };

        public FormNivel1(Jugador jugadorRecibido)
        {
            InitializeComponent();
            this.jugadorActual = jugadorRecibido;
        }

        private void FormNivel1_Load(object sender, EventArgs e)
        {
            // 1. CARGA DE FUENTES
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
            PrivateFontCollection pfc = new PrivateFontCollection();
            try
            {
                pfc.AddFontFile(rutaFuente);
                Font fuentePixel = new Font(pfc.Families[0], 11f);
                lblOswaldText.Font = fuentePixel;
                lblTiempo.Font = fuentePixel;
                lblPuntos.Font = fuentePixel;
            }
            catch { }

            if (jugadorActual.Billetera < 100)
            {
                MessageBox.Show("No tienes los $100 necesarios.", "Sin Fondos");
                this.Close();
                return;
            }

            tiempoRestante = tiempoLimite;
            lblTiempo.Text = "TIEMPO: " + tiempoRestante;
            lblPuntos.Text = "PUNTOS: " + puntos;
            ActualizarVidasVisuales();

            lobueno = Properties.Resources.lobueno;
            lomalo = Properties.Resources.lomalo;

            // --- CARGAR RUTAS DE LOS EFECTOS DE SONIDO (NUEVO) ---
            try
            {
                sfxBueno.SoundLocation = Path.Combine(Application.StartupPath, "Resources", "musica nivel 1", "sonidoCuboBueno.wav");
                sfxMalo.SoundLocation = Path.Combine(Application.StartupPath, "Resources", "musica nivel 1", "sonidoCuboMalo.wav");
            }
            catch { }

            tmrGameLoop.Interval = 20;
            tmrGenerador.Interval = 700;
            tmrReloj.Interval = 1000;
            timerEscritura.Interval = 50;

            int anchoPista = pnlEscenario.Width;
            int anchoJugador = pbxJugador.Width;
            xIzquierda = (anchoPista / 6) - (anchoJugador / 2);
            xCentro = (anchoPista / 2) - (anchoJugador / 2);
            xDerecha = (anchoPista * 5 / 6) - (anchoJugador / 2);
            carrilActual = 1;
            ActualizarPosicion();

            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            pnlIntro.Visible = true;
            pnlIntro.BringToFront();
            lblOswaldText.Text = "";
            timerEscritura.Start();
        }

        private void pnlEscenario_Paint(object sender, PaintEventArgs e)
        {
            foreach (ObjetoJuego obj in objetosLogicos)
            {
                if (obj.Imagen != null)
                {
                    e.Graphics.DrawImage(obj.Imagen, obj.X, obj.Y, 50, 50);
                }
            }
        }

        private void tmrGenerador_Tick(object sender, EventArgs e)
        {
            ObjetoJuego nuevo = new ObjetoJuego();
            int r = rnd.Next(0, 3);
            int posX = (r == 0) ? xIzquierda : (r == 1) ? xCentro : xDerecha;

            nuevo.X = posX;
            nuevo.Y = -70;

            if (rnd.Next(0, 100) < 60)
            {
                nuevo.Imagen = lobueno;
                nuevo.Tag = "bueno";
            }
            else
            {
                nuevo.Imagen = lomalo;
                nuevo.Tag = "malo";
            }

            objetosLogicos.Add(nuevo);
            pnlEscenario.Invalidate();
        }

        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            for (int i = objetosLogicos.Count - 1; i >= 0; i--)
            {
                ObjetoJuego obj = objetosLogicos[i];
                obj.Y += velocidadCaida;

                if (obj.Area.IntersectsWith(pbxJugador.Bounds))
                {
                    if (obj.Tag == "bueno")
                    {
                        // --- ¡AQUÍ SUENA EL EFECTO BUENO! ---
                        try { sfxBueno.Play(); } catch { }

                        if (puntos < 20)
                        {
                            puntos++;
                            lblPuntos.Text = "PUNTOS: " + puntos;
                        }
                    }
                    else if (obj.Tag == "malo")
                    {
                        // --- ¡AQUÍ SUENA EL EFECTO MALO! ---
                        try { sfxMalo.Play(); } catch { }

                        vidas--;
                        ActualizarVidasVisuales();
                        if (vidas <= 0) { DetenerJuego(); PerderNivel("Sin vidas"); return; }
                    }

                    objetosLogicos.RemoveAt(i);
                    continue;
                }

                if (obj.Y > pnlEscenario.Height)
                {
                    objetosLogicos.RemoveAt(i);
                }
            }
            pnlEscenario.Invalidate();
        }

        private void ActualizarPosicion()
        {
            int nuevaX = 0;
            if (carrilActual == 0) nuevaX = xIzquierda;
            else if (carrilActual == 1) nuevaX = xCentro;
            else if (carrilActual == 2) nuevaX = xDerecha;
            pbxJugador.Location = new Point(nuevaX, pbxJugador.Location.Y);
            pnlEscenario.Invalidate();
        }

        private void ActualizarVidasVisuales()
        {
            if (vidas == 3) { pbVida1.Visible = false; pbVida2.Visible = false; pbVida3.Visible = true; }
            else if (vidas == 2) { pbVida1.Visible = false; pbVida2.Visible = true; pbVida3.Visible = false; }
            else if (vidas == 1) { pbVida1.Visible = true; pbVida2.Visible = false; pbVida3.Visible = false; }
            else { pbVida1.Visible = false; pbVida2.Visible = false; pbVida3.Visible = false; }
        }

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

        private void lblOswaldText_Click(object sender, EventArgs e)
        {
            if (timerEscritura.Enabled)
            {
                timerEscritura.Stop();
                lblOswaldText.Text = discursoOswald[indiceFrase];
            }
            else
            {
                indiceFrase++;
                if (indiceFrase < discursoOswald.Length)
                {
                    lblOswaldText.Text = "";
                    indiceLetra = 0;
                    timerEscritura.Start();
                }
                else
                {
                    EmpezarJuegoReal();
                }
            }
        }

        private void EmpezarJuegoReal()
        {
            timerEscritura.Stop();
            pnlIntro.Visible = false;
            lblOswaldText.Visible = false;
            pictureBox1.Visible = false;
            pnlIntro.SendToBack();
            tmrGameLoop.Start();
            tmrGenerador.Start();
            tmrReloj.Start();
            this.Focus();

            ReproducirMusicaFondo();
        }

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

        private void DetenerJuego()
        {
            tmrGameLoop.Stop();
            tmrGenerador.Stop();
            tmrReloj.Stop();
            reproductorMusica.controls.stop();
        }

        private void GanarNivel()
        {
            if (jugadorActual.Nivel == 1)
            {
                jugadorActual.Nivel = 2;
                MessageBox.Show($"¡FELICIDADES!\nNota: {puntos}/20", "Nivel Completado");
            }
            else
            {
                MessageBox.Show($"¡Bien hecho!", "Repaso Completado");
            }

            ActualizarDatos(); // Guardamos el progreso en el .json
            this.Close();

        }

        private void PerderNivel(string motivo)
        {
            jugadorActual.Billetera -= 100;
            MessageBox.Show($"REPROBADO ({motivo}).\nMulta: $100", "Game Over");

            ActualizarDatos(); // Guardamos el progreso en el .json
            this.Close();
        }

        private void ReproducirMusicaFondo()
        {
            try
            {
                string rutaAudio = Path.Combine(Application.StartupPath, "Resources", "musica nivel 1", "musicaOswald.mp3");
                reproductorMusica.URL = rutaAudio;
                reproductorMusica.settings.setMode("loop", true);
                reproductorMusica.controls.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de audio: " + ex.Message);
            }
        }

        // Esto es para actualizar los datos en el .json

        private void ActualizarDatos()
        {
            string rutaArchivo = "jugadores.json";

            // Aqui se verifica que el archivo exista para que no haya errores
            if (!File.Exists(rutaArchivo)) return;

            // Se lee la linea completa de jugadores desde el .json
            string TextoJson = File.ReadAllText(rutaArchivo);
            List<Jugador> listaDeJugadores = JsonSerializer.Deserialize<List<Jugador>>(TextoJson) ?? new List<Jugador>();

            for (int i = 0; i < listaDeJugadores.Count; i++)
            {
                if (listaDeJugadores[i].IdJugador == jugadorActual.IdJugador)
                {
                    listaDeJugadores[i].Nivel = jugadorActual.Nivel;
                    listaDeJugadores[i].Billetera = jugadorActual.Billetera;
                    break;
                }
            }

            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string nuevoJson = JsonSerializer.Serialize(listaDeJugadores, opciones);
            File.WriteAllText(rutaArchivo, nuevoJson);
        }
    }
}