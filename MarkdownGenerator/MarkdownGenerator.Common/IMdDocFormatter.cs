namespace MarkdownGenerator.Interfaces
{
    using System.IO;

    public interface IDocument
    {
        Stream GetContent();
    }
}
