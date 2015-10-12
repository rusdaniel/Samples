namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;
    using System.Text;

    public class CodeState : ISignalState
    {
        private StringBuilder code;

        private char previousChar;

        public CodeState()
        {
            this.code = new StringBuilder();
            this.previousChar = ' ';
        }

        public void ProcessChar(char input, ParserStateMachine sm, ISignalState initialState)
        {
            this.OnCodeCompleted(input, sm);

            if (!ShouldIgnoreChar(input))
            {
                this.code.Append(input);
            }

            this.previousChar = input;
        }

        private void OnCodeCompleted(char input, ParserStateMachine sm)
        {
            if (previousChar.Equals('`') &&
                input.Equals('\n') &&
                this.code.Length > 0)
            {
                sm.MdDoc.AddCode(
                    new Code(this.code.ToString()));
                sm.NextState = new EntryState();
            }
        }

        private static bool ShouldIgnoreChar(char input)
        {
            return input.Equals('`');
        }
    }
}
