using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Kecskés Dominik Bálint
    public partial class Diakfelulet : Window
    {
        Dictionary<TextBox, string> alapSzovegek = new Dictionary<TextBox, string>();
        bool omAzonTilt = false;
        bool befejezve = false;
        public Diakfelulet(Felvetelizo felvetelizo, bool modosit = false)
        {
            InitializeComponent();
            
            if (modosit)
            {
                this.omAzonTilt = true;
                tbxAzon.IsReadOnly = true;
                
                Border brdr = tbxAzon.Parent as Border;
                brdr.Style = this.Resources["locked"] as Style;
                tbxDiakneve.Text = felvetelizo.Neve;
                tbxAzon.Text = felvetelizo.OM_Azonosito;
                tbxCim.Text = felvetelizo.ErtesitesiCime;
                tbxEmail.Text = felvetelizo.Email;
                tbxMagyar.Text = felvetelizo.Magyar == -1 ? "MAGYAR EREDMÉNY" : felvetelizo.Magyar.ToString();
                tbxMatek.Text = felvetelizo.Matematika == -1 ? "MATEMATIKA EREDMÉNY" : felvetelizo.Matematika.ToString();
                dpDatum.Text = felvetelizo.SzuletesiDatum.ToString();
                btnFelvesz.Content = "MÓDOSÍT";
                btnFelvesz.Style = this.Resources["modify"] as Style;
            }
            felvetelizo.Matematika = -1;
            felvetelizo.Magyar = -1;
            alapSzovegek[tbxDiakneve] = tbxDiakneve.Text;
            alapSzovegek[tbxAzon] = tbxAzon.Text;
            alapSzovegek[tbxCim] = tbxCim.Text;
            alapSzovegek[tbxEmail] = tbxEmail.Text;
            alapSzovegek[tbxMagyar] = tbxMagyar.Text;
            alapSzovegek[tbxMatek] = tbxMatek.Text;
            tbxDiakneve.TextChanged += (s, e) => {
                Border brdr = tbxDiakneve.Parent as Border;
                if (tbxDiakneve.Text.Trim().Length == 0)
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                if (tbxDiakneve.Text == "DIÁK NEVE")
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                brdr.Style = this.Resources["brdr"] as Style;
                felvetelizo.Neve = tbxDiakneve.Text.Trim();
            };
            tbxAzon.TextChanged += (s, e) => {
                Border brdr = tbxAzon.Parent as Border;
                if (omAzonTilt)
                {
                    brdr.Style = this.Resources["locked"] as Style;
                    return;
                }
                if (tbxAzon.Text.Trim().Length != 11)
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                brdr.Style = this.Resources["brdr"] as Style;
                felvetelizo.OM_Azonosito = tbxAzon.Text.Trim();
            };
            tbxCim.TextChanged += (s, e) => {
                Border brdr = tbxCim.Parent as Border;
                if (tbxCim.Text.Trim().Length == 0)
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                if (tbxCim.Text == "ÉRTESÍTÉSI CÍM")
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                brdr.Style = this.Resources["brdr"] as Style;
                felvetelizo.ErtesitesiCime = tbxCim.Text.Trim();
            };
            tbxEmail.TextChanged += (s, e) => {
                Border brdr = tbxEmail.Parent as Border;
                if (tbxEmail.Text.Trim().Length == 0)
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                if (tbxEmail.Text == "E-MAIL CÍM")
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                if (!Regex.IsMatch(tbxEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    brdr.Style = this.Resources["missing"] as Style;
                    return;
                }
                brdr.Style = this.Resources["brdr"] as Style;
                felvetelizo.Email = tbxEmail.Text.Trim();
            };
            tbxMagyar.TextChanged += (s, e) => {
                Border brdr = tbxMagyar.Parent as Border;
                int konvert = -1;
                if (tbxMagyar.Text == "MAGYAR EREDMÉNY")
                {
                    brdr.Style = this.Resources["brdr"] as Style;
                    felvetelizo.Magyar = -1;
                    return;
                }
                if (tbxMagyar.Text.Length > 0 && tbxMagyar.Text.Length < 3)
                {
                    try
                    {
                        konvert = int.Parse(tbxMagyar.Text.Trim());
                    }
                    catch (Exception)
                    {
                        brdr.Style = this.Resources["missing"] as Style;
                        throw;
                    }
                    if (konvert > 50 || konvert < 0)
                    {

                        brdr.Style = this.Resources["missing"] as Style;
                        return;
                    }
                    else
                    {
                        brdr.Style = this.Resources["brdr"] as Style;
                        felvetelizo.Magyar = konvert;
                    }

                }
               
            };
            tbxMatek.TextChanged += (s, e) => {
                Border brdr = tbxMatek.Parent as Border;
                int konvert = -1;
                if (tbxMatek.Text == "MATEMATIKA EREDMÉNY")
                {
                    brdr.Style = this.Resources["brdr"] as Style;
                    felvetelizo.Matematika = -1;
                    return;
                }
                if (tbxMatek.Text.Length > 0 && tbxMatek.Text.Length < 3)
                {
                    try
                    {
                        konvert = int.Parse(tbxMatek.Text.Trim());
                    }
                    catch (Exception)
                    {
                        brdr.Style = this.Resources["missing"] as Style;
                        throw;
                    }
                    if (konvert > 50 || konvert < 0)
                    {
                        brdr.Style = this.Resources["missing"] as Style;
                    }
                    else
                    {
                        brdr.Style = this.Resources["brdr"] as Style;
                        felvetelizo.Matematika = konvert;
                    }
                }
                
               
            };
            tbxDiakneve.GotFocus += TorolSzoveg;
            tbxAzon.GotFocus += (s, e) => {
                if (!omAzonTilt)
                {
                    tbxAzon.Text = string.Empty;
                }
            };
            tbxCim.GotFocus += TorolSzoveg;
            tbxEmail.GotFocus += TorolSzoveg;
            tbxMatek.GotFocus += TorolSzoveg;
            tbxMagyar.GotFocus += TorolSzoveg;
            tbxDiakneve.LostFocus += VisszaAllitSzoveg;
            tbxAzon.LostFocus += VisszaAllitSzoveg;
            tbxCim.LostFocus += VisszaAllitSzoveg;
            tbxEmail.LostFocus += VisszaAllitSzoveg;
            tbxMagyar.LostFocus += VisszaAllitSzoveg;
            tbxMatek.LostFocus += VisszaAllitSzoveg;
            tbxMagyar.PreviewTextInput += SzamValidalas;
            tbxMatek.PreviewTextInput += SzamValidalas;
            tbxAzon.PreviewTextInput += SzamValidalas;
            dpDatum.SelectedDateChanged += (s, e) => {
                felvetelizo.SzuletesiDatum = dpDatum.SelectedDate.Value;
            };

            btnFelvesz.Click += (s, e) => {
                if (Ellenoriz())
                {
                    this.befejezve = true;
                    this.Close();
                }
            };
        }
        

        private void VisszaAllitSzoveg(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox kuldo = sender as TextBox;
                if (kuldo.Text.Length == 0)
                {
                    kuldo.Text = alapSzovegek[kuldo];
                }
            }
        }
        private void SzamValidalas(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void TorolSzoveg(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox kuldo = sender as TextBox;
                kuldo.Text = string.Empty;
            }
        }

        public bool GetAllapot()
        {
            return this.befejezve;
        }

        public bool Ellenoriz()
        {
            bool vissza = true;
            Border brdr;
            if (tbxDiakneve.Text.Length == 0 || tbxDiakneve.Text == "DIÁK NEVE")
            {
                vissza = false;
                brdr = tbxDiakneve.Parent as Border;
                brdr.Style = this.Resources["missing"] as Style;
            }

            if (tbxAzon.Text.Length != 11 || tbxAzon.Text == "OM AZONOSÍTÓ")
            {
                vissza = false;
                brdr = tbxAzon.Parent as Border;
                brdr.Style = this.Resources["missing"] as Style;
            }

            if (tbxEmail.Text.Length == 0 || tbxEmail.Text == "E-MAIL CÍM")
            {
                vissza = false;
                brdr = tbxEmail.Parent as Border;
                brdr.Style = this.Resources["missing"] as Style;
            }

            if (tbxCim.Text.Length == 0 || tbxCim.Text == "ÉRTESÍTÉSI CÍM")
            {
                vissza = false;
                brdr = tbxCim.Parent as Border;
                brdr.Style = this.Resources["missing"] as Style;
            }

            int konvert;

            if (tbxMagyar.Text.Length > 0 && tbxMagyar.Text.Length < 3)
            {
                
                try
                {
                    konvert = int.Parse(tbxMagyar.Text);
                }
                catch (Exception)
                {
                    konvert = -1;
                }

                if (konvert > 50 || konvert < 0)
                {
                    vissza = false;
                    brdr = tbxMagyar.Parent as Border;
                    brdr.Style = this.Resources["missing"] as Style;
                }
            }

            if (tbxMatek.Text.Length > 0 && tbxMatek.Text.Length < 3)
            {
                try
                {
                    konvert = int.Parse(tbxMatek.Text);
                }
                catch (Exception)
                {
                    konvert = -1;
                }

                if (konvert > 50 || konvert < 0)
                {
                    vissza = false;
                    brdr = tbxMatek.Parent as Border;
                    brdr.Style = this.Resources["missing"] as Style;
                }
            }
            return vissza;
        }
    }
}
