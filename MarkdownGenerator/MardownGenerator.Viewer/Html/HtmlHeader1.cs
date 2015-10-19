namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;

    public class HtmlHeader1 : HtmlElement
    {
        private const string format = "<h1>{0}</h1>";

        private Header1 header;

        public HtmlHeader1(Header1 header)
        {
            this.header = header;
        }

        public string GetContent()
        {
            return string.Format(format, this.header.Text);
        }
    }
}
