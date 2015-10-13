namespace MarkdownGenerator.Common.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            return this.elements.OfType<Header>().ToList();
        }

        public void AddCode(Code code)
        {
            this.elements.Add(code);
        }

        public List<Code> GetCodeElements()
        {
            return this.elements.OfType<Code>().ToList();
        }

        public void AddOrderedList(OrderedList list)
        {
            this.elements.Add(list);
        }

        public List<OrderedList> GetOrderedLists()
        {
            return this.elements.OfType<OrderedList>().ToList();
        }
    }
}
