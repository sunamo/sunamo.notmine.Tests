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
