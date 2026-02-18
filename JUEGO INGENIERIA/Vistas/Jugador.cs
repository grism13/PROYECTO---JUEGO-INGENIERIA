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
        public int Billetera { get; set; }

        // Aqui e arma como tal los datos del jugador 
        public Jugador(string nombreRecibido)
        {
            Nombre = nombreRecibido;
            Nivel = 1;
            Billetera = 1000;


            // Aquí está la magia para acortar el ID a 6 caracteres
            IdJugador = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
        }

        // Se deja vacio para colocarle el .json
        public Jugador()
        {

        }
    }
}
