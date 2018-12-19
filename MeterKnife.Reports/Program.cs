using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Spire.Pdf;
using Font = iTextSharp.text.Font;

namespace MeterKnife.Reports
{
    internal class Program
    {
        private const string Stshi =
            "我是你河边上破旧的老水车\r\n数百年来纺着疲惫的歌\r\n我是你额上熏黑的矿灯\r\n照你在历史的隧洞里蜗行摸索\r\n我是干瘪的稻穗\r\n是失修的路基\r\n是淤滩上的驳船\r\n把纤绳深深勒进你的肩膊\r\n";

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Console.WriteLine($"=={DateTime.Now.ToShortTimeString()}===============");

            Fonts.Initialise();
//            foreach (string fontname in FontFactory.RegisteredFonts)
//                Console.Write($"[{fontname}]  ");
            BuildPdfPrint();

            Console.WriteLine("==End===============");
            Console.ReadKey();
        }

        public static void BuildPdfPrint()
        {
            var path = @"z:\hx-report.pdf";
            using (var stream = new FileStream(path, FileMode.Create))
            {
                using (var doc = new Document(PageSize.A4))
                {
                    #region 设置PDF的头信息，一些属性设置，在Document.Open 之前完成

                    doc.AddAuthor("作者幻想Zerow");
                    doc.AddCreationDate();
                    doc.AddCreator("创建人幻想Zerow");
                    doc.AddSubject("Dot Net 使用 itextsharp 类库创建PDF文件的例子");
                    doc.AddTitle("此PDF由幻想Zerow创建，嘿嘿");
                    doc.AddKeywords("ASP.NET,PDF,iTextSharp,幻想Zerow");
                    //自定义头 
                    doc.AddHeader("Expires", "0");

                    #endregion
                    var writer = PdfWriter.GetInstance(doc, stream);
                    //1.6版本
                    writer.PdfVersion = PdfWriter.VERSION_1_6;
                    //没有密码，但只能打开和打印
                    writer.SetEncryption(null, null, PdfWriter.AllowCopy | PdfWriter.AllowPrinting, true);
                    //设置阅读器的参数。单列显示，不显示大纲和缩略图
                    writer.ViewerPreferences = PdfWriter.FitWindow | PdfWriter.PageLayoutOneColumn | PdfWriter.PageModeUseNone;
                    writer.PageEvent = new HeaderFootEvent();
                    doc.Open();
                    FromHtml(doc, writer);
                    doc.Close();
                }
            }
            Spire.Pdf.PdfDocument pdfDoc = new Spire.Pdf.PdfDocument();
            pdfDoc.LoadFromFile(path);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = pdfDoc.PrintDocument;
            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            PrintDialog dialogPrint = new PrintDialog();
            dialogPrint.AllowPrintToFile = true;
            dialogPrint.AllowSomePages = true;
            dialogPrint.PrinterSettings.MinimumPage = 1;
            dialogPrint.PrinterSettings.MaximumPage = pdfDoc.Pages.Count;
            dialogPrint.PrinterSettings.FromPage = 1;
            dialogPrint.PrinterSettings.ToPage = pdfDoc.Pages.Count;
            if (dialogPrint.ShowDialog() == DialogResult.OK)
            {
                pdfDoc.PrintFromPage = dialogPrint.PrinterSettings.FromPage;
                pdfDoc.PrintToPage = dialogPrint.PrinterSettings.ToPage;
                pdfDoc.PrinterName = dialogPrint.PrinterSettings.PrinterName;
                PrintDocument printDoc = pdfDoc.PrintDocument;
                dialogPrint.Document = printDoc;
                printDoc.Print();
            }
        }

        public static void FromHtml(Document doc, PdfWriter writer)
        {
            try
            {
                var tmplt = Path.Combine(Application.StartupPath, "Datas/Tmplts/calibration-report.html");
                var css = Path.Combine(Application.StartupPath, "Datas/Tmplts/calibration-report.css");
                using (var tmpltFile = new FileStream(tmplt, FileMode.Open))
                {
                    using (var cssFile = new FileStream(css, FileMode.Open))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, tmpltFile, cssFile, Encoding.UTF8, new UnicodeFontFactory());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private class HeaderFootEvent : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                var header = "温湿度环境实验设备校准记录";
                var footer = $"第{writer.CurrentPageNumber}页, 共{writer.PageNumber}页";
                var font = FontFactory.GetFont("微软雅黑", BaseFont.IDENTITY_H, false, 8, 0, BaseColor.BLACK);
                var canvas = writer.DirectContent;
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase(header, font), document.Left, document.Top + 4, 0);
                ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase(footer, font), document.Left, document.Bottom - 12, 0);
                canvas.SetLineWidth(0.5f); // Make a bit thicker than 1.0 default
                canvas.MoveTo(document.Left, document.Top + 2);
                canvas.LineTo(document.Right, document.Top + 2);
                canvas.Stroke();
                canvas.MoveTo(document.Left, document.Bottom - 3);
                canvas.LineTo(document.Right, document.Bottom - 3);
                canvas.Stroke();
            }
        }

        public class UnicodeFontFactory : FontFactoryImp
        {
            public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
            {
                if (string.IsNullOrWhiteSpace(fontname))
                    fontname = "微软雅黑";
                return FontFactory.GetFont(fontname, BaseFont.IDENTITY_H, false, size, 0, color, cached);
            }
        }
    }
}