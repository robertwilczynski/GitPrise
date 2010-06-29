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
