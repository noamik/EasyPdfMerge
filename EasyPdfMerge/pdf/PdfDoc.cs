using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;

namespace EasyPdfMerge.pdf
{
    class PdfDoc
    {
        public string file { get; set; }
        public PdfDocument doc { get; set; }

        public PdfDoc(string file, PdfDocument doc) {
            this.file = file;
            this.doc  = doc;
        }
    }
}
