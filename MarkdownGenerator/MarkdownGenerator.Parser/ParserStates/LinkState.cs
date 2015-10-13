namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;
    using System.Text;

    public class LinkState : ISignalState
    {
        private StringBuilder id;

        private StringBuilder link;

        private StringBuilder builder;

        private ISignalSubElemState initialState;

        public LinkState(ISignalSubElemState initialState)
        {
            this.id = new StringBuilder();
            this.link = new StringBuilder();
            this.builder = this.id;
            this.initialState = initialState;
        }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            this.OnLinkIdCompleted(input);
            this.OnLinkCompleted(input, sm);

            if (ShouldAddChar(input))
            {
                this.builder.Append(input);
            }
        }

        private static bool ShouldAddChar(char input)
        {
            return !input.Equals(']') && !input.Equals('<');
        }

        private void OnLinkCompleted(char input, ParserStateMachine sm)
        {
            if (input.Equals('>'))
            {
                sm.NextState = this.initialState;
                this.initialState.OnSubElementCompleted(
                    new LinkItem(
                        this.link.Length > 0 ? this.link.ToString() : this.id.ToString(),
                        this.id.ToString()));
            }
        }

        private void OnLinkIdCompleted(char input)
        {
            if (input.Equals(']'))
            {
                this.builder = this.link;
            }
        }


    }
}
