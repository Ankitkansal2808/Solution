using System;
using Xunit;
using FeedApplication;
using System.IO;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void TestCase1()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Case for Return Code 1 (Key Count > 1)
            string[] args = new string[] { };

            Program.Main(args);
     
            var output = stringWriter.ToString();
            Assert.True(output.Contains("Enter input in right format like '$ import capterra feed-products/capterra.yaml'"), "Right text is not returned");
        }

        [Fact]
        public void TestCase2()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Case for Return Code 1 (Key Count > 1)
            string[] args = new string[] { "$", "import" };

            Program.Main(args);

            var output = stringWriter.ToString();
            Assert.True(output.Contains("Enter input in right format like '$ import capterra feed-products/capterra.yaml'"), "Right text is not returned");
        }

        [Fact]
        public void TestCase3()
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            // Case for Return Code 1 (Key Count > 1)
            string[] args = new string[] { "$", "import", "capterra", "feed-products/capterra.yaml" };

            Program.Main(args);

            var output = stringWriter.ToString();
            Assert.True(output.Contains("GitGHub"), "Right text is not returned");
        }
    }
}
