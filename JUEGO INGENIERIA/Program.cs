using System;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Vistas; // ¡Asegúrate de que esto esté para que reconozca a ResolucionPantalla!

namespace JUEGO_INGENIERIA
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. ANTES DE ABRIR CUALQUIER COSA: Forzamos la PC a modo Consola (720p)
            ResolucionPantalla.ForzarResolucionJuego();

            // 2. EL JUEGO SE EJECUTA NORMALMENTE
            Application.Run(new Form1());

            // 3. CUANDO EL JUGADOR CIERRA EL JUEGO: Todo vuelve a la normalidad automáticamente
            ResolucionPantalla.RestaurarResolucion();
        }
    }
}
