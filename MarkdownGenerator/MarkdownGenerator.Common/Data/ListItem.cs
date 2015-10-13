namespace MarkdownGenerator.Common.Data
{
    using System;
    using System.Text;

    [Serializable]
    public class ListItem : MdElement
    {
        //contains list of ListItemComponent
        private const string itemFormat = "<li>{0}</li>";

        public ListItem(string text)
            : base(text)
        { }

        public override string ToString()
        {
            return string.Format(itemFormat, this.ConcatenateComponents());
        }

        public void AddMdElement(MdElement elem)
        {
            base.subElements.Add(elem);
        }

        private string ConcatenateComponents()
        {
            StringBuilder result = new StringBuilder();
            base.subElements.ForEach(item => result.Append(item.ToString()));

            return result.ToString();
        }
    }
}
