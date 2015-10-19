namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;

    public class HtmlHeader4 : HtmlElement
    {
        private const string format = "<h4>{0}</h4>";

        private Header4 header;

        public HtmlHeader4(Header4 header)
        {
            this.header = header;
        }

        public string GetContent()
        {
            return string.Format(format, this.header.Text);
        }
    }
}
