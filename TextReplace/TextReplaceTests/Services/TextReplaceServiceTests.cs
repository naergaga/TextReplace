using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextReplace.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace TextReplace.Services.Tests
{
    [TestClass()]
    public class TextReplaceServiceTests
    {
        [TestMethod()]
        public void ReplaceTest()
        {
            var service = new TextReplaceService();
            service.Replace(@"D:\test.txt", @"D:\test1.txt", "！", "。");
        }
    }
}