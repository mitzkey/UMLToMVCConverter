using System.Windows;
using Microsoft.Win32;

namespace UMLToMVCConverter
{
    using System.IO;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TemporaryHardCodedDiagramPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\Praca Inżynierska\UMLToMVCConverter\Diagramy\MainDiagram.xml";
        private const string TemporaryHardCodedMvcProjectPath =
            @"C:\Users\mikolaj.bochajczuk\Desktop\priv\WebApplication1\WebApplication1";
        private const string TemporaryHardCodedConnectionString =
            @"Server=(localdb)\mssqllocaldb;Database=Default;Trusted_Connection=True;MultipleActiveResultSets=true";
        private string xmiPath;
        private string mvcProjectFolderPath;
        private string dbConnectionString;

        public MainWindow()
        {
            InitializeComponent();
            this.xmiPath = TemporaryHardCodedDiagramPath;
            this.mvcProjectFolderPath = TemporaryHardCodedMvcProjectPath;
            this.ProcessXmi();
            this.Close();
        }

        private void ProcessXmi()
        {
            var mvcProjectConfigurator = new MvcProjectConfigurator(this.mvcProjectFolderPath, TemporaryHardCodedConnectionString);
            var cg = new DataModelGenerator(this.xmiPath, mvcProjectConfigurator);
            MessageBox.Show(cg.GenerateMvcFiles());
        }

        private void BtnOpenXMI_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                this.xmiPath = openFileDialog.FileName;
                this.label_xmi_path.Content = this.xmiPath;
            }
        }

        private void btnProcessXmi_Click(object sender, RoutedEventArgs e)
        {
            this.ProcessXmi();
        }

        private void btnOpenVSSolution_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                this.mvcProjectFolderPath = Directory.GetParent(openFileDialog.FileName).ToString();
                this.label_vs_solution_path.Content = this.mvcProjectFolderPath;
            }
        }
    }
}
