namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class LinkItem : MdElement
    { 
        public string Id { get; private set; }

        public LinkItem(string text, string id)
            : base(text)
        {
            this.Id = id;
        }
    }
}
