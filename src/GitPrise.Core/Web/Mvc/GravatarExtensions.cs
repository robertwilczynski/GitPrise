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
using GitPrise.Core.Web.Utils.Gravatar;
using System.Web.Mvc;
using GitPrise.Core.Web.Utils;

namespace GitPrise.Core.Web.Mvc
{
    public static class GravatarExtensions
    {
        public static string Gravatar(this UrlHelper helper, string email, int size = 32, GravatarDefault def = GravatarDefault.Default, GravatarRating rating = GravatarRating.G)
        {
            return GravatarUrl.ForEmail(email).Size(size).Default(def).WithRating(rating);
        }

    }
}
