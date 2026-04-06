using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
        Image[] framesCaminarDer = new Image[4]; // Ciclo de caminata de 4 pasos
        Image[] framesCaminarIzq = new Image[4];
        Image frameIdleDer;
        Image frameIdleIzq;
        Image frameDisparoMedioDer;
        Image frameDisparoMedioIzq;
        Image frameActualSprite;
        int frameSaltoActual = 0;
        int frameCaminarActual = 0;
        int contadorAnimacionJugador = 0;
        int velocidadAnimacionJugador = 2; // Ajustable para dar el efecto de salto
        int velocidadCaminarJugador = 4; // Velocidad del ciclo de caminata

        // === DASH ===
        bool isDashing = false;
        int dashTimer = 0;
        int dashCooldown = 0;
        int dashSpeed = 16;

        // === BOLAS DEL JUGADOR ===
        List<BalaTesis> balasJugador = new List<BalaTesis>();
        int bulletSpeed = 18;
        int cooldownDisparo = 0;

        // ============================================
        // SISTEMA DE FASES Y JEFES
        // ============================================
        int currentPhase = 1;
        int flashBoss = 0;
        SolidBrush pincelDestello;
        List<BalaTesis> balasBoss = new List<BalaTesis>();
        Random rnd = new Random();

        // === IMÁGENES DE LOS ESCENARIOS (FONDOS) ===
        Image imgFondoFase1;
        Image imgFondoFase2;
        Image imgFondoFase3;

        Image[] framesVillanoAPA = new Image[4]; // SPRITES FASE 1
        int frameActualVillano = 0;
        int contadorAnimacionVillano = 0;
        int velocidadAnimacionVillano = 10;

        // === ANIMACIÓN DEL JEFE 2 (CEBOLLA / MARCO TEÓRICO) ===
        Image[] framesCebolla = new Image[4];
        int frameActualCebolla = 0;
        int contadorAnimacionCebolla = 0;
        int velocidadAnimacionCebolla = 10;

        // === ANIMACIÓN DEL JEFE 3 (ZANAHORIA / JURADO) ===
        Image[] framesZanahoria = new Image[4];
        int frameActualZanahoria = 0;
        int contadorAnimacionZanahoria = 0;
        int velocidadAnimacionZanahoria = 10;

        // ------------------------------
        // J1: LA PAPA (NORMAS APA)
        // ------------------------------
        Rectangle bossPapa;
        int papaHealth = 150;
        int papaState = -1; // -1 significa que está emergiendo del suelo
        int papaAttackCooldown = 80;
        int papaSpitCounter = 0;
        int papaSpitTimer = 0;

        // ------------------------------
        // J2: LA CEBOLLA 
        // ------------------------------
        Rectangle bossCebolla;
        int cebollaHealth = 250;
        int cebollaState = 0;
        int cebollaLluviaCooldown = 0;

        // ------------------------------
        // J3: LA ZANAHORIA GIGANTE
        // ------------------------------
        Rectangle bossZanahoria;
        int zanahoriaHealth = 400;
        int zanahoriaState = 0;
        int zanahoriaAttackCooldown = 150;
        int zanahoriaRayoCounter = 0;
        int zanahoriaRayoTimer = 0;
        int zanahoriaMiniCooldown = 250;

        public FormNivel4_Final()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormNivel4_Final_Load);
        }

        private void FormNivel4_Final_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;

            // EL SUELO DEL ESTUDIANTE VUELVE A SER LA TIERRA VISUAL
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

            // TODOS LOS JEFES NACEN EN pnlEscenario.Height (OCULTOS BAJO LA PANTALLA)
            bossPapa = new Rectangle(900, pnlEscenario.Height, 300, 350);
            // Le restamos 150 píxeles a la posición original. 
            // Mientras más grande sea el número que resta, más a la izquierda se irá.
            bossCebolla = new Rectangle(centroPantallaX - 150, pnlEscenario.Height, 450, 400);

            int carrotX = (this.ClientSize.Width / 2) - 200;
            bossZanahoria = new Rectangle(carrotX, pnlEscenario.Height, 400, 600);

            CargarSpritesJugador(); // Cargar animaciones del jugador
            CargarSpritesJefes(); // Cargar animaciones de los jefes y fondos

            tmrGameLoop.Interval = 10;
            tmrGameLoop.Tick += tmrGameLoop_Tick;
            tmrGameLoop.Start();
        }

        private void CargarSpritesJefes()
        {
            try
            {
                // Cargar los 3 escenarios de fondo
                imgFondoFase1 = (Image)Properties.Resources.ResourceManager.GetObject("fondo_apa");
                imgFondoFase2 = (Image)Properties.Resources.ResourceManager.GetObject("fondo_apa2");
                imgFondoFase3 = (Image)Properties.Resources.ResourceManager.GetObject("fondo_apa3");

                // Cargar Sprites Fase 1
                for (int i = 0; i < 4; i++)
                {
                    object obj = Properties.Resources.ResourceManager.GetObject($"tesis_f1-{i + 1}");
                    if (obj == null) obj = Properties.Resources.ResourceManager.GetObject($"tesis_f1_{i + 1}");
                    framesVillanoAPA[i] = (Image)obj ?? new Bitmap(10, 10);
                }

                // Cargar Sprites Fase 2
                for (int i = 0; i < 4; i++)
                {
                    object obj = Properties.Resources.ResourceManager.GetObject($"tesis_f2-{i + 1}");
                    if (obj == null) obj = Properties.Resources.ResourceManager.GetObject($"tesis_f2_{i + 1}");
                    framesCebolla[i] = (Image)obj ?? new Bitmap(10, 10);
                }

                // Cargar Sprites Fase 3
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
                string p = "gris"; // Por defecto
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
                        currentPhase = 2;
                        cebollaState = 1;
                        bossCebolla.Y = pnlEscenario.Height; // Aseguramos que empiece abajo
                        balasBoss.Clear();
                    }
                }
                else if (currentPhase == 2 && cebollaState == 2 && hitboxBala.IntersectsWith(bossCebolla) && cebollaHealth > 0)
                {
                    cebollaHealth -= playerDamage; flashBoss = 4; impactoRealizado = true;
                    if (cebollaHealth <= 0)
                    {
                        currentPhase = 3;
                        zanahoriaState = -1; // Activamos animación de salida para zanahoria
                        bossZanahoria.Y = pnlEscenario.Height; // Aseguramos que empiece abajo
                        balasBoss.Clear();
                    }
                }
                else if (currentPhase == 3 && hitboxBala.IntersectsWith(bossZanahoria) && zanahoriaHealth > 0)
                {
                    zanahoriaHealth -= playerDamage; flashBoss = 4; impactoRealizado = true;
                    if (zanahoriaHealth <= 0)
                    {
                        tmrGameLoop.Stop();
                        MessageBox.Show("¡HAS DEFENDIDO TU TESIS MAGISTRALMENTE Y HAS SIDO APROBADO CON HONORES!", "¡VICTORIA ABSOLUTA!");
                        this.Close();
                        return;
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

                // Animación inicial de subida
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

                if (cebollaState == 1) // Animación de subida
                {
                    bossCebolla.Y -= 3;
                    if (bossCebolla.Y <= groundY - 250)
                    {
                        bossCebolla.Y = groundY - 250; cebollaState = 2; cebollaLluviaCooldown = 30;
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

                if (zanahoriaState == -1) // Animación de subida
                {
                    bossZanahoria.Y -= 4; // Sube más rápido porque es más grande
                    if (bossZanahoria.Y <= groundY - 400)
                    {
                        bossZanahoria.Y = groundY - 400;
                        zanahoriaState = 0;
                        zanahoriaAttackCooldown = 150;
                    }
                }
                else if (zanahoriaState == 0) // Reposo muy largo
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

                // 2. MINI ZANAHORIAS RASTREADORAS 
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

                // MOVER LOS PROYECTILES DEL JEFE 3
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

            pnlEscenario.Invalidate();
        }

        private void RecibirDano()
        {
            playerHealth--;
            playerInvulnerability = 100;

            if (playerHealth <= 0)
            {
                tmrGameLoop.Stop();
                MessageBox.Show("¡La defensa de Tesis ha fracasado en manos del Jurado!\n¡Game Over!", "REPROBADO");
                this.Close();
            }
        }

        private void pnlEscenario_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            // 1. DIBUJAR EL FONDO
            if (currentPhase == 1 && imgFondoFase1 != null)
                e.Graphics.DrawImage(imgFondoFase1, 0, 0, pnlEscenario.Width, pnlEscenario.Height);
            else if (currentPhase == 2 && imgFondoFase2 != null)
                e.Graphics.DrawImage(imgFondoFase2, 0, 0, pnlEscenario.Width, pnlEscenario.Height);
            else if (currentPhase == 3 && imgFondoFase3 != null)
                e.Graphics.DrawImage(imgFondoFase3, 0, 0, pnlEscenario.Width, pnlEscenario.Height);
            else
                e.Graphics.Clear(Color.FromArgb(20, 20, 30));

            // 2. DIBUJAR AL VILLANO DE LA FASE 1 (La Papa / Normas APA)
            if (currentPhase == 1 && papaHealth > 0)
            {
                Image spriteAMostrar = framesVillanoAPA[frameActualVillano];
                if (spriteAMostrar != null) e.Graphics.DrawImage(spriteAMostrar, bossPapa);
                else e.Graphics.FillRectangle(Brushes.DarkRed, bossPapa);

                if (flashBoss > 0) e.Graphics.FillRectangle(pincelDestello, bossPapa);
                e.Graphics.DrawString("Normas APA HP: " + papaHealth, new Font("Arial", 16, FontStyle.Bold), Brushes.White, bossPapa.X, bossPapa.Y - 30);
            }
            // 3. DIBUJAR A LA CEBOLLA (Fase 2)
            else if (currentPhase == 2 && cebollaHealth > 0)
            {
                Image spriteCebolla = framesCebolla[frameActualCebolla];
                if (spriteCebolla != null) e.Graphics.DrawImage(spriteCebolla, bossCebolla);
                else e.Graphics.FillRectangle(Brushes.MediumPurple, bossCebolla);

                if (flashBoss > 0 && cebollaState == 2) e.Graphics.FillRectangle(pincelDestello, bossCebolla);
                if (cebollaState == 2) e.Graphics.DrawString("Marco Teórico HP: " + cebollaHealth, new Font("Arial", 16, FontStyle.Bold), Brushes.White, bossCebolla.X, bossCebolla.Y - 30);
            }
            // 4. DIBUJAR A LA ZANAHORIA (Fase 3)
            else if (currentPhase == 3 && zanahoriaHealth > 0)
            {
                Image spriteZanahoria = framesZanahoria[frameActualZanahoria];
                if (spriteZanahoria != null) e.Graphics.DrawImage(spriteZanahoria, bossZanahoria);
                else e.Graphics.FillRectangle(Brushes.DarkOrange, bossZanahoria);

                if (flashBoss > 0) e.Graphics.FillRectangle(pincelDestello, bossZanahoria);
                e.Graphics.DrawString("El Jurado HP: " + zanahoriaHealth, new Font("Arial", 20, FontStyle.Bold), Brushes.White, bossZanahoria.X + 15, bossZanahoria.Y - 30);
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

            e.Graphics.DrawString("Vidas Estudiante: " + playerHealth, new Font("Arial", 18, FontStyle.Bold), Brushes.LightPink, 20, 20);
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