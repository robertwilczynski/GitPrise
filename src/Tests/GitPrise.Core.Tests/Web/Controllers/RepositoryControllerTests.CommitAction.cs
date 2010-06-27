using System;
using MbUnit.Framework;
using GitPrise.Web.Models;
using MvcContrib.TestHelper;

namespace GitPrise.Core.Tests.Web.Controllers
{
    public partial class RepositoryControllerTests
    {
        public class CommitAction : RepositoryControllerTests
        {
            private CommitDetailsViewModel ExectueTreeAction(RepositoryNavigationRequest request)
            {
                var result = _controller.Commit(request);
                return result.AssertViewRendered().WithViewData<CommitDetailsViewModel>();
            }

            [Test]
            public void CopiesRequestDataToViewModel()
            {
                var request = new RepositoryNavigationRequest
                {
                    Treeish = "master",
                    RepositoryName = "Test",
                    Path = "src/file001.txt",
                    RepositoryLocation = _testRepositoryLocation
                };

                var model = ExectueTreeAction(request);

                Assert.AreEqual(request.RepositoryName, model.RepositoryName);
                Assert.AreEqual(request.RepositoryLocation, model.RepositoryLocation);
                Assert.AreEqual(request.Path, model.Path);
            }
        }
    }
}
