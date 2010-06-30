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
using MbUnit.Framework;
using GitPrise.Web.Mvc;
using System.Web.Mvc;
using Moq;
using GitPrise.Web.Controllers;
using GitPrise.Core.Configuration;
using GitPrise.Web.Models;
using System.Web.Routing;
using GitPrise.Core.Services;
using GitSharp;

namespace GitPrise.Core.Tests.Web
{
    public class RepositoryRequestAttributeTests
    {
        private RepositoryRequestAttribute _filter;
        private RepositoryController _controller;
        private Mock<IRepositoryResolver> _resolver;
        private RouteData _routeData;
        private IDictionary<string, object> _actionParameters;
        private Mock<ActionExecutingContext> _executingContext;

        [SetUp]
        public void Setup()
        {            
            _resolver = new Mock<IRepositoryResolver>();
            _resolver.Setup(x => x.GetRepository(It.IsAny<string>()))
                .Returns(new Repository(TestUtils.GetRepositoryPath("Test")));

            _controller = new RepositoryController(new Mock<ISettingsProvider>().Object, _resolver.Object, null);            

            _executingContext = new Mock<ActionExecutingContext>();
            _executingContext.Setup(x => x.Controller).Returns(_controller);

            _actionParameters = new Dictionary<string, object>();
            _executingContext.Setup(x => x.ActionParameters).Returns(_actionParameters);
            _actionParameters.Add("request", new RepositoryNavigationRequest());
            
            _routeData = new RouteData();
            _executingContext.Setup(x => x.RouteData).Returns(_routeData);            

            var requestContext = new Mock<RequestContext>();
            var httpContext = new HttpContextMock();
            requestContext.Setup(x => x.HttpContext).Returns(httpContext.Object);
            _executingContext.Setup(x => x.HttpContext).Returns(httpContext.Object);

            _filter = new RepositoryRequestAttribute
            {
                RepositoryResolver = _resolver.Object
            };            
        }

        [Test]
        public void PutsRepositoryNameInRequestObject()
        {
            _routeData.Values.Add("repositoryName", "Test");
            _filter.OnActionExecuting(_executingContext.Object);
            object request = _actionParameters["request"];
            Assert.IsNotNull(request);
            var navRequest = request as RepositoryNavigationRequest;
            Assert.IsNotNull(navRequest);
            Assert.AreEqual("Test", navRequest.RepositoryName);
        }

        [Test]
        public void PutsPathInRequestObject()
        {
            _routeData.Values.Add("repositoryName", "Test");
            string path = @"c:\myrepo";
            _routeData.Values.Add("path", path);

            var navRequest = ExecuteFilter();

            Assert.AreEqual(path, navRequest.Path);
        }

        [Test]
        public void AssignsRepositoryInstanceToController()
        {
            _routeData.Values.Add("repositoryName", "Test");

            var navRequest = ExecuteFilter();

            Assert.IsNotNull(_controller.Repository);
        }

        [Test]
        public void WhenNoTreeishSupplied_DefaultsToMaster()
        {
            _routeData.Values.Add("repositoryName", "Test");
            var navRequest = ExecuteFilter();

            Assert.AreEqual("master", navRequest.Treeish);
        }

        [Test]
        public void WhenTreeishIsSupplied_PutsValueInRequest()
        {
            _routeData.Values.Add("repositoryName", "Test");
            _routeData.Values.Add("id", "xyz");
            var navRequest = ExecuteFilter();

            Assert.AreEqual("xyz", navRequest.Treeish);
        }

        private RepositoryNavigationRequest ExecuteFilter()
        {
            _filter.OnActionExecuting(_executingContext.Object);

            object request = _actionParameters["request"];
            Assert.IsNotNull(request);
            var navRequest = request as RepositoryNavigationRequest;
            Assert.IsNotNull(navRequest);
            return navRequest;
        }
    }
}
