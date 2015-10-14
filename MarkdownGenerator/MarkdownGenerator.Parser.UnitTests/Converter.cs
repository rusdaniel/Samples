namespace MarkdownGenerator.Parser.UnitTests
{
    using MarkdownGenerator.Common.Data;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class Converter
    {
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        public static MdDoc StreamToMdDoc(Stream data)
        {
            var formatter = new BinaryFormatter();
            data.Seek(0, SeekOrigin.Begin);
            var result = formatter.Deserialize(data);

            return (MdDoc)result;
        }
    }
}
