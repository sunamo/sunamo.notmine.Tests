using System;
using Microsoft.Build.Utilities.ProjectCreation;
using Xunit;

namespace SlnGen.Common.Tests
{
    public class MSBuildProjectLoaderTests
    {
        [Fact]
        public void LoadProjectReferencesTest()
        {
            MSBuildProjectLoader m = new MSBuildProjectLoader(BuildEngine.Create());
            var p = new string [] { @"e:\Documents\vs\Projects\sunamo\shared\shared.csproj" };
            var pc = m.LoadProjectsAndReferences(p);
            int i = 0;
        }
    }
}