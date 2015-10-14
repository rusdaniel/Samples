namespace MarkdownGenerator.Interfaces
{
    using MarkdownGenerator.Common.Data;
    using System.IO;

    public interface IMdDocFormatter
    {
        Stream FormatMdDoc(MdDoc document);

        string FileName { get; }
    }
}
