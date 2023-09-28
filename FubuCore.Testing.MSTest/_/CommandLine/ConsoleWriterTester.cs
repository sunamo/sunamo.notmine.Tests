namespace FubuCore.Testing.CommandLine
{
    [TestFixture]
    public class ConsoleWriterTester
    {
        [Test]
        public void TryA81Character()
        {
            ConsoleWriter.Write(new string('a',81));
        }
    }
}
