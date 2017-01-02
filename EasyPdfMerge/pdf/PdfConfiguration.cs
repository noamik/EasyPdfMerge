using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;

namespace EasyPdfMerge.pdf {
    class PdfConfiguration {

        public string file { get; set; }
        public PageOrientation orientation { get; set; }
        public int pageBisections { get; set; }

        public int GetSubPageCount() {
            return pageBisections + 1;
        }

        public PdfConfiguration() {
            // prevent creation of public constructor
            pageBisections = 0;
            orientation = PageOrientation.Portrait;
            file = Path.Combine(Directory.GetCurrentDirectory(), "output.pdf");
        }
    }
}
