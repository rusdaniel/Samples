using MarkdownGenerator.Interfaces;
using System.Diagnostics;
using System.IO;

namespace MardownGenerator.Viewer
{
    public class MDViewer : IMarkdownViewer
    {
        private const string fileName = "markdownDoc.html";
        private string filePath;

        public MDViewer()
        {
            this.filePath = Path.Combine(Path.GetTempPath(), fileName);
        }

        public void DisplayDoc(Stream source)
        {
            this.SaveDataToFile(source);
            Process.Start(this.filePath);
        }

        private void SaveDataToFile(Stream data)
        {
            using (var fs = File.Create(this.filePath))
            {
                data.Seek(0, SeekOrigin.Begin);
                data.CopyTo(fs);
            }
        }
    }
}
