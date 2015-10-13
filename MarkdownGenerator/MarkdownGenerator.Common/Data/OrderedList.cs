namespace MarkdownGenerator.Common.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class OrderedList : MdElement
    {
        private const string listFormat = "<ol>{0}</ol>";

        private List<ListItem> items;

        public OrderedList(string text)
            : base(text)
        {
            this.items = new List<ListItem>();
        }

        public override string ToString()
        {
            return string.Format(listFormat, this.ConcatenateListItems());
        }

        public void AddListItem(ListItem item)
        {
            this.items.Add(item);
        }

        private string ConcatenateListItems()
        {
            StringBuilder result = new StringBuilder();
            items.ForEach(item => result.AppendLine(item.ToString()));

            return result.ToString();
        }
    }
}
