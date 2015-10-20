namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;

    public class HtmlLink : HtmlElement
    {
        private const string format = "<a href=\"{0}\">{1}</a> ";

        private LinkItem link;

        public HtmlLink(LinkItem link)
        {
            this.link = link;
        }

        public string GetContent()
        {
            return string.Format(format, link.Text, link.Id);
        }
    }
}
