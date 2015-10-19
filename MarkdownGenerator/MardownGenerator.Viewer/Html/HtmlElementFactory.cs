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

        public static HtmlElement CreateHtmlElement(MdElement mdElement)
        {
            var type = mdElement.GetType();
            switch (mdElements[type])
            {
                case MdElementType.Code:
                    return new HtmlCode(mdElement as Code);
                case MdElementType.Link:
                    return new HtmlLink(mdElement as LinkItem);
                case MdElementType.ListItem:
                    return new HtmlListItem(mdElement as ListItem);
                case MdElementType.Header1:
                    return new HtmlHeader1(mdElement as Header1);
                case MdElementType.Header2:
                    return new HtmlHeader2(mdElement as Header2);
                case MdElementType.Header3:
                    return new HtmlHeader3(mdElement as Header3);
                case MdElementType.Header4:
                    return new HtmlHeader4(mdElement as Header4);
                case MdElementType.Header5:
                    return new HtmlHeader5(mdElement as Header5);
                case MdElementType.Header6:
                    return new HtmlHeader6(mdElement as Header6);
                case MdElementType.Paragraph:
                    return new HtmlParagraph(mdElement as Paragraph);
                case MdElementType.OrderedList:
                    return new HtmlOrderedList(mdElement as OrderedList);
                case MdElementType.Text:
                    return new HtmlText(mdElement as TextItem);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
