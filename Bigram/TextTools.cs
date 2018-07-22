using System.Text.RegularExpressions;

namespace Bigram_VAE
{
    class TextTools
    {
        public string RemoveParentheses(string strInput)
        {
            strInput = Regex.Replace(strInput, "[({})[\\]]", "");
            return strInput;
        }
        public string RemovePeriod(string strInput)
        {
            if (strInput.Contains("."))
            {
                Regex rgx = new Regex(@"\.+[abc]");
                if (!rgx.IsMatch(strInput))
                {
                    strInput = Regex.Replace(strInput, @"\.+", string.Empty);
                }
            }
            return strInput;
        }
        public string Apostrophes(string striInput)
        {
            Regex rgx = new Regex(@"\w+[n]+[']+[t]");
            if (rgx.IsMatch(striInput)) return striInput;
            rgx = new Regex(@"\w+[']+[m]");
            if (rgx.IsMatch(striInput)) return striInput;
            rgx = new Regex(@"\w+[']+[r]+[e]");
            if (rgx.IsMatch(striInput)) return striInput;
            rgx = new Regex(@"\w+[']+[s]");
            if (rgx.IsMatch(striInput)) return striInput;
            return Regex.Replace(striInput, "[']", " ");
        }
        public string RemoveDoubleQoute(string strInput)
        {
            Regex rgx = new Regex("\"");
            if (rgx.IsMatch(strInput))
            {
                strInput = Regex.Replace(strInput, "['\"]", " ");
            }
            return strInput;
        }
        public string RemoveDash(string strInput)
        {
            if (strInput.StartsWith("-"))
            {
                    strInput = Regex.Replace(strInput, "[-]", "");
            }
            return strInput;
        }
    }
}
