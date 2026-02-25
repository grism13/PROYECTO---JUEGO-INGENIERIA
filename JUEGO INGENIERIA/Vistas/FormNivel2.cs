using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Modelos;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel2 : Form
    {
        // --- VARIABLES DEL JUGADOR ---
        int jugadorX = 50;
        int jugadorY = 300;
        int velocidadJugador = 15;
        int tamañoJugador = 50;

        int vidasJugador = 3;
        int tiempoInmunidad = 0;

        List<ObjetoJuego> balasJugador = new List<ObjetoJuego>();
        int velocidadBala = 25;
        int cooldownDisparo = 0;

        bool moverArriba = false;
        bool moverAbajo = false;
        bool moverIzquierda = false;
        bool moverDerecha = false;
        bool disparando = false;

        // --- VARIABLES DEL JEFE (Profesor Marcel) ---
        int bossBaseX;
        int bossX;
        int bossY = 50;
        int tamañoBoss = 150;
        int vidaBoss = 1500;
        int velocidadBoss = 8;

        bool bossSube = false;
        bool bossAvanza = true;

        int flashBoss = 0;
        int cooldownAtaqueBoss = 50;
        List<ObjetoJuego> balasBoss = new List<ObjetoJuego>();

        Random rnd = new Random();

        public FormNivel2()
        {
            InitializeComponent();
        }

        private void FormNivel2_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;

            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            bossBaseX = pnlEscenario.Width - tamañoBoss - 50;
            bossX = bossBaseX;

            tmrGameLoop.Start();
        }

        private void FormNivel2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) moverArriba = true;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) moverAbajo = true;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) moverIzquierda = true;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) moverDerecha = true;
            if (e.KeyCode == Keys.Space) disparando = true;
        }

        private void FormNivel2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) moverArriba = false;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) moverAbajo = false;
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) moverIzquierda = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) moverDerecha = false;
            if (e.KeyCode == Keys.Space) disparando = false;
        }

        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // --- 0. CRONÓMETRO DE INMUNIDAD (I-Frames) ---
            if (tiempoInmunidad > 0)
            {
                tiempoInmunidad--;
            }

            // --- 1. MOVIMIENTO DEL JUGADOR ---
            if (moverArriba && jugadorY > 0) jugadorY -= velocidadJugador;
            if (moverAbajo && jugadorY < pnlEscenario.Height - tamañoJugador) jugadorY += velocidadJugador;
            if (moverIzquierda && jugadorX > 0) jugadorX -= velocidadJugador;
            if (moverDerecha && jugadorX < pnlEscenario.Width - tamañoJugador) jugadorX += velocidadJugador;

            // --- 2. DISPARO DEL JUGADOR ---
            if (cooldownDisparo > 0) cooldownDisparo--;

            if (disparando == true && cooldownDisparo <= 0)
            {
                ObjetoJuego nuevaBala = new ObjetoJuego();
                nuevaBala.X = jugadorX + tamañoJugador;
                nuevaBala.Y = jugadorY + (tamañoJugador / 2) - 5;
                nuevaBala.Tag = "bala_jugador";
                balasJugador.Add(nuevaBala);
                cooldownDisparo = 5;
            }

            for (int i = balasJugador.Count - 1; i >= 0; i--)
            {
                balasJugador[i].X += velocidadBala;
                if (balasJugador[i].X > pnlEscenario.Width) balasJugador.RemoveAt(i);
            }

            // --- 3. INTELIGENCIA DEL PROFESOR MARCEL ---
            if (vidaBoss > 0)
            {
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

                // Daño al Jefe
                Rectangle areaBoss = new Rectangle(bossX, bossY, tamañoBoss, tamañoBoss);
                for (int i = balasJugador.Count - 1; i >= 0; i--)
                {
                    Rectangle areaBala = new Rectangle(balasJugador[i].X, balasJugador[i].Y, 20, 10);
                    if (areaBala.IntersectsWith(areaBoss))
                    {
                        vidaBoss -= 100; // MODO PRUEBA
                        balasJugador.RemoveAt(i);
                        flashBoss = 3;

                        // CONDICIÓN DE VICTORIA
                        if (vidaBoss <= 0)
                        {
                            vidaBoss = 0;
                            tmrGameLoop.Stop();
                            pnlEscenario.Invalidate();
                            MessageBox.Show("¡Has derrotado al temible Profesor Marcel!\n¡Aprobaste Matemáticas 2 con éxito!", "¡NIVEL COMPLETADO!");
                            this.Close();
                            return;
                        }
                    }
                }

                // Ataques del Profesor
                if (cooldownAtaqueBoss > 0)
                {
                    cooldownAtaqueBoss--;
                }
                else
                {
                    int probabilidad = rnd.Next(0, 100);

                    if (vidaBoss > 1000) // FASE 1
                    {
                        ObjetoJuego balaMala = new ObjetoJuego();
                        balaMala.X = bossX;
                        balaMala.Y = bossY + (tamañoBoss / 2);

                        if (probabilidad < 85) balaMala.Tag = "bala_boss_recta";
                        else balaMala.Tag = "bala_boss_perseguidora";

                        balasBoss.Add(balaMala);
                        cooldownAtaqueBoss = 45;
                    }
                    else if (vidaBoss <= 1000 && vidaBoss > 500) // FASE 2
                    {
                        velocidadBoss = 12;

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
                        cooldownAtaqueBoss = 35;
                    }
                    else // FASE 3
                    {
                        // BALANCEO FASE 3: 
                        velocidadBoss = 15; // Un poco menos rápido que antes

                        ObjetoJuego balaMala = new ObjetoJuego();
                        balaMala.X = bossX;
                        balaMala.Y = bossY + rnd.Next(0, tamañoBoss);

                        if (probabilidad < 85) balaMala.Tag = "bala_boss_fase2_recta";
                        else if (probabilidad < 92) balaMala.Tag = "bala_boss_perseguidora";
                        else balaMala.Tag = "bala_boss_rebotona_sube";

                        balasBoss.Add(balaMala);
                        cooldownAtaqueBoss = 13; // Aumentamos la recarga para crear más "espacio" entre balas
                    }
                }
            }

            // --- 4. MOVER BALAS DEL PROFESOR Y DAÑO AL JUGADOR ---
            Rectangle hitboxJugador = new Rectangle(jugadorX, jugadorY, tamañoJugador, tamañoJugador);

            for (int i = balasBoss.Count - 1; i >= 0; i--)
            {
                balasBoss[i].X -= 15;

                if (balasBoss[i].Tag == "bala_boss_arriba") balasBoss[i].Y -= 5;
                if (balasBoss[i].Tag == "bala_boss_abajo") balasBoss[i].Y += 5;

                if (balasBoss[i].Tag == "bala_boss_perseguidora")
                {
                    balasBoss[i].X += 3;
                    if (balasBoss[i].Y < jugadorY) balasBoss[i].Y += 4;
                    else if (balasBoss[i].Y > jugadorY) balasBoss[i].Y -= 4;
                }

                if (balasBoss[i].Tag.StartsWith("bala_boss_rebotona"))
                {
                    balasBoss[i].X += 8;

                    if (balasBoss[i].Tag == "bala_boss_rebotona_sube")
                    {
                        balasBoss[i].Y -= 15;
                        if (balasBoss[i].Y <= 0) balasBoss[i].Tag = "bala_boss_rebotona_baja";
                    }
                    else if (balasBoss[i].Tag == "bala_boss_rebotona_baja")
                    {
                        balasBoss[i].Y += 15;
                        if (balasBoss[i].Y >= pnlEscenario.Height - 30) balasBoss[i].Tag = "bala_boss_rebotona_sube";
                    }
                }

                // DAÑO POR BALA
                Rectangle areaBalaMala = new Rectangle(balasBoss[i].X, balasBoss[i].Y, 20, 20);

                if (areaBalaMala.IntersectsWith(hitboxJugador))
                {
                    balasBoss.RemoveAt(i);

                    if (tiempoInmunidad <= 0)
                    {
                        vidasJugador--;
                        tiempoInmunidad = 100; // 2 SEGUNDOS I-FRAMES

                        if (vidasJugador <= 0)
                        {
                            tmrGameLoop.Stop();
                            pnlEscenario.Invalidate();
                            MessageBox.Show("Te has quedado sin vidas.\nEl Profesor Marcel te mandó a reparación.", "¡GAME OVER!");
                            this.Close();
                            return;
                        }
                    }
                    continue;
                }

                if (balasBoss[i].X < -50 || balasBoss[i].Y < -100 || balasBoss[i].Y > pnlEscenario.Height + 100)
                {
                    balasBoss.RemoveAt(i);
                }
            }

            // --- 5. DAÑO POR CHOCAR CONTRA MARCEL (NUEVO) ---
            if (vidaBoss > 0)
            {
                Rectangle hitboxBoss = new Rectangle(bossX, bossY, tamañoBoss, tamañoBoss);

                if (hitboxJugador.IntersectsWith(hitboxBoss) && tiempoInmunidad <= 0)
                {
                    vidasJugador--;
                    tiempoInmunidad = 100; // 2 SEGUNDOS I-FRAMES por chocar

                    if (vidasJugador <= 0)
                    {
                        tmrGameLoop.Stop();
                        pnlEscenario.Invalidate();
                        MessageBox.Show("Te has quedado sin vidas.\n¡Te estrellaste contra el Profesor Marcel!", "¡GAME OVER!");
                        this.Close();
                        return;
                    }
                }
            }

            pnlEscenario.Invalidate();
        }

        // --- EL PINTOR ---
        private void pnlEscenario_Paint(object sender, PaintEventArgs e)
        {
            // 1. Dibujar jugador (Con I-Frames)
            if (tiempoInmunidad > 0)
            {
                if (tiempoInmunidad % 10 > 4)
                {
                    e.Graphics.FillRectangle(Brushes.LightSkyBlue, jugadorX, jugadorY, tamañoJugador, tamañoJugador);
                }
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.DodgerBlue, jugadorX, jugadorY, tamañoJugador, tamañoJugador);
            }

            // 2. Dibujar vidas del jugador
            Font fuenteVidas = new Font("Arial", 16, FontStyle.Bold);
            e.Graphics.DrawString("Vidas: " + vidasJugador, fuenteVidas, Brushes.LimeGreen, 20, 20);

            // 3. Dibujar balas del jugador
            foreach (ObjetoJuego bala in balasJugador)
            {
                e.Graphics.FillRectangle(Brushes.Yellow, bala.X, bala.Y, 20, 10);
            }

            // 4. Dibujar al Profesor Marcel y su Vida
            if (vidaBoss > 0)
            {
                if (flashBoss > 0) e.Graphics.FillRectangle(Brushes.White, bossX, bossY, tamañoBoss, tamañoBoss);
                else e.Graphics.FillRectangle(Brushes.Crimson, bossX, bossY, tamañoBoss, tamañoBoss);

                Font fuenteVidaBoss = new Font("Arial", 16, FontStyle.Bold);
                e.Graphics.DrawString("HP Marcel: " + vidaBoss, fuenteVidaBoss, Brushes.White, bossX, bossY - 25);
            }

            // 5. Dibujar balas del Jefe
            foreach (ObjetoJuego balaMala in balasBoss)
            {
                e.Graphics.FillRectangle(Brushes.OrangeRed, balaMala.X, balaMala.Y, 20, 20);
            }
        }
    }
}