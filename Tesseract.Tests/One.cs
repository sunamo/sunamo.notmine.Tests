//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Tesseract.Tests
//{
//    class One
//    {
//        static void Main()
//        {
//            string basePath = @"D:\vs\NotMine.Tests\Tesseract.Tests\";
//            // now add the following C# line in the code page  
//            var image = new Bitmap(basePath + @"phototest.tif");
//            var ocr = new Tesseract();
//            ocr.Init(basePath+ @"tessdata", "eng", false);
//            var result = ocr.DoOCR(image, Rectangle.Empty);
//            foreach (tessnet2.Word word in result)
//            {
//                Console.writeline(word.text);
//            }
//        }
//    }
//}