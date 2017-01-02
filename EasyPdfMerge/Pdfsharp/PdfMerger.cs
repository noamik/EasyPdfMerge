using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace EasyPdfMerge.Pdf {
    class PdfSharpPdfMerger {

        private PdfSharp.Drawing.XGraphics gfx;
        private PageConfiguration pageConfiguration;
        private PdfConfiguration outputConfiguration;
        private PdfDocument outputDocument;

        public PdfSharpPdfMerger() {
            outputDocument = new PdfDocument();
            //XPdfForm form = getNewFormFromFile(tempFile);

            outputDocument.PageLayout = PdfPageLayout.SinglePage;
        }

        public void MergePdfs(string[] files, PdfConfiguration outputConfiguration) {
            this.outputConfiguration = outputConfiguration;
            ConcatenatePdfs(LoadPdfs(files));
            outputDocument.Save(outputConfiguration.file);
            Process.Start(outputConfiguration.file);
        }

        private PdfDoc[] LoadPdfs(string[] files) {
            PdfDoc[] docs = new PdfDoc[files.Count()];
            for (int i = 0; i < files.Count(); i++) {
                //docs[i] = new PdfDoc(files[i], PdfReader.Open(files[i], PdfDocumentOpenMode.Import));
                docs[i] = new PdfDoc(files[i], XPdfForm.FromFile(files[i]));
            }
            return docs;
        }

        private XPdfForm getNewFormFromFile(string file) {
            // Open the external document as XPdfForm object
            XPdfForm form = XPdfForm.FromFile(file);
            return form;
        }

        private PdfDocument ConcatenatePdfs(PdfDoc[] docs) {
            foreach (PdfDoc doc in docs) {
                // Iterate pages
                int count = doc.doc.PageCount;
                for (int idx = 0; idx < count; idx += outputConfiguration.GetSubPageCount()) {
                    AddOutputPage(doc, idx + 1);                    
                }
            }
            return outputDocument;
        }

        private void AddOutputPage(PdfDoc doc, int firstPage) {
            PdfPage page = CreateConfiguredOutputPage();
            RenderSubPages(page, doc, firstPage);
        }

        private PdfPage CreateConfiguredOutputPage() {
            PdfPage page = outputDocument.AddPage();
            page.Orientation = outputConfiguration.orientation;
            gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 8, XFontStyle.Bold);
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Center;
            format.LineAlignment = XLineAlignment.Far;
            return page;
        }

        private void RenderSubPages(PdfPage page, PdfDoc doc, int firstPage) {
            PageConfiguration config = BoxCalculator(page);

            for (int i = 0; i < outputConfiguration.GetSubPageCount(); i++) {
                if (doc.doc.PageCount < firstPage + i) {
                    break;
                }
                doc.doc.PageNumber = firstPage + i;
                AutoRotatePage(config, doc);
                gfx.DrawImage(doc.doc, config.BoxConfigurations[i]);
            }
        }

        private void AutoRotatePage(PageConfiguration config, PdfDoc doc) {
            if (config.Rotate) {
                doc.doc.Page.Rotate = doc.doc.Page.Rotate + 90;
                if (outputConfiguration.orientation == PageOrientation.Landscape) {
                    doc.doc.Page.Orientation = PageOrientation.Portrait;
                } else {
                    doc.doc.Page.Orientation = PageOrientation.Landscape;
                }
            }
        }

        private PdfPage CreatePage() {
            // Add a new page to the output document
            PdfPage page = outputDocument.AddPage();
            page.Orientation = outputConfiguration.orientation;
            int rotate = page.Elements.GetInteger("/Rotate");
            return page;
        }

        private PageConfiguration BoxCalculator(PdfPage page) {
            PageConfiguration configuration = new PageConfiguration(this.outputConfiguration.pageBisections);
            int horizontalBisections = CalculateHorizontalBisections();
            int verticalBisections   = CalculateVerticalBisections();
            configuration.Rotate     = this.outputConfiguration.pageBisections % 2 == 1;
            int boxCounter           = 0;
            for (int i = 0; i < horizontalBisections; i++) {
                for (int j = 0; j < verticalBisections; j++) {
                    Console.WriteLine("box = new XRect(" + j + " * (page.Width / " + verticalBisections + "), " + i  + " * (page.Height / " + horizontalBisections + "), page.Width / " + verticalBisections + ", page.Height / " + horizontalBisections + ");");
                    configuration.BoxConfigurations[boxCounter] = new XRect(j * (page.Width / verticalBisections), i  * (page.Height / horizontalBisections), page.Width / verticalBisections, page.Height / horizontalBisections);
                    boxCounter++;
                }
            }
            //Console.WriteLine("Rotate = " + configuration.Rotate);
            return configuration;
        }

        public PageConfiguration BoxCalculatorTest() {
            PageConfiguration configuration = new PageConfiguration(this.outputConfiguration.pageBisections);
            int horizontalBisections = CalculateHorizontalBisections();
            int verticalBisections = CalculateVerticalBisections();
            configuration.Rotate = this.outputConfiguration.pageBisections % 2 == 1;
            int boxCounter = 0;
            for (int i = 0; i < horizontalBisections; i++) {
                for (int j = 0; j < verticalBisections; j++) {
                    Console.WriteLine("box = new XRect(" + j + " * (page.Width / " + verticalBisections + "), " + i + "* (page.Height / " + horizontalBisections + "), page.Width / " + verticalBisections + ", page.Height / " + horizontalBisections + ");");
                    //configuration.BoxConfigurations[boxCounter] = new XRect(j * (page.Width / verticalBisections), i  * (page.Height / horizontalBisections), page.Width / verticalBisections, page.Height / horizontalBisections);
                    boxCounter++;
                }
            }
            //Console.WriteLine("Rotate = " + configuration.Rotate);
            return configuration;
        }

        private int CalculateHorizontalBisections() {
            return this.outputConfiguration.orientation == PageOrientation.Portrait ? GetHigherDivisor() : GetLowerDivisor();
        }

        private int CalculateVerticalBisections() {
            return this.outputConfiguration.orientation == PageOrientation.Landscape ? GetHigherDivisor() : GetLowerDivisor();
        }

        private int GetHigherDivisor() {
            int div = GetNextEven(this.outputConfiguration.pageBisections);
            return div == 0 ? 1 : div;
        }

        private int GetLowerDivisor() {
            int div = GetPreviousEven(this.outputConfiguration.pageBisections);
            return div == 0 ? 1 : div;
        }

        private int GetNextEven(int num) {
            return num % 2 == 0 ? num : num + 1;
        }

        private int GetPreviousEven(int num) {
            return num % 2 == 0 ? num : num - 1;
        }
    }
}
