namespace MarkdownGenerator.Viewer
{
    using MarkdownGenerator.Common.Data;
    using System;
    using System.Collections.Generic;

    public enum MdElementType
    {
        Code = 0,
        Link,
        ListItem,
        Header1,
        Header2,
        Header3,
        Header4,
        Header5,
        Header6,
        Text,
        Paragraph,
        OrderedList
    }

    public class HtmlElementFactory
    {
        private static Dictionary<Type, MdElementType> mdElements = new Dictionary<Type, MdElementType>()
        {
            {typeof(Code), MdElementType.Code},
            {typeof(LinkItem), MdElementType.Link},
            {typeof(ListItem), MdElementType.ListItem},
            {typeof(Header1), MdElementType.Header1},
            {typeof(Header2), MdElementType.Header2},
            {typeof(Header3), MdElementType.Header3},
            {typeof(Header4), MdElementType.Header4},
            {typeof(Header5), MdElementType.Header5},
            {typeof(Header6), MdElementType.Header6},
            {typeof(Paragraph), MdElementType.Paragraph},
            {typeof(OrderedList), MdElementType.OrderedList},
            {typeof(TextItem), MdElementType.Text}
        };

        private static Dictionary<MdElementType, Func<MdElement, HtmlElement>> htmlElements = new Dictionary<MdElementType, Func<MdElement, HtmlElement>>()
        {
            {MdElementType.Code, e => {return new HtmlCode(e as Code);}},
            {MdElementType.Header1, e => {return new HtmlHeader1(e as Header1);}},
            {MdElementType.Header2, e => {return new HtmlHeader2(e as Header2);}},
            {MdElementType.Header3, e => {return new HtmlHeader3(e as Header3);}},
            {MdElementType.Header4, e => {return new HtmlHeader4(e as Header4);}},
            {MdElementType.Header5, e => {return new HtmlHeader5(e as Header5);}},
            {MdElementType.Header6, e => {return new HtmlHeader6(e as Header6);}},
            {MdElementType.Link, e => {return new HtmlLink(e as LinkItem);}},
            {MdElementType.ListItem, e => {return new HtmlListItem(e as ListItem);}},
            {MdElementType.OrderedList, e => {return new HtmlOrderedList(e as OrderedList);}},
            {MdElementType.Paragraph, e => {return new HtmlParagraph(e as Paragraph);}},
            {MdElementType.Text, e => {return new HtmlText(e as TextItem);}}
        };

        public static HtmlElement CreateHtmlElement(MdElement mdElement)
        {
            var type = mdElement.GetType();
            return htmlElements[mdElements[type]](mdElement);
        }
    }
}
