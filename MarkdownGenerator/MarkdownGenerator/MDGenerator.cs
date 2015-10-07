using System.IO;
using MarkdownGenerator.Interfaces;

namespace Generator
{
    public class MDGenerator
    {
        private Stream inputData;
        private IParser parser;
        private IMarkdownViewer viewer;

        public MDGenerator(Stream inputData, IParser parser, IMarkdownViewer viewer)
        {
            this.inputData = inputData;
            this.parser = parser;
            this.viewer = viewer;
        }

        private void GenerateHtml()
        {
            var parsedData = this.parser.Parse(inputData);
            this.viewer.DisplayDoc(parsedData);
        }

    }
}
