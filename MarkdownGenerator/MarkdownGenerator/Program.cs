using MarkdownGenerator.Interfaces;
using MarkdownGenerator.Parser;
using MarkdownGenerator.Viewer;
using System.IO;

namespace MarkdownGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                var path = args[0];
                using (var fs = File.OpenRead(path))
                {
                    var parser = new ParserStateMachine();
                    var outputStream = parser.Parse(fs);
                    var viewer = new MDViewer();
                    viewer.DisplayDoc(outputStream);
                }

            }
        }
    }
}
