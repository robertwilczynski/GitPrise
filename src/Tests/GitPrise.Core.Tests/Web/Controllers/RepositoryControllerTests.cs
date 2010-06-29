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
using MbUnit.Framework;
using GitPrise.Web.Controllers;
using GitPrise.Core.Configuration;
using Moq;
using GitPrise.Core.Services;
using GitPrise.Core.SyntaxHighlighting;
using GitSharp;
using GitPrise.Web.Models;

namespace GitPrise.Core.Tests.Web.Controllers
{
    [TestFixture]
    public partial class RepositoryControllerTests
    {
        private string _testRepositoryLocation;
        RepositoryController _controller;

        [SetUp]
        public void Setup()
        {
            var settingsProvider = new Mock<ISettingsProvider>();
            var repoResolver = new Mock<IRepositoryResolver>();
            var highlightingService = new Mock<IHighlightingService>();

            _controller = new RepositoryController(
                settingsProvider.Object,
                repoResolver.Object,
                highlightingService.Object);

            _testRepositoryLocation = TestUtils.GetAbsolutePathFromRelativeToTestAssembly(@"..\..\..\Repositories\Test");
            _controller.Repository = new Repository(_testRepositoryLocation);
        }

        protected static void AssertRequestIsCopiedToModel(RepositoryNavigationRequest request, RepositoryNavigationRequest model)
        {
            Assert.AreEqual(request.RepositoryName, model.RepositoryName);
            Assert.AreEqual(request.RepositoryLocation, model.RepositoryLocation);
            Assert.AreEqual(request.Path, model.Path);
            Assert.AreEqual(request.Treeish, model.Treeish);
        }
       
        [TearDown]
        public void TearDown()
        {
            _controller.Repository.Dispose();
        }
    }
}
