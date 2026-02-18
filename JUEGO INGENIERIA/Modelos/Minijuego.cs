using System;
using System.Drawing; // NECESARIO para Image y Rectangle

namespace JUEGO_INGENIERIA.Modelos // Asegúrate de que este namespace coincida
{
    public class ObjetoJuego
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Image Imagen { get; set; }
        public string Tag { get; set; } // Aquí guardaremos "bueno" o "malo"

        // Esto crea un rectángulo invisible para detectar choques
        public Rectangle Area
        {
            get { return new Rectangle(X, Y, 50, 50); }
        }
    }
}