using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using GitPrise.Web.Controllers;
using GitPrise.Core.Configuration;
using Moq;
using GitPrise.Core.Services;
using GitPrise.Core.SyntaxHighlighting;
using GitPrise.Web.Models;
using GitSharp;
using System.Reflection;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using MvcContrib.TestHelper.Ui;

namespace GitPrise.Core.Tests.Web.Controllers
{
    [TestFixture]
    class RepositoryControllerTests
    {
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

            _controller.Repository = new Repository(TestUtils.GetAbsolutePathFromRelativeToTestAssembly(@"..\..\..\Repositories\Test"));
        }

        [Test]
        public void Tree_()
        {
            var result = _controller.Tree(new RepositoryNavigationRequest
                {
                    Treeish = "master",
                    RepositoryName = "Test"
                });
            var model = result.AssertViewRendered().WithViewData<TreeViewModel>();
            Assert.AreEqual("Test", model.CurrentCommit.RepositoryName);
            Assert.AreEqual(null, model.CurrentCommit.RepositoryLocation);
        }
    }
}
