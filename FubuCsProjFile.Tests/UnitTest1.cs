using FubuCsprojFile;
using FubuCsprojFile.MSBuild;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace FubuCsProjFile.Tests
{
    public class FrameworkNameDetectorTests
    {
        [Fact]
        public void DetectTest()
        {
            List<string> pr = Directory.GetFiles(@"D:\_Test\sunamo.notmine\FubuCsProjFile\FrameworkNameDetector\").ToList() ;

            foreach (var item in pr)
            {
                var msb = MSBuildProject.LoadFrom(item);
                var result = FrameworkNameDetector.Detect(msb);
                var t = Path.GetFileName(item) + " = " + result;
                Debug.WriteLine(t);
                Console.WriteLine(t);
            }
        }
    }
}
