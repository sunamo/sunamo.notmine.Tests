using System;
using System.Reflection;
using FubuCore.Binding;
using FubuCore.Binding.InMemory;
using FubuCore.Conversion;
using FubuCore.Reflection;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuCore.Testing.Binding
{
    public enum ColorEnum
    {
        red, blue, green
    }

    public class Address
    {
        public Address()
        {
            StateOrProvince = string.Empty;
            Country = string.Empty;
            AddressType = string.Empty;
        }

        public int Order { get; set; }

        public bool IsActive { get; set; }

        public string AddressType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        //[Required]
        //public TimeZoneInfo TimeZone { get; set; }


        public DateTime DateEntered { get; set; }

        public ColorEnum Color { get; set; }
        public Guid Guid { get; set; }
    }

    [TestFixture]
    public class ConversionPropertyBinderTester : PropertyBinderTester
    {
        [SetUp]
        public void SetUp()
        {
            propertyBinder = new ConversionPropertyBinder(new BindingRegistry());
        }

        [Test]
        public void converter_matches_primitive_properties_out_of_the_box()
        {
            shouldMatch(x => x.Address.Address1);
            shouldMatch(x => x.Address.Order);
            shouldMatch(x => x.Address.IsActive);
            shouldMatch(x => x.Address.DateEntered);
            shouldMatch(x => x.Address.Color); // enum value
            shouldMatch(x => x.Address.Guid);
        }

        [Test]
        public void converter_should_not_match_non_primitive_properties_out_of_the_box()
        {
            shouldNotMatch(x => x.Address);
        }

        [Test]
        public void set_a_property_correctly_against_a_binding_context()
        {
            BindingScenario<Address>.For(x =>
            {
                x.Data(o => o.Address1, "2035 Ozark");
            }).Model.Address1.ShouldEqual("2035 Ozark");
        }

        [Test]
        public void setting_a_property_should_log_a_value_converter()
        {
            var scenario = BindingScenario<Address>.For(x =>
            {
                x.Data("Address1", "2035 Ozark");
            });

            var property = typeof(Address).GetProperty("Address1");
            var converter = propertyBinder.As<ConversionPropertyBinder>().FindConverter(property);

            var propertyReport = scenario.Report.For(property);
            propertyReport.Binder.ShouldBeOfType<ConversionPropertyBinder>();
            propertyReport.Converter.ShouldEqual(converter);
        }
    }

    [TestFixture]
    public class IgnorePropertyBinderTester
    {
        private IgnorePropertyBinder _binder;
        private IBindingContext _context;
        private PropertyInfo _property;

        private class SomeObject{public object SomeProperty { get; set; }}

        [SetUp]
        public void SetUp()
        {
            _binder = new IgnorePropertyBinder(info => info.Name == "SomeProperty");
            _context = MockRepository.GenerateMock<IBindingContext>();
        }

        [Test]
        public void should_match()
        {
            _property = ReflectionHelper.GetProperty<SomeObject>(o=>o.SomeProperty);
            _binder.Matches(_property).ShouldBeTrue();
        }

        [Test]
        public void should_ignore_binding()
        {
            //NOTE: Nothing to test, Bind method is empty
            _binder.Bind(_property, _context);
        }
    }
}