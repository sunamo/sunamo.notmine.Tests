using System.Diagnostics;
using System.Xml;
using FubuCore.Binding.Values;
using FubuCore.Configuration;
using NUnit.Framework;
using FubuTestingSupport;

namespace FubuCore.Testing.Configuration
{
    [TestFixture]
    public class XmlValueSourceTester
    {
        private SettingsData theSettings;

        [SetUp]
        public void SetUp()
        {
            var xml = @"
<appSettings>
  <add key='EntitySettings.DefaultUserCalendar' value='M-F 9-5'/>

  <add key='AttachmentSettings.MaximumFileSizeKB' value='10240'/>

  <add key='DatabaseSettings.ConnectionString' value='DbConnectionString' />
  <add key='DatabaseSettings.Dialect' value='NHibernate.Dialect.MsSql2005Dialect' />
  <add key='DatabaseSettings.Driver' value='NHibernate.Driver.SqlClientDriver' />
  <add key='DatabaseSettings.Provider' value='NHibernate.Connection.DriverConnectionProvider' />
  <add key='DatabaseSettings.ProxyFactory' value='NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle' />
  <add key='DatabaseSettings.ShowSql' value='false' />
  <add key='DatabaseSettings.UseOuterJoin' value='True' />
  <add key='DatabaseSettings.GenerateStatistics' value='true'/>

  <add key='EmailEngineSettings.AdministratorEmail' value='something@sunamo.net.fcs.local'/>
  <add key='EmailEngineSettings.PollingFrequency' value='15'/>
  
  <add key='EmailSettings.IncomingEmailCaseIdentifierPattern' value='About(\s*Case)?\s*(?&lt;Identifier>[\d\-]+)'/>
  <add key='EmailSettings.LogEmailReplyToAddress' value='support@sunamo.net.fcs.local'/>
  <add key='EmailSettings.OutgoingDefaultFromEmail' value='changeme@fcs.local'/>
  <add key='EmailSettings.OutgoingEmailSubjectPrefix' value='About Case'/>
  <add key='EmailSettings.SmtpEnableSsl' value='false'/>
  <add key='EmailSettings.SmtpHostAddress' value='127.0.0.1'/>
  <add key='EmailSettings.SmtpPort' value='25'/>
  <add key='EmailSettings.UseSmtpCredentials' value='false'/>
    
  <add key='IntegratedAuthenticationSettings.DefaultSite' value='Default Site'/>
  <add key='IntegratedAuthenticationSettings.InternalSiteType' value='Internal'/>
  
  <add key='LocalizationSettings.PrependCultureOnMissing' value='true'/>
  
  <add key='PollingServiceSettings.FrequencyInSeconds' value='5' />

  <add key='SearchSettings.IndexFilesPath' value='A thingie'/>

  <add key='SearchSettings.LuceneParams' value='(+domain:case +({0})) OR (+domain:solution -status:expired +({0})) OR (+domain:externalfile +({0}))'/>
  <add key='SearchSettings.NumberOfIndexChangesBeforeOptimization' value='500'/>
  <add key='SearchSettings.LuceneMaximumClauseCount' value='1024'/>
  <add key='SearchSettings.SelfServiceLuceneParams' value='+domain:solution +public:true +status:published +({0})'/>
 
  <add key='WebsiteSettings.MaxNotificationDisplayCount' value='10'/>  

  <add key='WebsiteSettings.PublicReportFrameUrlBase' value='http://sunamo.net/DovetailCRM.Reports/reportlist.aspx' />
  <add key='WebsiteSettings.PublicReportListUrl' value='http://sunamo.net/DovetailCRM.Reports/reports.axd'/>
  <add key='WebsiteSettings.PublicReportWidgetUrlBase' value='http://sunamo.net/DovetailCRM.Reports/DashboardReportViewer.aspx'/>

  <add key='WebsiteSettings.PublicMobileUrlBase' value='http://sunamo.net/mobile' />
  <add key='WebsiteSettings.PublicUrlBase' value='http://sunamo.net/DovetailCRM/' />
  <add key='WebsiteSettings.DiagnosticsEnabled' value='true' />
  <add key='WebsiteSettings.CustomViewPath' value='Overrides' />
  <add key='WebsiteSettings.AnonymousAccessFileExtensions' value='gif, png, jpg, css, js, htm, html' />

</appSettings>
".Replace("'", "\"");


            var document = new XmlDocument();
            document.LoadXml(xml);

            theSettings =XmlSettingsParser.Parse(document.DocumentElement);
        }

        [Test]
        public void all_keys()
        {
            theSettings.AllKeys.ShouldContain("WebsiteSettings.PublicMobileUrlBase");
            theSettings.AllKeys.ShouldContain("WebsiteSettings.PublicUrlBase");
            theSettings.AllKeys.ShouldContain("PollingServiceSettings.FrequencyInSeconds");
        }

        [Test]
        public void get_value()
        {
            theSettings["WebsiteSettings.DiagnosticsEnabled"].ShouldEqual("true");
        }

        [Test]
        public void has_setting_positive()
        {
            theSettings.Child("WebsiteSettings").Has("DiagnosticsEnabled").ShouldBeTrue();
        }

        [Test]
        public void has_setting_negative()
        {
            theSettings.Has("Not.A.Real.Setting").ShouldBeFalse();
        }

        [Test]
        public void category_is_core_if_not_specified_explicitly()
        {
            theSettings.Category.ShouldEqual(SettingCategory.core);
        }

        [Test]
        public void find_category_if_it_exists()
        {
            var document = new XmlDocument();
            document.AppendChild(document.CreateElement("appSettings"));
            document.DocumentElement.SetAttribute("category", SettingCategory.package.ToString());

            var settings = XmlSettingsParser.Parse(document.DocumentElement);
            settings.Category.ShouldEqual(SettingCategory.package);
        }

        [Test]
        public void write_and_read()
        {
            var settings = new SettingsData(SettingCategory.package);
            settings["a"]= "1";
            settings["b"] = "2";
        
            XmlSettingsParser.Write(settings, "config.xml");

            var settings2 = XmlSettingsParser.Parse("config.xml");

            settings2.Category.ShouldEqual(SettingCategory.package);

            settings2["a"].ShouldEqual("1");
            settings2["b"].ShouldEqual("2");
        }
    }
}