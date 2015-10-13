using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Interfaces;
using MarkdownGenerator.ParserStates;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MarkdownGenerator.Parser
{
    public class ParserStateMachine : IParser
    {
        public ISignalState NextState { get; set; }

        public MdDoc MdDoc { get; set; }

        public ParserStateMachine()
        {
            this.NextState = new EntryState();
            this.MdDoc = new MdDoc();
        }

        public Stream Parse(Stream inputData)
        {
            using (var reader = new StreamReader(inputData))
            {
                while (!reader.EndOfStream)
                {
                    this.NextState
                        .ProcessChar((char)reader.Read(), this);
                }
            }

            return this.MdDocToStream();
        }

        private Stream MdDocToStream()
        {
            var formatter = new BinaryFormatter();
            var result = new MemoryStream();
            formatter.Serialize(result, this.MdDoc);

            return result;
        }
    }
}
