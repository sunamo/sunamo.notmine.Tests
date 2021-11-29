using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using TurnerSoftware.SitemapTools.Parser;

public class TextSitemapParserTests
{
    [Test]
    public void ParseSitemapTest()
    {
        var reader = new StreamReader(new FileStream(@"e:\Documents\vs\Projects\sunamo.cz\lyrics.sunamo.cz\sitemap.xml", FileMode.Open));
        TextSitemapParser t = new TextSitemapParser();
        var file = t.ParseSitemapXml((TextReader)reader);

        var i =0;
    }
}