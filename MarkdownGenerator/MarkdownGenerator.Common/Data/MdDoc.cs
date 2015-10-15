namespace MarkdownGenerator.Common.Data
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class RootDocElem
    {
        public IEnumerable<MdElement> MdElements { get; set; }
    }

    [Serializable]
    public class MdDoc
    {
        private List<MdElement> elements;

        public RootDocElem RootElem
        {
            get
            {
                return new RootDocElem() { MdElements = this.elements };
            }
        }

        public MdDoc()
        {
            elements = new List<MdElement>();
        }

        //todo restrict the possibility to add any MdElement as a Header
        public void AddHeader(MdElement header)
        {
            this.elements.Add(header);
        }

        public void AddCode(Code code)
        {
            this.elements.Add(code);
        }

        public void AddOrderedList(OrderedList list)
        {
            this.elements.Add(list);
        }

        public void AddParagraph(Paragraph paragraph)
        {
            this.elements.Add(paragraph);
        }
    }
}
