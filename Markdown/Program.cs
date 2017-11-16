using System;
using System.IO;
using System.Linq;

namespace Markdown
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var md = new Md();
            var spec = File.ReadAllText("Spec.Md");
            var parts = spec.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var resultParts = parts.Select(md.RenderToHtml);
            var result = string.Join("\r\n\r\n", resultParts);
            File.WriteAllText("result.html", result);
        }
    }
}