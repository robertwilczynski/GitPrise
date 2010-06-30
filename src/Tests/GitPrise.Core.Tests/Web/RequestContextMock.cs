using System;
using System.Web.Routing;
using Moq;

namespace GitPrise.Core.Tests.Web
{
    public class RequestContextMock : Mock<RequestContext>
    {
        public RequestContextMock()
        {
            Setup(x => x.HttpContext).Returns(new HttpContextMock().Object);
        }
    }
}
