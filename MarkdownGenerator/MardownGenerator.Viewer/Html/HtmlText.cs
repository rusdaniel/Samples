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
        private const string format = "{0} ";

        public TextItem textItem;

        public HtmlText(TextItem textItem)
        {
            this.textItem = textItem;
        }

        public string GetContent()
        {
            return string.Format(format, this.textItem.Text);
        }
    }
}
