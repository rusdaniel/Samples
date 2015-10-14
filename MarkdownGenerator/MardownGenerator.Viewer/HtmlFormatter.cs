namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class HtmlFormatter : IMdDocFormatter
    {
        private const string fileName = "markdownDoc.html";

        private StringBuilder docBuilder;

        private Dictionary<Type, string> formats = new Dictionary<Type, string>()
        {
            {typeof(Code), "<pre><code>{0}</code></pre>"},
            {typeof(LinkItem), "<a href=\"{0}\">{1}</a>"},
            {typeof(ListItem), "<li>{0}</li>"},
            {typeof(OrderedList), "<ol>{0}</ol>"},
            {typeof(Header1), "<h1>{0}</h1>"},
            {typeof(Header2), "<h2>{0}</h2>"},
            {typeof(Header3), "<h3>{0}</h3>"},
            {typeof(Header4), "<h4>{0}</h4>"},
            {typeof(Header5), "<h5>{0}</h5>"},
            {typeof(Header6), "<h6>{0}</h6>"}
        };

        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        public HtmlFormatter()
        {
            this.docBuilder = new StringBuilder();
        }

        public Stream FormatMdDoc(MdDoc document)
        {
            this.FormatMdElements(document.RootElem.MdElements);
            return GenerateStreamFromString(this.docBuilder.ToString());
        }

        private void FormatMdElements(IEnumerable<MdElement> elements)
        {
            elements.ToList().ForEach(elem =>
            {
                if (elem.SubElements.Any())
                {
                    this.FormatMdElements(elem.SubElements);
                }
                else
                {
                    this.docBuilder.Append(
                        string.Format(this.formats[elem.GetType()], elem.Text));
                }
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
