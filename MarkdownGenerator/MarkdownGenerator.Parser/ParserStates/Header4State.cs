using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Parser;

namespace MarkdownGenerator.ParserStates
{
    public class Header4State : HeaderState, ISignalState
    {
        public void ProcessChar(char input, ParserStateMachine sm, ISignalState initialState)
        {
            if (this.ShouldChangeState(input))
            {
                sm.NextState = new Header5State();
            }
            else
            {
                if (ShouldIgnoreChar(input))
                {
                    return;
                }
                else
                {
                    this.OnHeaderComplete(input, sm);
                    this.header.Append(input);
                }
            }
        }

        protected override void AddHeader(ParserStateMachine sm)
        {
            sm.MdDoc.AddHeader(
                new Header(this.header.ToString(), HeaderType.H4));
        }
    }
}
