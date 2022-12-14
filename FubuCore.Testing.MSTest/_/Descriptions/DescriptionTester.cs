using System;
using System.Linq;
using FubuCore.Descriptions;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuCore.Testing.Descriptions
{
    [TestFixture]
    public class DescriptionTester
    {
        [Test]
        public void accept_visitor_if_it_only_has_itself()
        {
            var repo = new MockRepository();
            var visitor = repo.StrictMock<IDescriptionVisitor>();

            var description = new Description();

            using (repo.Record())
            {
                visitor.Start(description);
                visitor.End();
            }

            using (repo.Playback())
            {
                description.AcceptVisitor(visitor);
            }
        }

        [Test]
        public void accept_visitory_with_multiple_bullet_lists()
        {
            var repo = new MockRepository();
            var visitor = repo.StrictMock<IDescriptionVisitor>();

            var description = new Description();

            var list = new BulletList();
            list.Children.Add(new Description());
            list.Children.Add(new Description());
            list.Children.Add(new Description());

            description.BulletLists.Add(list);
            description.BulletLists.Add(new BulletList());


            using (repo.Record())
            {
                visitor.Start(description);

                visitor.StartList(list);

                visitor.Start(list.Children[0]);
                visitor.End();

                visitor.Start(list.Children[1]);
                visitor.End();

                visitor.Start(list.Children[2]);
                visitor.End();

                visitor.EndList();

                visitor.StartList(description.BulletLists.Last());
                visitor.EndList();


                visitor.End();
            }

            using (repo.Playback())
            {
                description.AcceptVisitor(visitor);
            }
        }

        [Test]
        public void bullet_list_accept_visitor_with_children()
        {
            var repo = new MockRepository();
            var visitor = repo.StrictMock<IDescriptionVisitor>();

            var list = new BulletList();
            list.Children.Add(new Description());
            list.Children.Add(new Description());
            list.Children.Add(new Description());

            using (repo.Record())
            {
                visitor.StartList(list);

                visitor.Start(list.Children[0]);
                visitor.End();

                visitor.Start(list.Children[1]);
                visitor.End();

                visitor.Start(list.Children[2]);
                visitor.End();

                visitor.EndList();
            }

            using (repo.Playback())
            {
                list.AcceptVisitor(visitor);
            }
        }

        [Test]
        public void bullet_list_accept_visitor_with_no_innards()
        {
            var repo = new MockRepository();
            var visitor = repo.StrictMock<IDescriptionVisitor>();

            var list = new BulletList();

            using (repo.Record())
            {
                visitor.StartList(list);
                visitor.EndList();
            }

            using (repo.Playback())
            {
                list.AcceptVisitor(visitor);
            }
        }

        [Test]
        public void create_description_for_HasDescription_class_just_delegates()
        {
            var description = Description.For(new MakesOwnDescription());
            description.Title.ShouldEqual("the title");
            description.ShortDescription.ShouldEqual("the short description");
            description.TargetType.ShouldEqual(typeof (MakesOwnDescription));
        }

        [Test]
        public void create_description_for_a_class_decorated_with_a_title()
        {
            Description.For(new TitledAndDescribedClass())
                .Title.ShouldEqual("The titled class");
        }

        [Test]
        public void create_description_for_class_decorated_with_the_description_attribute()
        {
            var target = new DecoratedClass();
            var description = Description.For(target);

            description.BulletLists.Any().ShouldBeFalse();
            description.LongDescription.ShouldBeNull();

            description.ShortDescription.ShouldEqual("description from the attribute");
            description.Title.ShouldEqual(typeof (DecoratedClass).Name);

            description.TargetType.ShouldEqual(typeof (DecoratedClass));
        }

        [Test]
        public void default_values_with_a_simple_type()
        {
            var target = new SimpleTarget();
            var description = Description.For(target);

            description.BulletLists.Any().ShouldBeFalse();
            description.LongDescription.ShouldBeNull();
            description.Title.ShouldEqual(typeof (SimpleTarget).Name);
            description.ShortDescription.ShouldEqual(target.ToString());
        }

        [Test]
        public void has_explicit_short_description()
        {
            Description.For(new SimpleTarget()).HasExplicitShortDescription().ShouldBeFalse();

            Description.For(new MakesOwnDescription()).HasExplicitShortDescription().ShouldBeTrue();
        
            Description.For(new TargetWithCustomToString()).HasExplicitShortDescription().ShouldBeTrue();

            new Description().HasExplicitShortDescription().ShouldBeFalse();

            new Description{
                ShortDescription = "something"
            }.HasExplicitShortDescription().ShouldBeTrue();
        }


    }

    [TestFixture]
    public class Description_HasMoreThanTitle_Tester
    {
        private Description description;

        [SetUp]
        public void SetUp()
        {
            description = Description.For(new SimpleTarget());
        }

        [Test]
        public void is_false_with_just_the_title()
        {
            description.HasMoreThanTitle().ShouldBeFalse();
        }

        [Test]
        public void is_true_with_a_property()
        {
            description.Properties["something"] = "else";
            description.HasMoreThanTitle().ShouldBeTrue();
        }

        [Test]
        public void is_true_with_a_child()
        {
            description.Children["something"] = new Description();
            description.HasMoreThanTitle().ShouldBeTrue();
        }

        [Test]
        public void is_true_with_a_short_description()
        {
            description.ShortDescription = "something special";
            description.HasMoreThanTitle().ShouldBeTrue();
        }

        [Test]
        public void is_true_with_a_bullet_list()
        {
            description.BulletLists.Add(new BulletList());

            description.HasMoreThanTitle().ShouldBeTrue();
        }
    }

    public class SimpleTarget
    {
    }

    public class TargetWithCustomToString
    {
        public override string ToString()
        {
            return "something not default";
        }
    }

    public class MakesOwnDescription : DescribesItself
    {
        public void Describe(Description description)
        {
            description.ShortDescription = "the short description";
            description.Title = "the title";
        }
    }


    [System.ComponentModel.Description("description from the attribute")]
    public class DecoratedClass
    {
    }


    [Title("The titled class")]
    public class TitledAndDescribedClass
    {
    }
}