using DotLiquid;
using NUnit.Framework;

namespace Pretzel.IncludeExtras.Tests
{
    [TestFixture]
    public class GistTests
    {
        private const string syntaxMessage = "Expected syntax: {% gist gist_id %}";

        [OneTimeSetUp]
        public void Init()
        {
            Template.RegisterTag<GistTag>("gist");
        }

        [Test]
        public void Initialize_GistIdIsPassed_ThrowsNothing()
        {
            Assert.That(() => Template.Parse("{% gist 90bcfca6ce85c9031a6f %}"), Throws.Nothing);
            Assert.That(() => Template.Parse("{% gist 90bcfca6ce85c9031a6f        %}"), Throws.Nothing);
        }

        [Test]
        public void Initialize_ZeroOrTooManyArgumentsArePassed_ThrowsArgumentException()
        {
            Assert.That(() => Template.Parse("{% gist %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
            Assert.That(() => Template.Parse("{% gist noJ6ztdlFU KMT6B7DTLm%}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
        }

        [Test]
        public void Render_MarkupIsValid_TagRendered()
        {
            const string gistId = "90bcfca6ce85c9031a6f";

            var template = Template.Parse($"{{% gist {gistId} %}}");

            Assert.That(template.Render(), Is.EqualTo($"<script src=\"https://gist.github.com/{gistId}.js\"></script>"));
        }
    }
}