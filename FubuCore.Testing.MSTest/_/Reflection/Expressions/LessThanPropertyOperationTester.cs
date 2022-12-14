using System;
using FubuCore.Reflection.Expressions;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuCore.Testing.Reflection.Expressions
{
    public class LessThanPropertyOperationTester
    {
    }

    [TestFixture]
    public class when_building_a_predicate_for_less_than
    {
        private Func<Contract, bool> _builtPredicate;

        [SetUp]
        public void Setup()
        {
            var builder = new LessThanPropertyOperation();
            _builtPredicate = builder.GetPredicateBuilder<Contract>(c => c.Purchased)(18).Compile();
        }

        [Test] public void should_succeed_when_the_property_is_less_than_the_given_value()
        {
            var contract = new Contract();
            contract.Purchased = 15;
            _builtPredicate(contract).ShouldBeTrue();
        }

        [Test]
        public void should_not_succeed_when_the_property_is_greater_than_the_given_value()
        {
            var contract = new Contract();
            contract.Purchased = 20;
            _builtPredicate(contract).ShouldBeFalse();
        }

        [Test]
        public void should_not_succeed_when_the_property_is_equal_to_the_given_value()
        {
            var contract = new Contract();
            contract.Purchased = 18;
            _builtPredicate(contract).ShouldBeFalse();
        }
    }

}