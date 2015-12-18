// Pretzel.IncludeExtras plugin
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.IncludeExtras
{
    [Export(typeof(ITag))]
    public class GistTag : Tag, ITag
    {
        private string gistId;

        public new string Name => "Gist";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            var arguments = markup.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (arguments.Count() != 1)
            {
                throw new ArgumentException("Expected syntax: {% gist gist_id %}");
            }

            this.gistId = arguments[0];
        }

        public override void Render(Context context, TextWriter result)
        {
            result.Write($"<script src=\"https://gist.github.com/{this.gistId}.js\"></script>");
        }
    }
}