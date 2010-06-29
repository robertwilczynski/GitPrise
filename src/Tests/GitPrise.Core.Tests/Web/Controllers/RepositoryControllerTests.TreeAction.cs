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
using System.Linq;

namespace GitPrise.Core.Tests.Web.Controllers
{
    public partial class RepositoryControllerTests
    {
        public class TreeAction : RepositoryControllerTests
        {
            private TreeViewModel ExectueTreeAction(RepositoryNavigationRequest request)
            {
                var result = _controller.Tree(request);
                return result.AssertViewRendered().WithViewData<TreeViewModel>();
            }

            [Test]
            public void CopiesRequestDataToViewModel()
            {
                var request = new RepositoryNavigationRequest
                {
                    Treeish = "master",
                    RepositoryName = "Test",
                    Path = "src",
                    RepositoryLocation = _testRepositoryLocation
                };

                var model = ExectueTreeAction(request);

                AssertRequestIsCopiedToModel(request, model);
            }

            [Test]
            public void WhenOnRoot_ProducesValidRepositoryTree()
            {
                var request = new RepositoryNavigationRequest
                {
                    Treeish = "master",
                    RepositoryName = "Test",
                };

                var model = ExectueTreeAction(request);
                Assert.AreEqual(4, model.Items.Count);
                Assert.AreEqual(1, model.Items.Count(x => x.Path == "src" && x.Type == ListItemViewModel.ItemType.Tree));
                Assert.AreEqual(1, model.Items.Count(x => x.Path == "doc" && x.Type == ListItemViewModel.ItemType.Tree));
                Assert.AreEqual(1, model.Items.Count(x => x.Path == "LICENSE.txt" && x.Type == ListItemViewModel.ItemType.Blob));
                Assert.AreEqual(1, model.Items.Count(x => x.Path == "README.txt" && x.Type == ListItemViewModel.ItemType.Blob));
            }

            [Test]
            public void WhenNested_ProducesValidRepositoryTree()
            {
                var request = new RepositoryNavigationRequest
                {
                    Treeish = "master",
                    RepositoryName = "Test",
                    Path = "src",
                };

                var model = ExectueTreeAction(request);
                Assert.AreEqual(2, model.Items.Count);
                Assert.AreEqual(1, model.Items.Count(x => x.Path == "src/file001.txt" && x.Type == ListItemViewModel.ItemType.Blob));
                Assert.AreEqual(1, model.Items.Count(x => x.Path == "src/file002.txt" && x.Type == ListItemViewModel.ItemType.Blob));
            }
        }
    }
}
