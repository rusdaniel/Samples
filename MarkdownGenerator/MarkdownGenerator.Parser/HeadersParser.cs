using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkdownGenerator.Parser
{
    public class HeadersParser
    {
        private static Regex SetextHeader = new Regex(@"
                ^(.+?)
                [ ]*
                \n
                (=+|-+)
                [ ]*
                \n+",
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        private static Regex AtxHeader = new Regex(@"^(#{1,6})[ ]*(.+)[ ]*#*",
                RegexOptions.Multiline | RegexOptions.Compiled);

        public static void ParseHeaders(ref string text)
        {
            text = SetextHeader.Replace(text, new MatchEvaluator(SetextHeaderEvaluator));
            text = AtxHeader.Replace(text, new MatchEvaluator(AtxHeaderEvaluator));
        }

        private static string SetextHeaderEvaluator(Match match)
        {
            string header = match.Groups[1].Value;
            int level = match.Groups[2].Value.StartsWith("=") ? 1 : 2;
            return string.Format("<h{1}>{0}</h{1}>\n\n", header, level);
        }

        private static string AtxHeaderEvaluator(Match match)
        {
            string header = match.Groups[2].Value;
            int level = match.Groups[1].Value.Length;
            return string.Format("<h{1}>{0}</h{1}>", header, level);
        }
    }
}
