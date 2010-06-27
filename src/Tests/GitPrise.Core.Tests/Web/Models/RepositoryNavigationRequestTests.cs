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
