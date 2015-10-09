
using System.Collections.Generic;
namespace MarkdownGenerator.Common.Data
{
    public class MdElement
    {
        protected string text;

        protected List<MdElement> subElements;

        public MdElement(string text)
        {
            this.text = text;
            this.subElements = new List<MdElement>();
        }

        public override string ToString()
        {
            return this.text;
        }
    }
}
