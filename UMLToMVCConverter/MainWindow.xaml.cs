﻿using System;
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
        private const string OutputPath = @"..\..\Output";
        private string xmiPath;
        public MainWindow()
        {
            InitializeComponent();
            //TODO: tymczasowo żeby nie klikać za każdym razem okna
            MvcFilesGenerator cg = new MvcFilesGenerator(TemporaryHardCodedDiagramPath, OutputPath);
            MessageBox.Show(cg.GenerateMvcFiles());
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
                MvcFilesGenerator cg = new MvcFilesGenerator(xmiPath, @"..\..\Output");
                MessageBox.Show(cg.GenerateMvcFiles());
            }
            else
            {
                MessageBox.Show("Failed to load XMI file", "Error");
            }
        }
    }
}
