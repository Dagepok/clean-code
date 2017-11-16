using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Markdown
{
    [TestFixture]
    public class Md_ShouldRender
    {
        [TestCase(@"\_", "_", TestName = "When simple text")]
        [TestCase(@"___", @"___", TestName = "When more than two underlines")]
        [TestCase(@"text_12_", @"text_12_", TestName = "When underlines near numbers")]
        [TestCase(@"_ text _", @"_ text _", TestName = "When underlines near spaces")]
        public void MD_Should_ReturnSimpleString(string markdown, string html)
        {
            var md = new Md();

            var result = md.RenderToHtml(markdown);

            result.Should().Be(html);
        }

        [TestCase(@"\_", "_", TestName = "Escaped underline")]
        [TestCase(@"\\", @"\", TestName = "Escaped escape char")]
        [TestCase(@"\\\_\\", @"\_\", TestName = "Escaped underline and escape char")]
        public void MD_ShouldRender_EscapeChars(string markdown, string html)
        {
            var md = new Md();

            var result = md.RenderToHtml(markdown);

            result.Should().Be(html);
        }

        [TestCase("_text_", @"<em>text</em>", TestName = "Simple italic text")]
        [TestCase(@"_\__", @"<em>_</em>", TestName = "With escaped chars")]
        [TestCase(@"_I __s__ e_", "<em>I __s__ e</em>", TestName = "With double underlines inside")]
        public void MD_ShouldRender_ItalicText(string markdown, string html)
        {
            var md = new Md();

            var result = md.RenderToHtml(markdown);

            result.Should().Be(html);
        }

        [TestCase(@"__text__", @"<strong>text</strong>", TestName = "When simple text")]
        [TestCase(@"__this _text_ is italic__", @"<strong>this <em>text</em> is italic</strong>",
            TestName = "When inner italic text")]
        public void MD_ShouldRender_BoldText(string markdown, string html)
        {
            var md = new Md();

            var result = md.RenderToHtml(markdown);

            result.Should().Be(html);
        }

        [TestCase(@"__text _asd", @"__text _asd", TestName = "Simple")]
        [TestCase(@"__t _a_", @"__t <em>a</em>", TestName = "With right italic")]
        [TestCase(@"_t __a__", @"_t <strong>a</strong>", TestName = "With right bold")]
        public void MD_ShouldRender_UnpairUnderlines(string markdown, string html)
        {
            var md = new Md();

            var result = md.RenderToHtml(markdown);

            result.Should().Be(html);
        }


    }
}