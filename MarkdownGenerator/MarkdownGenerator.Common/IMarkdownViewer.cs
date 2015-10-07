using System.IO;

namespace MarkdownGenerator.Interfaces
{
    public interface IMarkdownViewer
    {
        void DisplayDoc(Stream source);
    }
}
