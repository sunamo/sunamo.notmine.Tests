using System;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace UAParser.Tests
{
    public class ParserTests
    {
        [Fact]
        public void ParseTest()
        {
            var ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36";

            var uaParser = Parser.GetDefault();
            var p = uaParser.Parse(ua);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine( RH.DumpAsString(new DumpAsStringArgs{ name = "Device", o = p.Device }));
            sb.AppendLine(RH.DumpAsString(new DumpAsStringArgs { name = "OS", o = p.OS }));
            sb.AppendLine(RH.DumpAsString(new DumpAsStringArgs { name = "UA", o = p.UA }));
            sb.AppendLine(RH.DumpAsString(new DumpAsStringArgs { name = "p", o = p }));
            
            
            Debug.WriteLine(sb.ToString());
        }
    }
}