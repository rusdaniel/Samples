
namespace MarkdownGenerator.ParserStates
{
    using MarkdownGenerator.Common.Data;

    public interface ISignalSubElemState : ISignalState
    {
        void OnSubElementCompleted(MdElement subElem);
    }
}
