using System;
using System.Collections.Generic;
using System.IO;
using FubuTestingSupport;
using NUnit.Framework;
using System.Linq;

namespace FubuCore.Testing
{
    [TestFixture]
    public class FileSystemTester
    {      
        private string fullPath(params string[] paths)
        {
            return Path.GetFullPath(paths.Join(Path.DirectorySeparatorChar.ToString()));
        }

        [Test]
        public void copy_with_preserve()
        {
            var system = new FileSystem();
            system.WriteStringToFile("a.txt", "something");
            system.WriteStringToFile("b.txt", "else");
            system.Copy("a.txt", "b.txt", CopyBehavior.preserve);

            system.ReadStringFromFile("b.txt").ShouldEqual("else");

        }

        [Test]
        public void copy_with_overwrite()
        {
            var system = new FileSystem();
            system.WriteStringToFile("a.txt", "something");
            system.WriteStringToFile("b.txt", "else");
            system.Copy("a.txt", "b.txt", CopyBehavior.overwrite);

            system.ReadStringFromFile("b.txt").ShouldEqual("something"); 
        }

        [Test]
        public void get_relative_path()
        {
            fullPath("a", "b", "1.bat").PathRelativeTo(fullPath("a", "b"))
                .ShouldEqual("1.bat");

            fullPath("a", "b", "c", "1.bat").PathRelativeTo(fullPath("a", "b"))
                .ShouldEqual("c{0}1.bat".ToFormat(Path.DirectorySeparatorChar));

            fullPath("a", "b", "c", "d", "1.bat").PathRelativeTo(fullPath("a", "b"))
                .ShouldEqual("c{0}d{0}1.bat".ToFormat(Path.DirectorySeparatorChar));

            fullPath("a", "1.bat").PathRelativeTo(fullPath("a", "b"))
                .ShouldEqual("..{0}1.bat".ToFormat(Path.DirectorySeparatorChar));

            fullPath("a", "1.bat").PathRelativeTo(fullPath("a", "b", "c"))
                .ShouldEqual("..{0}..{0}1.bat".ToFormat(Path.DirectorySeparatorChar));

            fullPath("a", "1.bat").PathRelativeTo(fullPath("a", "b", "c", "d"))
                .ShouldEqual("..{0}..{0}..{0}1.bat".ToFormat(Path.DirectorySeparatorChar));

            fullPath("A", "b", "1.bat").PathRelativeTo(fullPath("A", "b"))
                .ShouldEqual("1.bat");

            fullPath("A", "b").PathRelativeTo(fullPath("A", "b"))
                .ShouldBeEmpty();
        }

        [Test]
        public void get_relative_path_should_not_be_case_sensitive()
        {
            @"C:\build\posh-tf".PathRelativeTo(@"C:\Build\scripts")
                .ShouldEqual(@"..\posh-tf");
        }

        [Test]
        public void path_relative_in_parallel_paths()
        {
            fullPath("folder2", "file2.txt")
                .PathRelativeTo(fullPath("folder1"))
                .ShouldEqual("..{0}folder2{0}file2.txt".ToFormat(Path.DirectorySeparatorChar));
        }

        [Test]
        public void combine_when_it_is_only_one_value()
        {
            FileSystem.Combine("a").ShouldEqual("a");
        }

        [Test]
        public void combine_with_two_values()
        {
            FileSystem.Combine("a", "b").ShouldEqual("a{0}b".ToFormat(Path.DirectorySeparatorChar));
        }

        [Test]
        public void combine_with_three_values()
        {
            FileSystem.Combine("a", "b", "c").ShouldEqual("a{0}b{0}c".ToFormat(Path.DirectorySeparatorChar));
        }

        [Test]
        public void combine_with_four_values()
        {
            FileSystem.Combine("a", "b", "c", "d").ShouldEqual("a{0}b{0}c{0}d".ToFormat(Path.DirectorySeparatorChar));
        }

        [Test]
        public void combine_with_rooted_first_value()
        {
            FileSystem.Combine("{0}a".ToFormat(Path.DirectorySeparatorChar), "b", "c").ShouldEqual("{0}a{0}b{0}c".ToFormat(Path.DirectorySeparatorChar));
        }

        [Test]
        public void combine_with_trailing_slashes()
        {
            FileSystem.Combine("a{0}".ToFormat(Path.DirectorySeparatorChar), "b", "c{0}".ToFormat(Path.DirectorySeparatorChar)).
				ShouldEqual("a{0}b{0}c{0}".ToFormat(Path.DirectorySeparatorChar));
        }

    }

    [TestFixture]
    public class FileSystemIntegrationTester
    {
        private FileSystem _fileSystem;
        private string _basePath;

        [SetUp]
        public void beforeEach()
        {
            _fileSystem = new FileSystem();
            _basePath = Path.GetTempPath();
        }

        [Test]
        public void find_file_in_directories_success()
        {
            _fileSystem.CreateDirectory("a");
            _fileSystem.CreateDirectory("b");
            _fileSystem.CreateDirectory("c");

            _fileSystem.WriteStringToFile("a".AppendPath("1.txt"), "a");
            _fileSystem.WriteStringToFile("b".AppendPath("2.txt"), "b");
            _fileSystem.WriteStringToFile("c".AppendPath("3.txt"), "a");

            var directories = new List<string>{
                "a",
                "b",
                "c"
            };

            _fileSystem.FindFileInDirectories(directories, "1.txt").ShouldEqual("a".AppendPath("1.txt"));
            _fileSystem.FindFileInDirectories(directories, "2.txt").ShouldEqual("b".AppendPath("2.txt"));
            _fileSystem.FindFileInDirectories(directories, "3.txt").ShouldEqual("c".AppendPath("3.txt"));
            _fileSystem.FindFileInDirectories(directories, "4.txt").ShouldBeNull();
        }

