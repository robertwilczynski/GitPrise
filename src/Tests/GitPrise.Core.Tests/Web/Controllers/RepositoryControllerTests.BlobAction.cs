using System;
using MbUnit.Framework;
using GitPrise.Web.Models;
using MvcContrib.TestHelper;

namespace GitPrise.Core.Tests.Web.Controllers
{
    public partial class RepositoryControllerTests
    {
        public class BlobAction : RepositoryControllerTests
        {
            private BlobViewModel ExectueTreeAction(RepositoryNavigationRequest request)
            {
                var result = _controller.Blob(request);
                return result.AssertViewRendered().WithViewData<BlobViewModel>();
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

                AssertRequestIsCopiedToModel(request, model);
            }
        }
    }
}
