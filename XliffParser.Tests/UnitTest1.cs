using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.XPath;

namespace XliffParser.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void XlfXPath()
        {
            //var xd = new XlfDocument();

            XmlNamespacesHolder x = new XmlNamespacesHolder();
            var content = TF.ReadFile( @"E:\vs\Projects\sunamo\sunamo\MultilingualResources\sunamo.en-US.xlf");
            var xd = x.ParseAndRemoveNamespacesXDocument(content);

            var nsmgr = x.nsmgr;

            //var xd = XHelper.CreateXDocument();
            //[original=@'WPF.TESTS/RESOURCES/EN-US.RESX']
            var d = xd.XPathEvaluate("/xliff/file");
            
            var d2 = xd.XPathSelectElements("/xliff/file");
            var d3 = xd.XPathSelectElements("/xliff");
            int i = 0;
        }
    }
}