using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace EasyPdfMerge.pdf {
    class PdfMerger {

        private XGraphics gfx;
        private PageConfiguration configuration;

        public void MergePdfs() {
            PdfDocument outputDocument = new PdfDocument();
            //XPdfForm form = getNewFormFromFile(tempFile);

            outputDocument.PageLayout = PdfPageLayout.SinglePage;
        }

        private XPdfForm getNewFormFromFile(string file) {
            // Open the external document as XPdfForm object
            XPdfForm form = XPdfForm.FromFile(file);
            return form;
        }

        private PageConfiguration BoxCalculator(PdfPage page) {
            PageConfiguration configuration = new PageConfiguration(PdfConfiguration.getInstance().pageBisections);
            int horizontalBisections = CalculateHorizontalBisections();
            int verticalBisections   = CalculateVerticalBisections();
            configuration.Rotate     = PdfConfiguration.getInstance().pageBisections % 2 == 1;
            int boxCounter           = 0;
            for (int i = 0; i < horizontalBisections; i++) {
                for (int j = 0; j < verticalBisections; j++) {
                    //Console.WriteLine("box = new XRect(" + j + " * (page.Width / " + verticalBisections + "), " + i  + "* (page.Height / " + horizontalBisections + "), page.Width / " + verticalBisections + ", page.Height / " + horizontalBisections + ");");
                    configuration.BoxConfigurations[boxCounter] = new XRect(j * (page.Width / verticalBisections), i  * (page.Height / horizontalBisections), page.Width / verticalBisections, page.Height / horizontalBisections);
                    boxCounter++;
                }
            }
            //Console.WriteLine("Rotate = " + configuration.Rotate);
            return configuration;
        }

        public PageConfiguration BoxCalculatorTest() {
            PageConfiguration configuration = new PageConfiguration(PdfConfiguration.getInstance().pageBisections);
            int horizontalBisections = CalculateHorizontalBisections();
            int verticalBisections = CalculateVerticalBisections();
            configuration.Rotate = PdfConfiguration.getInstance().pageBisections % 2 == 1;
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
            return PdfConfiguration.getInstance().orientation == PageOrientation.Portrait ? GetHigherDivisor() : GetLowerDivisor();
        }

        private int CalculateVerticalBisections() {
            return PdfConfiguration.getInstance().orientation == PageOrientation.Landscape ? GetHigherDivisor() : GetLowerDivisor();
        }

        private int GetHigherDivisor() {
            int div = GetNextEven(PdfConfiguration.getInstance().pageBisections);
            return div == 0 ? 1 : div;
        }

        private int GetLowerDivisor() {
            int div = GetPreviousEven(PdfConfiguration.getInstance().pageBisections);
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
