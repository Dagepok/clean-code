using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Markdown
{
    internal class Program
    {
        private static void Main(string[] args)
        {



            BenchmarkRunner.Run<Benchmarks>();
            //var md = new Md();
            //var spec = File.ReadAllText("Spec.Md");
            //var parts = spec.Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //var resultParts = parts.Select(md.RenderToHtml);
            //var result = string.Join("\r\n\r\n", resultParts);
            //File.WriteAllText("result.html", result);
        }

    }

    public class Benchmarks
    {
        public string Str => "_a_ __b__ \\_";
        public List<string> Strings = new List<string>();


        public Benchmarks()
        {
            foreach (var count in new[] { 1, 10, 100, 1000, 10000})
            {
                var sb = new StringBuilder();
                for (var i = 0; i < count; i++)
                    sb.Append(Str);
                Strings.Add(sb.ToString());
            }
        }
        

        [Benchmark]
        public void GetResult1() => new Md().RenderToHtml(Strings[0]);
        [Benchmark]
        public void GetResult10() => new Md().RenderToHtml(Strings[1]);
        [Benchmark]
        public void GetResult100() => new Md().RenderToHtml(Strings[2]);
        [Benchmark]
        public void GetResult1000() => new Md().RenderToHtml(Strings[3]);
        [Benchmark]
        public void GetResult10000() => new Md().RenderToHtml(Strings[4]);
        

    }
}