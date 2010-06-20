using System;

namespace Gwit.SyntaxHighlighting.Pygments
{
    public class PygmentLanguage
    {
        public string LongName;
        public string LookupName;

        public override string ToString()
        {
            return LongName;
        }
    }
}
