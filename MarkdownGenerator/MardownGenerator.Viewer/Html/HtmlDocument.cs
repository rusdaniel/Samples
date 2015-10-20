namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class HtmlDocument : IDocument
    {
        private const string cssRefFormat = "<link rel=\"stylesheet\" type=\"text/css\" href=\"Data/StyleSheets/{0}\" />";

        private StringBuilder docBuilder;

        private IEnumerable<MdElement> mdElements;

        private CssSettings cssSettings;

        public HtmlDocument(MdDoc mdDoc)
        {
            this.docBuilder = new StringBuilder();
            this.mdElements = mdDoc.RootElem.MdElements;
            this.cssSettings = new CssSettings();
        }

        public Stream GetContent()
        {
            this.FormatDocument();
            return GenerateStreamFromString(this.docBuilder.ToString());
        }

        private void FormatDocument()
        {
            this.SetDocType();
            this.FormatHeader();
            this.FormatBody();
            this.CloseDocument();
        }

        private void FormatHeader()
        {
            this.docBuilder.AppendLine("<head>");
            this.docBuilder.AppendLine("<meta charset=\"UTF-8\">");
            this.SetStylesheet();
            this.docBuilder.AppendLine("</head>");
        }

        private void SetStylesheet()
        {
            var settings = this.cssSettings.GetCssSettings();
            this.docBuilder.AppendFormat(cssRefFormat, settings.CssFileName).AppendLine();
        }

        private void CloseDocument()
        {
            this.docBuilder.AppendLine("</html>");
        }

        private void SetDocType()
        {
            this.docBuilder.AppendLine("<!DOCTYPE html>");
            this.docBuilder.AppendLine("<html>");
        }

        private void FormatBody()
        {
            this.docBuilder.AppendLine("<body>");
            this.FormatMdElements();
            this.docBuilder.AppendLine("</body>");
        }

        private void FormatMdElements()
        {
            this.mdElements.ToList().ForEach(elem =>
            {
                var htmlElem = HtmlElementFactory.CreateHtmlElement(elem);
                this.docBuilder.Append(htmlElem.GetContent());
                this.docBuilder.AppendLine();
            });
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
