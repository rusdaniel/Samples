namespace MarkdownGenerator.Common.Data
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class MdDoc
    {
        private List<MdElement> elements;

        public MdDoc()
        {
            elements = new List<MdElement>();
        }

        public void AddHeader(Header header)
        {
            this.elements.Add(header);
        }

        public List<Header> GetHeaders()
        {
            var result = new List<Header>();
            this.elements.ForEach(elem =>
                {
                    if (elem is Header)
                    {
                        result.Add(elem as Header);
                    }
                });

            return result;
        }
    }
}
