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

namespace GitPrise.Core.Tests.Web.Models
{
    [TestFixture]
    public class RepositoryNavigationRequestTests
    {
        [Test]
        public void CompyConstructor_Works()
        {
            var request = new RepositoryNavigationRequest()
            {
                RepositoryName = "a",
                Treeish = "b",
                Path = "c",
                RepositoryLocation = "d",
            };

            var newRequest = new RepositoryNavigationRequest(request);
            string requestString = AssertEx.XmlSerialize(request);
            string newRequestString = AssertEx.XmlSerialize(newRequest);
            Assert.AreEqual(requestString, newRequestString,
                "Copied request should equal the template but it doesn't. '{0}' != '{1}'", 
                requestString, 
                newRequestString);
        }
    }
}
