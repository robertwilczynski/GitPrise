using System;
using System.IO;
using Microsoft.Scripting;

namespace Gwit.SyntaxHighlighting.Pygments
{
    class BasicStreamContentProvider : StreamContentProvider
    {
        Stream _stream;

        public BasicStreamContentProvider(Stream stream)
        {
            _stream = stream;
        }
        public override Stream GetStream()
        {
            return _stream;
        }
    }
}
