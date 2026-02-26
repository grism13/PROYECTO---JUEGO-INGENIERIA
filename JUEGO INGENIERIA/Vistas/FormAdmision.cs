using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;                // Esto es para manejar archivos
using System.Text.Json;         // Esto para leer los archivos .json
using System.Drawing.Text;      // NUEVO: Para la fuente de Pokémon

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormAdmision : Form
    {
        // El molde de la lista de usuarios
        List<Jugador> listaDeJugadores = new List<Jugador>();

        // La mochila para nuestra fuente
        PrivateFontCollection pfc = new PrivateFontCollection();

        // Ruta de guardado
        string rutaArchivo = "jugadores.json";

        public FormAdmision()
        {
            InitializeComponent();
            CargarFuente(); // Cargamos la estética primero
            CargarDatos();  // Luego cargamos los datos guardados
        }

        // --- ESTÉTICA Y DISEÑO ---
        private void CargarFuente()
        {
            string rutaFuente = Path.Combine(Application.StartupPath, "Vistas", "Fuentes", "Pokemon Classic.ttf");

            if (File.Exists(rutaFuente))
            {
                pfc.AddFontFile(rutaFuente);

                Font fuenteTitulos = new Font(pfc.Families[0], 12f);
                Font fuenteBotones = new Font(pfc.Families[0], 10f);
                Font fuenteTabla = new Font(pfc.Families[0], 8f);

                label1.Font = fuenteTitulos;
                label2.Font = fuenteTitulos;
                txtNombre.Font = fuenteBotones;
                btnCrear.Font = fuenteBotones;
                btnEliminar.Font = fuenteBotones;
                button1.Font = fuenteBotones;
                registratetxt.Font = fuenteTitulos;

                dvJugadores.DefaultCellStyle.Font = fuenteTabla;
                dvJugadores.ColumnHeadersDefaultCellStyle.Font = fuenteBotones;

            }
            else
            {
                MessageBox.Show("Aviso: No se encontró la fuente en la ruta: " + rutaFuente);
            }
        }



        // --- LÓGICA DE DATOS (CRUD ORIGINAL) ---
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingresa un nombre");
                return;
            }

            Jugador Nuevojugador = new Jugador(txtNombre.Text);
            listaDeJugadores.Add(Nuevojugador);

            ActualizarTabla();
            txtNombre.Clear();
            GuardarDatos();
        }

        private void ActualizarTabla()
        {
            dvJugadores.DataSource = null;
            dvJugadores.DataSource = listaDeJugadores;
        }

        private void GuardarDatos()
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string textoJson = JsonSerializer.Serialize(listaDeJugadores, opciones);
            File.WriteAllText(rutaArchivo, textoJson);
        }

        private void CargarDatos()
        {
            if (File.Exists(rutaArchivo))
            {
                string textoJson = File.ReadAllText(rutaArchivo);
                listaDeJugadores = JsonSerializer.Deserialize<List<Jugador>>(textoJson) ?? new List<Jugador>();
                ActualizarTabla();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dvJugadores.SelectedRows.Count > 0)
            {
                DialogResult respuesta = MessageBox.Show(
                    "Estas seguro que quieres eliminar a este jugador permanentemente",
                    "Confirmar Eliminacion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );

                if (respuesta == DialogResult.Yes)
                {
                    int indice = dvJugadores.SelectedRows[0].Index;
                    listaDeJugadores.RemoveAt(indice);
                    ActualizarTabla();
                    GuardarDatos();
                }
                else
                {
                    MessageBox.Show("Operacion cancelada, el jugador no ha sido eliminado");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dvJugadores.SelectedRows.Count > 0)
            {
                Jugador jugadorSeleccionado = (Jugador)dvJugadores.SelectedRows[0].DataBoundItem;
                Form1.JugadorActual = jugadorSeleccionado;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor selecciona un jugador para continuar");
            }
        }
    }
}