namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;
    using System.Collections.Generic;

    public class ParagraphState : ISignalSubElemState
    {
        private Paragraph paragraph;

        private bool isEndPending;

        public ParagraphState()
        {
            this.paragraph = new Paragraph();
        }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            this.OnParagraphCompleted(input, sm);
            if (ShouldParseChar(input))
            {
                switch (input)
                {
                    case '`':
                        sm.NextState = new CodeState(this);
                        break;
                    case '[':
                    case '<':
                        sm.NextState = new LinkState(this);
                        break;
                    default:
                        sm.NextState = new TextState(this);
                        sm.NextState.ProcessChar(input, sm);
                        break;
                }
            }

            isEndPending = input.Equals('\n');
        }

        public void OnSubElementCompleted(MdElement subElem)
        {
            this.paragraph.AddMdElement(subElem);
        }

        private static bool ShouldParseChar(char input)
        {
            return !input.Equals('\r') &&
                !input.Equals('\n');
        }

        private void OnParagraphCompleted(char input, ParserStateMachine sm)
        {
            //we assume that a new line followed by a carriage return means end on list
            if (isEndPending)
            {
                if (input.Equals('\r'))
                {
                    sm.MdDoc.AddParagraph(this.paragraph);
                    sm.NextState = new EntryState();
                }
                else
                {
                    isEndPending = false;
                }
            }
        }
    }
}
