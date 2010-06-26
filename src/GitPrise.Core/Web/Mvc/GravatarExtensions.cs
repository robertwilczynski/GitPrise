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
