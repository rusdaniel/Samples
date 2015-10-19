namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;

    public class HtmlHeader2 : HtmlElement
    {
        private const string format = "<h2>{0}</h2>";

        private Header2 header;

        public HtmlHeader2(Header2 header)
        {
            this.header = header;
        }

        public string GetContent()
        {
            return string.Format(format, this.header.Text);
        }
    }
}
