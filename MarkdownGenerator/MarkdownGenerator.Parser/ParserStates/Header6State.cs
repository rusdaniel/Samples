using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Parser;

namespace MarkdownGenerator.ParserStates
{
    public class Header6State : HeaderState, ISignalState
    {
        public void ProcessChar(char input, ParserStateMachine sm)
        {
            if (!ShouldIgnoreChar(input))
            {
                this.OnHeaderComplete(input, sm);
                this.header.Append(input);
            }
        }

        protected override void AddHeader(ParserStateMachine sm)
        {
            sm.MdDoc.AddHeader(
                new Header6(this.header.ToString()));
        }
    }
}
