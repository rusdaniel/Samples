namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class OrderedList : MdElement
    {
        private const string listFormat = "<ol>{0}</ol>";

        public OrderedList(string text)
            : base(text) { }

        public void AddListItem(ListItem item)
        {
            base.SubElements.Add(item);
        }
    }
}
