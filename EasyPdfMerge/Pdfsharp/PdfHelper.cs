using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace EasyPdfMerge.Pdf {
    class PdfHelper
    {
    //    private static PdfHelper instance = new PdfHelper();

    //    private PdfHelper() {
    //        // prevents creation of public constructor
    //    }

    //    public static PdfHelper getInstance() {
    //        return instance;
    //    }

    //    public void mergePdf(String[] files) {
    //        string baseFileName;
    //        foreach (var nextFile in files) {
    //            baseFileName = System.IO.Path.GetFileName(nextFile);
    //            Console.Out.Write("File: " + nextFile + " filename: " + baseFileName + "\r\n");
               
    //        }
            
    //    }

    //    public void saveDocument(PdfDocument doc, string file) {
    //        doc.Save(file);
    //        Process.Start(file);
    //    }

    //    public PdfDocument concatenatePdfs(PdfDoc[] docs) {
    //        PdfDocument outputDocument = new PdfDocument();
    //        foreach (PdfDoc doc in docs) {
    //            // Iterate pages
    //            int count = doc.doc.PageCount;
    //            for (int idx = 0; idx < count; idx++) {
    //                // Get the page from the external document...
    //                PdfPage page = doc.doc.Pages[idx];
    //                // ...and add it to the output document.
    //                outputDocument.AddPage(page);
    //            }
    //        }
    //        return outputDocument;
    //    }

    //    public PdfDoc[] readPdfDocs(String[] files) {
    //        PdfDoc[] tempFiles = new PdfDoc[files.Count()];
    //        int i = 0;
    //        string baseFileName;
    //        string outputFile;
    //        foreach (var nextFile in files) {
    //            baseFileName = System.IO.Path.GetFileName(nextFile);
    //            outputFile = Path.Combine(getTemporaryOutputDirectory(), baseFileName);
    //            PdfDocument doc = PdfReader.Open(nextFile, PdfDocumentOpenMode.Import);
    //            tempFiles[i] = new PdfDoc(outputFile, doc);
    //            i++;
    //        }
    //        return tempFiles;
    //    }

    //    public PdfDoc[] prepareMultiPageDocs(String[] files, PageOrientation orientation)
    //    {
    //        PdfDoc[] tempFiles = new PdfDoc[files.Count()];
    //        string baseFileName;
    //        string outputFile;
    //        int i = 0;
    //        foreach (var nextFile in files) {
    //            baseFileName = System.IO.Path.GetFileName(nextFile);
    //            outputFile   = Path.Combine(getTemporaryOutputDirectory(), baseFileName);
    //            Console.Out.Write("File: " + nextFile + " filename: " + baseFileName + "\r\n");

    //            PdfDocument outputDocument = createMultiPageDoc(nextFile, orientation);
    //            MemoryStream tempStream = new MemoryStream();
    //            outputDocument.Save(tempStream, false);
    //            PdfDocument inputDocument = PdfReader.Open(tempStream, PdfDocumentOpenMode.Import);

    //            //outputDocument.Save(outputFile);
    //            tempFiles[i] = new PdfDoc(outputFile, inputDocument); // outputFile;
    //            i++;
    //            tempStream.Close();
    //            outputDocument.Close();
    //            //Process.Start(outputFile);
    //        }
    //        return tempFiles;
    //    }

    //    private PdfDocument createMultiPageDoc(string file, PageOrientation orientation) {
    //        string tempFile            = createTempFileCopy(file);
    //        Console.Out.Write("Created temp file: " + tempFile + "\r\n");
    //        PdfDocument outputDocument = new PdfDocument();
    //        XPdfForm form              = getNewFormFromFile(tempFile);

    //        outputDocument.PageLayout = PdfPageLayout.SinglePage;
    //        for (int idx = 0; idx < form.PageCount; idx += 2)
    //        {
    //            addMultiPage(outputDocument, form, idx, orientation);
    //        }

    //        /*// Save the document...
    //        string filename = "BV_Literaturbeispiele-2on1.pdf";
    //        outputDocument.Save(filename);
    //        // ...and start a viewer.
    //        Process.Start(filename); */
    //        return outputDocument;
    //    }

    //    private void addMultiPage(PdfDocument outputDocument, XPdfForm origDoc, int pageNum, PageOrientation orientation) {

    //        // Add a new page to the output document
    //        PdfPage page = createMultiPage(outputDocument, orientation);

    //        XGraphics gfx = XGraphics.FromPdfPage(page);

    //        // Set page number (which is one-based)
    //        origDoc.PageNumber = pageNum + 1;

    //        gfx.DrawImage(origDoc, getMultiPageBox(page, orientation, 1));

    //        if (pageNum + 1 < origDoc.PageCount)
    //        {
    //            // Set page number (which is one-based)
    //            origDoc.PageNumber = pageNum + 2;

    //            gfx.DrawImage(origDoc, getMultiPageBox(page, orientation, 2));
    //        }
    //    }

    //    private XRect getMultiPageBox(PdfPage page, PageOrientation orientation, int subPageNumber) {
    //        XRect box;
    //        if (subPageNumber == 1) {
    //            if (orientation == PageOrientation.Portrait) {
    //                box = new XRect(0, 0, page.Width, page.Height / 2);
    //            } else {
    //                box = new XRect(0, 0, page.Width / 2, page.Height);
    //            }
    //        } else if (subPageNumber == 2) {
    //            if (orientation == PageOrientation.Portrait) {
    //                box = new XRect(0, page.Height / 2, page.Width, page.Height / 2);
    //            } else {
    //                box = new XRect(page.Width / 2, 0, page.Width / 2, page.Height);
    //            }
    //        } else {
    //            box = new XRect(0, 0, 0, 0);
    //        }
    //        return box;
    //    }

    //    private PdfPage createMultiPage(PdfDocument outputDocument, PageOrientation orientation) {
    //        // Add a new page to the output document
    //        PdfPage page = outputDocument.AddPage();
    //        page.Orientation = orientation;
    //        int rotate = page.Elements.GetInteger("/Rotate");
    //        return page;
    //    }

    //    private XPdfForm getNewFormFromFile(string file) {
    //        XFont font = new XFont("Verdana", 8, XFontStyle.Bold);
    //        XStringFormat format = new XStringFormat();
    //        format.Alignment = XStringAlignment.Center;
    //        format.LineAlignment = XLineAlignment.Far;


    //        // Open the external document as XPdfForm object
    //        XPdfForm form = XPdfForm.FromFile(file);
    //        return form;
    //    }

    //    private string createTempFileCopy(string file) {
    //        string tempFile = Path.Combine(getTemporaryCopyDirectory(), System.IO.Path.GetFileName(file));
    //        File.Copy(Path.Combine(file), tempFile, true);
    //        return tempFile;
    //    }

    //    private string getTemporaryOutputDirectory() {
    //        string tempDirectory = Path.Combine(Path.GetTempPath(), "PdfHelper", "pdfoutput");
    //        if (!Directory.Exists(tempDirectory)) {
    //            Directory.CreateDirectory(tempDirectory);
    //        }
    //        return tempDirectory;
    //    }

    //    private string getTemporaryCopyDirectory() {
    //        string tempDirectory = Path.Combine(Path.GetTempPath(), "PdfHelper", "pdfcopies");
    //        if (!Directory.Exists(tempDirectory)) {
    //            Directory.CreateDirectory(tempDirectory);
    //        }
    //        return tempDirectory;
    //    }

    //    private string getTemporaryDirectory() {
    //        string tempDirectory = Path.Combine(Path.GetTempPath(), "PdfHelper");
    //        if (!Directory.Exists(tempDirectory)) {
    //            Directory.CreateDirectory(tempDirectory);
    //        }
    //        return tempDirectory;
    //    }

    //    public void mergePdfPortrait() {
    //        // Get a fresh copy of the sample PDF file
    //        string filename = "BV_Literaturbeispiele.pdf";
    //        File.Copy(Path.Combine("C:/Users/Bla/Dropbox/Ulli/toMerge/", filename),
    //          Path.Combine(Directory.GetCurrentDirectory(), filename), true);

    //        // Create the output document
    //        PdfDocument outputDocument = new PdfDocument();

    //        // Show single pages
    //        // (Note: one page contains two pages from the source document)
    //        outputDocument.PageLayout = PdfPageLayout.SinglePage;

    //        XFont font = new XFont("Verdana", 8, XFontStyle.Bold);
    //        XStringFormat format = new XStringFormat();
    //        format.Alignment = XStringAlignment.Center;
    //        format.LineAlignment = XLineAlignment.Far;
    //        XGraphics gfx;
    //        XRect box;

    //        // Open the external document as XPdfForm object
    //        XPdfForm form = XPdfForm.FromFile(filename);

    //        for (int idx = 0; idx < form.PageCount; idx += 2)
    //        {
    //            // Add a new page to the output document
    //            PdfPage page = outputDocument.AddPage();
    //            page.Orientation = PageOrientation.Portrait;
    //            double width = page.Width;
    //            double height = page.Height;

    //            int rotate = page.Elements.GetInteger("/Rotate");

    //            gfx = XGraphics.FromPdfPage(page);

    //            // Set page number (which is one-based)
    //            form.PageNumber = idx + 1;

    //            box = new XRect(0, 0, width, height / 2);
    //            // Draw the page identified by the page number like an image
    //            gfx.DrawImage(form, box);

    //            // Write document file name and page number on each page
    //            //box.Inflate(0, -10);
    //            //gfx.DrawString(String.Format("- {1} -", filename, idx + 1),
    //            //  font, XBrushes.Red, box, format);

    //            if (idx + 1 < form.PageCount)
    //            {
    //                // Set page number (which is one-based)
    //                form.PageNumber = idx + 2;

    //                box = new XRect(0, height / 2, width, height / 2);
    //                // Draw the page identified by the page number like an image
    //                gfx.DrawImage(form, box);

    //                // Write document file name and page number on each page
    //                //box.Inflate(0, -10);
    //                //gfx.DrawString(String.Format("- {1} -", filename, idx + 2),
    //                //  font, XBrushes.Red, box, format);
    //            }
    //        }

    //        // Save the document...
    //        filename = "BV_Literaturbeispiele-2on1.pdf";
    //        outputDocument.Save(filename);
    //        // ...and start a viewer.
    //        Process.Start(filename);
    //    }
    } 
}
