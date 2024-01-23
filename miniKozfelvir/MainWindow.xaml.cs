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

namespace miniKozfelvir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            ofd.DefaultExt = ".csv";
            if (ofd.ShowDialog() == true) {
                File.ReadAllLines(ofd.FileName).Skip(1).Select(x => new Felvetelizo(x)).ToList().ForEach(x => {
                    felvetelizok.Remove(felvetelizok.FirstOrDefault(y => y.OM_Azonosito == x.OM_Azonosito));
                    felvetelizok.Add(x);
                   });
            }
        }

        public void Export()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".csv";
            if (sfd.ShowDialog() == true)
            {
                File.WriteAllLines(sfd.FileName, felvetelizok.Select(x => x.ToString()).Prepend(Felvetelizo.CSVFEJ).ToList());
            }
        }

        private void btnUj_Click(object sender, RoutedEventArgs e)
        {
            
            Diakfelulet ujAblak = new Diakfelulet();
            ujAblak.ShowDialog();

            felvetelizok.Add(ujAblak.GetFelvetelizo());
        }

        private void btnTorol_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedIndex != -1) {
                felvetelizok.Remove(dgFelvetelizok.SelectedItem as Felvetelizo);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            Import();
        }
    }
}
