namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class LinkItem : MdElement
    {
        private const string format = "<a href=\"{0}\">{1}</a>";

        private string id;

        public LinkItem(string text, string id)
            : base(text)
        {
            this.id = string.IsNullOrWhiteSpace(id) ? text : id;
        }

        public override string ToString()
        {
            return string.Format(format, base.text, id);
        }
    }
}
