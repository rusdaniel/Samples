using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownGenerator
{
    public interface IParser
    {
        bool TryParse(string path, out string result);
    }
}
