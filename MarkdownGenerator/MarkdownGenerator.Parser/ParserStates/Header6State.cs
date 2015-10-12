using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Parser;
using System;

namespace MarkdownGenerator.ParserStates
{
    public class Header6State : HeaderState, ISignalState
    {
        public void ProcessChar(char input, ParserStateMachine sm, ISignalState initialState)
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

        protected override void AddHeader(ParserStateMachine sm)
        {
            sm.MdDoc.AddHeader(
                new Header(this.header.ToString(), HeaderType.H6));
        }
    }
}
