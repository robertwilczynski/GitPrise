using System;
using System.Collections.Generic;
using GitSharp;

namespace Gwit.Core.GitSharp
{
    public class CommitHarvester
    {
        private Commit _root;
        private DateTime? _commitTimeTreshold;
        private int _minCommits;
        private int _counter;
        private List<Commit> _results = new List<Commit>();

        public CommitHarvester(Commit root, DateTime? commitTimeTreshold, int minCommits)
        {
            _minCommits = minCommits;
            _commitTimeTreshold = commitTimeTreshold;
            _root = root;

        }

        private void Collect(Commit commit)
        {
            if (commit.CommitDate < _commitTimeTreshold && _counter > _minCommits)
            {
                return;
            }

            _results.Add(commit);
            _counter += 1;
            if (!commit.HasParents)
            {
                return;
            }

            foreach (var parent in commit.Parents)
            {
                Collect(parent);
            }
        }

        public IList<Commit> Collect()
        {
            _counter = 0;
            _results.Clear();
            Collect(_root);
            return _results;
        }

    }
}
