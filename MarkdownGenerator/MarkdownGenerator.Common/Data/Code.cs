namespace MarkdownGenerator.Common.Data
{
    using System;

    [Serializable]
    public class Code : MdElement
    {
        public Code(string text)
            : base(text)
        { }
    }
}
