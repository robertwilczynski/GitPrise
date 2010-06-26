using System;

namespace GitPrise.Core.SyntaxHighlighting
{
    public interface IHighlightingService
    {
        string GenerateHtml(string data, string fileName, object options);
    }
}
