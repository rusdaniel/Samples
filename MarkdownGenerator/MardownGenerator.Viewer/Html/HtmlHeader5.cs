namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;

    public class HtmlHeader5 : HtmlElement
    {
        private const string format = "<h5>{0}</h5>";

        private Header5 header;

        public HtmlHeader5(Header5 header)
        {
            this.header = header;
        }

        public string GetContent()
        {
            return string.Format(format, this.header.Text);
        }
    }
}
