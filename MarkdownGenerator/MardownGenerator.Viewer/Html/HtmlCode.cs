namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
    using System.Text;

    public class HtmlCode : HtmlElement
    {
        private const string format = "<code>{0}</code>";

        private Code mdCode;

        private StringBuilder builder;

        public HtmlCode(Code mdCode)
        {
            this.mdCode = mdCode;
            this.builder = new StringBuilder();
        }

        public string GetContent()
        {
            this.FormatCode();
            return builder.ToString();
        }

        private void FormatCode()
        {
            builder.AppendFormat(format, this.mdCode.Text);
            if (this.mdCode.Text.EndsWith("\n"))
            {
                builder.Append("<br>");
            }
        }
    }
}
