using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace MarkdownGenerator.Parser.UnitTests
{
    [TestClass]
    public class ParagraphsParserUnitTests
    {
        [TestMethod]
        public void Given_SeveralLinesText_When_Parsing_Then_ParagraphsAreCorrectlyFormated()
        {
            var inputStream = GenerateStreamFromString("\n\n\nthis is a paragraph\nwith two lines\n\n");
            var parser = new ParagraphsParser(inputStream);
            var result = GenerateStringFromStream(parser.ParseParagraphs());
            Assert.IsTrue(result.Equals("\r\n\r\n\r\n<p>this is a paragraph\r\nwith two lines\r\n</p>\r\n"));
        }

        [TestMethod]
        public void Given_MultipleParagraphsText_When_Parsing_Then_ParagraphsAreCorrectlyFormated()
        {
            var inputStream = GenerateStreamFromString("this is a paragraph\nwith two lines\n\n\nanother paragraph\n\nlast paragraph");
            var parser = new ParagraphsParser(inputStream);
            var result = GenerateStringFromStream(parser.ParseParagraphs());
            Assert.IsTrue(result.Equals("<p>this is a paragraph\r\nwith two lines\r\n</p>\r\n\r\n<p>another paragraph\r\n</p>\r\n<p>last paragraph\r\n</p>\r\n"));
        }

        [TestMethod]
        public void Given_EmptyText_When_Parsing_Then_ParagraphsAreCorrectlyFormated()
        {
            var inputStream = GenerateStreamFromString(string.Empty);
            var parser = new ParagraphsParser(inputStream);
            var result = GenerateStringFromStream(parser.ParseParagraphs());
            Assert.IsTrue(result.Equals(string.Empty));
        }

        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        private static string GenerateStringFromStream(Stream inputStream)
        {
            StringBuilder result = new StringBuilder();
            using (var reader = new StreamReader(inputStream))
            {
                while (!reader.EndOfStream)
                {
                    result.AppendLine(reader.ReadLine());
                }
            }

            return result.ToString();
        }
    }
}
