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
        private const string TemporaryHardCodedDiagramPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inżynierska\UMLToMVCConverter\Diagramy\MainDiagram.xml";
        private const string TemporaryHardCodedMvcProjectPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inżynierska\UMLToMVCConverter\DefaultMVCApp";
        private string xmiPath;

        public MainWindow()
        {
            InitializeComponent();
            this.ProcessXmi();
            this.Close();
        }

        private void ProcessXmi()
        {
            var filesGenerator = new EntityFrameworkFilesGenerator(TemporaryHardCodedMvcProjectPath);
            var cg = new DataModelGenerator(TemporaryHardCodedDiagramPath, filesGenerator);
            MessageBox.Show(cg.GenerateMvcFiles());
        }

        private void BtnOpenXMI_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                this.xmiPath = openFileDialog.FileName;                
            }
        }

        private void btnProcessXmi_Click(object sender, RoutedEventArgs e)
        {
            this.ProcessXmi();
        }
    }
}
