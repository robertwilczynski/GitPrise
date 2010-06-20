using System;
using GitSharp;

namespace Gwit.Core.Services
{
    public interface IRepositoryResolver
    {
        Repository GetRepository(string name);
    }
}
