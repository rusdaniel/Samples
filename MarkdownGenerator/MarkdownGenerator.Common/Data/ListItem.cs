namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class ListItem : MdElement
    {
        public ListItem(string text)
            : base(text)
        { }

        public void AddMdElement(MdElement elem)
        {
            base.SubElements.Add(elem);
        }
    }
}
