using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkdownGenerator.Parser;

namespace MarkdownGenerator.Parser.UnitTests
{
    [TestClass]
    public class HeadersParserTests
    {
        [TestMethod]
        public void Given_TextContainingAtxHeaders1_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "### Header 3\n";
            HeadersParser.ParseHeaders(ref inputText);
            Assert.IsTrue(string.Compare(
                    @"<h3>Header 3</h3>", 
                    inputText.Replace("\n", string.Empty), 
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingAtxHeaders2_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "########## Header 3\n";
            HeadersParser.ParseHeaders(ref inputText);
            Assert.IsTrue(string.Compare(
                    @"<h6>#### Header 3</h6>",
                    inputText.Replace("\n", string.Empty),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingAtxHeaders3_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "#### Header 3 ####\n";
            HeadersParser.ParseHeaders(ref inputText);
            Assert.IsTrue(string.Compare(
                    @"<h4>Header 3</h4>",
                    inputText.Replace("\n", string.Empty),
                    StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Given_TextContainingSetexHeaders_When_Parsed_Then_IsCorrectlyFormatted()
        {
            var inputText = "Header 1\n==";
            HeadersParser.ParseHeaders(ref inputText);
            Assert.IsTrue(string.Compare(@"<h1>Header 1</h1>", inputText, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
