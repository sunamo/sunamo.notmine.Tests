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
        var a1 = @"E:\Documents\vs\Projects\sunamo\sunamo\sunamo.csproj";
        var a2 = @"E:\Documents\vs\Projects\EverythingClient";

        var result = a1.PathRelativeTo(a2);
        Debugger.Break();
    }
}