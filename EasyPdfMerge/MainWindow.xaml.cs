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
using EasyPdfMerge.pdf;
using Microsoft.Win32;


namespace EasyPdfMerge
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mergeButton_Click(object sender, RoutedEventArgs e)
        {
            //var myVar = PdfHelper.getInstance();
            ////myVar.testPdf();
            ////myVar.mergePdfPortrait();
            //var files = new String[filesListBox.Items.Count];
            //PdfDoc[] pdfDocs;
            //int i = 0;
            //foreach (var file in filesListBox.Items)
            //{
            //    files[i] = (String)file;
            //    i++;
            //}
            //if (comboBoxOrientation.SelectedIndex == 1) {
            //    pdfDocs = myVar.prepareMultiPageDocs(files, PdfSharp.PageOrientation.Landscape);
            //} else if (comboBoxOrientation.SelectedIndex == 2) {
            //    pdfDocs = myVar.prepareMultiPageDocs(files, PdfSharp.PageOrientation.Portrait);
            //} else {
            //    pdfDocs = myVar.readPdfDocs(files);
            //}
            //myVar.saveDocument(myVar.concatenatePdfs(pdfDocs), "output.pdf");
            PdfConfiguration config = new PdfConfiguration();
            if (comboBoxOrientation.SelectedIndex == 0) {
                config.orientation = PdfSharp.PageOrientation.Landscape;
            } else {
                config.orientation = PdfSharp.PageOrientation.Portrait;
            }
            config.pageBisections = comboBoxPagesOnPage.SelectedIndex;
            string[] files = new string[filesListBox.Items.Count];
            int i = 0;
            foreach (var file in filesListBox.Items) {
                files[i] = (String)file;
                i++;
            }
            PdfMerger merger = new PdfMerger();
            merger.MergePdfs(files, config);

        }

        private void addFilesButton_Click(object sender, RoutedEventArgs e)
        {
            //var myVar = new PdfHelper();
            //myVar.testPdf();
            //var files = new String[2] { "C:/Users/Bla/Documents/Visual Studio 2015/Projects/EasyPdfMerge/EasyPdfMerge/bin/Debug/test.pdf", "C:/Users/Bla/Documents/Visual Studio 2015/Projects/EasyPdfMerge/EasyPdfMerge/bin/Debug/test2.pdf" };

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                    filesListBox.Items.Add(filename);
            }
            if (filesListBox.Items.Count > 0) {
                mergeButton.IsEnabled = true;
            }
        }

        private PdfConfiguration config = new PdfConfiguration();

        private void onComboBoxPagesOnPage_Closed(object sender, EventArgs e) {
            config.pageBisections = comboBoxPagesOnPage.SelectedIndex;
        }

        private void onComboBoxOrientation_Closed(object sender, EventArgs e) {
            if (comboBoxOrientation.SelectedIndex == 0) {
                config.orientation = PdfSharp.PageOrientation.Landscape;
            } else {
                config.orientation = PdfSharp.PageOrientation.Portrait;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            PdfConfiguration config = new PdfConfiguration();
            if (comboBoxOrientation.SelectedIndex == 0) {
                config.orientation = PdfSharp.PageOrientation.Landscape;
            } else {
                config.orientation = PdfSharp.PageOrientation.Portrait;
            }
            config.pageBisections = comboBoxPagesOnPage.SelectedIndex;
            string[] files = new string[filesListBox.Items.Count];
            int i = 0;
            foreach (var file in filesListBox.Items) {
                files[i] = (String)file;
                i++;
            }
            PdfMerger merger = new PdfMerger();
            merger.MergePdfs(files, config);
        }
    }
}
