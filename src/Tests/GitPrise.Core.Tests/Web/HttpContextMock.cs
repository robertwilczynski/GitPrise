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
using System.Collections;
using System.Web;
using Moq;

namespace GitPrise.Core.Tests.Web
{

    public class HttpContextMock : Mock<HttpContextBase>
    {
        readonly Mock<HttpServerUtilityBase> _server = new Mock<HttpServerUtilityBase>();


        public HttpRequestMock HttpRequest { get; private set; }
        public HttpResponseMock HttpResponse { get; private set; }

        public HttpContextMock()
        {
            HttpRequest = new HttpRequestMock();
            HttpResponse = new HttpResponseMock();

            Setup(x => x.Request).Returns(HttpRequest.Object);
            Setup(x => x.Response).Returns(HttpResponse.Object);
            Setup(x => x.Server).Returns(_server.Object);
            Setup(x => x.Items).Returns(new Hashtable());
            SetupProperty(x => x.User);
        }

    }

}
