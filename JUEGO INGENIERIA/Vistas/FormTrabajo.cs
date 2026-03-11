using JUEGO_INGENIERIA.Modelos; // Importante para usar la clase Jugador
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Drawing.Text;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormTrabajo : Form
    {
        // --- VARIABLE DEL JUGADOR ---
        private Jugador jugadorActual;

        private FormMovimiento movimientoJugador;

        // --- VARIABLES DEL MINIJUEGO ---
        private int tiempoRestante = 30;
        private int documentos = 0;
        private int documentosEntregados = 0;
        private int mesaIndicada = 0;
        private Random generadorAleatorio = new Random();

        // --- CACHÉ DE RENDERIZADO OPTIMIZADO ---
        private Dictionary<Control, Image> cacheImagenes = new Dictionary<Control, Image>();
        private List<Control> objetosZOrder = new List<Control>();

        // Recibimos al jugador en el constructor igual que en FormNivel1
        public FormTrabajo(Jugador jugadorRecibido)
        {
            InitializeComponent();
            AplicarFuente();
            this.DoubleBuffered = true;

            // --- LÍNEAS NUEVAS AGREGADAS ---
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            this.KeyPreview = true;
            // -------------------------------

            // Guardamos los datos del jugador
            this.jugadorActual = jugadorRecibido;

            this.Load += FormTrabajo_Load;
        }

        private void FormTrabajo_Load(object sender, EventArgs e)
        {
            // Ocultar objetos y pre-escalar imágenes para dibujarlos MUCHO más rápido manualmente
            List<Control> objetosEstaticos = new List<Control>
            {
                pbMesa1, pbMesa2, pbMesa3, pbMesa4, pbGenerador, pictureBox5, pictureBox6
            };

            foreach (var control in objetosEstaticos)
            {
                control.Visible = false; // Lo ocultamos para que WinForms no lo dibuje solo
                PictureBox pb = (PictureBox)control;
                Image imgOriginal = pb.Image ?? pb.BackgroundImage;
                if (imgOriginal != null)
                {
                    // Crear un bitmap pre-escalado al tamaño exacto del PictureBox
                    Bitmap bmpEscalado = new Bitmap(pb.Width, pb.Height);
                    using (Graphics g = Graphics.FromImage(bmpEscalado))
                    {
                        // Asegura que las imágenes no se vean borrosas
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                        // Si la imagen original usa BackgroundImageLayout.Stretch, aquí esto replica ese comportamiento
                        g.DrawImage(imgOriginal, 0, 0, pb.Width, pb.Height);
                    }
                    // Guardamos la imagen pre-escalonada en caché
                    cacheImagenes[pb] = bmpEscalado;
                }
            }

            // Lista maestra con todos los objetos, incluyendo el personaje
            objetosZOrder.AddRange(objetosEstaticos);
            objetosZOrder.Add(pbPersonaje);

            movimientoJugador = new FormMovimiento(this, pbPersonaje);
            movimientoJugador.ColisionConObjeto += MovimientoJugador_ColisionConObjeto;
            movimientoJugador.Start();

            timer1.Start();

            AsignarNuevaMesa();
            ActualizarMarcadores();
        }

        private void tmrJuego_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            ActualizarMarcadores();

            if (tiempoRestante <= 0)
            {
                timer1.Stop();
                movimientoJugador.Stop();

                // --- LÓGICA DE GANANCIAS ---
                int dineroGanado = documentosEntregados * 10;
                jugadorActual.Billetera += dineroGanado; // Sumamos el dinero al jugador

                // Guardamos en el JSON
                ActualizarDatos();

                MessageBox.Show($"¡Se acabó el turno!\nEntregaste {documentosEntregados} documentos.\n\n¡Has ganado ${dineroGanado}!", "Fin del Turno", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }

        private void AsignarNuevaMesa()
        {
            mesaIndicada = generadorAleatorio.Next(1, 5);

            if (lblMesaDestino != null)
            {
                lblMesaDestino.Text = $"¡Lleva el documento a la MESA {mesaIndicada}!";
            }
        }

        private void MovimientoJugador_ColisionConObjeto(object sender, Control objetoTocado)
        {
            if (objetoTocado.Name == "pbGenerador")
            {
                if (documentos == 0)
                {
                    documentos = 1;
                    ActualizarMarcadores();
                }
            }

            string nombreMesaCorrecta = "pbMesa" + mesaIndicada;

            if (objetoTocado.Name == nombreMesaCorrecta)
            {
                if (documentos == 1)
                {
                    documentos = 0;
                    documentosEntregados++;
                    AsignarNuevaMesa();
                    ActualizarMarcadores();
                }
            }
        }

        private void ActualizarMarcadores()
        {
            if (lblTiempo != null) lblTiempo.Text = $"Tiempo: {tiempoRestante}s";
            if (lblDocumentos != null) lblDocumentos.Text = $"En mano: {documentos}";
            if (lblEntregados != null) lblEntregados.Text = $"Entregados: {documentosEntregados}";
        }

        private void FormTrabajo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tiempoRestante > 0 && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                MessageBox.Show("¡No puedes abandonar tu puesto hasta que termine el turno!", "¡A trabajar!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Mejorar calidad del dibujo
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            // Ordenar todos los objetos por su coordenada Y (Top)
            // Esto hace que el personaje pueda pasar por delante o por detrás de las mesas/NPCs
            objetosZOrder.Sort((a, b) => a.Top.CompareTo(b.Top));

            foreach (var obj in objetosZOrder)
            {
                // Solo dibujar el objeto si intersecta con el área que se está actualizando (ClipRectangle)
                // Esto previene redibujar el formulario completo inútilmente y gana MUCHA velocidad.
                if (e.ClipRectangle.IntersectsWith(obj.Bounds))
                {
                    if (obj == pbPersonaje)
                    {
                        if (movimientoJugador != null)
                            movimientoJugador.DibujarPersonaje(e.Graphics);
                    }
                    else if (cacheImagenes.ContainsKey(obj))
                    {
                        // Se dibuja la imagen sin escalar porque ya la pre-escalamos al iniciar el Form
                        e.Graphics.DrawImageUnscaled(cacheImagenes[obj], obj.Location);
                    }
                }
            }
        }

        // --- SISTEMA DE GUARDADO (Idéntico a FormNivel1) ---
        private void ActualizarDatos()
        {
            string rutaArchivo = "jugadores.json";
            List<Jugador> listaDeJugadores = new List<Jugador>();

            try
            {
                // 1. Leemos el archivo si existe
                if (File.Exists(rutaArchivo))
                {
                    string TextoJson = File.ReadAllText(rutaArchivo);
                    listaDeJugadores = JsonSerializer.Deserialize<List<Jugador>>(TextoJson) ?? new List<Jugador>();
                }

                // 2. Buscamos al jugador en la lista (por ID o por Nombre para que las pruebas funcionen)
                bool jugadorEncontrado = false;
                for (int i = 0; i < listaDeJugadores.Count; i++)
                {
                    if (listaDeJugadores[i].IdJugador == jugadorActual.IdJugador || listaDeJugadores[i].Nombre == jugadorActual.Nombre)
                    {
                        // Actualizamos los datos
                        listaDeJugadores[i].Nivel = jugadorActual.Nivel;
                        listaDeJugadores[i].Billetera = jugadorActual.Billetera;
                        jugadorEncontrado = true;
                        break;
                    }
                }

                // 3. Si el jugador es nuevo (como el de prueba) y no estaba en el JSON, lo agregamos
                if (!jugadorEncontrado)
                {
                    listaDeJugadores.Add(jugadorActual);
                }

                // 4. Guardamos los cambios
                var opciones = new JsonSerializerOptions { WriteIndented = true };
                string nuevoJson = JsonSerializer.Serialize(listaDeJugadores, opciones);
                File.WriteAllText(rutaArchivo, nuevoJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en el JSON: " + ex.Message, "Error de Sistema");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            ActualizarMarcadores();

            if (tiempoRestante <= 0)
            {
                timer1.Stop();
                movimientoJugador.Stop();

                // --- LÓGICA DE GANANCIAS ---
                int dineroGanado = documentosEntregados * 10;
                jugadorActual.Billetera += dineroGanado; // Sumamos el dinero al jugador

                // Guardamos en el JSON
                ActualizarDatos();

                MessageBox.Show($"¡Se acabó el turno!\nEntregaste {documentosEntregados} documentos.\n\n¡Has ganado ${dineroGanado}!", "Fin del Turno", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }
        private void AplicarFuente()
        {
            try
            {
                string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");
                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile(rutaFuente);

                Font fuenteRetro = new Font(pfc.Families[0], 11f);

                lblTiempo.Font = fuenteRetro;
                lblDocumentos.Font = fuenteRetro;
                lblEntregados.Font = fuenteRetro;
                lblMesaDestino.Font = fuenteRetro;
                label1.Font = fuenteRetro;
                label2.Font = fuenteRetro;
                label3.Font = fuenteRetro;
                label4.Font = fuenteRetro;
            }
            catch { }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
