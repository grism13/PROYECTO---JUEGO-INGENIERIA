using System;
using System.Windows.Forms;
using JUEGO_INGENIERIA.Vistas;

namespace JUEGO_INGENIERIA
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {

            // Nota: Colocar otra vez el "new Form1()" 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FormNivel2())
            Application.Run(new FormTrabajo());
        }

    }
}