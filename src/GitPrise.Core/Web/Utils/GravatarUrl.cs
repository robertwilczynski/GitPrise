using System;
using System.Text;
using GitPrise.Core.Web.Utils.Gravatar;
using System.Security.Cryptography;

namespace GitPrise.Core.Web.Utils
{
    public class GravatarUrl
    {
        private readonly StringBuilder _builder = new StringBuilder(GravatarBaseUrl);
        private const string GravatarBaseUrl = @"http://www.gravatar.com/avatar/";
        private const string DefaultExtension = ".jpg";

        private GravatarDefault _default = GravatarDefault.Default;
        private GravatarRating? _rating;
        private int? _size;
        private bool _withExtension;
        private bool _optionsStarted;
        private string _email;

        public GravatarUrl(string email)
        {
            _email = email;
        }

        public static GravatarUrl ForEmail(string email)
        {
            return new GravatarUrl(email);
        }

        public GravatarUrl WithExtension()
        {
            _withExtension = true;
            return this;
        }

        public GravatarUrl Size(int size)
        {
            if (size < 1 || size > 512)
            {
                throw new ArgumentOutOfRangeException("Expected size between 1 and 512 but got {0} instead.".Fill(size));
            }
            _size = size;
            return this;
        }

        public GravatarUrl WithRating(GravatarRating rating)
        {
            _rating = rating;
            return this;
        }

        public GravatarUrl Default(GravatarDefault @default)
        {
            _default = @default;
            return this;
        }

        private void AppendOption<T>(Func<bool> condition, string paramter, T value)
        {
            if (condition())
            {
                if (!_optionsStarted)
                {
                    _builder.Append('?');
                    _optionsStarted = true;
                }
                {
                    _builder.Append('&');
                }
                _builder.Append(paramter).Append("=").Append(value);
            }
        }

        private static string GetDefault(GravatarDefault value)
        {
            switch (value)
            {
                case GravatarDefault.Identicon:
                    return "identicon";
                case GravatarDefault.Monsterid:
                    return "monsterid";
                case GravatarDefault.Wavatar:
                    return "wavatar";
                case GravatarDefault.NotFound:
                    return "404";
                default:
                    return String.Empty;
            }
        }

        private string Build()
        {
            AppendMD5(_builder, _email);

            if (_withExtension)
            {
                _builder.Append(DefaultExtension);
            }

            AppendOption(() => _size.HasValue, "s", _size.Value);
            AppendOption(() => _rating.HasValue, "r", _rating.Value);
            AppendOption(() => _default != GravatarDefault.Default, "d", GetDefault(_default));

            return _builder.ToString();
        }

        /// <summary>
        /// Appends MD5 hash to the StringBuilder.
        /// </summary>
        /// <param name="input"></param>
        /// <source>http://lifeofajackass.com/gravatarget/</source>
        private static void AppendMD5(StringBuilder builder, string input)
        {
            using (var md5Obj = new MD5CryptoServiceProvider())
            {
                var bytesToHash = Encoding.ASCII.GetBytes(input);
                bytesToHash = md5Obj.ComputeHash(bytesToHash);
                Array.ForEach(bytesToHash, b => builder.Append(b.ToString("x2")));
            }
        }

        public static implicit operator string(GravatarUrl url)
        {
            return url.Build();
        }
    }
}
