using MarkdownGenerator.Parser;
using System;

namespace MarkdownGenerator.ParserStates
{
    public class EntryState : ISignalState
    {
        public void ProcessChar(char input, ParserStateMachine sm, ISignalState initialState)
        {
            if (char.IsDigit(input))
            {
                sm.NextState = new OLState();
                return;
            }

            switch (input)
            {
                case '#':
                    sm.NextState = new Header1State();
                    break;
                case '`':
                    sm.NextState = new CodeState();
                    break;
                case '*':
                case '+':
                case '-':
                    sm.NextState = new ULState();
                    break;
                case ' ':
                case '\n':
                    break;
                default:
                    sm.NextState = new ParagraphState();
                    break;
            }
        }
    }
}
