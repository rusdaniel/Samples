namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;

    public class OLState : ISignalSubElemState
    {
        private char previousChar;

        private OrderedList orderedList;

        private ListItem listItem;

        public OLState()
        {
            this.listItem = new ListItem(string.Empty);
            this.orderedList = new OrderedList(string.Empty);
            this.previousChar = ' ';
        }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            this.OnListCompleted(input, sm);
            this.OnListItemCompleted(input, sm);
            this.previousChar = input;
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
        }

        public void OnSubElementCompleted(MdElement subElem)
        {
            this.listItem.AddMdElement(subElem);
        }

        private void OnListItemCompleted(char input, ParserStateMachine sm)
        {
            if (input.Equals('.') && char.IsDigit(previousChar))
            {
                this.orderedList.AddListItem(this.listItem);
                this.listItem = new ListItem(string.Empty);
            }
        }

        private void OnListCompleted(char input, ParserStateMachine sm)
        {
            if (input.Equals('\n') && previousChar.Equals('\n'))
            {
                this.orderedList.AddListItem(this.listItem);
                sm.MdDoc.AddOrderedList(this.orderedList);
                sm.NextState = new EntryState();
            }
        }

        private static bool ShouldParseChar(char input)
        {
            return !input.Equals('.') &&
                !input.Equals(' ') &&
                !char.IsDigit(input);
        }
    }
}
