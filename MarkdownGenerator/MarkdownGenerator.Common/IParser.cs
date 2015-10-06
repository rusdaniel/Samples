using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownGenerator.Interfaces
{
    public interface IParser
    {
        bool TryParse(string text, ref string result);
    }
}
