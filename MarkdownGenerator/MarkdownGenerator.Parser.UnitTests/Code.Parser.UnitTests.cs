namespace MarkdownGenerator.Parser.UnitTests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var inputText = "```this is a piece of code```\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of containing code elements is incorrect");
            Assert.IsTrue(string.Compare(
                    "this is a piece of code",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingMultipleCodeElements_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "```this is a piece of code```\n\n\n``\nList<InputField> list = (from i .... select i).Cast<IDataField>.ToList();\n``\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 2, "The number of containing code elements is incorrect");
            Assert.IsTrue(string.Compare(
                    "this is a piece of code",
                    mdDoc.RootElem.MdElements.First().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The first code sample is not correct");

            Assert.IsTrue(string.Compare(
                     "\nList<InputField> list = (from i .... select i).Cast<IDataField>.ToList();\n",
                    mdDoc.RootElem.MdElements.Last().Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "the second code sample is not correct");
        }
    }
}
