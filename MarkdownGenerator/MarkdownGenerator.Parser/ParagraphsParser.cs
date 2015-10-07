using System;
using System.IO;
using System.Text;

namespace MarkdownGenerator.Parser
{
    public class ParagraphsParser
    {
        private const string openTag = "<p>";
        private const string closeTag = "</p>";

        private Stream inputStream;
        private bool shouldOpenParagraph;
        //private bool shouldCloseParagraph;

        public ParagraphsParser(Stream inputStream)
        {
            this.inputStream = inputStream;
            this.shouldOpenParagraph = true;
            //this.shouldCloseParagraph = false;
        }

        public Stream ParseParagraphs()
        {
            StringBuilder result = new StringBuilder();
            using (var reader = new StreamReader(this.inputStream))
            {
                while (!reader.EndOfStream)
                {
                    this.ParseLine(reader.ReadLine(), result);
                }
            }

            this.AddFinalTag(result);
            return GenerateStreamFromString(result.ToString());
        }

        private void ParseLine(string text, StringBuilder result)
        {
            this.AddOpenParagraphTag(text, result);
            this.AddCloseParagraphTag(text, result);

            result.Append(text).AppendLine();
        }

        private void AddCloseParagraphTag(string text, StringBuilder result)
        {
            if (String.IsNullOrWhiteSpace(text) && !this.shouldOpenParagraph)
            {
                result.Append(closeTag);
                this.shouldOpenParagraph = true;
            }
        }

        private void AddOpenParagraphTag(string text, StringBuilder result)
        {
            if (!String.IsNullOrWhiteSpace(text) && this.shouldOpenParagraph)
            {
                result.Append(openTag);
                shouldOpenParagraph = false;
            }
        }

        private void AddFinalTag(StringBuilder result)
        {
            if (!this.shouldOpenParagraph)
            {
                result.Append(closeTag);
            }
        }

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
