using System;
using System.Collections.Generic;
using GitSharp;
using System.Linq;
using System.Web.Routing;
using GitPrise.Core.Web.Mvc;

namespace GitPrise.Web.Models
{
    public class CommitsViewModel : RepositoryNavigationViewModelBase
    {
        private List<Commit> List { get; set; }

        public IList<IGrouping<DateTime, Commit>> Grouping { get; private set; }

        public void AddCommit(Commit commit)
        {
            List.Add(commit);
        }

        public void AddCommits(IList<Commit> commits)
        {
            List.AddRange(commits);
        }

        public CommitsViewModel(Repository repository, RequestContext context)
            : this (repository, context.GetRepositoryName(), context.GetTreeish())
        {

        }

        public CommitsViewModel(Repository repository, string repositoryName, string treeish)
            : base(repository, repositoryName, treeish)
        {
            List = new List<Commit>();
        }

        public void ApplyGrouping()
        {
            Grouping = new List<IGrouping<DateTime, Commit>>(
                List
                    .GroupBy(x => x.CommitDate.Date)
                    .OrderByDescending(x => x.Key)
            );                
        }
    }

    
}
