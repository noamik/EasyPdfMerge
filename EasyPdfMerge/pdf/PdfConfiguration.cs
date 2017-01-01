using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;

namespace EasyPdfMerge.pdf {
    class PdfConfiguration {

        private static PdfConfiguration instance = new PdfConfiguration();

        public string file { get; set; }
        public PageOrientation orientation { get; set; }
        public int pageBisections { get; set; }

        private PdfConfiguration() {
            // prevent creation of public constructor
            pageBisections = 0;
            orientation = PageOrientation.Portrait;
            file = Path.Combine(Directory.GetCurrentDirectory(), "output.pdf");
        }

        public static PdfConfiguration getInstance() {
            return instance;
        }
    }
}
