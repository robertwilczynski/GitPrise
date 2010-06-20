using System;
using GitSharp;

namespace Gwit.Core.Services
{
    public interface IRepositoryResolver
    {
        string GetPath(string name);
        Repository GetRepository(string name);
    }
}
