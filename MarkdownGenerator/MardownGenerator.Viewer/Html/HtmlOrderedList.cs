namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
    using System.Text;

    public class HtmlOrderedList : HtmlElement
    {
        private const string format = "<ol>{0}</ol>";

        private StringBuilder builder;

        private OrderedList olList;

        public HtmlOrderedList(OrderedList olList)
        {
            this.builder = new StringBuilder();
            this.olList = olList;
        }

        public string GetContent()
        {
            this.FormatOrderedList();
            return string.Format(format, this.builder.ToString());
        }

        private void FormatOrderedList()
        {
            this.olList.SubElements.ForEach(elem =>
            {
                var htmlElem = HtmlElementFactory.CreateHtmlElement(elem);
                this.builder.Append(htmlElem.GetContent());
            });
        }
    }
}
