using JUEGO_INGENIERIA.Vistas;
using System.Drawing.Text;

namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
        private FormMovimiento motorMovimiento;
        public static Jugador JugadorActual { get; set; }
        private Jugador jugadorActual;

        private WMPLib.WindowsMediaPlayer musicaFondo;

        // Variables para la animación del rector
        private System.Windows.Forms.Timer timerRectorEstado;
        private System.Windows.Forms.Timer timerRectorMovimiento;
        private System.Windows.Forms.Timer timerRectorAnimacion;
        private Image[] rectorImages;
        private int rectorFrame = 1;
        private string rectorDirection = "centro";
        private bool isRectorWalking = false;
        private Point[] rectorPuntos;
        private Point rectorPuntoDestino;
        private int rectorSpeed = 2;
        private Queue<Point> rectorWaypoints = new Queue<Point>();
        private int avenidaY = 290; // Y de la calle principal (tierra horizontal)

        // Variables para la animación del geyser
        private System.Windows.Forms.Timer timerGeyzer;
        private bool isGeyzerFrame1 = true;
        private Image geyzer1;
        private Image geyzer2;

        // Esta variable guardará a qué nivel estamos intentando entrar (1 o 3)
        private int nivelSeleccionado = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            // --- OPTIMIZACIÓN EXTREMA DE FONDO (BYPASS STRETCH LAG) ---
            if (this.BackgroundImage != null)
            {
                this.BackgroundImageLayout = ImageLayout.None; // Apagamos el pesado recalculador estirado de Windows
                Bitmap fondoOptimizado = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (Graphics g = Graphics.FromImage(fondoOptimizado))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(this.BackgroundImage, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
                }
                this.BackgroundImage = fondoOptimizado;
            }

            musicaFondo = new WMPLib.WindowsMediaPlayer();

            // Pre-cachear los frames del geyzer en formato rápido
            geyzer1 = new Bitmap(Properties.Resources.geyzer1);
            geyzer2 = new Bitmap(Properties.Resources.geyzer2);

            // Pre-cachear los frames del rector
            rectorImages = new Image[8];
            rectorImages[0] = new Bitmap(Properties.Resources.rector_caminando_centro1);
            rectorImages[1] = new Bitmap(Properties.Resources.rector_caminando_centro2);
            rectorImages[2] = new Bitmap(Properties.Resources.rector_caminando_atras1);
            rectorImages[3] = new Bitmap(Properties.Resources.rector_caminando_atras2);
            rectorImages[4] = new Bitmap(Properties.Resources.rector_caminando_izquierda1);
            rectorImages[5] = new Bitmap(Properties.Resources.rector_caminando_izquierda2);
            rectorImages[6] = new Bitmap(Properties.Resources.rector_caminando_derecha1);
            rectorImages[7] = new Bitmap(Properties.Resources.rector_caminando_derecha2);

            // Definir puntos destino aleatorios leyendo tus PictureBox
            // ⚠ IMPORTANTE: Asegúrate de haber nombrado a tus PictureBox exactamente así en la ventana Propiedades y de haber Guardado el Formulario.
            rectorPuntos = new Point[] {
                pbDireccion1.Location,
                pbDireccion2.Location,
                pbDireccion3.Location,
                pbDireccion4.Location
            };

            // Setup timers del rector
            timerRectorEstado = new System.Windows.Forms.Timer() { Interval = 10000 };
            timerRectorEstado.Tick += TimerRectorEstado_Tick;
            timerRectorEstado.Start();

            timerRectorMovimiento = new System.Windows.Forms.Timer() { Interval = 30 };
            timerRectorMovimiento.Tick += TimerRectorMovimiento_Tick;

            timerRectorAnimacion = new System.Windows.Forms.Timer() { Interval = 250 };
            timerRectorAnimacion.Tick += TimerRectorAnimacion_Tick;
            timerRectorAnimacion.Start();

            // Desactivar completamente el PictureBox nativo del geyzer para que OnPaint tome el control total
            if (this.Controls.ContainsKey("geyzer"))
            {
                this.Controls["geyzer"].Visible = false;
                this.Controls["geyzer"].Enabled = false; // Evita que Windows Forms intente procesar eventos o redibujados internos
            }

            // Inicializar timer para animación del geyser
            timerGeyzer = new System.Windows.Forms.Timer();
            timerGeyzer.Interval = 300; // Ajusta este valor para hacer la animación más rápida o lenta
            timerGeyzer.Tick += TimerGeyzer_Tick;
            timerGeyzer.Start();

            // Ocultamos el panel universal por defecto
            pnlConfirmacionNivel1.Visible = false;

            pbPersonaje.Visible = false;
            EsconderMuros();

            EsconderMuros();

            // NUEVO: Ocultamos TODOS los obstáculos visuales "nativos" para que Windows no calcule su transparencia ni recortes super-lentos.
            // Las hitboxes seguirán vivas porque FormMovimiento evalúa los limites (.Bounds) lógicos, no visuales.
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox x && x != pbPersonaje)
                {
                    if ((x.Name.StartsWith("pictureBox") || x.Name == "rector") && x.BackColor == Color.Transparent)
                    {
                        // En lugar de matarlos, apagamos su dibujado nativo
                        x.Visible = false;
                    }
                }
            }

            NavegacionConsola.Configurar(this, btnSiNivel1, btnNoNivel1);
        }

        // --- SISTEMA DE DIBUJADO Y EFECTO DE CAMUFLAJE DE ÁRBOLES EN 3D (Z-Sort Puro) ---
        protected override void OnPaint(PaintEventArgs e)
        {
            // Nota Vital: Llamar a base.OnPaint dibuja el fondo (BackgroundImage).
            base.OnPaint(e);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            // 1. Recolectamos todos los PictureBoxes (Árboles, etc) que antes eran visibles
            List<PictureBox> objetosADibujar = new List<PictureBox>();
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox x && x != pbPersonaje)
                {
                    if ((string)x.Tag != "muro" && (x.Name.StartsWith("pictureBox") || x.Name == "rector") && x.Image != null)
                    {
                        objetosADibujar.Add(x);
                    }
                }
            }

            // 2. Separamos y ordenamos para el efecto 3D: Los que están "detrás" del jugador se dibujan antes.
            // Los que están "delante" del jugador se dibujan después de él para taparlo.
            List<PictureBox> capaFondo = new List<PictureBox>();
            List<PictureBox> capaFrente = new List<PictureBox>();

            foreach (var obj in objetosADibujar)
            {
                // Si la base inferior del árbol está más arriba que los pies del jugador, está "detrás"
                if (obj.Bottom <= pbPersonaje.Bottom)
                    capaFondo.Add(obj);
                else
                    capaFrente.Add(obj);
            }

            // 2.5 Lógica de profundidad para el Geyzer (Z-Order dinámico)
            bool geyzerEnFrente = false;
            Control g = this.Controls.ContainsKey("geyzer") ? this.Controls["geyzer"] : null;
            if (g != null && g.Bottom > (pbPersonaje.Bottom + 5))
                geyzerEnFrente = true;

            // 1. Dibujamos el Geyzer si está detrás de todo
            if (!geyzerEnFrente && g != null)
            {
                Image frameActual = isGeyzerFrame1 ? geyzer1 : geyzer2;
                if (frameActual != null) e.Graphics.DrawImage(frameActual, g.Left, g.Top, g.Width, g.Height);
            }

            // 2. Dibujamos Nivel de Fondo (Detrás del Jugador)
            foreach (var fondo in capaFondo)
            {
                e.Graphics.DrawImage(fondo.Image, fondo.Left, fondo.Top, fondo.Width, fondo.Height);
            }

            // 3. Dibujamos al Personaje en el medio
            if (motorMovimiento != null)
            {
                motorMovimiento.DibujarPersonaje(e.Graphics);
            }

            // 4. Dibujamos Nivel de Frente (Delante del Jugador)
            foreach (var frente in capaFrente)
            {
                e.Graphics.DrawImage(frente.Image, frente.Left, frente.Top, frente.Width, frente.Height);
            }

            // 5. Dibujamos el Geyzer si está delante de todo (tapa al jugador y a los objetos)
            if (geyzerEnFrente && g != null)
            {
                Image frameActual = isGeyzerFrame1 ? geyzer1 : geyzer2;
                if (frameActual != null) e.Graphics.DrawImage(frameActual, g.Left, g.Top, g.Width, g.Height);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();

            FormIntro intro = new FormIntro();
            intro.ShowDialog();

            FormAdmision registro = new FormAdmision();
            registro.ShowDialog();

            ElegirPersonaje seleccion = new ElegirPersonaje();
            seleccion.ShowDialog();

            jugadorActual = Form1.JugadorActual;

            motorMovimiento = new FormMovimiento(this, pbPersonaje);
            motorMovimiento.ColisionConObjeto += MotorMovimiento_ColisionConObjeto;
            motorMovimiento.Start();

            this.Show();
            this.Focus();

            ReproducirMusicaMapa();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(rutaFuente);
            Font fuentePixel = new Font(pfc.Families[0], 9f);
            Font fuentePanel = new Font(pfc.Families[0], 8f);

            lblNombreJugador.Font = fuentePixel;
            lblNivel.Font = fuentePixel;
            lblDinero.Font = fuentePixel;

            lblPreguntaNivel1.Font = fuentePanel;
            btnSiNivel1.Font = fuentePanel;
            btnNoNivel1.Font = fuentePanel;

            if (JugadorActual != null)
            {
                lblNombreJugador.Text = "Jugador: " + JugadorActual.Nombre;
                lblNivel.Text = "Nivel: " + JugadorActual.Nivel.ToString();
                lblDinero.Text = "Dinero: $" + JugadorActual.Billetera.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void TimerRectorEstado_Tick(object sender, EventArgs e)
        {
            if (!this.Controls.ContainsKey("rector")) return;
            Control rector = this.Controls["rector"];

            if (!isRectorWalking)
            {
                // Iniciar caminata
                Random rnd = new Random();
                Point target = rectorPuntos[rnd.Next(0, rectorPuntos.Length)];
                
                // Limpiar la cola de waypoints
                rectorWaypoints.Clear();

                // Busca dinámica de la avenida principal (si existe pbAvenida en tu diseño)
                int rutaY = avenidaY; // Default = 290
                Control[] avCtrls = this.Controls.Find("pbAvenida", true);
                if (avCtrls.Length > 0) rutaY = avCtrls[0].Top;

                // Ruteo Ortogonal (Solo Caminos de Tierra)
                // 1. Ir a la avenida horizontal desde la posición actual (bajar o subir)
                if (Math.Abs(rector.Top - rutaY) > rectorSpeed)
                    rectorWaypoints.Enqueue(new Point(rector.Left, rutaY));

                // 2. Moverse horizontalmente por la avenida hasta el X del target
                if (Math.Abs(target.X - rector.Left) > rectorSpeed)
                    rectorWaypoints.Enqueue(new Point(target.X, rutaY));

                // 3. Subir o bajar hacia el Y del target, saliendo de la avenida
                if (Math.Abs(target.Y - rutaY) > rectorSpeed)
                    rectorWaypoints.Enqueue(new Point(target.X, target.Y));

                if (rectorWaypoints.Count > 0)
                {
                    rectorPuntoDestino = rectorWaypoints.Dequeue();
                    isRectorWalking = true;
                    timerRectorMovimiento.Start();
                    timerRectorEstado.Stop(); // Parar timer de estado hasta llegar al destino final
                }
            }
        }

        private void TimerRectorMovimiento_Tick(object sender, EventArgs e)
        {
            if (!this.Controls.ContainsKey("rector")) return;
            Control rector = this.Controls["rector"];

            int dx = rectorPuntoDestino.X - rector.Left;
            int dy = rectorPuntoDestino.Y - rector.Top;

            if (Math.Abs(dx) == 0 && Math.Abs(dy) == 0)
            {
                // Llegó al waypoint actual
                if (rectorWaypoints.Count > 0)
                {
                    // Asignar siguiente waypoint
                    rectorPuntoDestino = rectorWaypoints.Dequeue();
                }
                else
                {
                    // Llegó al destino FINAL
                    isRectorWalking = false;
                    rectorDirection = "centro"; // Se queda mirando al frente
                    timerRectorMovimiento.Stop();
                    timerRectorEstado.Start(); // Iniciar cuenta de 10 seg de nuevo
                }
                return;
            }

            // Moverse estrictamente UN EJE A LA VEZ (Elimina el zigzag)
            if (Math.Abs(dx) > 0)
            {
                rector.Left += Math.Sign(dx) * Math.Min(rectorSpeed, Math.Abs(dx));
                rectorDirection = dx > 0 ? "derecha" : "izquierda";
            }
            else if (Math.Abs(dy) > 0)
            {
                rector.Top += Math.Sign(dy) * Math.Min(rectorSpeed, Math.Abs(dy));
                rectorDirection = dy > 0 ? "centro" : "atras"; // centro es hacia abajo
            }

            // Repintar el área anterior y nueva
            Rectangle moveArea = rector.Bounds;
            moveArea.Inflate(rectorSpeed * 4, rectorSpeed * 4);
            this.Invalidate(moveArea);
        }

        private void TimerRectorAnimacion_Tick(object sender, EventArgs e)
        {
            if (!this.Controls.ContainsKey("rector")) return;
            PictureBox rector = (PictureBox)this.Controls["rector"];

            rectorFrame = (rectorFrame == 1) ? 2 : 1;
            int baseIndex = 0;

            switch (rectorDirection)
            {
                case "centro": baseIndex = 0; break;
                case "atras": baseIndex = 2; break;
                case "izquierda": baseIndex = 4; break;
                case "derecha": baseIndex = 6; break;
            }

            rector.Image = rectorImages[baseIndex + (rectorFrame - 1)];

            // Invalidar para redibujar en el OnPaint
            this.Invalidate(rector.Bounds);
        }

        private void TimerGeyzer_Tick(object sender, EventArgs e)
        {
            isGeyzerFrame1 = !isGeyzerFrame1;

            if (this.Controls.ContainsKey("geyzer"))
            {
                Control geyzer = this.Controls["geyzer"];
                Rectangle areaGeyzer = geyzer.Bounds;

                // Evaluamos si el área de nuestro jugador choca o se superpone con el área de Geyzer
                if (pbPersonaje.Bounds.IntersectsWith(areaGeyzer))
                {
                    // Si se están tocando, unimos las dos áreas para redibujarlos a ambos limpiamente
                    Rectangle areaCombinada = Rectangle.Union(areaGeyzer, pbPersonaje.Bounds);
                    this.Invalidate(areaCombinada);
                }
                else
                {
                    // Si están lejos, solo redibujamos la zona de Geyzer (ahorra recursos)
                    this.Invalidate(areaGeyzer);
                }
            }
        }

        // --- MANEJO DE CHOQUES CON PUERTAS ---
        private void MotorMovimiento_ColisionConObjeto(object sender, Control x)
        {
            if (x.Name == "pbPuertaNivel1")
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;

                // Configuramos el panel universal para que hable del Nivel 1
                lblPreguntaNivel1.Text = "¿Estás listo para entrar a la clase del profesor Oswald (Nivel 1)?";
                nivelSeleccionado = 1;

                pnlConfirmacionNivel1.Visible = true;
                pnlConfirmacionNivel1.BringToFront();
            }
            else if (x.Name == "pbPuertaNivel3") // <--- COLISIÓN DE TU NUEVA PUERTA
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;

                // Configuramos EL MISMO panel universal para que ahora hable del Nivel 3
                lblPreguntaNivel1.Text = "¿Estás listo para entrar al Nivel 3?";
                nivelSeleccionado = 3;

                pnlConfirmacionNivel1.Visible = true;
                pnlConfirmacionNivel1.BringToFront();
            }
            else if (x.Name == "pbPuertaNivel2")
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;

                // Configuramos el panel universal para que hable del Nivel 2
                lblPreguntaNivel1.Text = "¿Estás listo para entrar al Nivel 2?";
                nivelSeleccionado = 2;

                pnlConfirmacionNivel1.Visible = true;
                pnlConfirmacionNivel1.BringToFront();
            }
            else if (x.Name == "pbPuertaNivel4")
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;

                lblPreguntaNivel1.Text = "¿Estás listo para entrar al Nivel 4?";
                nivelSeleccionado = 4;

                pnlConfirmacionNivel1.Visible = true;
                pnlConfirmacionNivel1.BringToFront();
            }
            else if (x.Name == "pbPuertaNivel5") // Decanato
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;
                musicaFondo.controls.stop();

                this.Hide();

                if (jugadorActual == null)
                {
                    jugadorActual = new Vistas.Jugador();
                    jugadorActual.Billetera = 0;
                    jugadorActual.Nivel = 1;
                    jugadorActual.Nombre = "Prueba";
                }

                FormDecanato decanato = new FormDecanato(jugadorActual);
                decanato.ShowDialog();

                this.Show();
                ReproducirMusicaMapa();

                this.Invalidate(pbPersonaje.Bounds);
                pbPersonaje.Top += 40;
                this.Invalidate(pbPersonaje.Bounds);

                motorMovimiento.Start();
                motorMovimiento.EstaPausado = false;
            }
        }

        // --- HACER INVISIBLES LOS MUROS ---
        private void EsconderMuros()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "muro" || x.Name == "pbPuertaNivel1" || x.Name == "pbPuertaNivel2" || x.Name == "pbPuertaNivel3" || x.Name == "pbPuertaNivel4")
                {
                    x.BackColor = Color.Transparent;
                }
            }
        }

        private void ReproducirMusicaMapa()
        {
            try
            {
                string rutaMusica = Path.Combine(Application.StartupPath, "Resources", "Musica Mapa", "musicaMapa.mp3");
                if (File.Exists(rutaMusica))
                {
                    musicaFondo.URL = rutaMusica;
                    musicaFondo.settings.setMode("loop", true);
                    musicaFondo.controls.play();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerRectorEstado != null) { timerRectorEstado.Stop(); timerRectorEstado.Dispose(); }
            if (timerRectorMovimiento != null) { timerRectorMovimiento.Stop(); timerRectorMovimiento.Dispose(); }
            if (timerRectorAnimacion != null) { timerRectorAnimacion.Stop(); timerRectorAnimacion.Dispose(); }

            if (timerGeyzer != null)
            {
                timerGeyzer.Stop();
                timerGeyzer.Dispose();
            }
            if (musicaFondo != null)
            {
                musicaFondo.controls.stop();
            }
        }

        private void pbPuertaNivel1_Click(object sender, EventArgs e)
        {
        }

        // --- BOTÓN SÍ UNIVERSAL ---
        private void btnSiNivel1_Click(object sender, EventArgs e)
        {
            // Ocultamos el panel
            pnlConfirmacionNivel1.Visible = false;
            musicaFondo.controls.stop();

            // Verificamos qué nivel estaba guardado en memoria cuando chocamos
            if (nivelSeleccionado == 1)
            {
                FormNivel1 nivel1 = new FormNivel1(jugadorActual);
                nivel1.ShowDialog();
            }
            else if (nivelSeleccionado == 3)
            {
                // Entramos al formulario Nivel3
                FormNivel3 nivel3 = new FormNivel3(jugadorActual);
                nivel3.ShowDialog();
            }
            else if (nivelSeleccionado == 2)
            {
                // Entramos al formulario Nivel2
                FormNivel2Juego nivel2 = new FormNivel2Juego();
                nivel2.ShowDialog();
            }
            else if (nivelSeleccionado == 4)
            {
                // Entramos al formulario Nivel 4 Inicio
                FormNivel4Inicio nivel4 = new FormNivel4Inicio();
                nivel4.ShowDialog();
            }

            // Al salir del respectivo nivel, reactivamos música y alejamos al pj de la puerta
            ReproducirMusicaMapa();

            this.Invalidate(pnlConfirmacionNivel1.Bounds);
            this.Invalidate(pbPersonaje.Bounds);
            pbPersonaje.Top += 40;
            this.Invalidate(pbPersonaje.Bounds);

            motorMovimiento.Start();
            motorMovimiento.EstaPausado = false;
        }

        // --- BOTÓN NO UNIVERSAL ---
        private void btnNoNivel1_Click(object sender, EventArgs e)
        {
            // Sin importar a cuál íbamos a entrar, simplemente quitamos el panel
            pnlConfirmacionNivel1.Visible = false;

            this.Invalidate(pnlConfirmacionNivel1.Bounds);
            this.Invalidate(pbPersonaje.Bounds);
            pbPersonaje.Top += 40;
            this.Invalidate(pbPersonaje.Bounds);

            motorMovimiento.Start();
            motorMovimiento.EstaPausado = false;
        }


    }
}