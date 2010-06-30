using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using Moq;

namespace GitPrise.Core.Tests.Web
{
    public class HttpRequestMock : Mock<HttpRequestBase>
    {
        readonly NameValueCollection _form = new NameValueCollection();
        readonly NameValueCollection _querystring = new NameValueCollection();
        readonly Stream _inputStream = new MemoryStream(new byte[8192]);

        /// <summary>
        /// Default Constructor
        /// </summary>
        public HttpRequestMock()
        {
            SetupProperty(x => x.ContentType);
            Setup(x => x.QueryString).Returns(_querystring);
            Setup(x => x.ApplicationPath).Returns("/");
            Setup(x => x.Form).Returns(_form);
            Setup(x => x.InputStream).Returns(_inputStream);
            SetupProperty(x => x.ContentType);
        }
    }
}
