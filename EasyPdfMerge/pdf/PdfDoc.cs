using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace EasyPdfMerge.pdf
{
    class PdfDoc
    {
        public string file { get; set; }
        public XPdfForm doc { get; set; }

        public PdfDoc(string file, XPdfForm doc) {
            this.file = file;
            this.doc  = doc;
        }
    }
}
