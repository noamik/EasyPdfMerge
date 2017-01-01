using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace EasyPdfMerge.pdf {
    class PageConfiguration {

        public XRect[] BoxConfigurations { get; set; }
        public Boolean Rotate { get; set; }

        public PageConfiguration(int pageDivisions) {
            int subPages = 2 ^ pageDivisions;
            BoxConfigurations = new XRect[subPages];
        }
    }
}
