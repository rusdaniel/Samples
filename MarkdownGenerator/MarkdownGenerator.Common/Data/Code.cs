
namespace MarkdownGenerator.Common.Data
{
    public class Code : MdElement
    {
        private const string codeFormat = "<pre><code>{0}</code></pre>";

        public Code(string text)
            : base(text)
        { }

        public override string ToString()
        {
            return string.Format(codeFormat, base.text);
        }
    }
}
