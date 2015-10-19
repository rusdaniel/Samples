namespace MarkdownGenerator.Parser.UnitTests
{
    using MarkdownGenerator.Common.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;

    [TestClass]
    public class OLParserUnitTests
    {
        private ParserStateMachine parser;

        [TestInitialize]
        public void Initialize()
        {
            this.parser = new ParserStateMachine();
        } 

        [TestMethod]
        public void Given_TextContainingOrderedList_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "1. [some link]<https://nodejs.org/api/>\n2. ```parser.Parse(GenerateStreamFromString(inputText)```\n\r\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of Ordered lists is incorrect");
            var orderedList = mdDoc.RootElem.MdElements.First();
            var linkItem = orderedList.SubElements.First().SubElements.First() as LinkItem; 
            Assert.IsTrue(string.Compare(
                    "https://nodejs.org/api/",
                    linkItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The link is not correct");
            Assert.IsTrue(string.Compare(
                    "some link",
                    linkItem.Id,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The link id is not correct");

            var codeItem = orderedList.SubElements.Last().SubElements.First() as Code;
            Assert.IsTrue(string.Compare(
                    "parser.Parse(GenerateStreamFromString(inputText)",
                    codeItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The code is not correct");

        }

        [TestMethod]
        public void Given_TextContainingOrderedList_WithMultipleElementsItems_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "1. [some link]<https://nodejs.org/api/>  `StreamToMdDoc(outputStream)` \n2. ```parser.Parse(GenerateStreamFromString(inputText)``` \n\r\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of Ordered lists is incorrect");
            var orderedList = mdDoc.RootElem.MdElements.First();
            var linkItem = orderedList.SubElements.First().SubElements.First() as LinkItem;
            Assert.IsTrue(string.Compare(
                    "https://nodejs.org/api/",
                    linkItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The link is not correct");
            Assert.IsTrue(string.Compare(
                    "some link",
                    linkItem.Id,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The link id is not correct");

            var codeItem = orderedList.SubElements.First().SubElements.Last() as Code;
            Assert.IsTrue(string.Compare(
                    "StreamToMdDoc(outputStream)",
                    codeItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The code is not correct");
        }

        [TestMethod]
        public void Given_TextContainingOrderedList_WithTextCode_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "1. Unit tests `StreamToMdDoc(outputStream)` for different reasons \r\n2. ```parser.Parse(GenerateStreamFromString(inputText)``` \n\r\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of Ordered lists is incorrect");
            var orderedList = mdDoc.RootElem.MdElements.First();
            var firstTextItem = orderedList.SubElements.First().SubElements.First() as MdElement;
            var secondTextItem = orderedList.SubElements.First().SubElements.Last() as MdElement;
            Assert.IsTrue(string.Compare(
                    "Unit Tests ",
                    firstTextItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The first text is not correct");
            Assert.IsTrue(string.Compare(
                    "for different reasons \r\n",
                    secondTextItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The second text is not correct");

            var codeItem = orderedList.SubElements.First().SubElements[1] as Code;
            Assert.IsTrue(string.Compare(
                    "StreamToMdDoc(outputStream)",
                    codeItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The code is not correct");
        }

        [TestMethod]
        public void Given_TextContainingOrderedList_WithMultilineText_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "1. Unit tests for different reasons\r\nhere is why\r\n2. ```parser.Parse(GenerateStreamFromString(inputText)``` \n\r\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of Ordered lists is incorrect");
            var orderedList = mdDoc.RootElem.MdElements.First();
            var firstTextItem = orderedList.SubElements.First().SubElements.First() as MdElement;
            var secondTextItem = orderedList.SubElements.First().SubElements.Last() as MdElement;
            Assert.IsTrue(string.Compare(
                    "Unit tests for different reasons\r\n",
                    firstTextItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The first text is not correct");
            Assert.IsTrue(string.Compare(
                    "here is why\r\n",
                    secondTextItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The second text is not correct");

            var codeItem = orderedList.SubElements.Last().SubElements.First() as Code;
            Assert.IsTrue(string.Compare(
                    "parser.Parse(GenerateStreamFromString(inputText)",
                    codeItem.Text,
                    StringComparison.InvariantCultureIgnoreCase) == 0, "The code is not correct");
          }
    }
}
