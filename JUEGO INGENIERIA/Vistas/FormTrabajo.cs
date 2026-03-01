using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using JUEGO_INGENIERIA.Modelos; // Importante para usar la clase Jugador

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

        // Recibimos al jugador en el constructor igual que en FormNivel1
        public FormTrabajo(Jugador jugadorRecibido)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // Guardamos los datos del jugador
            this.jugadorActual = jugadorRecibido;

            this.Load += FormTrabajo_Load;
            this.Paint += FormTrabajo_Paint;
            this.FormClosing += FormTrabajo_FormClosing;
        }

        private void FormTrabajo_Load(object sender, EventArgs e)
        {
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

            pbMesa1.BackColor = Color.Blue;
            pbMesa2.BackColor = Color.Blue;
            pbMesa3.BackColor = Color.Blue;
            pbMesa4.BackColor = Color.Blue;

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

        private void FormTrabajo_Paint(object sender, PaintEventArgs e)
        {
            if (movimientoJugador != null)
            {
                movimientoJugador.DibujarPersonaje(e.Graphics);
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
    }
}