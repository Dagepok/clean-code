namespace Markdown
{
    public class Md
    {
        public string RenderToHtml(string markdown)
        {
            var renderer = new HtmlRenderer(markdown);
            return renderer.Render();
        }
    }
}