using DotLiquid;
using NUnit.Framework;

namespace Pretzel.IncludeExtras.Tests
{
    [TestFixture]
    public class StackOverflowTests
    {
        private const string syntaxMessage = "Expected syntax: {% stack_overflow user_id [default|dark|clean|hotdog] [width [height]] %}";

        [OneTimeSetUp]
        public void Init()
        {
            Template.RegisterTag<StackOverflowTag>("stack_overflow");
        }

        [Test]
        public void Initialize_HeightIsPassedAndIsInvalid_ThrowsArgumentException()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 123456 280 5O %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
            Assert.That(() => Template.Parse("{% stack_overflow 123456 default 280 5O %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
        }

        [Test]
        public void Initialize_HeightIsPassedAndIsValid_ThrowsNothing()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 123456 280 50 %}"), Throws.Nothing);
            Assert.That(() => Template.Parse("{% stack_overflow 123456 default 280 50 %}"), Throws.Nothing);
        }

        [Test]
        public void Initialize_NoArgumentIsPassed_ThrowsArgumentException()
        {
            Assert.That(() => Template.Parse("{% stack_overflow %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
        }

        [Test]
        public void Initialize_ThemeIsPassedAndIsInvalid_ThrowsArgumentException()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 123456 theme %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
            Assert.That(() => Template.Parse("{% stack_overflow 123456 th3m3 %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
        }

        [Test]
        public void Initialize_ThemeIsPassedAndIsValid_ThrowsNothing()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 123456 default %}"), Throws.Nothing);
            Assert.That(() => Template.Parse("{% stack_overflow 123456 clean %}"), Throws.Nothing);
            Assert.That(() => Template.Parse("{% stack_overflow 123456 dark %}"), Throws.Nothing);
            Assert.That(() => Template.Parse("{% stack_overflow 123456 hotdog %}"), Throws.Nothing);
        }

        [Test]
        public void Initialize_UserIdIsPassedAndIsInvalid_ThrowsArgumentException()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 12E4S6 %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
        }

        [Test]
        public void Initialize_UserIdIsPassedAndIsValid_ThrowsNothing()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 123456 %}"), Throws.Nothing);
        }

        [Test]
        public void Initialize_WidthIsPassedAndIsInvalid_ThrowsArgumentException()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 123456 2B0 %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
            Assert.That(() => Template.Parse("{% stack_overflow 123456 theme 2B0 %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
        }

        [Test]
        public void Initialize_WidthIsPassedAndIsValid_ThrowsNothing()
        {
            Assert.That(() => Template.Parse("{% stack_overflow 123456 280 %}"), Throws.Nothing);
            Assert.That(() => Template.Parse("{% stack_overflow 123456 default 280 %}"), Throws.Nothing);
        }

        [Test]
        public void Render_MarkupIsValid_TagRendered()
        {
            const string baseUrl = "https://stackoverflow.com/users";
            const int userId = 123456;

            var render1 = Template.Parse($"{{% stack_overflow {userId} %}}").Render();
            var render2 = Template.Parse($"{{% stack_overflow {userId} default %}}").Render();
            var render3 = Template.Parse($"{{% stack_overflow {userId} dark %}}").Render();
            var render4 = Template.Parse($"{{% stack_overflow {userId} clean %}}").Render();
            var render5 = Template.Parse($"{{% stack_overflow {userId} hotdog %}}").Render();
            var render6 = Template.Parse($"{{% stack_overflow {userId} 280 %}}").Render();
            var render7 = Template.Parse($"{{% stack_overflow {userId} 280 50 %}}").Render();
            var render8 = Template.Parse($"{{% stack_overflow {userId} dark 280 50 %}}").Render();

            Assert.That(render1, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png\"></img></a>"));
            Assert.That(render2, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png?theme=default\"></img></a>"));
            Assert.That(render3, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png?theme=dark\"></img></a>"));
            Assert.That(render4, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png?theme=clean\"></img></a>"));
            Assert.That(render5, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png?theme=hotdog\"></img></a>"));
            Assert.That(render6, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png\" width=\"280\"></img></a>"));
            Assert.That(render7, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png\" width=\"280\" height=\"50\"></img></a>"));
            Assert.That(render8, Is.EqualTo($"<a href=\"{baseUrl}/{userId}\"><img src=\"{baseUrl}/flair/{userId}.png?theme=dark\" width=\"280\" height=\"50\"></img></a>"));
        }
    }
}