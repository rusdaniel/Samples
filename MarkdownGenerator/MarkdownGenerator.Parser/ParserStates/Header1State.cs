using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Parser;

namespace MarkdownGenerator.ParserStates
{
    public class Header1State : HeaderState, ISignalState
    {
        public Header1State() : base() { }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            if (this.ShouldChangeState(input))
            {
                sm.NextState = new Header2State();
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
                new Header1(this.header.ToString()));
        }
    }
}
