namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;

    public class ListItemState : ISignalSubElemState
    {
        private const char newListItemChar = '@';
        private const char endListChar = '&';

        private ListItem listItem;
        private ISignalSubElemState initialState;
        private bool isPendingEnd;

        public ListItemState(ISignalSubElemState initialState)
        {
            this.listItem = new ListItem(string.Empty);
            this.initialState = initialState;
            this.isPendingEnd = false;
        }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            this.OnListItemCompleted(input, sm);
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
                        break;
                }
            }

            isPendingEnd = input.Equals('\n');
        }

        public void OnSubElementCompleted(MdElement subElem)
        {
            this.listItem.AddMdElement(subElem);
        }

        private static bool ShouldParseChar(char input)
        {
            return !input.Equals(' ') &&
                !input.Equals('\r') &&
                !input.Equals('\n') &&
                !char.IsDigit(input) &&
                !input.Equals('.');
        }

        private void OnListItemCompleted(char input, ParserStateMachine sm)
        {
            //we assume that a digit on a new line means new list item 
            //we also assume that a new linea followed by a carriage return mean end on list
            if (isPendingEnd)
            {
                if (char.IsDigit(input))
                {
                    sm.NextState = this.initialState;
                    this.initialState.OnSubElementCompleted(this.listItem);
                    sm.NextState.ProcessChar(newListItemChar, sm);
                }
                else if (input.Equals('\r'))
                {
                    sm.NextState = this.initialState;
                    this.initialState.OnSubElementCompleted(this.listItem);
                    sm.NextState.ProcessChar(endListChar, sm);
                }
                else
                {
                    isPendingEnd = false;
                }
            }
        }
    }
}
