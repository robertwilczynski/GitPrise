using System;
using GitSharp;
using System.IO;

namespace Gwit.Web.Models
{
    public class ChangeViewModel
    {
        public Change Change { get; set; }
        public Diff Diff { get; set; }
        public string Name { get; set; }

        public ChangeViewModel(Change change, Diff diff)
        {
            Diff = diff;
            Change = change;
            Name = Path.GetFileName(change.Path);
        }
    }
}
