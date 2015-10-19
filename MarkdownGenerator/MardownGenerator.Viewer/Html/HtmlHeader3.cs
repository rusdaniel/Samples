namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;

    public class HtmlHeader3 : HtmlElement
    {
        private const string format = "<h3>{0}</h3>";

        private Header3 header;

        public HtmlHeader3(Header3 header)
        {
            this.header = header;
        }

        public string GetContent()
        {
            return string.Format(format, this.header.Text);
        }
    }
}
