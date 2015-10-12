namespace MarkdownGenerator.Parser.UnitTests
{
    using MarkdownGenerator.Common.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    [TestClass]
    public class ParserUnitTests
    {

        [TestMethod]
        public void Given_TextContainingHeader1_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1#\n";
            var parser = new ParserStateMachine();
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h1>Header 1</h1>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader1WithNewLine_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1\n";
            var parser = new ParserStateMachine();
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h1>Header 1</h1>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
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

        private static MdDoc StreamToMdDoc(Stream data)
        {
            var formatter = new BinaryFormatter();
            data.Seek(0, SeekOrigin.Begin);
            var result = formatter.Deserialize(data);

            return (MdDoc)result;
        }
    }
}
