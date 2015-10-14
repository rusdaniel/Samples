namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class OrderedList : MdElement
    {
        public OrderedList(string text)
            : base(text) { }

        public void AddListItem(ListItem item)
        {
            base.SubElements.Add(item);
        }
    }
}
