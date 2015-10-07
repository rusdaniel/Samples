using MarkdownGenerator.Interfaces;
using SimpleInjector;
using Generator;


namespace MarkdownGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Register<IParser, MarkdownGenerator.Parser.Parser>();
        }
    }
}
