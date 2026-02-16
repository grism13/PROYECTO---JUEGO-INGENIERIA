using System;
using System.Collections.Generic;
using System.Text;

namespace JUEGO_INGENIERIA.Vistas
{
    // Este es el molde del juggador, aqui especifico ue datos necesito para crear los datos del jugador
    public class Jugador
    {
        // Especifico que se usa el Nombre, Id y el Nivel
        public string Nombre { get; set; }
        public string IdJugador { get; set; }
        public int Nivel { get;set; }

        // Aqui e arma como tal los datos del jugador 
        public Jugador (string nombreRecibido)
        {
            Nombre = nombreRecibido;
            Nivel = 1;
            IdJugador = Guid.NewGuid().ToString(); // Estoy generando un ID unico para cada jugador usando Guid
        }

        // Se deja vacio para colocarle el .json
        public Jugador()
        {

        }
    }
}
