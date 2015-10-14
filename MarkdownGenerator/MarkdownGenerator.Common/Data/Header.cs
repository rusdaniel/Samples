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
        private Dictionary<HeaderType, string> headerFormats = new Dictionary<HeaderType, string>()
        {
            {HeaderType.H1, "<h1>{0}</h1>"},
            {HeaderType.H2, "<h2>{0}</h2>"},
            {HeaderType.H3, "<h3>{0}</h3>"},
            {HeaderType.H4, "<h4>{0}</h4>"},
            {HeaderType.H5, "<h5>{0}</h5>"},
            {HeaderType.H6, "<h6>{0}</h6>"}
        };

        private string headerFormat;

        public Header(string text, HeaderType type)
            : base(text)
        {
            this.headerFormat = headerFormats[type];
        }

        public override string ToString()
        {
            return string.Format(this.headerFormat, base.Text);
        }
    }
}
