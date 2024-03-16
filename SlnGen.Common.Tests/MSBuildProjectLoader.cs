namespace SlnGen.Common.Tests
{
    public class MSBuildProjectLoaderTests
    {
        [Fact]
        public void LoadProjectReferencesTest()
        {
            MSBuildProjectLoader m = new MSBuildProjectLoader(BuildEngine.Create());
            var p = new string [] { @"E:\vs\Projects\sunamoWithoutLocalDep\shared\shared.csproj" };
            var pc = m.LoadProjectsAndReferences(p);
            int i = 0;
        }
    }
}
