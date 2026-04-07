using JUEGO_INGENIERIA.Vistas;
using System.Drawing.Text;

namespace JUEGO_INGENIERIA
{
    public partial class Form1 : Form
    {
        private FormMovimiento motorMovimiento;
        public static Jugador JugadorActual { get; set; }
        private Jugador jugadorActual;
        public static bool SaltarIntroDirecto = false;

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
        private int avenidaY = 350; // Y de la calle principal (tierra horizontal)

        // Variables para la animación del geyser
        private System.Windows.Forms.Timer timerGeyzer;
        private bool isGeyzerFrame1 = true;
        private Image geyzer1;
        private Image geyzer2;

        // Variables para los postes
        private System.Windows.Forms.Timer timerPostes;
        private int estadoPostes = 0;

        // Variables para pibMantenimiento
        private System.Windows.Forms.Timer timerMantenimiento;
        private int mantenimientoDestino = 1;
        private Queue<Point> mantenimientoWaypoints = new Queue<Point>();
        private int mantenimientoSpeed = 2; // Misma velocidad del Rector
        private List<Image> mAnimAbajo = new List<Image>();
        private List<Image> mAnimArriba = new List<Image>();
        private List<Image> mAnimIzquierda = new List<Image>();
        private List<Image> mAnimDerecha = new List<Image>();
        
        // Variables de Lógica de Escalera y Reparación
        private int estadoMantenimiento = 0; // 0: Patrulla normal, 1: Buscando Escalera en la U, 2: Volviendo con escalera, 3: Regresando a la facultad con la escalera reparada (o simplemente vuelve a la normalidad)
        private int posteAReparar = 0;       // Guarda el Poste (1, 2, 3) que detonó la alarma
        private List<Image> mAnimEscaleraAbajo = new List<Image>();
        private List<Image> mAnimEscaleraArriba = new List<Image>();
        private List<Image> mAnimEscaleraIzquierda = new List<Image>();
        private List<Image> mAnimEscaleraDerecha = new List<Image>();
        private List<Image> mAnimArreglandoPoste = new List<Image>();
        
        private List<Image> mUltimaAnimacion;
        private int mFrameActual = 0;
        private int mPausaFrameArreglando = 0;
        private int mContadorLentitud = 0;
        private int mPausaMantenimiento = 0;
        private Queue<Point> mantenimientoDodgeWaypoints = new Queue<Point>();
        private Queue<Point> rectorDodgeWaypoints = new Queue<Point>();
        private int cooldownEncuentro = 0;
        private int encuentrosContador = 0;

        // Lista cacheada para optimizar OnPaint
        private List<PictureBox> objetosADibujarCache = new List<PictureBox>();

        // Esta variable guardará a qué nivel estamos intentando entrar (1 o 3)
        private int nivelSeleccionado = 0;
        
        private bool accesoDenegadoPorBilletera = false;

        // Aquí guardaremos la capa transparente de los árboles
        Image capaArboles;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();



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

            // Inicializar timer para animación de los postes
            timerPostes = new System.Windows.Forms.Timer();
            timerPostes.Interval = 40000; // 40 segundos
            timerPostes.Tick += TimerPostes_Tick;
            timerPostes.Start();

            // Inicializar animaciones de mantenimiento (Paso 1)
            mAnimAbajo.Add(Properties.Resources.mantenimiento_frente1);
            mAnimAbajo.Add(Properties.Resources.mantenimiento_frente2);
            mAnimArriba.Add(Properties.Resources.mantenimiento_espalda1);
            mAnimArriba.Add(Properties.Resources.mantenimiento_espalda2); 
            mAnimIzquierda.Add(Properties.Resources.mantenimiento_izquierda1);
            mAnimIzquierda.Add(Properties.Resources.mantenimiento_izquierda2);
            mAnimDerecha.Add(Properties.Resources.mantenimiento_derecha1);
            mAnimDerecha.Add(Properties.Resources.mantenimiento_derecha2);

