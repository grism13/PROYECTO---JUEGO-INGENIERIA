using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;                // Esto es para menejar archivos
using System.Text.Json;         // Esto para leer los arhivos .json shhh

namespace JUEGO_INGENIERIA.Vistas
{
    public partial class FormAdmision : Form
    {
        //Aqui declaro el molde de la lista que creare (la informacion del usuario) Dios mio
        List<Jugador> listaDeJugadores = new List<Jugador>();
        public FormAdmision()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingresa un nombre");
                return;
            }

            // Se crea al jugador en si, usando el nombre que coloco el usuario
            Jugador Nuevojugador = new Jugador(txtNombre.Text);
            listaDeJugadores.Add(Nuevojugador);

            // Se actualiza la tabla del DataGridView
            ActualizarTabla();
            txtNombre.Clear();

            // Esta funcion se ejecuta para guardar
            GuardarDatos();
        }

        private void ActualizarTabla()
        {
            // Se refresca poniendo e null :(
            dvJugadores.DataSource = null;
            dvJugadores.DataSource = listaDeJugadores;
        }

        // Aqui se coloca la ruta donde guardare los archivos 
        string rutaArchivo = "jugadores.json";


        // ¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬Toda esta parte son configuraciones del .JSON¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬
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


        //¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Aqui se verifica si el usuario esta seleccionando una de las filas
            if (dvJugadores.SelectedRows.Count > 0)
            {
                // Aqui solo se configura el mensaje de confirmar para eliminar a un usuario
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
