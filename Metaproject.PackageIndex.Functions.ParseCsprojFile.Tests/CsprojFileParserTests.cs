using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Metaproject.PackageIndex.Functions.ParseCsprojFile.Tests
{
    [TestClass]
    public class CsprojFileParserTests
    {
        [TestMethod]
        public void ParseCsprojTest()
        {
            var p = @"D:\_Test\sunamo.notmine\FubuCsProjFile\FrameworkNameDetector\FubuCsProjFile-net5.0-windows.csproj";
            var cs = CsprojFileParser.ParseCsproj(p);
            int i = 0;
        }
    }
}
