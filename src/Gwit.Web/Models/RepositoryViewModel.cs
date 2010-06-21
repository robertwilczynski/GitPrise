﻿using System;
using GitSharp;

namespace Gwit.Web.Models
{
    public class RepositoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public Commit CurrentCommit { get; set; }
    }
}