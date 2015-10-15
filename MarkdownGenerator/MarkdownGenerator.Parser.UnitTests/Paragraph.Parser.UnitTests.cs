namespace MarkdownGenerator.Parser.UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using MarkdownGenerator.Common.Data;

    [TestClass]
    public class ParagraphParserUnitTests
    {
        private ParserStateMachine parser;

        [TestInitialize]
        public void Initialize()
        {
            this.parser = new ParserStateMachine();
        }

        [TestMethod]
        public void Given_TextContainingSingleParagraph_WithMultipleElements_When_Parsed_Then_IsCorrectlyParsed()
        {
            var inputText = "Releases are available at <https://nodejs.org/dist/>, listed under\r\n"
                            + "their version string. The <https://nodejs.org/dist/latest/> symlink\r\n"
                            + "will point to the latest release directory. `checked`\r\n\r\n";
            var outputStream = parser.Parse(Converter.GenerateStreamFromString(inputText));

            var mdDoc = Converter.StreamToMdDoc(outputStream);

            Assert.IsTrue(mdDoc.RootElem.MdElements.Count() == 1, "The number of paragraphs is incorrect");
            Assert.IsTrue(mdDoc.RootElem.MdElements.First().SubElements.Count() == 8, "The number of paragraph elements is incorrect");

            var paragraph = mdDoc.RootElem.MdElements.First();
            var linkItems = paragraph.SubElements.OfType<LinkItem>();
            Assert.IsTrue(linkItems.Count() == 2, "The number of link items is incorrect");

            var codeItems = paragraph.SubElements.OfType<Code>();
            Assert.IsTrue(codeItems.Count() == 1, "The number of code items is incorrect");
        }
    }
}
