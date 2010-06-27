using System;

namespace GitPrise.Web.Models
{
    public class BranchViewModel
    {
        public string Name { get; set; }
        public RepositoryNavigationRequest Navigation { get; set; }

        public BranchViewModel(string name, RepositoryNavigationRequest navigation)
        {
            Name = name;
            Navigation = navigation;
        }
    }
}
