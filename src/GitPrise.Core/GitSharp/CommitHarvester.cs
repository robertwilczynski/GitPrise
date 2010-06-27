#region License

// Copyright 2010 Robert Wilczynski (http://github.com/robertwilczynski/gitprise)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using GitSharp;

namespace GitPrise.Core.GitSharp
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
