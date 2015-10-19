namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class TextItem : MdElement
    {
        public TextItem(string text) : base(text) { }
    }
}
