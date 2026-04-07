using JUEGO_INGENIERIA.Modelos;
using JUEGO_INGENIERIA.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text; // IMPORTANTE PARA LA FUENTE
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using WMPLib;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel3 : Form
    {
        // --- IMAGEN DE FONDO Y VARIABLES SEAMLESS (SCROLL INFINITO) ---
        Image fondoFase1;
        Image fondoFase2;
        Image fondoFase3;
        Image fondoActual;
        int fondoX = 0;
        int velocidadFondo = 3; // Ajustado al ritmo original (compensando FPS)

        // --- VARIABLES DEL JUGADOR ---
        Jugador jugadorActual;
        FormMovimiento movimiento;
        PictureBox pbJugador;
        int tamañoJugador = 150;
        int vidasJugador = 3;
        int tiempoInmunidad = 0;

        List<ObjetoJuego> balasJugador = new List<ObjetoJuego>();
        int velocidadBala = 18; // Ajustado al ritmo original
        int cooldownDisparo = 0;
        int danoJugador = 10;
        bool disparando = false;
        bool modoConcentrado = false;
        int targetTamañoJugador = 150;

        // --- IMÁGENES DE ESTADO DE VIDA DEL JUGADOR ---
        Image imgVidaFull;
        Image imgVidaMedia;
        Image imgVidaBaja;

        // --- VARIABLES DEL JEFE (Profesor Marcel) ---
        int bossBaseX;
        int bossX;
        int bossY = 50;
        int tamañoBoss = 180;
        int vidaBoss = 1500;
        int velocidadBoss = 5; // Ajustado al ritmo original
        bool bossSube = false;
        bool bossAvanza = true;
        int flashBoss = 0;
        int cooldownAtaqueBoss = 80; // Ajustado al ritmo original
        List<ObjetoJuego> balasBoss = new List<ObjetoJuego>();

        Random rnd = new Random();

        // --- ANIMACIONES DEL JEFE MARCEL ---
        private Image[] framesFase1;
        private Image[] framesFase2;
        private Image[] framesFase3;
        private Image imagenActualBoss;
        private int frameBossActual = 0;
        private int contadorAnimacionBoss = 0;
        private int velocidadAnimacionBoss = 10; // Ajustado al ritmo original

        // --- SISTEMA DE DIÁLOGOS Y NARRATIVA ---
        private int indiceFrase = 0;
        private int indiceLetra = 0;
        // Textos temporales para el diseñador
        private string[] discursoMarcel = {
            "Marcel: Hmm... Asi que tu eres quien cree poder con Matematicas 2.",
            "Marcel: Espero que sepas integrar, porque te voy a derivar a cero.",
            "Marcel: ¿Crees que aprobar mi materia es asi de facil? ¡Iluso!",
            "Marcel: ¡Preparate para la aniquilacion numerica!"
        };
        // Las variables pnlIntro, lblMarcelText, pbMarcel, pbFondoNarrativa, btnSkipDialogo y timerEscritura
        // deben ser creadas arrastrándolas desde el "Cuadro de herramientas" al diseñador visual.
        private bool enDialogo = true;
        private Image retratoMarcelDinamico;

        // --- VARIABLES DE FUENTES E IMÁGENES DE NARRATIVA ---
        private PrivateFontCollection pfc = new PrivateFontCollection();
        private Font fuentePixel;
        private Image[] imagenesDialogo;

        // --- AUDIO (MÚSICA DEL NIVEL MULTI-PISTA PARA EVITAR CORTES) ---
        WindowsMediaPlayer reproductorMusicaF1 = new WindowsMediaPlayer();
        WindowsMediaPlayer reproductorMusicaF2 = new WindowsMediaPlayer();
        WindowsMediaPlayer reproductorMusicaF3 = new WindowsMediaPlayer();
        int faseActualMusica = 1;
        int transicionVolumen = 0; // Temporizador para el Crossfade

        // --- EFECTOS DE SONIDO (DISPARO CUPHEAD) ---
        WindowsMediaPlayer sfxDisparoStart = new WindowsMediaPlayer();
        WindowsMediaPlayer sfxDisparoLoop = new WindowsMediaPlayer();
        WindowsMediaPlayer sfxDisparoEnd = new WindowsMediaPlayer();
        bool estabaDisparando = false;

        // --- ASYNC KEYBOARD INPUT ---
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        // --- CACHÉ GDI+ PARA OPTIMIZACIÓN ---
        private Image imgBalaJugador;
        private Image imgBalaJefe;
        private Font fuenteVidaBoss;
        private SolidBrush pincelDestello;

        public FormNivel3(Jugador jugadorRecibido)
        {
            InitializeComponent();
            this.jugadorActual = jugadorRecibido;
        }

        private Bitmap OptimizarImagen(Image img, int width, int height)
        {
            if (img == null) return null;
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        private void FormNivel2_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;

            if (jugadorActual != null && jugadorActual.Billetera < 100)
            {
                MessageBox.Show("No tienes los $100 necesarios.", "Sin Fondos");
                this.Close();
                return;
            }

            fondoFase1 = OptimizarImagen(Properties.Resources.fondoF1_marcel, 1280, 720);
            fondoFase2 = OptimizarImagen(Properties.Resources.fondoF2_marcel, 1280, 720);
            fondoFase3 = OptimizarImagen(Properties.Resources.fondoF3_marcel_a, 1280, 720);

            fondoActual = fondoFase1;

            framesFase1 = new Image[] {
                OptimizarImagen(Properties.Resources.MarcelF1_1, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF1_2, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF1_3, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF1_4, tamañoBoss, tamañoBoss)
            };

            framesFase2 = new Image[] {
                OptimizarImagen(Properties.Resources.MarcelF2_1, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF2_2, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF2_3, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF2_4, tamañoBoss, tamañoBoss)
            };

            framesFase3 = new Image[] {
                OptimizarImagen(Properties.Resources.MarcelF3_1, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF3_2, tamañoBoss, tamañoBoss),
                OptimizarImagen(Properties.Resources.MarcelF3_3, tamañoBoss, tamañoBoss),
            };

            imagenActualBoss = framesFase1[0];

            imgVidaFull = OptimizarImagen(Properties.Resources.vida_3, 120, 40);
            imgVidaMedia = OptimizarImagen(Properties.Resources.vida_2, 120, 40);
            imgVidaBaja = OptimizarImagen(Properties.Resources.vida_1, 120, 40);

            imgBalaJugador = OptimizarImagen(Properties.Resources.balas_personaje, 50, 25);
            imgBalaJefe = OptimizarImagen(Properties.Resources.bala_marcel, 50, 50);
            fuenteVidaBoss = new Font("Arial", 16, FontStyle.Bold);
            pincelDestello = new SolidBrush(Color.FromArgb(120, Color.White));

            // --- APLICAR FUENTE PERSONALIZADA ---
            try
            {
                string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
                if (File.Exists(rutaFuente))
                {
                    pfc.AddFontFile(rutaFuente);
                    fuentePixel = new Font(pfc.Families[0], 10f); // Tamaño 10
                }
                else
                {
                    fuentePixel = new Font("Courier New", 12f);
                }
            }
            catch
            {
                fuentePixel = new Font("Courier New", 12f);
            }

            try { lblMarcelText.Font = fuentePixel; } catch { }
            try { btnSkipDialogo.Font = fuentePixel; } catch { }

            // --- CARGAR IMÁGENES ROTATIVAS DE LA NARRATIVA ---
            try
            {
                imagenesDialogo = new Image[] {
                    (Image)Properties.Resources.ResourceManager.GetObject("marcel-tranquilo") ?? framesFase1[0],
                    (Image)Properties.Resources.ResourceManager.GetObject("marcel-fase1") ?? framesFase1[0],
                    (Image)Properties.Resources.ResourceManager.GetObject("marcel-fase2malvado") ?? framesFase2[0],
                    (Image)Properties.Resources.ResourceManager.GetObject("marcel-fase2malvado") ?? framesFase2[0]
                };
            }
            catch { }


            // --- UI DEL DIÁLOGO (DISEÑADOR VISUAL) ---
            try
            {
                if (imagenesDialogo != null && imagenesDialogo.Length > 0)
                    pbMarcel.Image = imagenesDialogo[0];
                else
                    pbMarcel.Image = framesFase1[0];
            }
            catch { }

            // Limpiamos el texto para arrancar el efecto máquina de escribir limpio
            try { lblMarcelText.Text = ""; } catch { }

            // Para asegurar fondo transparente real si usas el nivel 3:
            try { pnlEscenario.Controls.Add(pnlIntro); } catch { }

            try { NavegacionConsola.Configurar(this, btnSkipDialogo); } catch { }

            try { timerEscritura.Interval = 50; } catch { }
            try { timerEscritura.Tick += TimerEscritura_Tick; } catch { }

            try { lblMarcelText.MouseClick += Control_ClickDialogo; } catch { }
            try { pbMarcel.MouseClick += Control_ClickDialogo; } catch { }
            try { pnlIntro.MouseClick += Control_ClickDialogo; } catch { }
            this.MouseClick += Control_ClickDialogo;
            this.KeyDown += FormNivel3_KeyDown;
            this.KeyPreview = true;

            try { btnSkipDialogo.Click -= BtnSkipDialogo_Click; } catch { } // Prevención duplicado
            try { btnSkipDialogo.Click += BtnSkipDialogo_Click; } catch { }

            try { pnlIntro.BringToFront(); } catch { }
            enDialogo = true;
            try { timerEscritura.Start(); } catch { }
            try { btnSkipDialogo.Select(); btnSkipDialogo.Focus(); } catch { } // Obliga a enfocar para el mando

            // --- INICIAR MÚSICA Y SFX ---
            try
            {
                // Música de fondo (Inicializamos los 3 reproductores invisibles al tiempo)
                string rutaAudio = Path.Combine(Application.StartupPath, "Resources", "OST nivel 3.wav");

                reproductorMusicaF1.URL = rutaAudio;
                reproductorMusicaF1.settings.setMode("loop", true);
                reproductorMusicaF1.settings.rate = 1.0;
                reproductorMusicaF1.settings.volume = 30; // Solo suena esta al inicio
                reproductorMusicaF1.controls.stop(); // Solo lo preparamos, comienza después del diálogo

                reproductorMusicaF2.URL = rutaAudio;
                reproductorMusicaF2.settings.setMode("loop", true);
                reproductorMusicaF2.settings.rate = 1.08;
                reproductorMusicaF2.settings.volume = 0; // Se carga en silencio total
                reproductorMusicaF2.controls.stop();

                reproductorMusicaF3.URL = rutaAudio;
                reproductorMusicaF3.settings.setMode("loop", true);
                reproductorMusicaF3.settings.rate = 1.15;
                reproductorMusicaF3.settings.volume = 0; // Se carga en silencio total
                reproductorMusicaF3.controls.stop();

                // SFX DISPARO CUPHEAD (Volumen al 15 para acompañar)
                string rutaStart = Path.Combine(Application.StartupPath, "Resources", "player_plane_weapon_fire_start_01.wav");
                string rutaLoop = Path.Combine(Application.StartupPath, "Resources", "player_plane_weapon_fire_loop_01.wav");
                string rutaEnd = Path.Combine(Application.StartupPath, "Resources", "player_plane_weapon_fire_loop_01_end_01.wav");

                sfxDisparoStart.URL = rutaStart;
                sfxDisparoStart.settings.volume = 15;
                sfxDisparoStart.controls.stop();

                sfxDisparoLoop.URL = rutaLoop;
                sfxDisparoLoop.settings.setMode("loop", true);
                sfxDisparoLoop.settings.volume = 15;
                sfxDisparoLoop.controls.stop();

                sfxDisparoEnd.URL = rutaEnd;
                sfxDisparoEnd.settings.volume = 15;
                sfxDisparoEnd.controls.stop();
            }
            catch { }

            // FPS A 100 FPS (10 ms por tick para que se vea súper fluido)
            tmrGameLoop.Interval = 10;

            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            pbJugador = new PictureBox();
            pbJugador.Size = new Size(tamañoJugador, tamañoJugador);
            pbJugador.Location = new Point(200, 200);

            movimiento = new FormMovimiento(this, pbJugador, true);
            // No iniciamos nada aún

            bossBaseX = pnlEscenario.Width - tamañoBoss - 50;
            bossX = bossBaseX;

            // Esperamos a EmpezarJuegoReal() para soltar el kraken
        }

        // --- METODOS DE LA NARRATIVA ---

        private void TimerEscritura_Tick(object sender, EventArgs e)
        {
            string fraseCompleta = discursoMarcel[indiceFrase];
            if (indiceLetra < fraseCompleta.Length)
            {
                lblMarcelText.Text += fraseCompleta[indiceLetra];
                indiceLetra++;
            }
            else
            {
                timerEscritura.Stop();
            }
        }

        private void Control_ClickDialogo(object sender, MouseEventArgs e)
        {
            if (enDialogo) SaltarOContinuarDialogo();
        }

        private void FormNivel3_KeyDown(object sender, KeyEventArgs e)
        {
            if (enDialogo) SaltarOContinuarDialogo();
        }

        private void SaltarOContinuarDialogo()
        {
            if (!enDialogo) return;

            if (timerEscritura.Enabled)
            {
                // Acelera si no había terminado
                timerEscritura.Stop();
                lblMarcelText.Text = discursoMarcel[indiceFrase];
            }
            else
            {
                // Pasa a la siguiente frase si ya habíamos completado
                indiceFrase++;
                if (indiceFrase < discursoMarcel.Length)
                {
                    lblMarcelText.Text = "";
                    indiceLetra = 0;

                    // --- ACTUALIZAR IMAGEN DEL JEFE ---
                    try
                    {
                        if (imagenesDialogo != null && indiceFrase < imagenesDialogo.Length && imagenesDialogo[indiceFrase] != null)
                            pbMarcel.Image = imagenesDialogo[indiceFrase];
                    }
                    catch { }

                    timerEscritura.Start();
                }
                else
                {
                    EmpezarJuegoReal();
                }
            }
        }

        private void BtnSkipDialogo_Click(object sender, EventArgs e)
        {
            EmpezarJuegoReal();
        }

        private void EmpezarJuegoReal()
        {
            if (!enDialogo) return;
            enDialogo = false;

            try { pnlIntro.Visible = false; } catch { }
            try { pbMarcel.Visible = false; } catch { } // Se oculta por si quedó fuera del panel
            try { lblMarcelText.Visible = false; } catch { }
            try { btnSkipDialogo.Visible = false; } catch { }

            try { timerEscritura.Stop(); } catch { }

            // Iniciar variables de juego reales
            movimiento.Start();
            tmrGameLoop.Start();

            try
            {
                reproductorMusicaF1.controls.play();
                reproductorMusicaF2.controls.play();
                reproductorMusicaF3.controls.play();
            }
            catch { }

            this.Focus();
        }

        // --- FIN METODOS DE NARRATIVA ---

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_KEYMENU = 0xF100;

            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() & 0xFFF0) == SC_KEYMENU)
            {
                return;
            }

            base.WndProc(ref m);
        }

        private void FormNivel2_KeyDown(object sender, KeyEventArgs e) { }
        private void FormNivel2_KeyUp(object sender, KeyEventArgs e) { }

        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // Verificamos de inmediato si te estás achicando
            bool altPresionado = (GetAsyncKeyState(Keys.Menu) & 0x8000) != 0 || (GetAsyncKeyState(Keys.Alt) & 0x8000) != 0;

            // Reemplazamos la tecla Space por la X (igual que en el Nivel 4)
            // Y de paso, anulamos por completo el disparo si estás chiquito (!altPresionado)
            disparando = ((GetAsyncKeyState(Keys.X) & 0x8000) != 0) && !altPresionado;

            // --- LÓGICA DE TRANSICIÓN DE MÚSICA FLUIDA (DJ CROSSFADE) ---
            if (transicionVolumen > 0)
            {
                transicionVolumen--;
                if (transicionVolumen == 0)
                {
                    if (faseActualMusica == 2)
                    {
                        reproductorMusicaF1.settings.volume = 0; // Apagamos la normal
                        reproductorMusicaF2.settings.volume = 30; // Prendemos la veloz instanteamente
                    }
                    else if (faseActualMusica == 3)
                    {
                        reproductorMusicaF2.settings.volume = 0; // Apagamos la veloz
                        reproductorMusicaF3.settings.volume = 30; // Prendemos la ultra-veloz
                    }
                }
            }


            // --- LÓGICA DE AUDIO ESTILO CUPHEAD ---
            if (disparando && !estabaDisparando)
            {
                sfxDisparoEnd.controls.stop();

                sfxDisparoStart.controls.stop();
                sfxDisparoStart.controls.play();

                sfxDisparoLoop.controls.stop();
                sfxDisparoLoop.controls.play();
            }
            else if (!disparando && estabaDisparando)
            {
                sfxDisparoStart.controls.stop();
                sfxDisparoLoop.controls.stop();

                // Al soltar la tecla (o al achicarse a la fuerza), esto pondrá el sonido de "stop fire" :)
                sfxDisparoEnd.controls.stop();
                sfxDisparoEnd.controls.play();
            }
            estabaDisparando = disparando;

            if (altPresionado && !modoConcentrado)
            {
                modoConcentrado = true;
                targetTamañoJugador = 60;
                danoJugador = 5;
            }
            else if (!altPresionado && modoConcentrado)
            {
                modoConcentrado = false;
                targetTamañoJugador = 150;
                danoJugador = 10;
            }

            fondoX -= velocidadFondo;



            // --- LÓGICA DE AUDIO ESTILO CUPHEAD ---
            if (disparando && !estabaDisparando)
            {
                sfxDisparoEnd.controls.stop();

                sfxDisparoStart.controls.stop();
                sfxDisparoStart.controls.play();

                sfxDisparoLoop.controls.stop();
                sfxDisparoLoop.controls.play();
            }
            else if (!disparando && estabaDisparando)
            {
                sfxDisparoStart.controls.stop();
                sfxDisparoLoop.controls.stop();

                sfxDisparoEnd.controls.stop();
                sfxDisparoEnd.controls.play();
            }
            estabaDisparando = disparando;



            if (altPresionado && !modoConcentrado)
            {
                modoConcentrado = true;
                targetTamañoJugador = 60;
                danoJugador = 5;
            }
            else if (!altPresionado && modoConcentrado)
            {
                modoConcentrado = false;
                targetTamañoJugador = 150;
                danoJugador = 10;
            }

            fondoX -= velocidadFondo;
            if (fondoX <= -pnlEscenario.Width)
            {
                fondoX = 0;
            }

            if (tiempoInmunidad > 0)
            {
                tiempoInmunidad--;
            }

            if (tamañoJugador != targetTamañoJugador)
            {
                int velocidadAnimacion = 10;
                int nuevoTamaño = targetTamañoJugador < tamañoJugador ? tamañoJugador - velocidadAnimacion : tamañoJugador + velocidadAnimacion;

                if (targetTamañoJugador < tamañoJugador && nuevoTamaño < targetTamañoJugador) nuevoTamaño = targetTamañoJugador;
                if (targetTamañoJugador > tamañoJugador && nuevoTamaño > targetTamañoJugador) nuevoTamaño = targetTamañoJugador;

                int diferencia = tamañoJugador - nuevoTamaño;
                tamañoJugador = nuevoTamaño;

                pbJugador.Size = new Size(tamañoJugador, tamañoJugador);
                pbJugador.Left += diferencia / 2;
                pbJugador.Top += diferencia / 2;
            }

            if (pbJugador.Left < 0) pbJugador.Left = 0;
            if (pbJugador.Top < 0) pbJugador.Top = 0;
            if (pbJugador.Right > pnlEscenario.Width) pbJugador.Left = pnlEscenario.Width - pbJugador.Width;
            if (pbJugador.Bottom > pnlEscenario.Height) pbJugador.Top = pnlEscenario.Height - pbJugador.Height;

            if (cooldownDisparo > 0) cooldownDisparo--;

            if (disparando == true && cooldownDisparo <= 0 && !modoConcentrado)
            {
                ObjetoJuego nuevaBala = new ObjetoJuego();
                nuevaBala.X = pbJugador.Left + tamañoJugador;
                nuevaBala.Y = pbJugador.Top + (tamañoJugador / 2) - 5;
                nuevaBala.Tag = "bala_jugador";
                balasJugador.Add(nuevaBala);

                cooldownDisparo = 10;
            }

            for (int i = balasJugador.Count - 1; i >= 0; i--)
            {
                balasJugador[i].X += velocidadBala;
                if (balasJugador[i].X > pnlEscenario.Width) balasJugador.RemoveAt(i);
            }

            if (vidaBoss > 0)
            {
                contadorAnimacionBoss++;
                if (contadorAnimacionBoss >= velocidadAnimacionBoss)
                {
                    frameBossActual++;
                    contadorAnimacionBoss = 0;

                    if (vidaBoss > 1000)
                    {
                        if (frameBossActual >= framesFase1.Length) frameBossActual = 0;
                        imagenActualBoss = framesFase1[frameBossActual];
                    }
                    else if (vidaBoss <= 1000 && vidaBoss > 500)
                    {
                        if (frameBossActual >= framesFase2.Length) frameBossActual = 0;
                        imagenActualBoss = framesFase2[frameBossActual];
                    }
                    else
                    {
                        if (frameBossActual >= framesFase3.Length) frameBossActual = 0;
                        imagenActualBoss = framesFase3[frameBossActual];
                    }
                }

                if (bossSube)
                {
                    bossY -= velocidadBoss;
                    if (bossY <= 0) bossSube = false;
                }
                else
                {
                    bossY += velocidadBoss;
                    if (bossY >= pnlEscenario.Height - tamañoBoss) bossSube = true;
                }

                if (vidaBoss > 500)
                {
                    if (bossAvanza)
                    {
                        bossX -= (velocidadBoss / 2);
                        if (bossX <= bossBaseX - 350) bossAvanza = false;
                    }
                    else
                    {
                        bossX += (velocidadBoss / 2);
                        if (bossX >= bossBaseX) bossAvanza = true;
                    }
                }
                else
                {
                    if (bossX < bossBaseX) bossX += (velocidadBoss / 2);
                }

                if (flashBoss > 0) flashBoss--;

                Rectangle areaBoss = new Rectangle(bossX, bossY, tamañoBoss, tamañoBoss);

                for (int i = balasJugador.Count - 1; i >= 0; i--)
                {
                    Rectangle areaBala = new Rectangle(balasJugador[i].X, balasJugador[i].Y, 20, 10);
                    if (areaBala.IntersectsWith(areaBoss))
                    {
                        vidaBoss -= danoJugador;
                        balasJugador.RemoveAt(i);
                        flashBoss = 3;

                        if (vidaBoss <= 0)
                        {
                            vidaBoss = 0;
                            // Ganaste
                            reproductorMusicaF1.controls.stop();
                            reproductorMusicaF2.controls.stop();
                            reproductorMusicaF3.controls.stop();
                            sfxDisparoStart.controls.stop();
                            sfxDisparoLoop.controls.stop();
                            sfxDisparoEnd.controls.stop();

                            tmrGameLoop.Stop();
                            pnlEscenario.Invalidate();
                            FormVictoria.Mostrar("¡Has derrotado al temible Profesor Marcel!\n¡Aprobaste Matemáticas 2 con éxito!", "¡NIVEL COMPLETADO!", () => this.Close());
                            if (jugadorActual != null && jugadorActual.Nivel < 3)
                            {
                                jugadorActual.Nivel = 3;
                            }
                            ActualizarDatos();
                            return;
                        }
                    }
                }

                if (cooldownAtaqueBoss > 0)
                {
                    cooldownAtaqueBoss--;
                }
                else
                {
                    int probabilidad = rnd.Next(0, 100);

                    if (vidaBoss > 1000)
                    {
                        fondoActual = fondoFase1;

                        ObjetoJuego balaMala = new ObjetoJuego();
                        balaMala.X = bossX;
                        balaMala.Y = bossY + (tamañoBoss / 2);
                        if (probabilidad < 85) balaMala.Tag = "bala_boss_recta";
                        else balaMala.Tag = "bala_boss_perseguidora";
                        balasBoss.Add(balaMala);

                        cooldownAtaqueBoss = 75;
                    }
                    else if (vidaBoss <= 1000 && vidaBoss > 500)
                    {
                        // --- CAMBIO A FASE 2 ---
                        if (faseActualMusica == 1)
                        {
                            reproductorMusicaF2.controls.currentPosition = reproductorMusicaF1.controls.currentPosition;
                            transicionVolumen = 15;
                            faseActualMusica = 2;
                        }

                        fondoActual = fondoFase2;
                        velocidadBoss = 7;

                        if (probabilidad < 85)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                ObjetoJuego balaMala = new ObjetoJuego();
                                balaMala.X = bossX;
                                balaMala.Y = bossY + (tamañoBoss / 2);
                                if (j == -1) balaMala.Tag = "bala_boss_arriba";
                                else if (j == 1) balaMala.Tag = "bala_boss_abajo";
                                else balaMala.Tag = "bala_boss_fase2_recta";

                                balasBoss.Add(balaMala);
                            }
                        }
                        else
                        {
                            ObjetoJuego balaMala = new ObjetoJuego();
                            balaMala.X = bossX;
                            balaMala.Y = bossY + (tamañoBoss / 2);
                            balaMala.Tag = "bala_boss_rebotona_sube";
                            balasBoss.Add(balaMala);
                        }

                        cooldownAtaqueBoss = 55;
                    }
                    else
                    {
                        // --- CAMBIO A FASE 3 ---
                        if (faseActualMusica == 2)
                        {
                            reproductorMusicaF3.controls.currentPosition = reproductorMusicaF2.controls.currentPosition;
                            transicionVolumen = 15;
                            faseActualMusica = 3;
                        }

                        fondoActual = fondoFase3;
                        velocidadBoss = 9;

                        ObjetoJuego balaMala = new ObjetoJuego();
                        balaMala.X = bossX;
                        balaMala.Y = bossY + rnd.Next(0, tamañoBoss);

                        if (probabilidad < 85) balaMala.Tag = "bala_boss_fase2_recta";
                        else if (probabilidad < 92) balaMala.Tag = "bala_boss_perseguidora";
                        else balaMala.Tag = "bala_boss_rebotona_sube";

                        balasBoss.Add(balaMala);

                        cooldownAtaqueBoss = 40;
                    }
                }
            }

            Rectangle hitboxJugador = new Rectangle(pbJugador.Left, pbJugador.Top, tamañoJugador, tamañoJugador);

            for (int i = balasBoss.Count - 1; i >= 0; i--)
            {
                balasBoss[i].X -= 9;

                if (balasBoss[i].Tag == "bala_boss_arriba") balasBoss[i].Y -= 3;
                if (balasBoss[i].Tag == "bala_boss_abajo") balasBoss[i].Y += 3;

                if (balasBoss[i].Tag == "bala_boss_perseguidora")
                {
                    balasBoss[i].X += 2;
                    if (balasBoss[i].Y < pbJugador.Top) balasBoss[i].Y += 2;
                    else if (balasBoss[i].Y > pbJugador.Top) balasBoss[i].Y -= 2;
                }

                if (balasBoss[i].Tag.StartsWith("bala_boss_rebotona"))
                {
                    balasBoss[i].X += 5;
                    if (balasBoss[i].Tag == "bala_boss_rebotona_sube")
                    {
                        balasBoss[i].Y -= 9;
                        if (balasBoss[i].Y <= 0) balasBoss[i].Tag = "bala_boss_rebotona_baja";
                    }
                    else if (balasBoss[i].Tag == "bala_boss_rebotona_baja")
                    {
                        balasBoss[i].Y += 9;
                        if (balasBoss[i].Y >= pnlEscenario.Height - 30) balasBoss[i].Tag = "bala_boss_rebotona_sube";
                    }
                }

                Rectangle areaBalaMala = new Rectangle(balasBoss[i].X, balasBoss[i].Y, 20, 20);
                if (areaBalaMala.IntersectsWith(hitboxJugador))
                {
                    balasBoss.RemoveAt(i);

                    if (tiempoInmunidad <= 0)
                    {
                        vidasJugador--;
                        tiempoInmunidad = 100;

                        if (vidasJugador <= 0)
                        {
                            DetenerJuego();
                            PerderNivel("Te has quedado sin vidas.\nEl Profesor Marcel te mandó a reparación.");
                            return;
                        }
                    }
                    continue;
                }

                if (balasBoss[i].X < -50 || balasBoss[i].X > pnlEscenario.Width + 100 || balasBoss[i].Y < -100 || balasBoss[i].Y > pnlEscenario.Height + 100)
                {
                    balasBoss.RemoveAt(i);
                }
            }

            if (vidaBoss > 0)
            {
                Rectangle hitboxBoss = new Rectangle(bossX, bossY, tamañoBoss, tamañoBoss);
                if (hitboxJugador.IntersectsWith(hitboxBoss) && tiempoInmunidad <= 0)
                {
                    vidasJugador--;
                    tiempoInmunidad = 100;

                    if (vidasJugador <= 0)
                    {
                        DetenerJuego();
                        PerderNivel("Te has quedado sin vidas.\n¡Te estrellaste contra el Profesor Marcel!");
                        return;
                    }
                }
            }

            pnlEscenario.Invalidate();
        }

        private void pnlEscenario_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            if (fondoActual != null)
            {
                e.Graphics.DrawImageUnscaled(fondoActual, fondoX, 0);
                e.Graphics.DrawImageUnscaled(fondoActual, fondoX + fondoActual.Width, 0);
            }
            else
            {
                e.Graphics.Clear(Color.FromArgb(20, 20, 30));
            }

            if (tiempoInmunidad > 0)
            {
                if (tiempoInmunidad % 10 > 4)
                {
                    movimiento.DibujarPersonaje(e.Graphics);
                }
            }
            else
            {
                movimiento.DibujarPersonaje(e.Graphics);
            }

            Image imagenAVisualizar = null;

            if (vidasJugador == 3)
            {
                imagenAVisualizar = imgVidaFull;
            }
            else if (vidasJugador == 2)
            {
                imagenAVisualizar = imgVidaMedia;
            }
            else if (vidasJugador == 1)
            {
                imagenAVisualizar = imgVidaBaja;
            }

            if (imagenAVisualizar != null)
            {
                e.Graphics.DrawImageUnscaled(imagenAVisualizar, 20, 20);
            }

            foreach (ObjetoJuego bala in balasJugador)
            {
                if (imgBalaJugador != null)
                {
                    e.Graphics.DrawImageUnscaled(imgBalaJugador, bala.X, bala.Y);
                }
            }

            if (vidaBoss > 0)
            {
                if (imagenActualBoss != null)
                {
                    e.Graphics.DrawImageUnscaled(imagenActualBoss, bossX, bossY);
                }

                if (flashBoss > 0)
                {
                    e.Graphics.FillRectangle(pincelDestello, bossX, bossY, tamañoBoss, tamañoBoss);
                }

                e.Graphics.DrawString("HP Marcel: " + vidaBoss, fuenteVidaBoss, Brushes.White, bossX, bossY - 25);

                foreach (ObjetoJuego balaMala in balasBoss)
                {
                    if (imgBalaJefe != null)
                    {
                        e.Graphics.DrawImageUnscaled(imgBalaJefe, balaMala.X, balaMala.Y);
                    }
                }
            }
        }

        private void DetenerJuego()
        {
            tmrGameLoop.Stop();

            // Apagamos toda la discoteca de las 3 pistas
            reproductorMusicaF1.controls.stop();
            reproductorMusicaF2.controls.stop();
            reproductorMusicaF3.controls.stop();

            // Apagamos los audios de la metralleta
            sfxDisparoStart.controls.stop();
            sfxDisparoLoop.controls.stop();
            sfxDisparoEnd.controls.stop();

            pnlEscenario.Invalidate();
        }

        private void PerderNivel(string mensaje)
        {
            Action cerrarAct = () => this.Close();
            if (jugadorActual != null)
            {
                jugadorActual.Billetera -= 100;
                FormDerrota.Mostrar($"{mensaje}\nMulta: $100", "¡GAME OVER!", cerrarAct);
                ActualizarDatos();
            }
            else
            {
                FormDerrota.Mostrar(mensaje, "¡GAME OVER!", cerrarAct);
            }
        }

        private void ActualizarDatos()
        {
            string rutaArchivo = "jugadores.json";

            if (!File.Exists(rutaArchivo)) return;

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