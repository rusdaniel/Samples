namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class LinkItem : MdElement
    {
        private const string format = "<a href=\"{0}\">{1}</a>";

        public LinkItem(string text) : base(text) { }

        public override string ToString()
        {
            return string.Format(format, base.text);
        }
    }
}
