using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPdfMerge.Pdf;

namespace EasyPdfMerge.iText {
    class PdfMerger {
        internal void MergePdfs(string[] files, PdfConfiguration config) {
            printConfig(files, config);
        }

        private void printConfig(string[] files, PdfConfiguration config) {
            Console.WriteLine("Files to process: ");
            foreach (string file in files) {
                Console.WriteLine("  " + file);
            }
            Console.WriteLine("Configuration:");
            Console.WriteLine("  Orientation    : " + config.orientation);
            Console.WriteLine("  Page bisections: " + config.pageBisections);
        }
    }
}
