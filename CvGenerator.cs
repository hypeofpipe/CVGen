using System;
using HandlebarsDotNet;
using Microsoft.Win32;

namespace CVGen
{
    public class CvGenerator
    {
        public void ConvertJSONToCV(object data, string template)
        {
            var renderedTemplate = RenderCV(data, template);
            var PDF = HTMLToPDF(renderedTemplate);
            SaveToPDF(PDF);
        }

        public string RenderCV(object data, string templateSource)
        {
            var template = Handlebars.Compile(templateSource);
            return template(data);
        }

        public IronPdf.PdfDocument HTMLToPDF(string HTML)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderHtmlAsPdf(HTML);
            return PDF;
        }

        private void SaveToPDF(IronPdf.PdfDocument doc)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if(saveFileDialog.ShowDialog() == true)
            {
                doc.SaveAs(saveFileDialog.FileName);
            }

        }
    }
}
