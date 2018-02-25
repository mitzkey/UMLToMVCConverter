using System;
using System.Windows;
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

        private void BtnOpenXMI_Click(object sender, RoutedEventArgs e)
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
                ClassGenerator cg = new ClassGenerator(xmiPath, @"..\..\Output");
                MessageBox.Show(cg.GenerateTypes());
            }
            else
            {
                MessageBox.Show("Nie załadowano pliku XMI", "Błąd");
            }
        }
    }
}
