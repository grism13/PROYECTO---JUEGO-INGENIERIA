using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace JUEGO_INGENIERIA.Vistas
{
    public static class NavegacionConsola
    {
        private static Dictionary<Form, List<Control>> controlesPorFormulario = new Dictionary<Form, List<Control>>();
        private static Dictionary<Form, Control> controlEnfocadoPorForm = new Dictionary<Form, Control>();

        public static void Configurar(Form form, params Control[] controlesNavegables)
        {
            if (controlesNavegables == null || controlesNavegables.Length == 0) return;

            form.KeyPreview = true;

            controlesPorFormulario[form] = new List<Control>(controlesNavegables);

            if (!controlEnfocadoPorForm.ContainsKey(form))
            {
                form.KeyDown += (s, e) => ManejarTeclas(form, e);
            }
            controlEnfocadoPorForm[form] = null; // No se resalta nada hasta interactuar
        }

        public static void LimpiarFoco(Form form)
        {
            if (controlEnfocadoPorForm.ContainsKey(form))
            {
                Control viejo = controlEnfocadoPorForm[form];
                if (viejo != null) ResaltarControl(viejo, false);
                controlEnfocadoPorForm[form] = null;
            }
        }

        private static void ManejarTeclas(Form form, KeyEventArgs e)
        {
            if (!controlesPorFormulario.ContainsKey(form)) return;

            var lista = controlesPorFormulario[form];
            var visibles = new List<Control>();

            foreach (var c in lista)
            {
                if (c.Visible && c.Enabled) visibles.Add(c);
            }

            if (visibles.Count == 0) return;

            Control actual = controlEnfocadoPorForm.ContainsKey(form) ? controlEnfocadoPorForm[form] : null;

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (actual != null && actual.Visible && actual.Enabled)
                {
                    EjecutarClicMagico(actual);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                return;
            }

            if (actual == null || !actual.Visible || !actual.Enabled)
            {
                if (EsTeclaNavegacion(e.KeyCode))
                {
                    Enfocar(form, visibles[0]);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                return;
            }

            Control siguiente = null;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                siguiente = EncontrarMasCercano(visibles, actual, Keys.Right);
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                siguiente = EncontrarMasCercano(visibles, actual, Keys.Left);
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                siguiente = EncontrarMasCercano(visibles, actual, Keys.Down);
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                siguiente = EncontrarMasCercano(visibles, actual, Keys.Up);

            if (siguiente != null && siguiente != actual)
            {
                Enfocar(form, siguiente);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (EsTeclaNavegacion(e.KeyCode))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private static bool EsTeclaNavegacion(Keys k)
        {
            return k == Keys.Right || k == Keys.Left || k == Keys.Down || k == Keys.Up ||
                   k == Keys.W || k == Keys.A || k == Keys.S || k == Keys.D;
        }

        private static void Enfocar(Form form, Control nuevoControl)
        {
            Control viejo = controlEnfocadoPorForm[form];
            if (viejo != null)
            {
                ResaltarControl(viejo, false);
            }
            controlEnfocadoPorForm[form] = nuevoControl;
            ResaltarControl(nuevoControl, true);
        }

        private static Control EncontrarMasCercano(List<Control> visibles, Control actual, Keys direccion)
        {
            Control mejor = null;
            double mejorDist = double.MaxValue;
            Point c1 = ObtenerCentro(actual);

            foreach (var c2 in visibles)
            {
                if (c2 == actual) continue;
                Point p2 = ObtenerCentro(c2);
                int dx = p2.X - c1.X;
                int dy = p2.Y - c1.Y;

                bool enDireccion = false;
                if (direccion == Keys.Right && dx > 0 && Math.Abs(dx) >= Math.Abs(dy) * 0.5) enDireccion = true;
                if (direccion == Keys.Left && dx < 0 && Math.Abs(dx) >= Math.Abs(dy) * 0.5) enDireccion = true;
                if (direccion == Keys.Down && dy > 0 && Math.Abs(dy) >= Math.Abs(dx) * 0.5) enDireccion = true;
                if (direccion == Keys.Up && dy < 0 && Math.Abs(dy) >= Math.Abs(dx) * 0.5) enDireccion = true;

                if (enDireccion)
                {
                    double dist = Math.Sqrt(dx * dx + dy * dy);
                    if (dist < mejorDist)
                    {
                        mejorDist = dist;
                        mejor = c2;
                    }
                }
            }

            if (mejor == null)
            {
                foreach (var c2 in visibles)
                {
                    if (c2 == actual) continue;
                    Point p2 = ObtenerCentro(c2);
                    int dx = p2.X - c1.X;
                    int dy = p2.Y - c1.Y;

                    bool lado = false;
                    if (direccion == Keys.Right && dx > 0) lado = true;
                    if (direccion == Keys.Left && dx < 0) lado = true;
                    if (direccion == Keys.Down && dy > 0) lado = true;
                    if (direccion == Keys.Up && dy < 0) lado = true;

                    if (lado)
                    {
                        double dist = Math.Sqrt(dx * dx + dy * dy);
                        if (dist < mejorDist)
                        {
                            mejorDist = dist;
                            mejor = c2;
                        }
                    }
                }
            }

            return mejor;
        }

        private static Point ObtenerCentro(Control c)
        {
            return new Point(c.Left + (c.Width / 2), c.Top + (c.Height / 2));
        }

        private static void ResaltarControl(Control control, bool enfocado)
        {
            if (enfocado)
            {
                if (control.Tag == null)
                {
                    if (control is PictureBox p)
                        control.Tag = new Tuple<Padding, Color>(p.Padding, p.BackColor);
                    else if (control is Button b && b.FlatStyle == FlatStyle.Flat)
                        control.Tag = b.FlatAppearance.BorderColor;
                    else if (control is Label l) // AGREGADO PARA LABELS
                        control.Tag = new Tuple<Color, Color, BorderStyle>(l.BackColor, l.ForeColor, l.BorderStyle);
                    else
                        control.Tag = control.BackColor;
                }

                if (control is PictureBox pb)
                {
                    pb.Padding = new Padding(5);
                    pb.BackColor = Color.Red;
                }
                else if (control is Button btn && btn.FlatStyle == FlatStyle.Flat)
                {
                    btn.FlatAppearance.BorderColor = Color.Red;
                    btn.FlatAppearance.BorderSize = 6;
                }
                else if (control is Label lbl)
                {
                    lbl.BackColor = Color.Red;
                    lbl.ForeColor = Color.Yellow; // Color de letra amarillo de alerta
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    control.BackColor = Color.Red;
                }
            }
            else
            {
                if (control.Tag is Tuple<Padding, Color> imgTag && control is PictureBox pbox)
                {
                    pbox.Padding = imgTag.Item1;
                    pbox.BackColor = imgTag.Item2;
                }
                else if (control.Tag is Color btnBorder && control is Button btn && btn.FlatStyle == FlatStyle.Flat)
                {
                    btn.FlatAppearance.BorderColor = btnBorder;
                    btn.FlatAppearance.BorderSize = 0;
                }
                else if (control.Tag is Tuple<Color, Color, BorderStyle> lblTag && control is Label lbl)
                {
                    lbl.BackColor = lblTag.Item1;
                    lbl.ForeColor = lblTag.Item2;
                    lbl.BorderStyle = lblTag.Item3;
                }
                else if (control.Tag is Color colorOriginal)
                {
                    control.BackColor = colorOriginal;
                }
                else
                {
                    control.BackColor = Color.Transparent;
                }
            }
        }

        private static void EjecutarClicMagico(Control control)
        {
            if (control is Button btn)
            {
                btn.PerformClick();
            }
            else
            {
                MethodInfo onClickMethod = typeof(Control).GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
                if (onClickMethod != null)
                {
                    onClickMethod.Invoke(control, new object[] { EventArgs.Empty });
                }
            }
        }
    }
}
