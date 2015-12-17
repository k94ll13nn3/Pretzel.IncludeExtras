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
        public void Initialize_OneInvalidArgument_ThrowsArgumentException()
        {
            const string gistId = "noJ6ztdlFU";

            Assert.That(() => Template.Parse($"{{% gist {gistId} %}}"), Throws.ArgumentException.And.Message.EqualTo($"The gist with the id \"{gistId}\" does not exist."));
        }

        [Test]
        public void Initialize_OneValidArgument_ThrowsNothing()
        {
            Assert.That(() => Template.Parse("{% gist 90bcfca6ce85c9031a6f %}"), Throws.Nothing);
        }

        [Test]
        public void Initialize_ZeroOrTooManyArgumentsPassed_ThrowsArgumentException()
        {
            Assert.That(() => Template.Parse("{% gist %}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
            Assert.That(() => Template.Parse("{% gist noJ6ztdlFU KMT6B7DTLm%}"), Throws.ArgumentException.And.Message.EqualTo(syntaxMessage));
        }

        [Test]
        public void Render_GistIdPassed_TagRendered()
        {
            var template = Template.Parse("{% gist 90bcfca6ce85c9031a6f %}");

            Assert.That(template.Render(), Is.EqualTo("<script src=\"https://gist.github.com/90bcfca6ce85c9031a6f.js\"></script>"));
        }
    }
}