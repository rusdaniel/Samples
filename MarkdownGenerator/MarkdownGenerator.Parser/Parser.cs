using MarkdownGenerator.Interfaces;
using System.IO;

namespace MarkdownGenerator.Parser
{
    public class Parser : IParser
    {
        public Parser() { }

        public Stream Parse(Stream inputData)
        {
            var paragraphsParser = new ParagraphsParser(inputData);
            return paragraphsParser.ParseParagraphs();
        }
    }
}
