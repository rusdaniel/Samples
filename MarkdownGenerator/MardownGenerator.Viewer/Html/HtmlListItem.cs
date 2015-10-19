namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
    using System.Text;

    public class HtmlListItem : HtmlElement
    {
        private const string format = "<li>{0}</li>";

        private ListItem listItem;

        private StringBuilder builder;

        public HtmlListItem(ListItem listItem)
        {
            this.listItem = listItem;
            this.builder = new StringBuilder();
        }

        public string GetContent()
        {
            this.FormatListItem();
            return string.Format(format, builder.ToString());
        }

        private void FormatListItem()
        {
            this.listItem.SubElements.ForEach(elem =>
            {
                var htmlElem = HtmlElementFactory.CreateHtmlElement(elem);
                this.builder.Append(htmlElem.GetContent());
            });
        }
    }
}
