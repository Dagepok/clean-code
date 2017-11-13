using System;
using System.Collections.Generic;
using System.Linq;

namespace Markdown
{
    public class Md
    {

        public string RenderToHtml(string markdown)
        {
            var renderer = new ToHtmlRenderer(markdown);
            return renderer.Render();
        }
    }
}