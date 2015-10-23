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
        private StringBuilder docBuilder;

        private IEnumerable<MdElement> mdElements;

        public HtmlDocument(MdDoc mdDoc)
        {
            this.docBuilder = new StringBuilder();
            this.mdElements = mdDoc.RootElem.MdElements;
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
            this.docBuilder.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"Data/StyleSheets/MdStylesheet.css\" />");
            this.docBuilder.AppendLine("<script src=\"http://ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js\"></script>");
            this.docBuilder.AppendLine("<script type=\"text/javascript\" src=\"Data/theme.js\"></script>");
            this.docBuilder.AppendLine("</head>");
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
            this.docBuilder.AppendLine("<div>");
            this.docBuilder.AppendLine("<button id=\"themeABtn\">Theme A</button>");
            this.docBuilder.AppendLine("<button id=\"themeBBtn\">Theme B</button>");
            this.docBuilder.AppendLine("<button id=\"themeCBtn\">Theme C</button>");
            this.docBuilder.AppendLine("</div>");
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
