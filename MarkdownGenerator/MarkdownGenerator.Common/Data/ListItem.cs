
using System.Text;
namespace MarkdownGenerator.Common.Data
{
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

        private string ConcatenateComponents()
        {
            StringBuilder result = new StringBuilder();
            base.subElements.ForEach(item => result.Append(item.ToString()));

            return result.ToString();
        }
    }
}
