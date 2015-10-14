namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;

    class Header2State : HeaderState, ISignalState
    {
        public Header2State() : base() { }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            if (this.ShouldChangeState(input))
            {
                sm.NextState = new Header3State();
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
                new Header2(this.header.ToString()));
        }
    }
}
