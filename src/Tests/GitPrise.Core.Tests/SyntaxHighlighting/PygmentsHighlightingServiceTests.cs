using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using GitPrise.SyntaxHighlighting.Pygments;

namespace GitPrise.Core.Tests.SyntaxHighlighting
{
    [TestFixture]
    public class PygmentsHighlightingServiceTests
    {
        private PygmentsHighlightingService _service;

        [SetUp]
        public void Setup()
        {
            _service = new PygmentsHighlightingService();
        }

        [Test]
        public void Test()
        {
            var html = _service.GenerateHtml(@"int a = 2;", "", null);
        }
    }

}
