using MarkdownGenerator.Parser;

namespace MarkdownGenerator.ParserStates
{
    public interface ISignalState
    {
        void ProcessChar(char input, ParserStateMachine sm, ISignalState initialState);
    }
}
