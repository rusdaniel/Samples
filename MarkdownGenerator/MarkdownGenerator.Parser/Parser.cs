using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownGenerator.Interfaces;
using System.IO;

namespace MarkdownGenerator.Parser
{
    public class Parser : IParser
    {
        private Stream inputData;

        public Parser(Stream stream)
        {
            this.inputData = stream;
        }

        public Stream Parse()
        {
            var paragraphsParser = new ParagraphsParser(this.inputData);
            return paragraphsParser.ParseParagraphs();
        }
    }
}
