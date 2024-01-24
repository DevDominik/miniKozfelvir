using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace miniKozfelvir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// Györfi Marcell
    public partial class MainWindow : Window
    {
        ObservableCollection<Felvetelizo> felvetelizok;

        public MainWindow()
        {
            InitializeComponent();
            felvetelizok = new ObservableCollection<Felvetelizo>(File.ReadAllLines("felvetelizok.csv").Skip(1).Select(x => new Felvetelizo(x)));
            dgFelvetelizok.ItemsSource = felvetelizok;
        }

        public void Import() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV vagy JSON|*.csv;*.json";
            if (ofd.ShowDialog() == true) {
                if (felvetelizok.Count != 0 && MessageBox.Show("Szeretné törölni a meglévő adatokat?", "Adatok törlése", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    felvetelizok.Clear();
                }

                if (System.IO.Path.GetExtension(ofd.FileName) == ".csv") {
                    File.ReadAllLines(ofd.FileName).Skip(1).Select(x => new Felvetelizo(x)).ToList().ForEach(x => felvetelizok.Add(x));
                }
                else {
                    JsonSerializer.Deserialize<List<Felvetelizo>>(File.ReadAllText(ofd.FileName)).ForEach(x => felvetelizok.Add(x));
                }
            }
        }

        public void Export()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV fájl|*.csv|JSON fájl|*.json";
            if (sfd.ShowDialog() == true)
            {
                if (sfd.FilterIndex == 1)
                {
                    File.WriteAllLines(sfd.FileName, felvetelizok.Select(x => x.ToString()).Prepend(Felvetelizo.CSVFEJ).ToList());
                }
                else {
                    var opciok = new JsonSerializerOptions();
                    opciok.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    opciok.WriteIndented = true;

                    File.WriteAllText(sfd.FileName, JsonSerializer.Serialize(felvetelizok, opciok));
                }
            }
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            Felvetelizo ujDiak = new Felvetelizo();

            Diakfelulet ujAblak = new Diakfelulet(ujDiak);
            ujAblak.ShowDialog();

            if (ujAblak.GetAllapot()) {
                felvetelizok.Add(ujDiak);
            }
        }

        private void btnTorol_Click(object sender, RoutedEventArgs e)
        {
            dgFelvetelizok.SelectedItems.Cast<Felvetelizo>().ToList().ForEach(x => felvetelizok.Remove(x));
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            Import();
        }

        private void btnModosit_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedIndex != -1)
            {
                Felvetelizo diak = dgFelvetelizok.SelectedItem as Felvetelizo;
                int index = felvetelizok.IndexOf(diak);

                Diakfelulet ujAblak = new Diakfelulet(diak, true);
                ujAblak.ShowDialog();

                if (ujAblak.GetAllapot())
                {
                    felvetelizok[index] = diak;
                    dgFelvetelizok.Items.Refresh();
                }
            }
            
        }
    }
}
