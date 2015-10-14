namespace MarkdownGenerator.Parser.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class HeaderParserUnitTests
    {
        private ParserStateMachine parser;

        [TestInitialize]
        public void Initialize()
        {
            this.parser = new ParserStateMachine();
        }

        [TestMethod]
        public void Given_TextContainingHeader1_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1#\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "Header 1",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader1WithNewLine_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "Header 1",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingMultipleHeader1_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1\n\n\n# Another header#\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 2, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "Another header",
                    mdDoc.RootElem.MdElements.Last().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader2_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "## This is a header#\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "This is a header",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader3_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "### This is a header\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "This is a header",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader4_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "#### This is a header#\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "This is a header",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader5_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "##### This is a header####\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "This is a header",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingHeader6_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "###### This is a header####\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "This is a header",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingDifferentHeaders_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "# Header 1\n\n\r\n###### Another header#####\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 2, "The number of containing headers is incorrect");
            Assert.IsTrue(string.Compare(
                    "Header 1",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The first header is not correct");
            Assert.IsTrue(string.Compare(
                    "Another header",
                    mdDoc.RootElem.MdElements.Last().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The second header is not correct");
        }
    }
}
