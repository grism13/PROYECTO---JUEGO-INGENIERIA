using System;
using System.Runtime.InteropServices;

namespace JUEGO_INGENIERIA.Vistas
{
    public static class ResolucionPantalla
    {
        // Traemos las herramientas nativas profundas de Windows (P/Invoke)
        [DllImport("user32.dll")]
        public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

        [DllImport("user32.dll")]
        public static extern int ChangeDisplaySettings(IntPtr devMode, int flags);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        // Estructura de datos que Windows necesita para leer los monitores
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmDeviceName;
            public short dmSpecVersion; public short dmDriverVersion; public short dmSize;
            public short dmDriverExtra; public int dmFields; public short dmOrientation;
            public short dmPaperSize; public short dmPaperLength; public short dmPaperWidth;
            public short dmScale; public short dmCopies; public short dmDefaultSource;
            public short dmPrintQuality; public short dmColor; public short dmDuplex;
            public short dmYResolution; public short dmTTOption; public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string dmFormName;
            public short dmLogPixels; public int dmBitsPerPel; public int dmPelsWidth;
            public int dmPelsHeight; public int dmDisplayFlags; public int dmDisplayFrequency;
        }

        // --- LAS DOS FUNCIONES QUE TÚ VAS A USAR ---

        // 1. Llama a esto al arrancar tu juego
        public static void ForzarResolucionJuego()
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

            // Le pedimos permiso a la PC para ver qué tamaño tiene
            if (EnumDisplaySettings(null, -1, ref dm))
            {
                // Solo si el usuario tiene un monitor de alta resolución (como 1080p o 4K)
                if (dm.dmPelsWidth > 1280 || dm.dmPelsHeight > 720)
                {
                    dm.dmPelsWidth = 1280;
                    dm.dmPelsHeight = 720;
                    // Windows reduce la pantalla temporalmente sin guardar permanentemente el ajuste
                    ChangeDisplaySettings(ref dm, 4);

                }
            }
        }

        // 2. Llama a esto en el botón de "Salir" de tu menú
        public static void RestaurarResolucion()
        {
            try { ChangeDisplaySettings(IntPtr.Zero, 0); } catch { }
        }
    }
}
