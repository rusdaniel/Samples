using MarkdownGenerator.Common.Data;
using System.Text;
namespace MarkdownGenerator.Viewer
{
    public class HtmlParagraph : HtmlElement
    {
        private const string format = "<p>{0}</p>";

        private StringBuilder builder;

        private Paragraph paragraph;

        public HtmlParagraph(Paragraph paragraph)
        {
            this.builder = new StringBuilder();
            this.paragraph = paragraph;
        }

        public string GetContent()
        {
            this.FormatParagraph();
            return string.Format(format, this.builder.ToString());
        }

        private void FormatParagraph()
        {
            this.paragraph.SubElements.ForEach(elem => 
            {
                var htmlElem = HtmlElementFactory.CreateHtmlElement(elem);
                this.builder.Append(htmlElem.GetContent());
            });
        }
    }
}