        [Test]
        public void folders_should_be_created_when_writing_to_a_file_path_having_folders_that_do_not_exist()
        {
            var pathDoesNotExist = Path.Combine(_basePath, randomName());
            var stream = new MemoryStream(new byte[] { 55, 66, 77, 88 });

            _fileSystem.WriteStreamToFile(Path.Combine(pathDoesNotExist, "file.txt"), stream);

            Directory.Exists(pathDoesNotExist).ShouldBeTrue();
        }

        [Test]
        public void writing_a_large_file()
        {
            const string OneKLoremIpsum = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur varius arcu eget nisi porta sit amet aliquet enim laoreet. Mauris at lorem velit, in venenatis augue. Pellentesque dapibus eros ac ipsum rutrum varius. Mauris non velit euismod odio tincidunt fermentum eget a enim. Pellentesque in erat nisl, consectetur lacinia leo. Suspendisse hendrerit blandit justo, sed aliquet libero eleifend sed. Fusce nisi tortor, ultricies sed tempor sit amet, viverra at quam. Vivamus sem mi, semper nec cursus vel, vehicula sit amet nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Cras commodo commodo tortor congue bibendum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Pellentesque vel magna vitae dui accumsan venenatis. Nullam sed ante mauris, nec iaculis erat. Cras eu nibh vel ante adipiscing volutpat. Integer ullamcorper tempus facilisis. Vestibulum eu magna sit amet dolor condimentum vestibulum non a ligula. Nunc purus nibh amet.";
            var path = Path.Combine(_basePath, randomName());

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            for (var i = 0; i < FileSystem.BufferSize / 512; i++)
            {
                writer.Write(OneKLoremIpsum);
            }
            stream.Position = 0;

            _fileSystem.WriteStreamToFile(path, stream);

            var fileInfo = new FileInfo(path);
            fileInfo.Exists.ShouldBeTrue();
            fileInfo.Length.ShouldEqual(stream.Length);
        }

        [Test]
        public void moving_a_file_should_create_the_target_directory_path_if_necessary()
        {
            var fromDir = Path.Combine(_basePath, randomName());
            var fromPath = Path.Combine(fromDir, "file.txt");
            var stream = new MemoryStream(new byte[] { 55, 66, 77, 88 });
            _fileSystem.WriteStreamToFile(fromPath, stream);

            var toDir = Path.Combine(_basePath, randomName());
            var toPath = Path.Combine(toDir, "newfilename.txt");

            _fileSystem.MoveFile(fromPath, toPath);

            Directory.Exists(toDir).ShouldBeTrue();
        }

        [Test]
        public void moving_a_file()
        {
            var stream = new MemoryStream(new byte[] { 55, 66, 77, 88 });
            var fromPath = Path.Combine(_basePath, randomName());
            _fileSystem.WriteStreamToFile(fromPath, stream);

            var toDir = Path.Combine(_basePath, randomName());
            var toPath = Path.Combine(toDir, "newfilename.txt");

            _fileSystem.MoveFile(fromPath, toPath);

            File.Exists(toPath).ShouldBeTrue();
        }

        private static string randomName()
        {
            return Guid.NewGuid().ToString().Replace("-", String.Empty);
        }


        [Test]
        public void copy_directory()
        {
            var system = new FileSystem();

            system.ResetDirectory("dagobah");
            system.WriteStringToFile("dagobah".AppendPath("f1", "a.txt"), "something");
            system.WriteStringToFile("dagobah".AppendPath("f2", "a.txt"), "something");
            system.WriteStringToFile("dagobah".AppendPath("f3", "a.txt"), "something");
            system.WriteStringToFile("dagobah".AppendPath("f1", "f1a", "a.txt"), "something");
            system.WriteStringToFile("dagobah".AppendPath("f1", "f1a", "f1b", "a.txt"), "something");
            system.WriteStringToFile("dagobah".AppendPath("a.txt"), "something");

            system.DeleteDirectory("rhenvar");
            system.Copy("dagobah", "rhenvar");
            
            system.FindFiles("rhenvar", FileSet.Everything()).Select(x => x.PathRelativeTo("rhenvar")).OrderBy(x => x)
                .ShouldHaveTheSameElementsAs(
                    "a.txt",
                    FileSystem.Combine("f1", "a.txt"),
                    FileSystem.Combine("f1", "f1a", "a.txt"),
                    FileSystem.Combine("f1", "f1a", "f1b", "a.txt"),
                    FileSystem.Combine("f2", "a.txt"),
                    FileSystem.Combine("f3", "a.txt")
                );
        }
    }


    [TestFixture]
    public class Searching_up_the_tree_for_a_dir
    {
        private IFileSystem _fileSystem;

        [SetUp]
        public void SetUp()
        {
            _fileSystem = new FileSystem();
            _fileSystem.CreateDirectory("deep".AppendPath("a","b","c"));
            _fileSystem.CreateDirectory("deep".AppendPath("config"));
        }

        [Test]
        public void found()
        {
            var expected = Environment.CurrentDirectory.AppendPath("deep".AppendPath("config"));
            
            var dir = _fileSystem.SearchUpForDirectory("deep/a/b/c".ToFullPath(), "config");

            dir.ShouldEqual(expected);
        }
        [Test]
        public void miss()
        {
           
            var dir = _fileSystem.SearchUpForDirectory("deep".AppendPath("a","b","c").ToFullPath(), "conig");

            dir.ShouldBeNull();
        }
    }

}