using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using Moq;

namespace GitPrise.Core.Tests.Web
{
    public class HttpResponseMock : Mock<HttpResponseBase>
    {
        readonly Stream _outputStream = new MemoryStream(new byte[8192]);
        readonly StreamWriter _writer;
        NameValueCollection _headers = new NameValueCollection();

        public HttpResponseMock()
        {
            _writer = new StreamWriter(_outputStream);
            Setup(x => x.OutputStream).Returns(_outputStream);
            SetupProperty(x => x.ContentType);
            SetupProperty(x => x.StatusCode);
            Setup(x => x.Write(It.IsAny<string>())).Callback(new Action<string>(s => 
            { 
                _writer.Write(s); 
                _writer.Flush(); 
            }));
            _headers = new NameValueCollection();
            Setup(x => x.Headers).Returns(_headers);
        }
    }
}
