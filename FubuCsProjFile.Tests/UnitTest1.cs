using FubuCsProjFile.MSBuild;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace FubuCsProjFile.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FrameworkNameDetectorTest()
        {
            List<string> pr = Directory.GetFiles(@"d:\_Test\sunamo.notmine\FubuCsProjFile\FrameworkNameDetector\").ToList() ;

            foreach (var item in pr)
            {
                
                var msb = MSBuildProject.LoadFrom(item);
                var result = FrameworkNameDetector.Detect(msb);
                Debug.WriteLine(result);
            }


        }
    }
}
