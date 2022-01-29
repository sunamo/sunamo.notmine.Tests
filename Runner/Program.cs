using FubuCsProjFile.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner
{
    public class Program
    {
        static void Main(string[] args)
        {
            FrameworkNameDetectorTests t = new FrameworkNameDetectorTests();
            t.DetectTest();

            Console.ReadLine();
        }
    }
}
