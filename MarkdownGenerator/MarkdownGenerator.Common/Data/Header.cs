namespace MarkdownGenerator.Common.Data
{
    using System;
    using System.Collections.Generic;

    public enum HeaderType
    {
        H1 = 0,
        H2,
        H3,
        H4,
        H5,
        H6
    }

    [Serializable]
    public class Header : MdElement
    { 
        public HeaderType HeaderType { get; private set; }

        public Header(string text, HeaderType type)
            : base(text)
        {
            this.HeaderType = type;
        }
    }
}
