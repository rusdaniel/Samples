namespace MarkdownGenerator.Parser.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class CodeParserUnitTests
    {
        private ParserStateMachine parser;

        [TestInitialize]
        public void Initialize()
        {
            this.parser = new ParserStateMachine();
        }

        [TestMethod]
        public void Given_TextContainingCode_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "```this is a piece of code```\r\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing code elements is incorrect");
            Assert.IsTrue(string.Compare(
                    "this is a piece of code\r\n",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingMultipleCodeElements_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "```this is a piece of code```\r\n\r\n\r\n``\r\nList<InputField> list = (from i .... select i).Cast<IDataField>.ToList();\r\n``\r\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 2, "The number of containing code elements is incorrect");
            Assert.IsTrue(string.Compare(
                    "this is a piece of code\r\n",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The first code sample is not correct");

            Assert.IsTrue(string.Compare(
                     "\r\nList<InputField> list = (from i .... select i).Cast<IDataField>.ToList();\r\n\r\n",
                    mdDoc.RootElem.MdElements.Last().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "the second code sample is not correct");
        }
    }
}
