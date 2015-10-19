namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;

    public class HtmlHeader6 : HtmlElement
    {
        private const string format = "<h6>{0}</h6>";

        private Header6 header;

        public HtmlHeader6(Header6 header)
        {
            this.header = header;
        }

        public string GetContent()
        {
            return string.Format(format, this.header.Text);
        }
    }
}
