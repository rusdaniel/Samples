namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class LinkItem : MdElement
    {
        private const string format = "<a href=\"{0}\">{1}</a>";

        public string Id { get; private set; }

        public LinkItem(string text, string id)
            : base(text)
        {
            this.Id = id;
        }
    }
}
