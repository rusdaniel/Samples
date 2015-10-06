using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkdownGenerator.Parser;
using System.IO;

namespace MarkdownGenerator.Parser.UnitTests
{
    [TestClass]
    public class ParserUnitTests
    { 
        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}
