using MarkdownGenerator.Common.Data;
using MarkdownGenerator.Parser;
using System.Text;

namespace MarkdownGenerator.ParserStates
{
    public class Header1State : ISignalState
    {
        StringBuilder headerElem;

        private char previousChar;

        public Header1State()
        {
            this.headerElem = new StringBuilder();
            this.previousChar = ' ';
        }

        public void ProcessChar(char input, ParserStateMachine sm, ISignalState initialState)
        {
            if (this.ShouldChangeState(input))
            {
                sm.NextState = new Header2State();
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
                    this.headerElem.Append(input);
                    this.previousChar = input;
                }
            }
        }

        private void AddHeader(ParserStateMachine sm)
        {
            sm.MdDoc.AddHeader(
                new Header(this.headerElem.ToString(), HeaderType.H1));
        }

        private bool ShouldChangeState(char input)
        {
            if (input.Equals('#') && this.headerElem.Length == 0)
            {
                return true;
            }

            return false;
        }

        private bool ShouldIgnoreChar(char input)
        {
            if (input.Equals(' ') && this.headerElem.Length == 0)
            {
                return true;
            }

            return false;
        }

        private void OnHeaderComplete(char input, ParserStateMachine sm)
        {
            if (input.Equals('#') || (input.Equals('n') && previousChar.Equals('\\')))
            {
                if (previousChar.Equals('\\'))
                {
                    this.headerElem.Remove(this.headerElem.Length - 1, 1);
                }
                this.AddHeader(sm);
                sm.NextState = new EntryState();
            }
        }
    }
}
