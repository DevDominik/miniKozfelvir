using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace miniKozfelvir
{
    /// <summary>
    /// Interaction logic for Diakfelulet.xaml
    /// </summary>
    public partial class Diakfelulet : Window
    {
        static Felvetelizo felvetelizo;
        public Diakfelulet(Felvetelizo uj)
        {
            InitializeComponent();
            tbxDiakneve.TextInput += (s, e) => {
                if (e.Text.Trim().Length == 0)
                {
                    tbxDiakneve.Style = Application.Current.FindResource("missing") as Style;
                    tbxDiakneve.Text = "DIÁK NEVE";
                    return;
                }
                if (e.Text == "DIÁK NEVE")
                {
                    tbxDiakneve.Style = Application.Current.FindResource("missing") as Style;
                    return;
                }
                tbxDiakneve.Style = Application.Current.FindResource("generalStyle") as Style;
                felvetelizo.Neve = e.Text.Trim();
            };
            tbxAzon.TextInput += (s, e) => {
                if (e.Text.Trim().Length != 11)
                {
                    tbxAzon.Style = Application.Current.FindResource("missing") as Style;
                    if (e.Text.Length == 0)
                    {
                        tbxAzon.Text = "OM AZONOSÍTÓ";
                        return;
                    }
                    if (e.Text == "OM AZONOSÍTÓ")
                    {
                        tbxAzon.Style = Application.Current.FindResource("missing") as Style;
                        return;
                    }
                    return;
                }
                tbxAzon.Style = Application.Current.FindResource("generalStyle") as Style;
                felvetelizo.OM_Azonosito = e.Text.Trim();
            };
            tbxCim.TextInput += (s, e) => {
                if (e.Text.Trim().Length == 0)
                {
                    tbxCim.Style = Application.Current.FindResource("missing") as Style;
                    tbxCim.Text = "ÉRTESÍTÉSI CÍM";
                    return;
                }
                if (e.Text == "ÉRTESÍTÉSI CÍM")
                {
                    tbxCim.Style = Application.Current.FindResource("missing") as Style;
                    return;
                }
                tbxCim.Style = Application.Current.FindResource("generalStyle") as Style;
                felvetelizo.ErtesitesiCime = e.Text.Trim();
            };
            tbxEmail.TextInput += (s, e) => {
                if (e.Text.Trim().Length == 0)
                {
                    tbxEmail.Style = Application.Current.FindResource("missing") as Style;
                    tbxEmail.Text = "E-MAIL CÍM";
                    return;
                }
                if (e.Text == "E-MAIL CÍM")
                {
                    tbxEmail.Style = Application.Current.FindResource("missing") as Style;
                    return;
                }
                tbxEmail.Style = Application.Current.FindResource("generalStyle") as Style;
                felvetelizo.Email = e.Text.Trim();
            };
            tbxMagyar.TextInput += (s, e) => {
                int konvert;
                try
                {
                    konvert = int.Parse(e.Text.Trim());
                }
                catch (Exception)
                {
                    tbxMagyar.Style = Application.Current.FindResource("missing") as Style;
                    throw;
                }
                if (konvert > 50 || konvert < 0)
                {

                    tbxMagyar.Style = Application.Current.FindResource("missing") as Style;
                }
                else
                {
                    tbxMagyar.Style = Application.Current.FindResource("generalStyle") as Style;
                    felvetelizo.Magyar = konvert;
                }
            };
            tbxMatek.TextInput += (s, e) => {
                int konvert;
                try
                {
                    konvert = int.Parse(e.Text.Trim());
                }
                catch (Exception)
                {
                    tbxMatek.Style = Application.Current.FindResource("missing") as Style;
                    throw;
                }
                if (konvert > 50 || konvert < 0)
                {
                    tbxMatek.Style = Application.Current.FindResource("missing") as Style;
                }
                else
                {
                    tbxMatek.Style = Application.Current.FindResource("generalStyle") as Style;
                    felvetelizo.Matematika = konvert;
                }
            };
            dpDatum.SelectedDateChanged += (s, e) => {
                felvetelizo.SzuletesiDatum = dpDatum.SelectedDate.Value;
            };
            
            btnFelvesz.Click += (s, e) => { 
                if (Ellenoriz())
                {
                    this.Close();
                }
            };
        }

        public bool Ellenoriz()
        {
            bool vissza = false;
            if (tbxDiakneve.Text.Length == 0 || tbxDiakneve.Text == "DIÁK NEVE")
            {
                vissza = false;
                tbxDiakneve.Style = Application.Current.FindResource("missing") as Style;
            } 
            
            if (tbxAzon.Text.Length != 11 || tbxAzon.Text == "OM AZONOSÍTÓ")
            {
                vissza = false;
                tbxAzon.Style = Application.Current.FindResource("missing") as Style;
            }

            if (tbxEmail.Text.Length == 0 || tbxEmail.Text == "E-MAIL CÍM")
            {
                vissza = false;
                tbxEmail.Style = Application.Current.FindResource("missing") as Style;
            }

            if (tbxCim.Text.Length == 0 || tbxCim.Text == "ÉRTESÍTÉSI CÍM")
            {
                vissza = false;
                tbxCim.Style = Application.Current.FindResource("missing") as Style;
            }

            int konvert;
            try
            {
                konvert = int.Parse(tbxMagyar.Text);
            }
            catch (Exception)
            {
                konvert = -1;
            }
            
            if (konvert > 50 || konvert < 0 || tbxMagyar.Text == "MAGYAR EREDMÉNY")
            {
                vissza = false;
                tbxMagyar.Style = Application.Current.FindResource("missing") as Style;
            }

            try
            {
                konvert = int.Parse(tbxMatek.Text);
            }
            catch (Exception)
            {
                konvert = -1;
            }

            if (konvert > 50 || konvert < 0 || tbxMatek.Text == "MATEMATIKA EREDMÉNY")
            {
                vissza = false;
                tbxMatek.Style = Application.Current.FindResource("missing") as Style;
            }

            if (dpDatum.SelectedDate.Value == null)
            {
                vissza = false;
                dpDatum.Style = Application.Current.FindResource("missing") as Style;
            }
            return vissza;
        }

        public Felvetelizo GetFelvetelizo()
        {
            return felvetelizo;
        }
    }
}
