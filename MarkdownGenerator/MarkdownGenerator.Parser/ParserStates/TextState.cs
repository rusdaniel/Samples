namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;
    using System.Text;

    public class TextState : ISignalState
    {
        private ISignalSubElemState initialState;

        private StringBuilder textBuilder;

        public TextState(ISignalSubElemState initialState)
        {
            this.initialState = initialState;
            this.textBuilder = new StringBuilder();
        }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            this.OnTextElemCompleted(input, sm);
            this.textBuilder.Append(input);
        }

        private void OnTextElemCompleted(char input, ParserStateMachine sm)
        {
            var isLinkItem = input.Equals('[') || input.Equals('<');
            var isTextCompleted = input.Equals('\n') || input.Equals('\r');
            var isCodeItem = input.Equals('`');
            this.AddLineTerminators(isTextCompleted);

            if (isTextCompleted || isLinkItem || isCodeItem)
            {
                sm.NextState = initialState;
                this.initialState.OnSubElementCompleted(
                    new TextItem(this.textBuilder.ToString()));
                sm.NextState.ProcessChar(input, sm);
            }
        }


        private void AddLineTerminators(bool shouldAdd)
        {
            if (shouldAdd)
            {
                this.textBuilder.Append("\r\n");
            }
        }
    }
}
