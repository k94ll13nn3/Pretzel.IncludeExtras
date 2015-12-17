// Pretzel.IncludeExtras plugin
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.IncludeExtras
{
    [Export(typeof(ITag))]
    internal class GistTag : Tag, ITag
    {
        public new string Name => "Gist";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, TextWriter result)
        {
            base.Render(context, result);
        }
    }
}