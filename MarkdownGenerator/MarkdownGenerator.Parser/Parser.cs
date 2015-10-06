using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkdownGenerator.Interfaces;
using System.IO;

namespace MarkdownGenerator.Parser
{
    public class Parser// : IParser
    {
        private Stream inputData;

        public Parser(Stream stream)
        {
            this.inputData = stream;
        }

        public string Parse()
        {
            StringBuilder result = new StringBuilder();
            this.ParseHeaders(result);

            return result.ToString();
        }

        private void ParseHeaders(StringBuilder result)
        {
        }
    }
}
