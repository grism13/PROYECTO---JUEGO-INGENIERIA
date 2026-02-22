using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Modelos; // Tu clase mágica

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel2 : Form
    {
        // --- VARIABLES DEL JUGADOR ---
        int jugadorX = 50;
        int jugadorY = 200;
        int velocidadJugador = 15;
        int tamañoJugador = 50;

        // --- VARIABLES DE BALAS JUGADOR ---
        List<ObjetoJuego> balasJugador = new List<ObjetoJuego>();
        int velocidadBala = 25;

        // --- VARIABLES DE DISPARO (ESTILO CUPHEAD) ---
        bool disparando = false;
        int cooldownDisparo = 0;

        // --- VARIABLES DEL JEFE ---
        int bossX;
        int bossY = 50;
        int tamañoBoss = 150;
        int vidaBoss = 1500;
        int velocidadBoss = 8;
        bool bossSube = false;

        // --- NUEVAS VARIABLES: DESTELLO Y ATAQUES ---
        int flashBoss = 0;
        int cooldownAtaqueBoss = 50;
        List<ObjetoJuego> balasBoss = new List<ObjetoJuego>();

        public FormNivel2()
        {
            InitializeComponent();
        }

        private void FormNivel2_Load(object sender, EventArgs e)
        {
            // Conectar el dibujo
            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);

            // Evitar lag
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            // Posicionar al jefe a la derecha al arrancar
            bossX = pnlEscenario.Width - tamañoBoss - 50;

            // ¡MUY IMPORTANTE! Encender el motor
            tmrGameLoop.Start();
        }

        // --- CONTROLES: PRESIONAR TECLA ---
        private void FormNivel2_KeyDown(object sender, KeyEventArgs e)
        {
            // Moverse
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && jugadorY > 0)
                jugadorY -= velocidadJugador;

            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && jugadorY < pnlEscenario.Height - tamañoJugador)
                jugadorY += velocidadJugador;

            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && jugadorX > 0)
                jugadorX -= velocidadJugador;

            if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && jugadorX < pnlEscenario.Width - tamañoJugador)
                jugadorX += velocidadJugador;

            // Activar el gatillo
            if (e.KeyCode == Keys.Space)
            {
                disparando = true;
            }

            pnlEscenario.Invalidate();
        }

        // --- CONTROLES: SOLTAR TECLA ---
        private void FormNivel2_KeyUp(object sender, KeyEventArgs e)
        {
            // Soltar el gatillo
            if (e.KeyCode == Keys.Space)
            {
                disparando = false;
            }
        }

        // --- MOTOR PRINCIPAL (Física e Inteligencia) ---
        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // 1. RELOJ DE RECARGA DEL JUGADOR
            if (cooldownDisparo > 0)
            {
                cooldownDisparo--;
            }

            // 2. CREAR BALA JUGADOR
            if (disparando == true && cooldownDisparo <= 0)
            {
                ObjetoJuego nuevaBala = new ObjetoJuego();
                nuevaBala.X = jugadorX + tamañoJugador;
                nuevaBala.Y = jugadorY + (tamañoJugador / 2) - 5;
                nuevaBala.Tag = "bala_jugador";

                balasJugador.Add(nuevaBala);

                cooldownDisparo = 5; // Esperar 100ms
            }

            // 3. MOVER LAS BALAS DEL JUGADOR
            for (int i = balasJugador.Count - 1; i >= 0; i--)
            {
                balasJugador[i].X += velocidadBala;

                if (balasJugador[i].X > pnlEscenario.Width)
                {
                    balasJugador.RemoveAt(i);
                }
            }

            // --- 4. INTELIGENCIA DEL BOSS ---
            if (vidaBoss > 0)
            {
                // A) Movimiento del Boss
                if (bossSube == true)
                {
                    bossY -= velocidadBoss;
                    if (bossY <= 0) bossSube = false;
                }
                else
                {
                    bossY += velocidadBoss;
                    if (bossY >= pnlEscenario.Height - tamañoBoss) bossSube = true;
                }

                // B) Reducir el destello blanco
                if (flashBoss > 0) flashBoss--;

                // C) Colisiones (Balas Jugador -> Boss)
                Rectangle areaBoss = new Rectangle(bossX, bossY, tamañoBoss, tamañoBoss);
                for (int i = balasJugador.Count - 1; i >= 0; i--)
                {
                    Rectangle areaBala = new Rectangle(balasJugador[i].X, balasJugador[i].Y, 20, 10);

                    if (areaBala.IntersectsWith(areaBoss))
                    {
                        vidaBoss -= 4; // Daño por impacto
                        balasJugador.RemoveAt(i); // Destruir la bala

                        flashBoss = 3; // Activar destello blanco

                        if (vidaBoss <= 0) vidaBoss = 0; // Evitar vida negativa
                    }
                }

                // D) El Jefe Dispara
                if (cooldownAtaqueBoss > 0)
                {
                    cooldownAtaqueBoss--;
                }
                else
                {
                    ObjetoJuego balaMala = new ObjetoJuego();
                    balaMala.X = bossX;
                    balaMala.Y = bossY + (tamañoBoss / 2);
                    balaMala.Tag = "bala_boss";

                    balasBoss.Add(balaMala);

                    cooldownAtaqueBoss = 50; // Recarga del jefe (1 segundo)
                }
            }

            // --- 5. MOVER BALAS DEL BOSS ---
            for (int i = balasBoss.Count - 1; i >= 0; i--)
            {
                balasBoss[i].X -= 15; // Las balas rojas van hacia la izquierda

                // Detectar si la bala roja toca al jugador
                Rectangle areaBalaMala = new Rectangle(balasBoss[i].X, balasBoss[i].Y, 20, 20);
                Rectangle areaJugador = new Rectangle(jugadorX, jugadorY, tamañoJugador, tamañoJugador);

                if (areaBalaMala.IntersectsWith(areaJugador))
                {
                    // AQUÍ AÑADIREMOS QUE PIERDES VIDA LUEGO
                    Console.WriteLine("¡TE DIERON!");
                    balasBoss.RemoveAt(i);
                    continue;
                }

                // Borrar balas rojas que salen de pantalla
                if (balasBoss[i].X < 0)
                {
                    balasBoss.RemoveAt(i);
                }
            }

            // 6. ACTUALIZAR PANTALLA
            pnlEscenario.Invalidate();
        }

        // --- EL PINTOR ---
        private void pnlEscenario_Paint(object sender, PaintEventArgs e)
        {
            // 1. Dibujar jugador
            e.Graphics.FillRectangle(Brushes.DodgerBlue, jugadorX, jugadorY, tamañoJugador, tamañoJugador);

            // 2. Dibujar balas del jugador
            foreach (ObjetoJuego bala in balasJugador)
            {
                e.Graphics.FillRectangle(Brushes.Yellow, bala.X, bala.Y, 20, 10);
            }

            // 3. Dibujar al Boss y su texto de Vida
            if (vidaBoss > 0)
            {
                if (flashBoss > 0)
                {
                    e.Graphics.FillRectangle(Brushes.White, bossX, bossY, tamañoBoss, tamañoBoss);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Crimson, bossX, bossY, tamañoBoss, tamañoBoss);
                }

                Font fuenteVida = new Font("Arial", 16, FontStyle.Bold);
                e.Graphics.DrawString("HP: " + vidaBoss, fuenteVida, Brushes.White, bossX, bossY - 25);
            }

            // 4. Dibujar balas del Boss
            foreach (ObjetoJuego balaMala in balasBoss)
            {
                e.Graphics.FillRectangle(Brushes.OrangeRed, balaMala.X, balaMala.Y, 20, 20);
            }
        }
    }
}