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
