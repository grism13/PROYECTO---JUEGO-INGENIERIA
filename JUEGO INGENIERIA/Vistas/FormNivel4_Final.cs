using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text; // IMPORTANTE PARA LA FUENTE
using System.IO; // IMPORTANTE PARA LAS RUTAS
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WMPLib;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel4_Final : Form
    {
        // === LIBRERÍA DE TECLADO ===
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        // === JUGADOR ===
        Rectangle player;
        int playerSpeed = 8;
        int facingDirection = 1;
        int playerHealth = 3;
        int playerDamage = 15;
        int playerInvulnerability = 0;

        // === FÍSICAS ===
        bool isJumping = false;
        int jumpSpeed = 0;
        int force = 0;
        int gravity = 2;
        int groundY;

        // === ANIMACIONES DEL JUGADOR CACHEADA ===
        Image[] framesSaltoDer = new Image[5];
        Image[] framesSaltoIzq = new Image[5];
        Image[] framesCaminarDer = new Image[4];
        Image[] framesCaminarIzq = new Image[4];
        Image frameIdleDer;
        Image frameIdleIzq;
        Image frameDisparoMedioDer;
        Image frameDisparoMedioIzq;
        Image frameActualSprite;
        int frameSaltoActual = 0;
        int frameCaminarActual = 0;
        int contadorAnimacionJugador = 0;
        int velocidadAnimacionJugador = 2;
        int velocidadCaminarJugador = 4;

        // === DASH ===
        bool isDashing = false;
        int dashTimer = 0;
        int dashCooldown = 0;
        int dashSpeed = 16;

        // === BOLAS DEL JUGADOR ===
        List<BalaTesis> balasJugador = new List<BalaTesis>();
        int bulletSpeed = 18;
        int cooldownDisparo = 0;

        // === AUDIO DE DISPARO ===
        WindowsMediaPlayer sfxDisparoStart = new WindowsMediaPlayer();
        WindowsMediaPlayer sfxDisparoLoop = new WindowsMediaPlayer();
        bool disparando = false;
        bool estabaDisparando = false;

        // ============================================
        // SISTEMA DE FASES Y JEFES
        // ============================================
        int currentPhase = 1;
        int flashBoss = 0;
        SolidBrush pincelDestello;
        List<BalaTesis> balasBoss = new List<BalaTesis>();
        Random rnd = new Random();

        Image imgFondoFase1;
        Image imgFondoFase2;
        Image imgFondoFase3;

        Image[] framesVillanoAPA = new Image[4];
        int frameActualVillano = 0;
        int contadorAnimacionVillano = 0;
        int velocidadAnimacionVillano = 10;

        Image[] framesCebolla = new Image[4];
        int frameActualCebolla = 0;
        int contadorAnimacionCebolla = 0;
        int velocidadAnimacionCebolla = 10;

        Image[] framesZanahoria = new Image[4];
        int frameActualZanahoria = 0;
        int contadorAnimacionZanahoria = 0;
        int velocidadAnimacionZanahoria = 10;

        Rectangle bossPapa;
        int papaHealth = 350;
        int papaState = -1;
        int papaAttackCooldown = 80;
        int papaSpitCounter = 0;
        int papaSpitTimer = 0;

        Rectangle bossCebolla;
        int cebollaHealth = 550;
        int cebollaState = 0;
        int cebollaLluviaCooldown = 0;

        Rectangle bossZanahoria;
        int zanahoriaHealth = 800;
        int zanahoriaState = 0;
        int zanahoriaAttackCooldown = 150;
        int zanahoriaRayoCounter = 0;
        int zanahoriaRayoTimer = 0;
        int zanahoriaMiniCooldown = 250;

        // === SISTEMA NARRATIVO Y MÁQUINA DE ESCRIBIR ===
        int estadoDialogo = 0;

        // CORRECCIÓN CS0104: Especificamos de qué librería es el Timer
        System.Windows.Forms.Timer tmrMaquinaEscribir;

        string textoCompletoDialogo = "";
        int indiceTexto = 0;

        // === FUENTES PERSONALIZADAS ===
        PrivateFontCollection pfc = new PrivateFontCollection();
        Font fuentePixel;
        Font fuenteTitulo;
        Font fuenteUI;

        public FormNivel4_Final()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormNivel4_Final_Load);
        }

        private void FormNivel4_Final_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- 1. CARGAR FUENTES PERSONALIZADAS ---
            try
            {
                string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
                if (File.Exists(rutaFuente))
                {
                    pfc.AddFontFile(rutaFuente);
                    fuentePixel = new Font(pfc.Families[0], 9f);
                    fuenteUI = new Font(pfc.Families[0], 12f);
                    fuenteTitulo = new Font(pfc.Families[0], 16f, FontStyle.Bold);
                }
                else
                {
                    fuentePixel = new Font("Courier New", 10f);
                    fuenteUI = new Font("Courier New", 12f);
                    fuenteTitulo = new Font("Courier New", 16f, FontStyle.Bold);
                }
            }
            catch
            {
                fuentePixel = new Font("Courier New", 10f);
                fuenteUI = new Font("Courier New", 12f);
                fuenteTitulo = new Font("Courier New", 16f, FontStyle.Bold);
            }

            // --- 2. CONFIGURAR TIMER MÁQUINA DE ESCRIBIR ---
            // CORRECCIÓN CS0104: Instanciamos usando el nombre completo
            tmrMaquinaEscribir = new System.Windows.Forms.Timer();
            tmrMaquinaEscribir.Interval = 30; // Velocidad de la escritura (30ms por letra)
            tmrMaquinaEscribir.Tick += TmrMaquinaEscribir_Tick;

            // CONEXIÓN FORZADA DEL BOTÓN POR CÓDIGO
            Control[] btns = this.Controls.Find("btnContinuar", true);
            if (btns.Length > 0)
            {
                Button btn = (Button)btns[0];
                btn.Click -= btnContinuar_Click;
                btn.Click += new EventHandler(btnContinuar_Click);
                if (fuentePixel != null) btn.Font = fuentePixel;

                // ACTIVAMOS LA NAVEGACIÓN CON FLECHITAS Y ENTER PARA EL MANDO
                NavegacionConsola.Configurar(this, btn);
            }

            // EL SUELO DEL ESTUDIANTE
            groundY = pnlEscenario.Height - 150;

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);
            pincelDestello = new SolidBrush(Color.FromArgb(120, Color.White));

            player = new Rectangle(150, groundY - 120, 90, 120);
            int centroPantallaX = (this.ClientSize.Width / 2) - 100;

            // LOS JEFES NACEN OCULTOS
            bossPapa = new Rectangle(900, pnlEscenario.Height, 300, 350);
            bossCebolla = new Rectangle(centroPantallaX - 150, pnlEscenario.Height, 450, 400);

            int carrotX = (this.ClientSize.Width / 2) - 200;
            bossZanahoria = new Rectangle(carrotX, pnlEscenario.Height, 400, 600);

            CargarSpritesJugador();
            CargarSpritesJefes();

            try
            {
                // SFX DISPARO CUPHEAD
                string rutaStart = Path.Combine(Application.StartupPath, "Resources", "player_default_fire_start_01.wav");
                string rutaLoop = Path.Combine(Application.StartupPath, "Resources", "player_default_fire_loop_01.wav");

                sfxDisparoStart.URL = rutaStart;
                sfxDisparoStart.settings.volume = 15;
                sfxDisparoStart.controls.stop();

                sfxDisparoLoop.URL = rutaLoop;
                sfxDisparoLoop.settings.setMode("loop", true);
                sfxDisparoLoop.settings.volume = 15;
                sfxDisparoLoop.controls.stop();
            }
            catch { }

            tmrGameLoop.Interval = 10;
            tmrGameLoop.Tick += tmrGameLoop_Tick;

            // MOSTRAMOS LA INTRODUCCIÓN AL ABRIR LA VENTANA
            MostrarDialogo(0);
        }

        // ============================================
        // LÓGICA DE LA MÁQUINA DE ESCRIBIR Y SALTO DE TEXTO
        // ============================================
        private void TmrMaquinaEscribir_Tick(object sender, EventArgs e)
        {
            Control[] lblTextos = this.Controls.Find("lblTextoDialogo", true);
            if (lblTextos.Length > 0 && indiceTexto < textoCompletoDialogo.Length)
            {
                Label lblTexto = (Label)lblTextos[0];
                lblTexto.Text += textoCompletoDialogo[indiceTexto];
                indiceTexto++;
            }
            else
            {
                TerminarEscrituraYMostrarBoton();
            }
        }

        private void TerminarEscrituraYMostrarBoton()
        {
            if (tmrMaquinaEscribir != null) tmrMaquinaEscribir.Stop();

            Control[] lblTextos = this.Controls.Find("lblTextoDialogo", true);
            if (lblTextos.Length > 0)
            {
                Label lblTexto = (Label)lblTextos[0];
                lblTexto.Text = textoCompletoDialogo;
            }

            Control[] btns = this.Controls.Find("btnContinuar", true);
            if (btns.Length > 0)
            {
                btns[0].Visible = true;
                btns[0].Focus(); // Fuerza a que lo gane para el Mando
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // SI ESTÁ ESCRIBIENDO Y EL JUGADOR PRESIONA UNA TECLA DE ACCIÓN (Adelantar Diálogo)
            if (tmrMaquinaEscribir != null && tmrMaquinaEscribir.Enabled)
            {
                if (keyData == Keys.Enter || keyData == Keys.Space || keyData == Keys.Z || keyData == Keys.X)
                {
                    TerminarEscrituraYMostrarBoton();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ============================================
        // APAGAR AUDIO AL SALIR O MORIR (Evita el Bug en Segundo Plano)
        // ============================================
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            tmrGameLoop.Stop();
            if (sfxDisparoStart != null) sfxDisparoStart.controls.stop();
            if (sfxDisparoLoop != null) sfxDisparoLoop.controls.stop();
        }

        // ============================================
        // SISTEMA NARRATIVO Y DE TRANSICIÓN CON IMÁGENES
        // ============================================
        private void MostrarDialogo(int faseSiguiente)
        {
            tmrGameLoop.Stop();

            // --- CORTAR SONIDO AL INICIAR DIÁLOGO ---
            disparando = false;
            estabaDisparando = false;
            if (sfxDisparoStart != null) sfxDisparoStart.controls.stop();
            if (sfxDisparoLoop != null) sfxDisparoLoop.controls.stop();

            estadoDialogo = faseSiguiente;

            Control[] pnlControles = this.Controls.Find("pnlDialogo", true);
            if (pnlControles.Length > 0)
            {
                Panel pnlDialogo = (Panel)pnlControles[0];
                pnlDialogo.Visible = true;
                pnlDialogo.BringToFront();

                Control[] lblNombres = pnlDialogo.Controls.Find("lblNombreJefe", true);
                Control[] lblTextos = pnlDialogo.Controls.Find("lblTextoDialogo", true);
                Control[] pbRetratos = pnlDialogo.Controls.Find("pbRetratoJefe", true);
                Control[] botones = pnlDialogo.Controls.Find("btnContinuar", true);

                Label lblNombre = lblNombres.Length > 0 ? (Label)lblNombres[0] : null;
                Label lblTexto = lblTextos.Length > 0 ? (Label)lblTextos[0] : null;
                PictureBox pbRetrato = pbRetratos.Length > 0 ? (PictureBox)pbRetratos[0] : null;
                Button btnAvanzar = botones.Length > 0 ? (Button)botones[0] : null;

                // Aplicar fuentes al panel
                if (lblNombre != null && fuenteTitulo != null) lblNombre.Font = fuenteTitulo;
                if (lblTexto != null && fuentePixel != null) lblTexto.Font = fuentePixel;

                // Ocultar botón y limpiar texto para el efecto de máquina de escribir
                if (btnAvanzar != null) btnAvanzar.Visible = false;
                if (lblTexto != null) lblTexto.Text = "";
                indiceTexto = 0;

                if (faseSiguiente == 0) // Intro Fase 1
                {
                    if (lblNombre != null) lblNombre.Text = "INVETIGACION CIENTIFICA";
                    textoCompletoDialogo = "Segun la profesía, si quieres alcanzar el maximo titulo, deberás vencerme a mi! Ahora estas dentro de los escritos antiguos, no olvides usar las normas APA. ¡Mucha suerte!";
                    if (pbRetrato != null) pbRetrato.Image = (Image)Properties.Resources.ResourceManager.GetObject("retrato_fase1");
                }
                else if (faseSiguiente == 1) // Transición a Fase 2
                {
                    if (lblNombre != null) lblNombre.Text = "INVETIGACION DE CAMPO";
                    textoCompletoDialogo = "¿¡CÓMO ES POSIBLE QUE ESQUIVASTE MIS ATAQUES DE SUEÑO EN LA CLASE DE METODOLOGIA DE LA INVESTIGACIÓN!?";
                    if (pbRetrato != null) pbRetrato.Image = (Image)Properties.Resources.ResourceManager.GetObject("retrato_fase2");
                }
                else if (faseSiguiente == 2) // Transición a Fase 3
                {
                    if (lblNombre != null) lblNombre.Text = "ETAPA FINAL";
                    textoCompletoDialogo = "¡¡YA COLMASTE MI PACIENCIA!! Te daré noches de desvelos, y que cuando puedas dormir... ¡SUEÑES CONMIGO!";
                    if (pbRetrato != null) pbRetrato.Image = (Image)Properties.Resources.ResourceManager.GetObject("retrato_fase3");
                }
                else if (faseSiguiente == 3) // Victoria Final
                {
                    if (lblNombre != null) lblNombre.Text = "¡TESIS APROBADA CON HONORES!";
                    textoCompletoDialogo = "Vaya... Ni tú ni yo sabemos como fue que pudiste llegar hasta aquí. Felicidades INGE, no cualquiera";
                    if (btnAvanzar != null) btnAvanzar.Text = "Finalizar";
                    if (pbRetrato != null) pbRetrato.Image = (Image)Properties.Resources.ResourceManager.GetObject("retrato_victoria");
                }

                // Iniciar la máquina de escribir
                tmrMaquinaEscribir.Start();
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            Control[] pnlControles = this.Controls.Find("pnlDialogo", true);
            if (pnlControles.Length > 0)
            {
                pnlControles[0].Visible = false;
            }

            if (estadoDialogo == 0)
            {
                tmrGameLoop.Start();
            }
            else if (estadoDialogo == 1)
            {
                currentPhase = 2;
                cebollaState = 1;
                bossCebolla.Y = pnlEscenario.Height;
                balasBoss.Clear();
                tmrGameLoop.Start();
            }
            else if (estadoDialogo == 2)
            {
                currentPhase = 3;
                zanahoriaState = -1;
                bossZanahoria.Y = pnlEscenario.Height;
                balasBoss.Clear();
                tmrGameLoop.Start();
            }
            else if (estadoDialogo == 3)
            {
                // REDIRECCIÓN A LA PANTALLA DE VICTORIA FINAL CON LA TRANSICIÓN
                Action cerrarAct = () => this.Close();
                FormVictoria.Mostrar("¡TESIS APROBADA CON HONORES!", "¡VICTORIA!", cerrarAct);
            }

            this.Focus();
        }


        private void CargarSpritesJefes()
        {
            try
            {
                imgFondoFase1 = (Image)Properties.Resources.ResourceManager.GetObject("fondo_apa");
                imgFondoFase2 = (Image)Properties.Resources.ResourceManager.GetObject("fondo_apa2");
                imgFondoFase3 = (Image)Properties.Resources.ResourceManager.GetObject("fondo_apa3");

                for (int i = 0; i < 4; i++)
                {
                    object obj = Properties.Resources.ResourceManager.GetObject($"tesis_f1-{i + 1}");
                    if (obj == null) obj = Properties.Resources.ResourceManager.GetObject($"tesis_f1_{i + 1}");
                    framesVillanoAPA[i] = (Image)obj ?? new Bitmap(10, 10);
                }

                for (int i = 0; i < 4; i++)
                {
                    object obj = Properties.Resources.ResourceManager.GetObject($"tesis_f2-{i + 1}");
                    if (obj == null) obj = Properties.Resources.ResourceManager.GetObject($"tesis_f2_{i + 1}");
                    framesCebolla[i] = (Image)obj ?? new Bitmap(10, 10);
                }

                for (int i = 0; i < 4; i++)
                {
                    object obj = Properties.Resources.ResourceManager.GetObject($"tesis_f3-{i + 1}");
                    if (obj == null) obj = Properties.Resources.ResourceManager.GetObject($"tesis_f3_{i + 1}");
                    framesZanahoria[i] = (Image)obj ?? new Bitmap(10, 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando imágenes: " + ex.Message);
            }
        }

        private void CargarSpritesJugador()
        {
            try
            {
                string p = "gris";
                if (!string.IsNullOrEmpty(DatosJuego.PersonajeElegido))
                {
                    p = DatosJuego.PersonajeElegido.ToLower();
                }

                int w = 90;
                int h = 120;

                float saltoEscala = 1.0f;
                int saltoElevar = 0;

                if (p == "gris")
                {
                    saltoEscala = 0.78f;
                    saltoElevar = 10;
                }
                else if (p == "roand")
                {
                    saltoEscala = 1.0f;
                    saltoElevar = 0;
                }
                else if (p == "eliezer")
                {
                    saltoEscala = 1.0f;
                    saltoElevar = 0;
                }

                for (int i = 0; i < 5; i++)
                {
                    object objSalto = Properties.Resources.ResourceManager.GetObject($"{p}-saltando{i + 1}");
                    if (objSalto == null) objSalto = Properties.Resources.ResourceManager.GetObject($"{p}_saltando{i + 1}");
                    if (objSalto == null) objSalto = Properties.Resources.ResourceManager.GetObject($"gris-saltando{i + 1}");

                    framesSaltoDer[i] = OptimizarImagen((Image)objSalto, h, saltoEscala, saltoElevar, 0, 0);

                    Image imgIzq = (Image)framesSaltoDer[i].Clone();
                    imgIzq.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    framesSaltoIzq[i] = imgIzq;
                }

                for (int i = 0; i < 3; i++)
                {
                    object objCamina = Properties.Resources.ResourceManager.GetObject($"{p}_ladoderecho{i + 1}");
                    if (objCamina == null) objCamina = Properties.Resources.ResourceManager.GetObject($"gris_ladoderecho{i + 1}");

                    Image optimizadaDer = OptimizarImagen((Image)objCamina, h);
                    Image optimizadaIzq = (Image)optimizadaDer.Clone();
                    optimizadaIzq.RotateFlip(RotateFlipType.RotateNoneFlipX);

                    if (i == 0)
                    {
                        framesCaminarDer[0] = optimizadaDer;
                        framesCaminarIzq[0] = optimizadaIzq;
                    }
                    else if (i == 1)
                    {
                        framesCaminarDer[1] = optimizadaDer;
                        framesCaminarIzq[1] = optimizadaIzq;
                        framesCaminarDer[3] = optimizadaDer;
                        framesCaminarIzq[3] = optimizadaIzq;
                    }
                    else if (i == 2)
                    {
                        framesCaminarDer[2] = optimizadaDer;
                        framesCaminarIzq[2] = optimizadaIzq;
                    }
                }

                object objIdle = Properties.Resources.ResourceManager.GetObject($"{p}_ladoderecho1");
                if (objIdle == null) objIdle = Properties.Resources.ResourceManager.GetObject("gris_ladoderecho1");

                frameIdleDer = OptimizarImagen((Image)objIdle, h);

                Image idleIzq = (Image)frameIdleDer.Clone();
                idleIzq.RotateFlip(RotateFlipType.RotateNoneFlipX);
                frameIdleIzq = idleIzq;

                object objDisparoMedio = Properties.Resources.ResourceManager.GetObject($"{p}-disparo-medio-derecha");
                if (objDisparoMedio == null) objDisparoMedio = Properties.Resources.ResourceManager.GetObject($"{p}_disparo_medio_derecha");
                if (objDisparoMedio == null) objDisparoMedio = Properties.Resources.ResourceManager.GetObject("gris-disparo-medio-derecha");
                if (objDisparoMedio == null) objDisparoMedio = Properties.Resources.ResourceManager.GetObject("gris_disparo_medio_derecha");

                if (objDisparoMedio != null)
                {
                    float disparoEscala = 1.0f;
                    int disparoElevar = 0;
                    int disparoIzq = 0;
                    int disparoDer = 0;

                    if (p == "eliezer")
                    {
                        disparoEscala = 0.84f;
                        disparoElevar = 7;
                        disparoIzq = 7;
                        disparoDer = 0;
                    }
                    else if (p == "roand")
                    {
                        disparoEscala = 0.90f;
                        disparoElevar = 4;
                        disparoIzq = 15;
                        disparoDer = 0;
                    }
                    else
                    {
                        disparoEscala = 0.75f;
                        disparoElevar = 11;
                        disparoIzq = 13;
                        disparoDer = 0;
                    }

                    frameDisparoMedioDer = OptimizarImagen((Image)objDisparoMedio, h, disparoEscala, disparoElevar, disparoIzq, disparoDer);
                    Image disparoIzqImg = (Image)frameDisparoMedioDer.Clone();
                    disparoIzqImg.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    frameDisparoMedioIzq = disparoIzqImg;
                }
                else
                {
                    frameDisparoMedioDer = frameIdleDer;
                    frameDisparoMedioIzq = frameIdleIzq;
                }

                frameActualSprite = frameIdleDer;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando sprites: " + ex.Message);
            }
        }

        private Bitmap OptimizarImagen(Image img, int targetHeight, float contentScale = 1.0f, int paddingBottom = 0, int paddingLeft = 0, int paddingRight = 0)
        {
            if (img == null) return null;

            float ratio = (float)img.Width / img.Height;

            int contentHeight = (int)(targetHeight * contentScale);
            int contentWidth = (int)(contentHeight * ratio);

            int canvasHeight = targetHeight;
            int canvasWidth = contentWidth + paddingLeft + paddingRight;

            Bitmap bmp = new Bitmap(canvasWidth, canvasHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                int paintY = canvasHeight - contentHeight - paddingBottom;
                int paintX = paddingLeft;

                g.DrawImage(img, paintX, paintY, contentWidth, contentHeight);
            }
            return bmp;
        }

        // ====== GAME LOOP PRINCIPAL ======
        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // === LECTURA DE CONTROLES ===
            bool goLeft = (GetAsyncKeyState(Keys.Left) & 0x8000) != 0;
            bool goRight = (GetAsyncKeyState(Keys.Right) & 0x8000) != 0;
            bool keyJump = (GetAsyncKeyState(Keys.Z) & 0x8000) != 0 || (GetAsyncKeyState(Keys.Space) & 0x8000) != 0;
            bool keyShoot = (GetAsyncKeyState(Keys.X) & 0x8000) != 0;
            bool keyDash = (GetAsyncKeyState(Keys.ShiftKey) & 0x8000) != 0;

            bool isMovingLeftOrRight = false;

            if (dashCooldown > 0) dashCooldown--;
            if (cooldownDisparo > 0) cooldownDisparo--;
            if (flashBoss > 0) flashBoss--;
            if (playerInvulnerability > 0) playerInvulnerability--;

            // =========================
            // LÓGICA DEL JUGADOR
            // =========================
            if (keyDash && !isDashing && dashCooldown == 0)
            {
                isDashing = true; dashTimer = 18; dashCooldown = 60;
            }

            if (isDashing)
            {
                player.X += dashSpeed * facingDirection;
                dashTimer--;
                if (dashTimer <= 0) isDashing = false;

                if (player.X < 0) player.X = 0;
                if (player.X > pnlEscenario.Width - player.Width) player.X = pnlEscenario.Width - player.Width;
                isMovingLeftOrRight = true;
            }
            else
            {
                if (goLeft && player.X > 0) { player.X -= playerSpeed; facingDirection = -1; isMovingLeftOrRight = true; }
                if (goRight && player.X < pnlEscenario.Width - player.Width) { player.X += playerSpeed; facingDirection = 1; isMovingLeftOrRight = true; }
            }

            if (keyJump && !isJumping && player.Y + player.Height >= groundY)
            {
                isJumping = true; force = 16;
            }

            if (isJumping)
            {
                jumpSpeed = -force; force -= 1;

                if (force > 15)
                {
                    frameSaltoActual = 0;
                }
                else if (jumpSpeed < -4)
                {
                    frameSaltoActual = 1;
                }
                else if (jumpSpeed >= -4 && jumpSpeed <= 6)
                {
                    frameSaltoActual = 2;
                }
                else
                {
                    if (player.Y + player.Height >= groundY - 40)
                    {
                        frameSaltoActual = 4;
                    }
                    else
                    {
                        frameSaltoActual = 3;
                    }
                }
                frameActualSprite = (facingDirection == 1) ? framesSaltoDer[frameSaltoActual] : framesSaltoIzq[frameSaltoActual];
            }
            else
            {
                jumpSpeed = gravity * 4;

                if (isMovingLeftOrRight)
                {
                    contadorAnimacionJugador++;
                    if (contadorAnimacionJugador >= velocidadCaminarJugador)
                    {
                        contadorAnimacionJugador = 0;
                        frameCaminarActual++;
                        if (frameCaminarActual >= 4) frameCaminarActual = 0;
                    }
                    frameActualSprite = (facingDirection == 1) ? framesCaminarDer[frameCaminarActual] : framesCaminarIzq[frameCaminarActual];
                }
                else
                {
                    frameCaminarActual = 0;
                    contadorAnimacionJugador = 0;
                    frameActualSprite = (facingDirection == 1) ? frameIdleDer : frameIdleIzq;
                }
            }

            if (cooldownDisparo > 8)
            {
                frameActualSprite = (facingDirection == 1) ? frameDisparoMedioDer : frameDisparoMedioIzq;
            }

            player.Y += jumpSpeed;
            if (player.Y + player.Height >= groundY) { player.Y = groundY - player.Height; isJumping = false; }

            if (keyShoot && cooldownDisparo <= 0)
            {
                BalaTesis nuevaBala = new BalaTesis();
                nuevaBala.X = player.X + (player.Width / 2);
                nuevaBala.Y = player.Y + (player.Height / 2) - 10;
                nuevaBala.Tag = facingDirection == 1 ? "der" : "izq";
                balasJugador.Add(nuevaBala);
                cooldownDisparo = 12;
            }

            // =========================
            // LÓGICA DE AUDIO (CUPHEAD)
            // =========================
            disparando = keyShoot;
            if (disparando && !estabaDisparando)
            {
                sfxDisparoStart.controls.stop();
                sfxDisparoStart.controls.play();

                sfxDisparoLoop.controls.stop();
                sfxDisparoLoop.controls.play();
            }
            else if (!disparando && estabaDisparando)
            {
                sfxDisparoStart.controls.stop();
                sfxDisparoLoop.controls.stop();
            }
            estabaDisparando = disparando;

            // =========================
            // COLISIONES: TUS BALAS VS MUNDO
            // =========================
            for (int i = balasJugador.Count - 1; i >= 0; i--)
            {
                BalaTesis balaActual = balasJugador[i];
                balaActual.X += bulletSpeed * (balaActual.Tag == "der" ? 1 : -1);
                Rectangle hitboxBala = new Rectangle((int)balaActual.X, (int)balaActual.Y, 20, 10);
                bool impactoRealizado = false;

                for (int m = balasBoss.Count - 1; m >= 0; m--)
                {
                    if (balasBoss[m].Tag == "boss_minizanahoria")
                    {
                        Rectangle rectMini = new Rectangle((int)balasBoss[m].X, (int)balasBoss[m].Y, 30, 30);
                        if (hitboxBala.IntersectsWith(rectMini))
                        {
                            balasBoss.RemoveAt(m);
                            impactoRealizado = true;
                            break;
                        }
                    }
                }

                if (impactoRealizado) { balasJugador.RemoveAt(i); continue; }

                if (currentPhase == 1 && hitboxBala.IntersectsWith(bossPapa) && papaHealth > 0)
                {
                    papaHealth -= playerDamage; flashBoss = 4; impactoRealizado = true;
                    if (papaHealth <= 0)
                    {
                        MostrarDialogo(1);
                    }
                }
                else if (currentPhase == 2 && cebollaState == 2 && hitboxBala.IntersectsWith(bossCebolla) && cebollaHealth > 0)
                {
                    cebollaHealth -= playerDamage; flashBoss = 4; impactoRealizado = true;
                    if (cebollaHealth <= 0)
                    {
                        MostrarDialogo(2);
                    }
                }
                else if (currentPhase == 3 && hitboxBala.IntersectsWith(bossZanahoria) && zanahoriaHealth > 0)
                {
                    zanahoriaHealth -= playerDamage; flashBoss = 4; impactoRealizado = true;
                    if (zanahoriaHealth <= 0)
                    {
                        MostrarDialogo(3);
                    }
                }

                if (impactoRealizado) { balasJugador.RemoveAt(i); }
                else if (balaActual.X > pnlEscenario.Width || balaActual.X < -50) { balasJugador.RemoveAt(i); }
            }

            // ====================================================
            // I.A. DE LOS JEFES (POR FASES)
            // ====================================================
            if (currentPhase == 1) // ====== LA PAPA (NORMAS APA) ======
            {
                if (papaHealth > 0)
                {
                    contadorAnimacionVillano++;
                    if (contadorAnimacionVillano >= velocidadAnimacionVillano)
                    {
                        contadorAnimacionVillano = 0;
                        frameActualVillano++;
                        if (frameActualVillano >= 4) frameActualVillano = 0;
                    }
                }

                if (papaState == -1)
                {
                    bossPapa.Y -= 3;
                    if (bossPapa.Y <= groundY - 350)
                    {
                        bossPapa.Y = groundY - 350;
                        papaState = 0;
                    }
                }
                else if (papaState == 0)
                {
                    papaAttackCooldown--;
                    if (papaAttackCooldown <= 0) { papaState = 1; papaSpitCounter = 0; papaSpitTimer = 0; }
                }
                else if (papaState == 1)
                {
                    papaSpitTimer--;
                    if (papaSpitTimer <= 0)
                    {
                        BalaTesis bolaTierra = new BalaTesis();
                        bolaTierra.X = bossPapa.X; bolaTierra.Y = groundY - 60; bolaTierra.Tag = "boss_tierra";
                        balasBoss.Add(bolaTierra);

                        papaSpitCounter++;
                        if (papaSpitCounter >= 6) { papaState = 0; papaAttackCooldown = 80; }
                        else { papaSpitTimer = 50; }
                    }
                }

                for (int i = balasBoss.Count - 1; i >= 0; i--)
                {
                    BalaTesis bola = balasBoss[i];
                    bola.X -= 11;
                    Rectangle hitboxBola = new Rectangle((int)bola.X, (int)bola.Y, 50, 50);
                    if (hitboxBola.IntersectsWith(player) && playerInvulnerability <= 0)
                    {
                        RecibirDano(); balasBoss.RemoveAt(i); if (playerHealth <= 0) return;
                    }
                    else if (bola.X < -150) { balasBoss.RemoveAt(i); }
                }
            }
            else if (currentPhase == 2) // ====== LA CEBOLLA ======
            {
                if (cebollaHealth > 0)
                {
                    contadorAnimacionCebolla++;
                    if (contadorAnimacionCebolla >= velocidadAnimacionCebolla)
                    {
                        contadorAnimacionCebolla = 0;
                        frameActualCebolla++;
                        if (frameActualCebolla >= 4) frameActualCebolla = 0;
                    }
                }

                if (cebollaState == 1)
                {
                    bossCebolla.Y -= 3;
                    if (bossCebolla.Y <= groundY - 400)
                    {
                        bossCebolla.Y = groundY - 400; cebollaState = 2; cebollaLluviaCooldown = 30;
                    }
                }
                else if (cebollaState == 2)
                {
                    cebollaLluviaCooldown--;
                    if (cebollaLluviaCooldown <= 0)
                    {
                        BalaTesis lagrima = new BalaTesis();
                        lagrima.X = rnd.Next(20, pnlEscenario.Width - 50);
                        lagrima.Y = -50;
                        lagrima.Tag = "boss_lagrima";
                        balasBoss.Add(lagrima);
                        cebollaLluviaCooldown = rnd.Next(15, 35);
                    }

                    for (int i = balasBoss.Count - 1; i >= 0; i--)
                    {
                        BalaTesis gota = balasBoss[i];
                        gota.Y += 11;
                        Rectangle hitboxLagrima = new Rectangle((int)gota.X, (int)gota.Y, 30, 50);
                        if (hitboxLagrima.IntersectsWith(player) && playerInvulnerability <= 0)
                        {
                            RecibirDano(); balasBoss.RemoveAt(i); if (playerHealth <= 0) return;
                        }
                        else if (gota.Y > groundY) { balasBoss.RemoveAt(i); }
                    }
                }
            }
            else if (currentPhase == 3) // ====== ZANAHORIA GIGANTE ======
            {
                if (zanahoriaHealth > 0)
                {
                    contadorAnimacionZanahoria++;
                    if (contadorAnimacionZanahoria >= velocidadAnimacionZanahoria)
                    {
                        contadorAnimacionZanahoria = 0;
                        frameActualZanahoria++;
                        if (frameActualZanahoria >= 4) frameActualZanahoria = 0;
                    }
                }

                if (zanahoriaState == -1)
                {
                    bossZanahoria.Y -= 4;
                    if (bossZanahoria.Y <= groundY - 400)
                    {
                        bossZanahoria.Y = groundY - 400;
                        zanahoriaState = 0;
                        zanahoriaAttackCooldown = 150;
                    }
                }
                else if (zanahoriaState == 0)
                {
                    zanahoriaAttackCooldown--;
                    if (zanahoriaAttackCooldown <= 0)
                    {
                        zanahoriaState = 1;
                        zanahoriaRayoCounter = 0;
                        zanahoriaRayoTimer = 0;
                    }
                }
                else if (zanahoriaState == 1)
                {
                    zanahoriaRayoTimer--;
                    if (zanahoriaRayoTimer <= 0)
                    {
                        BalaTesis rayo = new BalaTesis();
                        rayo.X = bossZanahoria.X + (bossZanahoria.Width / 2);
                        rayo.Y = bossZanahoria.Y - 20;
                        rayo.Tag = "boss_rayo";

                        float dirX = (player.X + 30) - rayo.X;
                        float dirY = (player.Y + 40) - rayo.Y;
                        float distancia = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

                        rayo.VX = (dirX / distancia) * 6.5f;
                        rayo.VY = (dirY / distancia) * 6.5f;

                        balasBoss.Add(rayo);

                        zanahoriaRayoCounter++;

                        if (zanahoriaRayoCounter >= 2)
                        {
                            zanahoriaState = 0;
                            zanahoriaAttackCooldown = rnd.Next(180, 250);
                        }
                        else
                        {
                            zanahoriaRayoTimer = 110;
                        }
                    }
                }

                zanahoriaMiniCooldown--;
                if (zanahoriaMiniCooldown <= 0)
                {
                    BalaTesis mini = new BalaTesis();
                    if (player.X < (pnlEscenario.Width / 2)) mini.X = pnlEscenario.Width + 50;
                    else mini.X = -50;

                    mini.Y = rnd.Next(groundY - 100, groundY - 30);
                    mini.Tag = "boss_minizanahoria";
                    balasBoss.Add(mini);

                    zanahoriaMiniCooldown = rnd.Next(250, 400);
                }

                List<BalaTesis> basuraFuegoAmigo = new List<BalaTesis>();
                foreach (BalaTesis laser in balasBoss)
                {
                    if (laser.Tag == "boss_rayo")
                    {
                        Rectangle rectLaserFront = new Rectangle((int)laser.X - 15, (int)laser.Y - 15, 30, 30);
                        foreach (BalaTesis miniz in balasBoss)
                        {
                            if (miniz.Tag == "boss_minizanahoria" && !basuraFuegoAmigo.Contains(miniz))
                            {
                                Rectangle rectMini = new Rectangle((int)miniz.X, (int)miniz.Y, 30, 30);
                                if (rectLaserFront.IntersectsWith(rectMini))
                                    basuraFuegoAmigo.Add(miniz);
                            }
                        }
                    }
                }
                foreach (BalaTesis destruida in basuraFuegoAmigo) { balasBoss.Remove(destruida); }

                for (int i = balasBoss.Count - 1; i >= 0; i--)
                {
                    BalaTesis bola = balasBoss[i];
                    int tamHitbox = 40;

                    if (bola.Tag == "boss_rayo")
                    {
                        bola.X += bola.VX;
                        bola.Y += bola.VY;
                        tamHitbox = 30;
                    }
                    else if (bola.Tag == "boss_minizanahoria")
                    {
                        float distX = (player.X + 30) - bola.X;
                        float distY = (player.Y + 40) - bola.Y;
                        float distTotal = (float)Math.Sqrt(distX * distX + distY * distY);

                        if (distTotal > 0)
                        {
                            bola.X += (distX / distTotal) * 3.0f;
                            bola.Y += (distY / distTotal) * 3.0f;
                        }
                        tamHitbox = 30;
                    }

                    Rectangle chocaPoder = new Rectangle((int)bola.X - 15, (int)bola.Y - 15, tamHitbox, tamHitbox);

                    if (chocaPoder.IntersectsWith(player) && playerInvulnerability <= 0)
                    {
                        RecibirDano(); balasBoss.RemoveAt(i); if (playerHealth <= 0) return;
                    }
                    else if (bola.X < -250 || bola.X > pnlEscenario.Width + 250 || bola.Y > pnlEscenario.Height + 250 || bola.Y < -250)
                    {
                        balasBoss.RemoveAt(i);
                    }
                }
            }

            // ====================================================
            // COLISIONES POR CONTACTO CON EL JEFE
            // ====================================================
            if (playerInvulnerability <= 0)
            {
                if (currentPhase == 1 && papaHealth > 0 && papaState >= 0 && player.IntersectsWith(bossPapa))
                {
                    RecibirDano();
                }
                else if (currentPhase == 2 && cebollaHealth > 0 && cebollaState == 2 && player.IntersectsWith(bossCebolla))
                {
                    RecibirDano();
                }
            }

            pnlEscenario.Invalidate();
        }

        private void RecibirDano()
        {
            playerHealth--;
            playerInvulnerability = 100;

            if (playerHealth <= 0)
            {
                tmrGameLoop.Stop();
                // REDIRECCIÓN A LA PANTALLA DE DERROTA CON LA TRANSICIÓN
                Action cerrarAct = () => this.Close();
                FormDerrota.Mostrar("¡La defensa de Tesis ha fracasado en manos del Jurado!", "¡REPROBADO!", cerrarAct);
            }
        }


        private void pnlEscenario_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            if (currentPhase == 1 && imgFondoFase1 != null)
                e.Graphics.DrawImage(imgFondoFase1, 0, 0, pnlEscenario.Width, pnlEscenario.Height);
            else if (currentPhase == 2 && imgFondoFase2 != null)
                e.Graphics.DrawImage(imgFondoFase2, 0, 0, pnlEscenario.Width, pnlEscenario.Height);
            else if (currentPhase == 3 && imgFondoFase3 != null)
                e.Graphics.DrawImage(imgFondoFase3, 0, 0, pnlEscenario.Width, pnlEscenario.Height);
            else
                e.Graphics.Clear(Color.FromArgb(20, 20, 30));

            Font fUI = fuenteUI ?? new Font("Arial", 16, FontStyle.Bold);
            Font fUI_big = fuenteTitulo ?? new Font("Arial", 20, FontStyle.Bold);

            if (currentPhase == 1 && papaHealth > 0)
            {
                Image spriteAMostrar = framesVillanoAPA[frameActualVillano];
                if (spriteAMostrar != null) e.Graphics.DrawImage(spriteAMostrar, bossPapa);
                else e.Graphics.FillRectangle(Brushes.DarkRed, bossPapa);

                if (flashBoss > 0) e.Graphics.FillRectangle(pincelDestello, bossPapa);
                // OCULTADO: e.Graphics.DrawString("Normas APA HP: " + papaHealth, fUI, Brushes.White, bossPapa.X, bossPapa.Y - 30);
            }
            else if (currentPhase == 2 && cebollaHealth > 0)
            {
                Image spriteCebolla = framesCebolla[frameActualCebolla];
                if (spriteCebolla != null) e.Graphics.DrawImage(spriteCebolla, bossCebolla);
                else e.Graphics.FillRectangle(Brushes.MediumPurple, bossCebolla);

                if (flashBoss > 0 && cebollaState == 2) e.Graphics.FillRectangle(pincelDestello, bossCebolla);
                // OCULTADO: if (cebollaState == 2) e.Graphics.DrawString("Marco Teórico HP: " + cebollaHealth, fUI, Brushes.White, bossCebolla.X, bossCebolla.Y - 30);
            }
            else if (currentPhase == 3 && zanahoriaHealth > 0)
            {
                Image spriteZanahoria = framesZanahoria[frameActualZanahoria];
                if (spriteZanahoria != null) e.Graphics.DrawImage(spriteZanahoria, bossZanahoria);
                else e.Graphics.FillRectangle(Brushes.DarkOrange, bossZanahoria);

                if (flashBoss > 0) e.Graphics.FillRectangle(pincelDestello, bossZanahoria);
                // OCULTADO: e.Graphics.DrawString("El Jurado HP: " + zanahoriaHealth, fUI_big, Brushes.White, bossZanahoria.X + 15, bossZanahoria.Y - 30);
            }

            foreach (BalaTesis bola in balasBoss)
            {
                if (bola.Tag == "boss_tierra")
                {
                    e.Graphics.FillEllipse(Brushes.SaddleBrown, (int)bola.X, (int)bola.Y, 50, 50);
                }
                else if (bola.Tag == "boss_lagrima")
                {
                    e.Graphics.FillEllipse(Brushes.SkyBlue, (int)bola.X, (int)bola.Y, 30, 50);
                }
                else if (bola.Tag == "boss_rayo")
                {
                    float estela = 15.0f;
                    Pen laserPen = new Pen(Color.Cyan, 26);
                    laserPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    laserPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(laserPen, bola.X, bola.Y, bola.X - (bola.VX * estela), bola.Y - (bola.VY * estela));
                    e.Graphics.FillEllipse(Brushes.White, bola.X - 12, bola.Y - 12, 24, 24);
                }
                else if (bola.Tag == "boss_minizanahoria")
                {
                    e.Graphics.FillEllipse(Brushes.Orange, (int)bola.X, (int)bola.Y, 30, 30);
                    e.Graphics.FillRectangle(Brushes.Green, (int)bola.X + 10, (int)bola.Y - 10, 10, 15);
                }
            }

            if (!(playerInvulnerability > 0 && (playerInvulnerability / 5) % 2 == 0))
            {
                if (frameActualSprite != null)
                {
                    int drawX = player.X + (player.Width / 2) - (frameActualSprite.Width / 2);
                    int drawY = player.Y + player.Height - frameActualSprite.Height;

                    if (isDashing)
                    {
                        e.Graphics.DrawImageUnscaled(frameActualSprite, drawX, drawY);
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Cyan)), player);
                    }
                    else
                    {
                        e.Graphics.DrawImageUnscaled(frameActualSprite, drawX, drawY);
                    }
                }
                else
                {
                    Brush colorJugador = isDashing ? Brushes.Cyan : Brushes.Blue;
                    e.Graphics.FillRectangle(colorJugador, player);
                }
            }

            foreach (BalaTesis bala in balasJugador)
            {
                e.Graphics.FillEllipse(Brushes.Yellow, (int)bala.X, (int)bala.Y, 20, 10);
            }

            e.Graphics.DrawString("Vidas Estudiante: " + playerHealth, fUI_big, Brushes.LightPink, 20, 20);
        }

        // CORRECCIÓN CS0103: Dejamos el método vacío para que el diseñador no reclame
        private void pnlDialogo_Paint(object sender, PaintEventArgs e)
        {
        }
    }

    public class BalaTesis
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VX { get; set; }
        public float VY { get; set; }
        public string Tag { get; set; }
    }
}
