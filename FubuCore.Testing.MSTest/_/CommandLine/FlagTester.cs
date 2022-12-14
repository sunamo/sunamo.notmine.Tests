using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuCore.CommandLine;
using FubuCore.Conversion;
using FubuTestingSupport;
using NUnit.Framework;
using FubuCore.Reflection;

namespace FubuCore.Testing.CommandLine
{
    [TestFixture]
    public class FlagTester
    {
        private Flag forProp(Expression<Func<FlagTarget, object>> expression)
        {
            return new Flag(expression.ToAccessor().InnerProperty, new ObjectConverter());
        }

        [Test]
        public void to_usage_description_for_a_simple_non_aliased_field()
        {
            forProp(x => x.NameFlag).ToUsageDescription().ShouldEqual("[-n, --name <name>]");
        }

        [Test]
        public void to_usage_description_for_a_simple_aliased_field()
        {
            forProp(x => x.AliasFlag).ToUsageDescription().ShouldEqual("[-a, --aliased <alias>]");
        }

    

        [Test]
        public void to_usage_description_for_an_enum_field()
        {
            forProp(x => x.EnumFlag).ToUsageDescription().ShouldEqual("[-e, --enum red|blue|green]");
        }


        [Test]
        public void should_provide_useful_error_message_when_no_value_provided()
        {
            typeof(InvalidUsageException).ShouldBeThrownBy(() =>
                forProp(x => x.AliasFlag).Handle(new FlagTarget(), new Queue<string>(new[] { "-a" })))
                .Message.ShouldEqual("No value specified for flag -a.");
        }

        [Test]
        public void should_catch_invalid_enum_value()
        {
            typeof(ArgumentException).ShouldBeThrownBy(() =>
                forProp(x => x.EnumFlag).Handle(new FlagTarget(), new Queue<string>(new[] { "-e", "x" })));
        }

        private EnumerableFlag forArg(Expression<Func<FlagTarget, object>> expression)
        {
            return new EnumerableFlag(expression.ToAccessor().InnerProperty, new ObjectConverter());
        }

        [Test]
        public void to_usage_description_for_a_enumerable_flag()
        {
            forArg(x => x.HerpDerpFlag).ToUsageDescription().ShouldEqual("[-h, --herp-derp [<herpderp1 herpderp2 herpderp3 ...>]]");
        }

        [Test]
        public void should_provide_error_message()
        {
            typeof(InvalidUsageException).ShouldBeThrownBy(() =>
               forArg(x => x.HerpDerpFlag).Handle(new FlagTarget(), new Queue<string>(new[] { "-h" })))
               .Message.ShouldEqual("No values specified for flag -h.");
        }

        [Test]
        public void enumerable_flag_should_handle_arguments_correctly()
        {
            var flagTarget = new FlagTarget();
            forArg(x => x.HerpDerpFlag).Handle(flagTarget, new Queue<string>(new[] {"-h", "a", "b", "-c"}));

            flagTarget.HerpDerpFlag.ShouldHaveTheSameElementsAs("a","b");
        }
    }

    public enum FlagEnum
    {
        red, blue, green
    }

    public class FlagTarget
    {
        public string NameFlag { get; set; }

        public FlagEnum EnumFlag { get; set; }

        [FlagAlias("aliased", 'a')]
        public string AliasFlag { get; set;}

        public IEnumerable<string> HerpDerpFlag { get; set; }
    }
}