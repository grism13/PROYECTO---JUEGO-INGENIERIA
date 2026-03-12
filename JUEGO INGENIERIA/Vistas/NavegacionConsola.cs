using System;
using System.Drawing;
using System.Windows.Forms;

namespace JUEGO_INGENIERIA.Vistas
{
    public static class NavegacionConsola
    {
        public static void ConfigurarFormulario(Form form)
        {
            // Para que cuando el formulario cargue, enfoque el primer botón automáticamente
            form.Shown += (s, e) => EnfocarPrimerBoton(form);

            foreach (Control control in form.Controls)
            {
                ConfigurarControl(control);
            }
        }

        private static void ConfigurarControl(Control control)
        {
            if (control is Button btn)
            {
                // Guardamos la configuración original del botón
                Color colorFondoOriginal = btn.BackColor;
                Color colorTextoOriginal = btn.ForeColor;
                
                int borderSizeOriginal = 0;
                Color borderColorOriginal = Color.Transparent;

                if (btn.FlatStyle == FlatStyle.Flat)
                {
                    borderSizeOriginal = btn.FlatAppearance.BorderSize;
                    borderColorOriginal = btn.FlatAppearance.BorderColor;
                }

                btn.Enter += (s, e) => 
                {
                    // Efecto visual cuando el botón es "Seleccionado" con el mando (flechas)
                    btn.BackColor = Color.Gold; // Color amarillo dorado para llamar la atención
                    btn.ForeColor = Color.Black;
                    
                    if (btn.FlatStyle == FlatStyle.Flat)
                    {
                        btn.FlatAppearance.BorderSize = 4;
                        btn.FlatAppearance.BorderColor = Color.Red;
                    }
                };

                btn.Leave += (s, e) => 
                {
                    // Volver a la normalidad cuando pierde la selección
                    btn.BackColor = colorFondoOriginal;
                    btn.ForeColor = colorTextoOriginal;
                    if (btn.FlatStyle == FlatStyle.Flat)
                    {
                        btn.FlatAppearance.BorderSize = borderSizeOriginal;
                        btn.FlatAppearance.BorderColor = borderColorOriginal;
                    }
                };
            }

            // Si hay paneles (Panel, GroupBox) dentro del formulario con botones, los procesamos también
            if (control.Controls.Count > 0)
            {
                foreach (Control subControl in control.Controls)
                {
                    ConfigurarControl(subControl);
                }
            }
        }

        private static bool EnfocarPrimerBoton(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button btn && btn.Visible && btn.Enabled)
                {
                    btn.Focus();
                    return true; // Enfoque listo
                }
                
                if (control.Controls.Count > 0)
                {
                     if (EnfocarPrimerBoton(control))
                         return true;
                }
            }
            return false;
        }
    }
}
