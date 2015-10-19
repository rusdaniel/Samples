namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Interfaces;
    using System;
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
            this.FormatHeader();
            this.FormatBody();
            this.FormatFooter();
        }

        private void FormatFooter()
        {
            this.docBuilder.AppendLine("</html>");
        }

        private void FormatHeader()
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
