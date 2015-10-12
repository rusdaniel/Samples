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
        private ParserStateMachine parser;

        [TestInitialize]
        public void Initialize()
        {
            this.parser = new ParserStateMachine();
        }

        #region Header tests

        [TestMethod]
        public void Given_TextContainingHeader1_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1#\n";
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
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h1>Header 1</h1>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingMultipleHeader1_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1\n\n\n# Another header#\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 2, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h1>Another header</h1>",
                    mdDoc.GetHeaders().Last().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader2_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "## This is a header#\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h2>This is a header</h2>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader3_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "### This is a header\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h3>This is a header</h3>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader4_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "#### This is a header#\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h4>This is a header</h4>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader5_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "##### This is a header####\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h5>This is a header</h5>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader6_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "###### This is a header####\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h6>This is a header</h6>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingDifferentHeaders_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1\n\n\n###### Another header#####\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetHeaders().Count == 2, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<h1>Header 1</h1>",
                    mdDoc.GetHeaders().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The first header is not correct");
            Assert.IsTrue(string.Compare(
                    @"<h6>Another header</h6>",
                    mdDoc.GetHeaders().Last().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The second header is not correct");
        }

        #endregion

        #region Code tests

        [TestMethod]
        public void Given_TextContainingCode_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "```this is a piece of code```\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetCodeElements().Count == 1, "The number of containing code elements is incorrect");
            Assert.IsTrue(string.Compare(
                    @"<pre><code>this is a piece of code</code></pre>",
                    mdDoc.GetCodeElements().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingMultipleCodeElements_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "```this is a piece of code```\n\n\n``\nList<InputField> list = (from i .... select i).Cast<IDataField>.ToList();\n``\n";
            var outputStream = parser.Parse(GenerateStreamFromString(inputText));

            var mdDoc = StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.GetCodeElements().Count == 2, "The number of containing code elements is incorrect");
            Assert.IsTrue(string.Compare(
                    "<pre><code>this is a piece of code</code></pre>",
                    mdDoc.GetCodeElements().First().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The first code sample is not correct");

            Assert.IsTrue(string.Compare(
                     "<pre><code>\nList<InputField> list = (from i .... select i).Cast<IDataField>.ToList();\n</code></pre>",
                    mdDoc.GetCodeElements().Last().ToString(),
                    StringComparison.InvariantCultureIgnoreCase) == 0, "the second code sample is not correct");
        }

        #endregion

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
