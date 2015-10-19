namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class HtmlText: HtmlElement
    {
        public TextItem textItem;

        private StringBuilder builder;

        public HtmlText(TextItem textItem)
        {
            this.textItem = textItem;
            this.builder = new StringBuilder();
        }

        public string GetContent()
        {
            this.FormatText();
            return this.builder.ToString();
        }

        private void FormatText()
        {
            builder.Append(this.textItem.Text);
            if (textItem.Text.EndsWith("\n"))
            {
                builder.Append("<br>");
            }
        }
    }
}
