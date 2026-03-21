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
        // Moverse: Flechas 
        // Saltar: Z o Espacio
        // Disparar: X
        // Dash: Shift
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        // === JUGADOR ===
        Rectangle player;
        int playerSpeed = 8;
        int facingDirection = 1;
        int playerHealth = 3;
        int playerInvulnerability = 0; // Tiempo de invencibilidad tras recibir daño

        // === FÍSICAS ===
        bool isJumping = false;
        int jumpSpeed = 0;
        int force = 0;
        int gravity = 2;
        int groundY;

        // === DASH ===
        bool isDashing = false;
        int dashTimer = 0;
        int dashCooldown = 0;
        int dashSpeed = 16;

        // === DISPAROS JUGADOR ===
        List<BalaTesis> balasJugador = new List<BalaTesis>();
        int bulletSpeed = 18;
        int cooldownDisparo = 0;

        // === JEFE TESIS (FASE 1 - TUTOR/PAPA) ===
        Rectangle boss;
        int bossHealth = 350;
        int flashBoss = 0;
        SolidBrush pincelDestello;

        // CEREBRO DEL JEFE
        List<BalaTesis> balasBoss = new List<BalaTesis>();
        int bossState = 0; // 0 = Esperando, 1 = Atacando
        int bossAttackCooldown = 150; // Tiempo de pausa antes de la siguiente ráfaga
        int bossSpitCounter = 0; // Bolas escupidas en la ráfaga actual
        int bossSpitTimer = 0; // Espaciado entre bola y bola (Ajustado)

        public FormNivel4_Final()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormNivel4_Final_Load);
        }

        private void FormNivel4_Final_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            groundY = pnlEscenario.Height - 150;

            // SUPER-OPTIMIZACIÓN GRAFICA
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);
            pincelDestello = new SolidBrush(Color.FromArgb(120, Color.White));

            player = new Rectangle(150, groundY - 80, 60, 80);
            boss = new Rectangle(1000, groundY - 250, 200, 250);

            tmrGameLoop.Interval = 10;
            tmrGameLoop.Tick += tmrGameLoop_Tick;
            tmrGameLoop.Start();
        }

        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // === CONTROLES CUPHEAD EXACTOS ===
            bool goLeft = (GetAsyncKeyState(Keys.Left) & 0x8000) != 0;
            bool goRight = (GetAsyncKeyState(Keys.Right) & 0x8000) != 0;
            bool keyJump = (GetAsyncKeyState(Keys.Z) & 0x8000) != 0 || (GetAsyncKeyState(Keys.Space) & 0x8000) != 0; // Salto = Z o Espacio
            bool keyShoot = (GetAsyncKeyState(Keys.X) & 0x8000) != 0; // Disparo = X
            bool keyDash = (GetAsyncKeyState(Keys.ShiftKey) & 0x8000) != 0; // Dash = Shift

            // Retardos y Enfriamientos
            if (dashCooldown > 0) dashCooldown--;
            if (cooldownDisparo > 0) cooldownDisparo--;
            if (flashBoss > 0) flashBoss--;
            if (playerInvulnerability > 0) playerInvulnerability--;

            // =========================
            // LÓGICA DEL DASH
            // =========================
            if (keyDash && !isDashing && dashCooldown == 0)
            {
                isDashing = true;
                dashTimer = 18;
                dashCooldown = 60;
            }

            if (isDashing)
            {
                player.X += dashSpeed * facingDirection;
                dashTimer--;
                if (dashTimer <= 0) isDashing = false;

                if (player.X < 0) player.X = 0;
                if (player.X > pnlEscenario.Width - player.Width) player.X = pnlEscenario.Width - player.Width;
            }
            else
            {
                // =========================
                // MOVER NORMALMENTE
                // =========================
                if (goLeft && player.X > 0)
                {
                    player.X -= playerSpeed;
                    facingDirection = -1;
                }
                if (goRight && player.X < pnlEscenario.Width - player.Width)
                {
                    player.X += playerSpeed;
                    facingDirection = 1;
                }
            }

            // =========================
            // SALTO Y GRAVEDAD
            // =========================
            if (keyJump && !isJumping && player.Y + player.Height >= groundY)
            {
                isJumping = true;
                force = 22; // Fuerza de brinco
            }

            if (isJumping)
            {
                jumpSpeed = -force;
                force -= 1;
            }
            else
            {
                jumpSpeed = gravity * 4;
            }

            player.Y += jumpSpeed;

            if (player.Y + player.Height >= groundY)
            {
                player.Y = groundY - player.Height;
                isJumping = false;
            }

            // =========================
            // DISPAROS JUGADOR (Tecla X)
            // =========================
            if (keyShoot && cooldownDisparo <= 0)
            {
                BalaTesis nuevaBala = new BalaTesis();
                nuevaBala.X = player.X + (player.Width / 2);
                nuevaBala.Y = player.Y + (player.Height / 2) - 10;
                nuevaBala.Tag = facingDirection == 1 ? "der" : "izq";

                balasJugador.Add(nuevaBala);
                cooldownDisparo = 12;
            }

            for (int i = balasJugador.Count - 1; i >= 0; i--)
            {
                BalaTesis balaActual = balasJugador[i];
                balaActual.X += bulletSpeed * (balaActual.Tag == "der" ? 1 : -1);

                Rectangle hitboxBala = new Rectangle(balaActual.X, balaActual.Y, 20, 10);

                if (hitboxBala.IntersectsWith(boss) && bossHealth > 0)
                {
                    balasJugador.RemoveAt(i);
                    bossHealth -= 1; // Hacer Daño a la Tesis
                    flashBoss = 4;
                }
                else if (balaActual.X > pnlEscenario.Width || balaActual.X < -50)
                {
                    balasJugador.RemoveAt(i);
                }
            }

            // =========================
            // INTELIGENCIA DEL JEFE (FASE 1 - LA PAPA / EL TUTOR)
            // =========================
            if (bossHealth > 0)
            {
                if (bossState == 0) // Estado 0: Pausa
                {
                    bossAttackCooldown--;
                    if (bossAttackCooldown <= 0)
                    {
                        bossState = 1; // Inicia el Ataque
                        bossSpitCounter = 0;
                        bossSpitTimer = 0;
                    }
                }
                else if (bossState == 1) // Estado 1: Escupiendo la ráfaga
                {
                    bossSpitTimer--;
                    if (bossSpitTimer <= 0)
                    {
                        BalaTesis bolaTierra = new BalaTesis();
                        bolaTierra.X = boss.X;
                        bolaTierra.Y = groundY - 60; // Casi pegadas al suelo
                        bolaTierra.Tag = "boss_tierra";
                        balasBoss.Add(bolaTierra);

                        bossSpitCounter++;

                        if (bossSpitCounter >= 4) // Ya escupió las 4
                        {
                            bossState = 0; // Termina la ráfaga
                            bossAttackCooldown = 180;
                        }
                        else
                        {
                            // AJUSTE: Separación más grande entre cada bala (Antes 30, Ahora 55)
                            bossSpitTimer = 55;
                        }
                    }
                }

                for (int i = balasBoss.Count - 1; i >= 0; i--)
                {
                    BalaTesis bola = balasBoss[i];
                    // AJUSTE: Ahora viajan un poco más lento hacia la izquierda (Antes 12, Ahora 9)
                    bola.X -= 9;

                    Rectangle hitboxBalaBoss = new Rectangle(bola.X, bola.Y, 60, 60);

                    // Si la bola le da al jugador (y él no es inmune ni está en pleno Dash)
                    if (hitboxBalaBoss.IntersectsWith(player) && playerInvulnerability <= 0 && !isDashing)
                    {
                        playerHealth--;
                        playerInvulnerability = 100; // Inmunidad temporal por golpe
                        balasBoss.RemoveAt(i);

                        if (playerHealth <= 0)
                        {
                            tmrGameLoop.Stop();
                            MessageBox.Show("¡Llovieron demasiadas correcciones sobre tu Tesis!\n¡Game Over!", "REPROBADO");
                            this.Close();
                            return;
                        }
                    }
                    else if (bola.X < -150)
                    {
                        balasBoss.RemoveAt(i);
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

            // 1. EL CIELO
            e.Graphics.Clear(Color.FromArgb(20, 20, 30));

            // 2. EL SUELO 
            e.Graphics.FillRectangle(Brushes.DarkOliveGreen, 0, groundY, pnlEscenario.Width, pnlEscenario.Height - groundY);

            // 3. EL JEFE (TESIS / TUTOR)
            if (bossHealth > 0)
            {
                e.Graphics.FillRectangle(Brushes.DarkRed, boss);
                if (flashBoss > 0)
                {
                    e.Graphics.FillRectangle(pincelDestello, boss);
                }
                e.Graphics.DrawString("La Tesis HP: " + bossHealth, new Font("Arial", 16, FontStyle.Bold), Brushes.White, boss.X - 10, boss.Y - 30);
            }

            // 4. BALAS DEL JEFE (Bolas Gigantes)
            foreach (BalaTesis bola in balasBoss)
            {
                e.Graphics.FillEllipse(Brushes.SaddleBrown, bola.X, bola.Y, 60, 60);
            }

            // 5. EL JUGADOR 
            if (playerInvulnerability > 0 && (playerInvulnerability / 5) % 2 == 0)
            {
                // Dejamos en blanco para Efecto Parpadeo al recibir daño
            }
            else
            {
                Brush colorJugador = isDashing ? Brushes.Cyan : Brushes.Blue;
                e.Graphics.FillRectangle(colorJugador, player);
            }

            // 6. TUS DISPAROS 
            foreach (BalaTesis bala in balasJugador)
            {
                e.Graphics.FillEllipse(Brushes.Yellow, bala.X, bala.Y, 20, 10);
            }

            // 7. GUI (Vidas)
            e.Graphics.DrawString("Vidas Estudiante: " + playerHealth, new Font("Arial", 18, FontStyle.Bold), Brushes.LightPink, 20, 20);
        }
    }

    public class BalaTesis
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Tag { get; set; }
    }
}
