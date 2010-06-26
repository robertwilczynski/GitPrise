using System;
using GitSharp;

namespace GitPrise.Core.Services
{
    public interface IRepositoryResolver
    {
        Repository GetRepository(string name);
    }
}
