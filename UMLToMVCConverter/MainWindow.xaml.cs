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

namespace UMLToMVCConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string xmiPath;
        public MainWindow()
        {
            InitializeComponent();
            //TODO: tymczasowo żeby nie klikać za każdym razem okna
            //ClassGenerator.GenerateClassesFromXmi(@"C:\Users\Mikołaj\Desktop\Informatyka\Praca Inżynierska\MD Projects\Test01\Test01_xmi.xml");
        }

        private void btnOpenXMI_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                xmiPath = openFileDialog.FileName;                
            }
        }

        private void btnProcessXmi_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(xmiPath))
            {
                ClassGenerator cg = new ClassGenerator(xmiPath);
                MessageBox.Show(cg.GenerateTypes());
            }
            else
            {
                MessageBox.Show("Nie załadowano pliku XMI", "Błąd");
            }
        }
    }
}
