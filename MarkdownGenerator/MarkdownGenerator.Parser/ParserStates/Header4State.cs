using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Parser;

namespace MarkdownGenerator.ParserStates
{
    public class Header4State : HeaderState, ISignalState
    {
        public void ProcessChar(char input, ParserStateMachine sm)
        {
            if (this.ShouldChangeState(input))
            {
                sm.NextState = new Header5State();
            }
            else if (!ShouldIgnoreChar(input))
            {
                this.OnHeaderComplete(input, sm);
                this.header.Append(input);
            }
        }

        protected override void AddHeader(ParserStateMachine sm)
        {
            sm.MdDoc.AddHeader(
                new Header4(this.header.ToString()));
        }
    }
}
