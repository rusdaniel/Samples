namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;

    public class Header3State : HeaderState, ISignalState
    {
        public void ProcessChar(char input, ParserStateMachine sm)
        {
            if (this.ShouldChangeState(input))
            {
                sm.NextState = new Header4State();
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
                new Header3(this.header.ToString()));
        }
    }
}
