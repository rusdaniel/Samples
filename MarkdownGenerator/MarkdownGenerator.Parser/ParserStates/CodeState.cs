﻿namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;
    using MarkdownGenerator.Parser;
    using System.Text;

    public class CodeState : ISignalState
    {
        private StringBuilder code;

        private char previousChar;

        private ISignalSubElemState initialState;

        public CodeState(ISignalSubElemState initialState = null)
        {
            this.code = new StringBuilder();
            this.previousChar = ' ';
            this.initialState = initialState;
        }

        public void ProcessChar(char input, ParserStateMachine sm)
        {
            if (this.initialState == null)
            {
                this.OnCodeCompleted(input, sm);
            }
            else
            {
                this.OnSubElemCompleted(input, sm);
            }

            this.AppendChar(input);
        }

        private void AppendChar(char input)
        {
            if (!ShouldIgnoreChar(input))
            {
                this.code.Append(input);
            }

            this.previousChar = input;
        }

        private void OnSubElemCompleted(char input, ParserStateMachine sm)
        {
            if (previousChar.Equals('`') &&
                 (input.Equals(' ') || input.Equals('\n')) &&
                 this.code.Length > 0)
            {
                sm.NextState = this.initialState;
                this.initialState.OnSubElementCompleted(
                    new Code(this.code.ToString()));
                this.initialState.ProcessChar(input, sm);
            }
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