            // Animaciones de mantenimiento con escalera
            mAnimEscaleraAbajo.Add(Properties.Resources.manteniemiento_centro_escalera1);
            mAnimEscaleraAbajo.Add(Properties.Resources.mantenimiento_centro_escalera2);
            mAnimEscaleraArriba.Add(Properties.Resources.mantenimiento_espalda_escalera1);
            mAnimEscaleraArriba.Add(Properties.Resources.mantenimiento_espalda_escalera2);
            mAnimEscaleraIzquierda.Add(Properties.Resources.mantenimiento_izquierda_escalera1);
            mAnimEscaleraIzquierda.Add(Properties.Resources.mantenimiento_izquierda_escalera2);
            mAnimEscaleraDerecha.Add(Properties.Resources.mantenimiento_derecha_escalera1);
            mAnimEscaleraDerecha.Add(Properties.Resources.mantenimiento_derecha_escalera2);

            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera1);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera2);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera3);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera4);

            // Subida de la escalera
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_subir_escalera1);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_subir_escalera2);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_subir_escalera3);

            // Mantenimiento en la cima de la escalera por 5 segundos exactos (24 iteraciones del Timer)
            for (int i = 0; i < 24; i++)
            {
                mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_subir_escalera4);
            }

            // Bajada de la escalera (en reversa)
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_subir_escalera3);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_subir_escalera2);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_subir_escalera1);

            // Guardar escalera (en reversa)
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera4);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera3);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera2);
            mAnimArreglandoPoste.Add(Properties.Resources.mantenimiento_colocar_escalera1);

            // Utilizamos el pibMantenimiento del Designer. Lo forzamos a nacer en el Poste 1
            mUltimaAnimacion = mAnimAbajo;
            pibMantenimiento.Visible = false; // Sera dibujado invisiblemente en el OnPaint (Z-Sort)
            pibMantenimiento.Location = new Point(190, 330);

            // Inicializamos destino actual en el Poste 1
            mantenimientoDestino = 1;

            timerMantenimiento = new System.Windows.Forms.Timer();
            timerMantenimiento.Interval = 30;
            timerMantenimiento.Tick += TimerMantenimiento_Tick;
            timerMantenimiento.Start();

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
                    // Excluimos geyzer (manual). Obligamos al rector y demás a ocultarse para Z-Sort.
                    if (x.Name != "geyzer" && x.BackColor == Color.Transparent && x.Image != null)
                    {
                        x.Visible = false;
                    }
                }
            }

            // Pre-cargamos la lista de objetos para evitar lag en cada OnPaint
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox x && x != pbPersonaje)
                {
                    // Excluimos geyzer. Todo lo demás (incluido el rector) entra al Z-Sort OnPaint.
                    if ((string)x.Tag != "muro" && x.Name != "geyzer" && x.Image != null)
                    {
                        objetosADibujarCache.Add(x);
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

            // 1. Usamos la lista cacheada en lugar de iterar por todos los controles de nuevo
            List<PictureBox> objetosADibujar = objetosADibujarCache;

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
                if (fondo.Image != null)
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
                if (frente.Image != null)
                    e.Graphics.DrawImage(frente.Image, frente.Left, frente.Top, frente.Width, frente.Height);
            }

            // 5. Dibujamos el Geyzer si está delante de todo (tapa al jugador y a los objetos)
            if (geyzerEnFrente && g != null)
            {
                Image frameActual = isGeyzerFrame1 ? geyzer1 : geyzer2;
                if (frameActual != null) e.Graphics.DrawImage(frameActual, g.Left, g.Top, g.Width, g.Height);
            }
            // === 6. NUEVO: DIBUJAMOS LOS ÁRBOLES DEL FRENTE ===
            // Al colocarlo de último, garantizamos que las hojas tapen al jugador, 
            // al rector y al personal de mantenimiento cuando pasen por debajo.
            if (capaArboles != null)
            {
                e.Graphics.DrawImage(capaArboles, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
        } // Fin del método OnPaint
        


        private void Form1_Shown(object sender, EventArgs e)
        {
            if (SaltarIntroDirecto)
            {
                ReproducirMusicaMapa();
                return;
            }
            this.Hide();

            // 1 y 2. Historia Inicial y Formulario de Admisión (Solapados)
            FormAdmision registro = new FormAdmision();
            
            // En cuanto el formulario de admisión termine de dibujarse (fondo), abrirá la Intro encima
            registro.Shown += (s, ev) => 
            {
                FormIntro intro = new FormIntro();
                intro.ShowDialog(registro); // ShowDialog bloquea la admisión y la pone de fondo hasta que termine
            };

            registro.ShowDialog();

            // PRE-CARGA DE PANTALLA: Instanciamos CargaDeJuegos y Selección al mismo tiempo
            // Esto evita que cuando se cierre la selección, haya un delay de milisegundos 
            // intentando construir el FormCargaDeJuegos (lo que causaba ver el escritorio).
            FormCargaDeJuegos carga = new FormCargaDeJuegos();
            carga.TopMost = true;

            // 3. Selección de Personaje (Abre el Iris al entrar, la seleccion cierra de golpe al continuar)
            ElegirPersonaje seleccion = new ElegirPersonaje();
            IrisTransitions.Transicion(seleccion, null, false);

            // En cuenta cierra, mandamos el Show ultra-rápido de la pantalla ya pre-cargada
            carga.Show();
            carga.Update(); // Forzamos que se dibuje instantáneamente sobre el fondo negro

            // Ocultamos el iris negro LUEGO de mostrar la pantalla de carga, 
            IrisTransitions.OcultarSinc();

            // Mostramos silenciosamente el mapa por detrás de la pantalla de carga
            this.Show();
            this.Update();

            // Esperamos 2.5 segundos para la carga visual
            System.Windows.Forms.Timer tCarga = new System.Windows.Forms.Timer();
            tCarga.Interval = 2500;
            tCarga.Tick += (s2, e2) => {
                tCarga.Stop();
                carga.Close();
                
                // Finalmente damos control al jugador
                this.Focus();
                ReproducirMusicaMapa();
            };
            tCarga.Start();

            // Inicialización del motor de juego (Mapa)
            jugadorActual = Form1.JugadorActual;
            motorMovimiento = new FormMovimiento(this, pbPersonaje);
            motorMovimiento.ColisionConObjeto += MotorMovimiento_ColisionConObjeto;
            motorMovimiento.Start();
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
            capaArboles = Properties.Resources.fondoCapaArboles;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            // --- OPTIMIZACIÓN EXTREMA DE FONDO (BYPASS STRETCH LAG) ---
            if (this.BackgroundImage != null)
            {
                this.BackgroundImageLayout = ImageLayout.None; // Apagamos el pesado recalculador estirado de Windows
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                Bitmap fondoOptimizado = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (Graphics g = Graphics.FromImage(fondoOptimizado))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(this.BackgroundImage, 0, 0, screenWidth, screenHeight);
                }
                this.BackgroundImage = fondoOptimizado;
            }
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
                if (rectorDodgeWaypoints.Count > 0)
                {
                    rectorPuntoDestino = rectorDodgeWaypoints.Dequeue();
                    return;
                }

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

            int oldX = rector.Left;
            int oldY = rector.Top;

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

            // Repintar limpia y exactamente las áreas afectada por el rector (sin inflar tanto)
            Rectangle oldArea = new Rectangle(oldX, oldY, rector.Width, rector.Height);
            Rectangle newArea = rector.Bounds;
            this.Invalidate(oldArea);
            this.Invalidate(newArea);
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

        private void TimerMantenimiento_Tick(object sender, EventArgs e)
        {
            if (pibMantenimiento == null) return;

            if (mPausaMantenimiento > 0)
            {
                mPausaMantenimiento--;
                
                // Animar colocar_escalera si está arreglando (estado 2)
                if (estadoMantenimiento == 2)
                {
                    mContadorLentitud++;
                    if (mContadorLentitud > 6)
                    {
                        mPausaFrameArreglando++;
                        if (mPausaFrameArreglando >= mAnimArreglandoPoste.Count) mPausaFrameArreglando = 0;
                        pibMantenimiento.Image = mAnimArreglandoPoste[mPausaFrameArreglando];
                        mContadorLentitud = 0;
                        this.Invalidate(pibMantenimiento.Bounds);
                    }
                }

                if (mPausaMantenimiento > 0) return; // Sigue esperando en el sitio
                
                // Ya pasaron 5 segundos, reanudar y recalcular la ruta.
                mPausaMantenimiento = -1; // -1 significa que acaba de terminar la pausa y puede moverse libremente
            }
            else if (mantenimientoWaypoints.Count > 0)
            {
                // Solo animar marcos normales si está moviéndose y no estaba en pausa
                mContadorLentitud++;
                if (mContadorLentitud > 6)
                {
                    mFrameActual++;
                    if (mFrameActual >= mUltimaAnimacion.Count) mFrameActual = 0;
                    pibMantenimiento.Image = mUltimaAnimacion[mFrameActual];
                    mContadorLentitud = 0;
                    this.Invalidate(pibMantenimiento.Bounds);
                }
            }

            if (mantenimientoWaypoints.Count == 0)
            {
                if (mPausaMantenimiento == 0) 
                {
                    // --- LLEGAMOS AL DESTINO ---
                    if (estadoMantenimiento == 0)
                    {
                        // ESTADO 0: Llegamos a un poste durante patrulla.
                        bool posteRoto = false;
                        if (mantenimientoDestino == 1) { Control[] p = this.Controls.Find("pbPoste1", true); if(p.Length > 0 && p[0].Tag != null && p[0].Tag.ToString() == "malo") posteRoto = true; }
                        else if (mantenimientoDestino == 2) { Control[] p = this.Controls.Find("pbPoste2", true); if(p.Length > 0 && p[0].Tag != null && p[0].Tag.ToString() == "malo") posteRoto = true; }
                        else if (mantenimientoDestino == 3) { Control[] p = this.Controls.Find("pbPoste3", true); if(p.Length > 0 && p[0].Tag != null && p[0].Tag.ToString() == "malo") posteRoto = true; }
                        
                        if (posteRoto)
                        {
                            // Al detectar poste roto, cambiamos Inmediatamente a Buscar Escalera
                            estadoMantenimiento = 1; 
                            posteAReparar = mantenimientoDestino;
                            mPausaMantenimiento = -1; // Sin pausa, sigue de largo en el tick
                        }
                        else
                        {
                            // Poste SANO. Pausa 5s normal.
                            mPausaMantenimiento = 166; 
                            if (mantenimientoDestino == 1 || mantenimientoDestino == 2) pibMantenimiento.Image = Properties.Resources.mantenimiento_izquierda2;
                            else if (mantenimientoDestino == 3) pibMantenimiento.Image = Properties.Resources.mantenimiento_derecha2;
                            this.Invalidate(pibMantenimiento.Bounds);
                            return;
                        }
                    }
                    else if (estadoMantenimiento == 1)
                    {
                        // ESTADO 1: Llegó al punto SUR del mapa. Esperar 5s buscando la escalera.
                        mPausaMantenimiento = 166; 
                        pibMantenimiento.Image = Properties.Resources.mantenimiento_frente2;
                        this.Invalidate(pibMantenimiento.Bounds);
                        return;
                    }
                    else if (estadoMantenimiento == 2)
                    {
                        // ESTADO 2: Llegó de vuelta al poste con la escalera.
                        mPausaMantenimiento = 266; // Tiempo exacto: 38 frames a mostrar (Desplegar, estar arriba 5s y bajar) 
                        mPausaFrameArreglando = 0; // Iniciar loop desde cero
                        
                        // Forzamos directamente poner el primer frame donde empieza a sacar la escalera
                        pibMantenimiento.Image = mAnimArreglandoPoste[0];
                        this.Invalidate(pibMantenimiento.Bounds);
                        return;
                    }
                }
                
                // --- FIN DE PAUSA (mPausa = -1) ---
                mPausaMantenimiento = 0;
                int origen = mantenimientoDestino;
                
                if (estadoMantenimiento == 0)
                {
                    // Patrulla normal: ir a otro poste al azar
                    Random rnd = new Random();
                    int nextDest = rnd.Next(1, 4);
                    while(nextDest == origen && origen != 0) nextDest = rnd.Next(1, 4);
                    mantenimientoDestino = nextDest;
                }
                else if (estadoMantenimiento == 1)
                {
                    // ¿Acaba de convertirse en 1 por detectar el poste roto? Entonces va al SUR (destino 4).
                    // ¿O acaba de terminar de esperar en el SUR y tiene que volver al poste?
                    // Mmmm, si `origen` es un POSTE, significa que ACABA de encontrarlo roto. Va al sur.
                    // Si `origen` es 4 (SUR), significa que terminó de esperar en el sur. Va a reparar.
                    if (origen != 4)
                    {
                        // Acaba de encontrarlo. Va al Sur.
                        mantenimientoDestino = 4;
                    }
                    else
                    {
                        // Terminó 5s en el Sur. Sube al poste.
                        estadoMantenimiento = 2;
                        mantenimientoDestino = posteAReparar;
                    }
                }
                else if (estadoMantenimiento == 2)
                {
                    // Terminó de reparar el poste. Vuelve a patrulla normal
                    estadoMantenimiento = 0; 
                    
                    // Restaurar visualmente el poste a "bueno" y limpiar Tag
                    PictureBox pbRoto = null;
                    if (posteAReparar == 1) { Control[] p = this.Controls.Find("pbPoste1", true); if(p.Length>0) pbRoto = p[0] as PictureBox; if(pbRoto!=null) { pbRoto.Image = Properties.Resources.poste_luz; pbRoto.Tag = ""; } }
                    else if (posteAReparar == 2) { Control[] p = this.Controls.Find("pbPoste2", true); if(p.Length>0) pbRoto = p[0] as PictureBox; if(pbRoto!=null) { pbRoto.Image = Properties.Resources.poste_luz; pbRoto.Tag = ""; } }
                    else if (posteAReparar == 3) { Control[] p = this.Controls.Find("pbPoste3", true); if(p.Length>0) pbRoto = p[0] as PictureBox; if(pbRoto!=null) { pbRoto.Image = Properties.Resources.poste_luz2; pbRoto.Tag = ""; } }
                    if (pbRoto != null) this.Invalidate(pbRoto.Bounds);
                    
                    // Elegimos otro poste.
                    Random rnd = new Random();
                    int nextDest = rnd.Next(1, 4);
                    while(nextDest == posteAReparar) nextDest = rnd.Next(1, 4);
                    mantenimientoDestino = nextDest;
                }

                // --- GENERACION DE RUTA ---
                Control[] c1 = this.Controls.Find("pb1", true);
                Control[] c2 = this.Controls.Find("pb2", true);
                Control[] c3 = this.Controls.Find("pb3", true);
                // Ya no usamos pbDireccion3, usamos el punto rojo en la zona sur inferior central
                int mWidth = pibMantenimiento.Width;
                int mHeight = pibMantenimiento.Height;

                Point P1_Pole = c1.Length > 0 ? new Point(c1[0].Left + (c1[0].Width/2) - (mWidth/2), c1[0].Bottom - mHeight) : new Point(200, 320);
                Point P2_Pole = c2.Length > 0 ? new Point(c2[0].Left + (c2[0].Width/2) - (mWidth/2), c2[0].Bottom - mHeight) : new Point(200, 90);
                Point P3_Pole = c3.Length > 0 ? new Point(c3[0].Left + (c3[0].Width/2) - (mWidth/2), c3[0].Bottom - mHeight) : new Point(1090, 230);

                int EJE_X_IZQ = P2_Pole.X; 
                int EJE_Y_MAIN_IZQ = 340;  
                int EJE_X_SUBIDA = 600;    
                int EJE_Y_MAIN_DER = 340;  
                int EJE_X_DER = 900;       

                // El destino es el sur desde el eje de subida (Hacia el punto rojo de tu imagen)
                Point P_UNIV = new Point(EJE_X_SUBIDA, 850); 
                int EJE_X_UNIV = P_UNIV.X;

                Point CruceIzq = new Point(EJE_X_IZQ, EJE_Y_MAIN_IZQ);
                Point CruceMid_Abajo = new Point(EJE_X_SUBIDA, EJE_Y_MAIN_IZQ);
                Point CruceMid_Arriba = new Point(EJE_X_SUBIDA, EJE_Y_MAIN_DER);
                Point CruceDer = new Point(EJE_X_DER, EJE_Y_MAIN_DER);
                Point CruceUniv = new Point(EJE_X_UNIV, EJE_Y_MAIN_IZQ);

                // 1. ESCAPAR DEL ORIGEN a la calle primaria correspondiente
                if (origen == 1) {
                    mantenimientoWaypoints.Enqueue(new Point(EJE_X_IZQ, P1_Pole.Y));
                    mantenimientoWaypoints.Enqueue(CruceIzq); 
                } else if (origen == 2) {
                    mantenimientoWaypoints.Enqueue(CruceIzq); 
                } else if (origen == 3) {
                    mantenimientoWaypoints.Enqueue(new Point(EJE_X_DER, P3_Pole.Y));
                    mantenimientoWaypoints.Enqueue(CruceDer); 
                } else if (origen == 4) {
                    mantenimientoWaypoints.Enqueue(CruceUniv);
                }

                // 2. CONEXIÓN
                if (origen == 3 && (mantenimientoDestino == 1 || mantenimientoDestino == 2 || mantenimientoDestino == 4)) {
                    mantenimientoWaypoints.Enqueue(CruceMid_Arriba);
                    mantenimientoWaypoints.Enqueue(CruceMid_Abajo);
                }
                if (mantenimientoDestino == 3 && (origen == 1 || origen == 2 || origen == 4)) {
                    mantenimientoWaypoints.Enqueue(CruceMid_Abajo);
                    mantenimientoWaypoints.Enqueue(CruceMid_Arriba);
                    mantenimientoWaypoints.Enqueue(CruceDer);
                }
                if (mantenimientoDestino == 1 && (origen == 3 || origen == 4)) {
                    mantenimientoWaypoints.Enqueue(CruceIzq);
                }
                if (mantenimientoDestino == 2 && (origen == 3 || origen == 4)) {
                    mantenimientoWaypoints.Enqueue(CruceIzq);
                }
                if (mantenimientoDestino == 4 && (origen == 1 || origen == 2)) {
                    mantenimientoWaypoints.Enqueue(CruceUniv);
                }

                // 3. ATERRIZAJE DIRECTO EN EL DESTINO DESDE SU EJE
                if (mantenimientoDestino == 1) {
                    mantenimientoWaypoints.Enqueue(new Point(EJE_X_IZQ, P1_Pole.Y));
                    mantenimientoWaypoints.Enqueue(P1_Pole);
                } else if (mantenimientoDestino == 2) {
                    mantenimientoWaypoints.Enqueue(P2_Pole); 
                } else if (mantenimientoDestino == 3) {
                    mantenimientoWaypoints.Enqueue(new Point(EJE_X_DER, P3_Pole.Y));
                    mantenimientoWaypoints.Enqueue(P3_Pole);
                } else if (mantenimientoDestino == 4) {
                    mantenimientoWaypoints.Enqueue(CruceUniv);
                    mantenimientoWaypoints.Enqueue(P_UNIV);
                }
                return;
            }

            if (mantenimientoDodgeWaypoints.Count == 0 && rectorDodgeWaypoints.Count == 0 && this.Controls.ContainsKey("rector"))
            {
                if (cooldownEncuentro > 0) cooldownEncuentro--;
                if (cooldownEncuentro <= 0)
                {
                    Control rector = this.Controls["rector"];
                    int distSq = (rector.Left - pibMantenimiento.Left) * (rector.Left - pibMantenimiento.Left) + 
                                 (rector.Top - pibMantenimiento.Top) * (rector.Top - pibMantenimiento.Top);
                    if (distSq < 4225) // aprox 65 píxeles de detección
                    {
                        encuentrosContador++;

                        if (!isRectorWalking || encuentrosContador % 2 != 0)
                        {
                            // MANTENIMIENTO ESQUIVA (IMPAR O RECTOR DETENIDO)
                            Point objM = mantenimientoWaypoints.Peek();
                            int dirX = Math.Sign(objM.X - pibMantenimiento.Left);
                            int dirY = Math.Sign(objM.Y - pibMantenimiento.Top);

                            if (Math.Abs(dirX) > 0)
                            {
                                Rectangle rectArriba = new Rectangle(pibMantenimiento.Left, pibMantenimiento.Top - 50, pibMantenimiento.Width, pibMantenimiento.Height);
                                bool arribaLibre = true;
                                Rectangle rectAbajo = new Rectangle(pibMantenimiento.Left, pibMantenimiento.Top + 50, pibMantenimiento.Width, pibMantenimiento.Height);
                                bool abajoLibre = true;

                                foreach (Control c in this.Controls) {
                                    if (c is PictureBox pb && pb.Tag != null && pb.Tag.ToString() == "muro") {
                                        if (pb.Bounds.IntersectsWith(rectArriba)) arribaLibre = false;
                                        if (pb.Bounds.IntersectsWith(rectAbajo)) abajoLibre = false;
                                    }
                                }

                                if (arribaLibre) {
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left, pibMantenimiento.Top - 50));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left + (dirX * 80), pibMantenimiento.Top - 50));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left + (dirX * 80), pibMantenimiento.Top));
                                    cooldownEncuentro = 150;
                                } else if (abajoLibre) {
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left, pibMantenimiento.Top + 50));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left + (dirX * 80), pibMantenimiento.Top + 50));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left + (dirX * 80), pibMantenimiento.Top));
                                    cooldownEncuentro = 150;
                                } else {
                                    cooldownEncuentro = 10;
                                    return;
                                }
                            }
                            else if (Math.Abs(dirY) > 0)
                            {
                                Rectangle rectDcha = new Rectangle(pibMantenimiento.Left + 50, pibMantenimiento.Top, pibMantenimiento.Width, pibMantenimiento.Height);
                                bool dchaLibre = true;
                                Rectangle rectIzq = new Rectangle(pibMantenimiento.Left - 50, pibMantenimiento.Top, pibMantenimiento.Width, pibMantenimiento.Height);
                                bool izqLibre = true;

                                foreach (Control c in this.Controls) {
                                    if (c is PictureBox pb && pb.Tag != null && pb.Tag.ToString() == "muro") {
                                        if (pb.Bounds.IntersectsWith(rectDcha)) dchaLibre = false;
                                        if (pb.Bounds.IntersectsWith(rectIzq)) izqLibre = false;
                                    }
                                }

                                if (dchaLibre) {
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left + 50, pibMantenimiento.Top));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left + 50, pibMantenimiento.Top + (dirY * 80)));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left, pibMantenimiento.Top + (dirY * 80)));
                                    cooldownEncuentro = 150;
                                } else if (izqLibre) {
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left - 50, pibMantenimiento.Top));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left - 50, pibMantenimiento.Top + (dirY * 80)));
                                    mantenimientoDodgeWaypoints.Enqueue(new Point(pibMantenimiento.Left, pibMantenimiento.Top + (dirY * 80)));
                                    cooldownEncuentro = 150;
                                } else {
                                    cooldownEncuentro = 10;
                                    return;
                                }
                            }
                        }
                        else
                        {
                            // RECTOR ESQUIVA (PAR)
                            int dirX = Math.Sign(rectorPuntoDestino.X - rector.Left);
                            int dirY = Math.Sign(rectorPuntoDestino.Y - rector.Top);

                            if (Math.Abs(dirX) > 0)
                            {
                                Rectangle rectArriba = new Rectangle(rector.Left, rector.Top - 50, rector.Width, rector.Height);
                                bool arribaLibre = true;
                                Rectangle rectAbajo = new Rectangle(rector.Left, rector.Top + 50, rector.Width, rector.Height);
                                bool abajoLibre = true;

                                foreach (Control c in this.Controls) {
                                    if (c is PictureBox pb && pb.Tag != null && pb.Tag.ToString() == "muro") {
                                        if (pb.Bounds.IntersectsWith(rectArriba)) arribaLibre = false;
                                        if (pb.Bounds.IntersectsWith(rectAbajo)) abajoLibre = false;
                                    }
                                }

                                if (arribaLibre) {
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left, rector.Top - 50));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left + (dirX * 80), rector.Top - 50));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left + (dirX * 80), rector.Top));
                                    rectorDodgeWaypoints.Enqueue(rectorPuntoDestino);
                                    rectorPuntoDestino = rectorDodgeWaypoints.Dequeue();
                                    cooldownEncuentro = 150;
                                } else if (abajoLibre) {
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left, rector.Top + 50));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left + (dirX * 80), rector.Top + 50));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left + (dirX * 80), rector.Top));
                                    rectorDodgeWaypoints.Enqueue(rectorPuntoDestino);
                                    rectorPuntoDestino = rectorDodgeWaypoints.Dequeue();
                                    cooldownEncuentro = 150;
                                } else {
                                    cooldownEncuentro = 10;
                                    return; // Rector no puede, Mantenimiento pausa en este tick para evitar avance.
                                }
                            }
                            else if (Math.Abs(dirY) > 0)
                            {
                                Rectangle rectDcha = new Rectangle(rector.Left + 50, rector.Top, rector.Width, rector.Height);
                                bool dchaLibre = true;
                                Rectangle rectIzq = new Rectangle(rector.Left - 50, rector.Top, rector.Width, rector.Height);
                                bool izqLibre = true;

                                foreach (Control c in this.Controls) {
                                    if (c is PictureBox pb && pb.Tag != null && pb.Tag.ToString() == "muro") {
                                        if (pb.Bounds.IntersectsWith(rectDcha)) dchaLibre = false;
                                        if (pb.Bounds.IntersectsWith(rectIzq)) izqLibre = false;
                                    }
                                }

                                if (dchaLibre) {
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left + 50, rector.Top));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left + 50, rector.Top + (dirY * 80)));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left, rector.Top + (dirY * 80)));
                                    rectorDodgeWaypoints.Enqueue(rectorPuntoDestino);
                                    rectorPuntoDestino = rectorDodgeWaypoints.Dequeue();
                                    cooldownEncuentro = 150;
                                } else if (izqLibre) {
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left - 50, rector.Top));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left - 50, rector.Top + (dirY * 80)));
                                    rectorDodgeWaypoints.Enqueue(new Point(rector.Left, rector.Top + (dirY * 80)));
                                    rectorDodgeWaypoints.Enqueue(rectorPuntoDestino);
                                    rectorPuntoDestino = rectorDodgeWaypoints.Dequeue();
                                    cooldownEncuentro = 150;
                                } else {
                                    cooldownEncuentro = 10;
                                    return; // pausa
                                }
                            }
                        }
                    }
                }
            }

            Point objetivo = mantenimientoDodgeWaypoints.Count > 0 ? mantenimientoDodgeWaypoints.Peek() : mantenimientoWaypoints.Peek();
            int oldX = pibMantenimiento.Left;
            int oldY = pibMantenimiento.Top;
            
            int dx = objetivo.X - oldX;
            int dy = objetivo.Y - oldY;

            if (Math.Abs(dx) <= mantenimientoSpeed && Math.Abs(dy) <= mantenimientoSpeed)
            {
                pibMantenimiento.Location = objetivo;
                if (mantenimientoDodgeWaypoints.Count > 0)
                    mantenimientoDodgeWaypoints.Dequeue();
                else
                    mantenimientoWaypoints.Dequeue();
            }
            else
            {
                // Mover primero en X o primero en Y dependiendo de la ruta:
                // Para cruces, como es un camino, nos movemos puramente en una direccion a la vez hacia el waypoint.
                if (Math.Abs(dx) > 0)
                {
                    int step = Math.Min(mantenimientoSpeed, Math.Abs(dx));
                    pibMantenimiento.Left += Math.Sign(dx) * step;
                    List<Image> targetAnimX = dx > 0 ? 
                        (estadoMantenimiento == 2 ? mAnimEscaleraDerecha : mAnimDerecha) : 
                        (estadoMantenimiento == 2 ? mAnimEscaleraIzquierda : mAnimIzquierda);

                    if (mUltimaAnimacion != targetAnimX)
                    {
                        mUltimaAnimacion = targetAnimX;
                        mFrameActual = 0;
                    }
                }
                else if (Math.Abs(dy) > 0)
                {
                    int step = Math.Min(mantenimientoSpeed, Math.Abs(dy));
                    pibMantenimiento.Top += Math.Sign(dy) * step;
                    List<Image> targetAnimY = dy > 0 ? 
                        (estadoMantenimiento == 2 ? mAnimEscaleraAbajo : mAnimAbajo) : 
                        (estadoMantenimiento == 2 ? mAnimEscaleraArriba : mAnimArriba);

                    if (mUltimaAnimacion != targetAnimY)
                    {
                        mUltimaAnimacion = targetAnimY;
                        mFrameActual = 0;
                    }
                }
            }
            
            Rectangle oldArea = new Rectangle(oldX, oldY, pibMantenimiento.Width, pibMantenimiento.Height);
            Rectangle newArea = pibMantenimiento.Bounds;
            this.Invalidate(oldArea);
            this.Invalidate(newArea);
        }

        private void TimerPostes_Tick(object sender, EventArgs e)
        {
            estadoPostes++;

            if (estadoPostes == 1)
            {
                Control[] postes = this.Controls.Find("pbPoste1", true);
                if (postes.Length > 0 && postes[0] is PictureBox pb)
                {
                    pb.Image = Properties.Resources.poste_luz_malo;
                    pb.Tag = "malo";
                    this.Invalidate(pb.Bounds);
                }
            }
            else if (estadoPostes == 2)
            {
                Control[] postes = this.Controls.Find("pbPoste3", true);
                if (postes.Length > 0 && postes[0] is PictureBox pb)
                {
                    pb.Image = Properties.Resources.poste_luz_malo2;
                    pb.Tag = "malo";
                    this.Invalidate(pb.Bounds);
                }
            }
            else if (estadoPostes >= 3)
            {
                Control[] postes = this.Controls.Find("pbPoste2", true);
                if (postes.Length > 0 && postes[0] is PictureBox pb)
                {
                    pb.Image = Properties.Resources.poste_luz_malo;
                    pb.Tag = "malo";
                    this.Invalidate(pb.Bounds);
                }
                
                // Reiniciamos el ciclo para que los postes se sigan rompiendo eternamente
                estadoPostes = 0;
            }
        }

        // --- MANEJO DE CHOQUES CON PUERTAS ---
        private void MotorMovimiento_ColisionConObjeto(object sender, Control x)
        {
            if (x.Name == "pbPuertaNivel1" || x.Name == "pbPuertaNivel2" || x.Name == "pbPuertaNivel3" || x.Name == "pbPuertaNivel4")
            {
                accesoDenegadoPorBilletera = false;
                if (jugadorActual != null && jugadorActual.Billetera < 100)
                {
                    accesoDenegadoPorBilletera = true;
                }

                if (accesoDenegadoPorBilletera)
                {
                    motorMovimiento.Stop();
                    motorMovimiento.EstaPausado = true;
                    MessageBox.Show("No tienes suficiente dinero para acceder. Necesitas $100.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    this.Invalidate(pbPersonaje.Bounds);
                    pbPersonaje.Top += 40;
                    this.Invalidate(pbPersonaje.Bounds);
                    
                    motorMovimiento.Start();
                    motorMovimiento.EstaPausado = false;
                    return;
                }
            }

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
                
                // NO OCULTAMOS EL MAPA PRINCIPAL, PORQUE SINO SE VE EL ESCRITORIO
                // this.Hide();

                if (jugadorActual == null)
                {
                    jugadorActual = new Vistas.Jugador();
                    jugadorActual.Billetera = 0;
                    jugadorActual.Nivel = 0;
                    jugadorActual.Nombre = "Prueba";
                }

                FormDecanato decanato = new FormDecanato(jugadorActual);
                
                // USAMOS LA NUEVA TRANSICION ESTILO CUPHEAD (Iris)
                IrisTransitions.Transicion(decanato);

                // NO HACE FALTA MOSTRAR EL MAPA PRINCIPAL PORQUE NUNCA LO OCULTAMOS
                // this.Show();
                ReproducirMusicaMapa();

                this.Invalidate(pbPersonaje.Bounds);
                pbPersonaje.Top += 40;
                this.Invalidate(pbPersonaje.Bounds);

                motorMovimiento.Start();
                motorMovimiento.EstaPausado = false;
            }
            else if (x.Name == "pnFinal")
            {
                motorMovimiento.Stop();
                motorMovimiento.EstaPausado = true;
                musicaFondo.controls.stop();

                FormFinal formFinal = new FormFinal();
                IrisTransitions.Transicion(formFinal);

                string videoPath = System.IO.Path.Combine(Application.StartupPath, "Resources", "creditos.mp4");
                if (System.IO.File.Exists(videoPath))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = videoPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("No se encontró el video de los créditos en: " + videoPath, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Application.Exit();
            }
        }

        // --- HACER INVISIBLES LOS MUROS ---
        private void EsconderMuros()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "muro" || x.Name == "pbPuertaNivel1" || x.Name == "pbPuertaNivel2" || x.Name == "pbPuertaNivel3" || x.Name == "pbPuertaNivel4" || x.Name == "pnFinal")
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
            if (timerPostes != null)
            {
                timerPostes.Stop();
                timerPostes.Dispose();
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

            if (nivelSeleccionado == 1)
            {
                FormCargaDeJuegos carga = new FormCargaDeJuegos(() => new FormNivel1(jugadorActual));
                carga.ShowDialog();
            }
            else if (nivelSeleccionado == 3)
            {
                FormCargaDeJuegos carga = new FormCargaDeJuegos(() => new FormNivel3(jugadorActual));
                carga.ShowDialog();
            }
            else if (nivelSeleccionado == 2)
            {
                FormCargaDeJuegos carga = new FormCargaDeJuegos(() => new FormNivel2Juego(jugadorActual));
                carga.ShowDialog();
            }
            else if (nivelSeleccionado == 4)
            {
                FormCargaDeJuegos carga = new FormCargaDeJuegos(() => new FormNivel4_Final(jugadorActual));
                carga.ShowDialog();
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