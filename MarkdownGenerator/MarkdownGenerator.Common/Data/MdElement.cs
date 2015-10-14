namespace MarkdownGenerator.Common.Data
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class MdElement
    {
        public string Text { get; protected set; }

        public List<MdElement> SubElements { get; protected set; }

        public MdElement(string text)
        {
            this.Text = text;
            this.SubElements = new List<MdElement>();
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
