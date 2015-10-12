namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Parser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class HeaderState
    {
        protected StringBuilder header;

        public HeaderState()
        {
            this.header = new StringBuilder();
        }

        protected abstract void AddHeader(ParserStateMachine sm);

        protected bool ShouldChangeState(char input)
        {
            return input.Equals('#') && this.header.Length == 0;
        }

        protected bool ShouldIgnoreChar(char input)
        {
            return (input.Equals(' ') && this.header.Length == 0) ||
                input.Equals('#');
        }

        protected void OnHeaderComplete(char input, ParserStateMachine sm)
        {
            if (input.Equals('\n'))
            {
                this.AddHeader(sm);
                sm.NextState = new EntryState();
            }
        }

    }
}
