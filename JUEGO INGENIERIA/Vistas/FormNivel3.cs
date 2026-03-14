using JUEGO_INGENIERIA.Modelos;
using JUEGO_INGENIERIA.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormNivel3 : Form
    {
        // --- IMAGEN DE FONDO Y VARIABLES SEAMLESS (SCROLL INFINITO) ---
        // --- IMÁGENES DE FONDO POR FASES ---
        Image fondoFase1;
        Image fondoFase2;
        Image fondoFase3;
        Image fondoActual; // Este será el que actúe como "pantalla" cambiando según la fase
        int fondoX = 0;
        int velocidadFondo = 7;


        // --- VARIABLES DEL JUGador ---
        FormMovimiento movimiento;
        PictureBox pbJugador;
        int tamañoJugador = 150;
        int vidasJugador = 3;
        int tiempoInmunidad = 0;
        List<ObjetoJuego> balasJugador = new List<ObjetoJuego>();
        int velocidadBala = 25; // ¡Disparos a la velocidad de la luz originales!
        int cooldownDisparo = 0;
        int danoJugador = 10;
        bool disparando = false;
        bool modoConcentrado = false;
        // --- Animación Concentrado ---
        int targetTamañoJugador = 150;
        // --- NUEVO: IMÁGENES DE ESTADO DE VIDA DEL JUGADOR ---
        Image imgVidaFull;  // Imagen con 3 corazones
        Image imgVidaMedia; // Imagen con 2 corazones
        Image imgVidaBaja;  // Imagen con 1 corazón


        // --- VARIABLES DEL JEFE (Profesor Marcel) ---
        int bossBaseX;
        int bossX;
        int bossY = 50;
        int tamañoBoss = 180;
        int vidaBoss = 1500;
        int velocidadBoss = 8; // Velocidad original restaurada
        bool bossSube = false;
        bool bossAvanza = true;
        int flashBoss = 0;
        int cooldownAtaqueBoss = 50;
        List<ObjetoJuego> balasBoss = new List<ObjetoJuego>();
        Random rnd = new Random();
        // --- ANIMACIONES DEL JEFE MARCEL ---
        private Image[] framesFase1;
        private Image[] framesFase2;
        private Image[] framesFase3;
        private Image imagenActualBoss; 
        private int frameBossActual = 0; 
        private int contadorAnimacionBoss = 0; 
        private int velocidadAnimacionBoss = 6; // A menor número, más rápido aletea/se mueve Marcel


        // --- ASYNC KEYBOARD INPUT ---
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);
        public FormNivel3()
        {
            InitializeComponent();
        }
        private void FormNivel2_Load(object sender, EventArgs e)
        {

            this.ClientSize = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            fondoFase1 = new Bitmap(Properties.Resources.fondoFase1, 1280, 720);
            fondoFase2 = new Bitmap(Properties.Resources.fondoFase2, 1280, 720);
            fondoFase3 = new Bitmap(Properties.Resources.fondoFase3, 1280, 720);

            // Le decimos al juego que arranque mostrando el fondo de la Fase 1
            fondoActual = fondoFase1;

            // --- CARGAR ANIMACIONES DE MARCEL (4 frames por fase) ---
            framesFase1 = new Image[] {
                Properties.Resources.MarcelF1_1,
                Properties.Resources.MarcelF1_2,
                Properties.Resources.MarcelF1_3,
                Properties.Resources.MarcelF1_4
            };

            framesFase2 = new Image[] {
                Properties.Resources.MarcelF2_1,
                Properties.Resources.MarcelF2_2,
                Properties.Resources.MarcelF2_3,
                Properties.Resources.MarcelF2_4
            };

            framesFase3 = new Image[] {
                Properties.Resources.MarcelF3_1,
                Properties.Resources.MarcelF3_2,
                Properties.Resources.MarcelF3_3,
                
            };
            imagenActualBoss = framesFase1[0]; // Arranca con la primera imagen

            // --- CARGAR IMÁGENES DE VIDA DEL JUGADOR ---
            imgVidaFull = Properties.Resources.vida_3;   // Reemplaza con tus nombres reales
            imgVidaMedia = Properties.Resources.vida_2; // Reemplaza con tus nombres reales
            imgVidaBaja = Properties.Resources.vida_1;   // Reemplaza con tus nombres reales

            // --- INYECCIÓN DE 60 FPS ---
            tmrGameLoop.Interval = 16;
            pnlEscenario.Paint += new PaintEventHandler(pnlEscenario_Paint);
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, pnlEscenario, new object[] { true });

            // INICIALIZACIÓN DE pbJugador Y FormMovimiento
            pbJugador = new PictureBox();
            pbJugador.Size = new Size(tamañoJugador, tamañoJugador);
            pbJugador.Location = new Point(200, 200);
            pnlEscenario.Controls.Add(pbJugador); // Necesario para que pertenezca al form
            movimiento = new FormMovimiento(this, pbJugador, true); // true = esNivelEspacial
            movimiento.Start();
            bossBaseX = pnlEscenario.Width - tamañoBoss - 50;
            bossX = bossBaseX;
            tmrGameLoop.Start();
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_KEYMENU = 0xF100;
            // Bloquea que la tecla Alt active el menú de la ventana, permitiendo movimiento de flechas libre
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt32() & 0xFFF0) == SC_KEYMENU)
            {
                return;
            }
            base.WndProc(ref m);
        }
        // --- SE ELIMINARON LOS KEYDOWN/KEYUP A FAVOR DEL GETASYNCKEYSTATE EN EL TICK ---
        private void FormNivel2_KeyDown(object sender, KeyEventArgs e) { }
        private void FormNivel2_KeyUp(object sender, KeyEventArgs e) { }
        private void tmrGameLoop_Tick(object sender, EventArgs e)
        {
            // --- EVALUACIÓN DE TECLADO ASÍNCRONO ---
            disparando = (GetAsyncKeyState(Keys.Space) & 0x8000) != 0;
            bool altPresionado = (GetAsyncKeyState(Keys.Menu) & 0x8000) != 0 || (GetAsyncKeyState(Keys.Alt) & 0x8000) != 0;
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
            // --- SCROLL INFINITO DEL FONDO (SEAMLESS) ---
            fondoX -= velocidadFondo;
            if (fondoX <= -pnlEscenario.Width)
            {
                fondoX = 0;
            }
            // --- 0. CRONÓMETRO DE INMUNIDAD (I-Frames) Y ANIMACIÓN ---
            if (tiempoInmunidad > 0)
            {
                tiempoInmunidad--;
            }
            // --- Animación de Reducción/Aumento de la Nave ---
            if (tamañoJugador != targetTamañoJugador)
            {
                int velocidadAnimacion = 15; // Modifica esto para hacerlo más rápido o lento
                int nuevoTamaño = targetTamañoJugador < tamañoJugador ? tamañoJugador - velocidadAnimacion : tamañoJugador + velocidadAnimacion;
                // Asegurar que no rebase el tamaño exacto del objetivo
                if (targetTamañoJugador < tamañoJugador && nuevoTamaño < targetTamañoJugador) nuevoTamaño = targetTamañoJugador;
                if (targetTamañoJugador > tamañoJugador && nuevoTamaño > targetTamañoJugador) nuevoTamaño = targetTamañoJugador;
                int diferencia = tamañoJugador - nuevoTamaño;
                tamañoJugador = nuevoTamaño;
                pbJugador.Size = new Size(tamañoJugador, tamañoJugador);
                pbJugador.Left += diferencia / 2; // Mantener la nave centrada
                pbJugador.Top += diferencia / 2;
            }
            // --- 1. MOVIMIENTO DEL JUGADOR ---
            // Manejado automáticamente por FormMovimiento.
            // Restringimos bordes (opcional si Move no lo hace tan exacto, pero no interfiere)
            if (pbJugador.Left < 0) pbJugador.Left = 0;
            if (pbJugador.Top < 0) pbJugador.Top = 0;
            if (pbJugador.Right > pnlEscenario.Width) pbJugador.Left = pnlEscenario.Width - pbJugador.Width;
            if (pbJugador.Bottom > pnlEscenario.Height) pbJugador.Top = pnlEscenario.Height - pbJugador.Height;
            // --- 2. DISPARO DEL JUGADOR ---
            if (cooldownDisparo > 0) cooldownDisparo--;
            // Solo puede disparar si no está en modo concentrado (Alt)
            if (disparando == true && cooldownDisparo <= 0 && !modoConcentrado)
            {
                ObjetoJuego nuevaBala = new ObjetoJuego();
                nuevaBala.X = pbJugador.Left + tamañoJugador;
                nuevaBala.Y = pbJugador.Top + (tamañoJugador / 2) - 5;
                nuevaBala.Tag = "bala_jugador";
                balasJugador.Add(nuevaBala);
                cooldownDisparo = 6; // Ajuste fino para que sea metralleta pero no sature
            }
            for (int i = balasJugador.Count - 1; i >= 0; i--)
            {
                balasJugador[i].X += velocidadBala;
                if (balasJugador[i].X > pnlEscenario.Width) balasJugador.RemoveAt(i);
            }
            // --- 3. INTELIGENCIA DEL PROFESOR MARCEL ---
            if (vidaBoss > 0)
            {
                // --- MOTOR DE ANIMACIÓN DE MARCEL ---
                contadorAnimacionBoss++;
                if (contadorAnimacionBoss >= velocidadAnimacionBoss)
                {
                    frameBossActual++;
                    contadorAnimacionBoss = 0; // Reiniciamos el relojito

                    // Asignamos el dibujo correcto según la fase de vida
                    if (vidaBoss > 1000) // FASE 1
                    {
                        if (frameBossActual >= framesFase1.Length) frameBossActual = 0;
                        imagenActualBoss = framesFase1[frameBossActual];
                    }
                    else if (vidaBoss <= 1000 && vidaBoss > 500) // FASE 2
                    {
                        if (frameBossActual >= framesFase2.Length) frameBossActual = 0;
                        imagenActualBoss = framesFase2[frameBossActual];
                    }
                    else // FASE 3
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
                // Daño al Jefe
                Rectangle areaBoss = new Rectangle(bossX, bossY, tamañoBoss, tamañoBoss);
                for (int i = balasJugador.Count - 1; i >= 0; i--)
                {
                    Rectangle areaBala = new Rectangle(balasJugador[i].X, balasJugador[i].Y, 20, 10);
                    if (areaBala.IntersectsWith(areaBoss))
                    {
                        vidaBoss -= danoJugador; // DAÑO VARIABLE SEGUN EL MODO
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
                        fondoActual = fondoFase1;
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
                        fondoActual = fondoFase2;
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
                        fondoActual = fondoFase3;
                        velocidadBoss = 15;
                        ObjetoJuego balaMala = new ObjetoJuego();
                        balaMala.X = bossX;
                        balaMala.Y = bossY + rnd.Next(0, tamañoBoss);
                        if (probabilidad < 85) balaMala.Tag = "bala_boss_fase2_recta";
                        else if (probabilidad < 92) balaMala.Tag = "bala_boss_perseguidora";
                        else balaMala.Tag = "bala_boss_rebotona_sube";
                        balasBoss.Add(balaMala);
                        cooldownAtaqueBoss = 25; // Aumentado de 13 a 25 para evitar saturación de balas en GDI+
                    }
                }
            }
            // --- 4. MOVER BALAS DEL PROFESOR Y DAÑO AL JUGADOR ---
            Rectangle hitboxJugador = new Rectangle(pbJugador.Left, pbJugador.Top, tamañoJugador, tamañoJugador);

            // ¡Velocidades originales de balas restauradas!
            for (int i = balasBoss.Count - 1; i >= 0; i--)
            {
                balasBoss[i].X -= 15;
                if (balasBoss[i].Tag == "bala_boss_arriba") balasBoss[i].Y -= 5;
                if (balasBoss[i].Tag == "bala_boss_abajo") balasBoss[i].Y += 5;
                if (balasBoss[i].Tag == "bala_boss_perseguidora")
                {
                    balasBoss[i].X += 3;
                    if (balasBoss[i].Y < pbJugador.Top) balasBoss[i].Y += 4;
                    else if (balasBoss[i].Y > pbJugador.Top) balasBoss[i].Y -= 4;
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
                        tiempoInmunidad = 100; // I-FRAMES
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
                if (balasBoss[i].X < -50 || balasBoss[i].X > pnlEscenario.Width + 100 || balasBoss[i].Y < -100 || balasBoss[i].Y > pnlEscenario.Height + 100)
                {
                    balasBoss.RemoveAt(i);
                }
            }
            // --- 5. DAÑO POR CHOCAR CONTRA MARCEL ---
            if (vidaBoss > 0)
            {
                Rectangle hitboxBoss = new Rectangle(bossX, bossY, tamañoBoss, tamañoBoss);
                if (hitboxJugador.IntersectsWith(hitboxBoss) && tiempoInmunidad <= 0)
                {
                    vidasJugador--;
                    tiempoInmunidad = 100; // I-FRAMES por chocar
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
            // --- 0. DIBUJAR EL FONDO EN MOVIMIENTO (SEAMLESS) ---
            if (fondoActual != null)
            {
                // Optimización crítica: Solo dibujamos la parte visible del fondo, sin escalar pesadamente
                e.Graphics.DrawImageUnscaled(fondoActual, fondoX, 0);
                e.Graphics.DrawImageUnscaled(fondoActual, fondoX + fondoActual.Width, 0);
            }
            else
            {
                e.Graphics.Clear(Color.FromArgb(20, 20, 30));
            }
            // 1. Dibujar jugador (Con I-Frames)
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
            
           
            // --- DIBUJAR VIDAS DEL JUGADOR (Asset Swapping) ---
            // 1. Creamos una variable temporal para guardar la imagen que vamos a decidir mostrar
            Image imagenAVisualizar = null;

            // 2. Decidimos matemáticamente cuál imagen usar según las vidas actuales
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

            // 3. Si encontramos una imagen válida para ese estado, la dibujamos
            if (imagenAVisualizar != null)
            {
                // Dibuja la imagen elegida en la esquina superior izquierda
                // (imagen, posición X, posición Y, ancho, alto)
                // Ajusta el ancho (120) y alto (40) al tamaño real de tu asset para que no se vea estirado
                e.Graphics.DrawImage(imagenAVisualizar, 20, 20, 120, 40);
            }
            // 3. Dibujar balas del jugador
            foreach (ObjetoJuego bala in balasJugador)
            {
                e.Graphics.FillRectangle(Brushes.Yellow, bala.X, bala.Y, 20, 10);
            }

            // 4. Dibujar al Profesor Marcel y su Vida
            if (vidaBoss > 0)
            {
                // 1. Dibujamos la imagen animada del profesor
                if (imagenActualBoss != null)
                {
                    e.Graphics.DrawImage(imagenActualBoss, bossX, bossY, tamañoBoss, tamañoBoss);
                }

                // 2. Si recibió daño, le dibujamos un cuadrado blanco semitransparente encima para el destello
                if (flashBoss > 0)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, Color.White)), bossX, bossY, tamañoBoss, tamañoBoss);
                }

                // 3. El texto de su vida
                Font fuenteVidaBoss = new Font("Arial", 16, FontStyle.Bold);
                e.Graphics.DrawString("HP Marcel: " + vidaBoss, fuenteVidaBoss, Brushes.White, bossX, bossY - 25);

                foreach (ObjetoJuego balaMala in balasBoss)
                {
                    e.Graphics.FillRectangle(Brushes.OrangeRed, balaMala.X, balaMala.Y, 20, 20);
                }
            }
        }
    }
}