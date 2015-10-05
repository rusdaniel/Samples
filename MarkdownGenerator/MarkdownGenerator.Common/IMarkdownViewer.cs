using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownGenerator
{
    public interface IMarkdownViewer
    {
        void DisplayDoc(string source);
    }
}
