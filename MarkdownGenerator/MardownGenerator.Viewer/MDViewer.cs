using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Interfaces;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace MarkdownGenerator.Viewer
{
    public class MDViewer : IMarkdownViewer
    {
        private string filePath;

        private IMdDocFormatter formatter;

        public MDViewer()
        {
            this.formatter = new HtmlFormatter();
            this.filePath = Path.Combine(Path.GetTempPath(), this.formatter.FileName);
        }

        public void DisplayDoc(Stream source)
        {
            var formattedStream = this.formatter.FormatMdDoc(
                this.GetMarkdownDoc(source));
            this.SaveDataToFile(formattedStream);

            this.ViewDoc();
        }

        private void ViewDoc()
        {
            var viewerThread = new Thread(
                new ThreadStart(() =>
                {
                    var process = new Process();
                    process.StartInfo.FileName = this.filePath;
                    //process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }));

            viewerThread.Start();
        }

        private MdDoc GetMarkdownDoc(Stream source)
        {
            var bf = new BinaryFormatter();
            source.Seek(0, SeekOrigin.Begin);
            return (MdDoc)bf.Deserialize(source);
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
