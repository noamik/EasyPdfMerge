using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPdfMerge.Pdf {

    public enum PageOrientation {
        Landscape, Portrait
    }

    class PdfConfiguration {

        public string file { get; set; }
        public PageOrientation orientation { get; set; }
        public int pageBisections { get; set; }

        public PdfConfiguration() {
            // prevent creation of public constructor
            pageBisections = 0;
            orientation = PageOrientation.Portrait;
            file = Path.Combine(Directory.GetCurrentDirectory(), "output.pdf");
        }

        /// <summary>
        /// Calculates the amount of subpages to place on a single output page from the number of bisections made on the output page.
        /// The result has to be the same as HorizontalPageCount * VerticalPageCount
        /// </summary>
        /// <returns>The amount of subpages to position on an output page.</returns>
        public int GetSubPageCount() {
            return (int)Math.Pow(2, pageBisections);
        }

        /// <summary>
        /// Calculates the amount of subpages to place horizontally.
        /// </summary>
        /// <returns>The amount of pages to place horizontally.</returns>
        public int CalculateHorizontalPageCount() {
            return this.orientation == PageOrientation.Landscape ? GetHigherDivisor() : GetLowerDivisor();
        }

        /// <summary>
        /// Calculates the amount of subpages to place vertically.
        /// </summary>
        /// <returns>The amount of pages to place vertically.</returns>
        public int CalculateVerticalPageCount() {
            return this.orientation == PageOrientation.Portrait ? GetHigherDivisor() : GetLowerDivisor();
        }

        private int GetHigherDivisor() {
            int div = GetNextEven(this.pageBisections);
            return div == 0 ? 1 : div;
        }

        private int GetLowerDivisor() {
            int div = GetPreviousEven(this.pageBisections);
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
