namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;
    using System.Linq;

    public class OLState : ISignalSubElemState
    { 
        private OrderedList orderedList;

        public OLState()
        {
            this.orderedList = new OrderedList(string.Empty);
        }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            this.OnListCompleted(input, sm);
            this.ChangeState(input, sm);
        }

        private void ChangeState(char input, ParserStateMachine sm)
        {
            var newListItemDetected = input.Equals('@');
            if (!this.orderedList.SubElements.Any() || newListItemDetected)
            {
                sm.NextState = new ListItemState(this);
            }
        }

        public void OnSubElementCompleted(MdElement subElem)
        {
            this.orderedList.AddListItem(subElem as ListItem);
        }

        private void OnListCompleted(char input, ParserStateMachine sm)
        {
            var isListCompleted = input.Equals('&');
            if (isListCompleted)
            {
                sm.MdDoc.AddOrderedList(this.orderedList);
                sm.NextState = new EntryState();
            }
        }
    }
}
