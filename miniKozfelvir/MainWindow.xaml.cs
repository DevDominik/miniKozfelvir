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

namespace miniKozfelvir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Felvetelizo> felvetelizok;

        public MainWindow()
        {
            InitializeComponent();
            Import(File.ReadAllLines("felvetelizok.csv"));
        }

        public void Import(string[] lines)
        {
            felvetelizok = lines.Skip(1).Select(x => new Felvetelizo(x)).ToList();
        }

        public void ShowImport() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".csv";
            if (ofd.ShowDialog() == true) {
                Import(File.ReadAllLines(ofd.FileName));
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
    }
}
