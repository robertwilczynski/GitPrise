using System;
using GitSharp;
using System.Collections.Generic;

namespace Gwit.Web.Models
{
    public class RepositoryNavigationViewModelBase
    {
        public string RepositoryName { get; private set; }
        public string CommitId { get; private set; }
        public Commit CurrentCommit { get; set; }
        public List<string> Branches { get; set; }
        public List<string> Tags { get; set; }

        private RepositoryNavigationViewModelBase()
        {
            Branches = new List<string>();
            Tags = new List<string>();
        }

        public RepositoryNavigationViewModelBase(Repository repository, string name, string treeish)
            : this()
        {            
            RepositoryName = name;
            CommitId = treeish;
            FillFromRepository(repository);
        }

        public void FillFromRepository(Repository repository)
        {
            Branches.AddRange(repository.Branches.Keys);
            Tags.AddRange(repository.Tags.Keys);
            var obj = repository.Get<AbstractObject>(CommitId);

            if (obj.IsTag)
            {
                obj = (obj as Tag).Target;
            }

            if (obj.IsCommit)
            {
                CurrentCommit = obj as Commit;
            }

            //else if (obj is AbstractTreeNode)
            //{
            //    CurrentCommit = (obj as AbstractTreeNode).GetLastCommit();
            //}
        }
    }
}
