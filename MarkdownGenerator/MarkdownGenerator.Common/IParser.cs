using System.IO;

namespace MarkdownGenerator.Interfaces
{
    public interface IParser
    {
        Stream Parse(Stream inputData);
    }
}
