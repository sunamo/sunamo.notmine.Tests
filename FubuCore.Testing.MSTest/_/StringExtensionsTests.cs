using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FubuCore;
using System.Diagnostics;

[TestClass]
public class StringExtensionsTests
{
    [TestMethod]
    public void PathRelativeToTest()
    {
        var a1 = @"e:\Documents\Visual Studio 2017\Projects\sunamo\sunamo\sunamo.csproj";
        var a2 = @"e:\Documents\Visual Studio 2017\Projects\EverythingClient";

        var result = a1.PathRelativeTo(a2);
        Debugger.Break();
    }
}
