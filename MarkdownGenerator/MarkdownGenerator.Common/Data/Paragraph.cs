namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class Paragraph : MdElement
    {
        public Paragraph() : base(string.Empty) { }

        public void AddMdElement(MdElement elem)
        {
            base.SubElements.Add(elem);
        }
    }
}
