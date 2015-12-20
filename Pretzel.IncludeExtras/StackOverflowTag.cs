// Pretzel.IncludeExtras plugin
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Text.RegularExpressions;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.IncludeExtras
{
    [Export(typeof(ITag))]
    public class StackOverflowTag : Tag, ITag
    {
        private string html;

        public new string Name => "StackOverflow";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            // Group 1 : user id | Group 2 : theme
            // Group 3 : width   | Group 4 : height
            var match = Regex.Match(markup.Trim(), @"^\s*(\d+)(?:\s+(dark|default|clean|hotdog))?(?:\s+(?:(\d+)(?:\s+(\d+))?))?$");
            if (!match.Success)
            {
                throw new ArgumentException("Expected syntax: {% stack_overflow user_id [default|dark|clean|hotdog] [width [height]] %}");
            }

            var attributes = new string[3];
            var theme = string.Empty;
            if (match.Groups[2].Success)
            {
                theme = $"?theme={match.Groups[2].Value}";
            }

            attributes[0] = $"src=\"https://stackoverflow.com/users/flair/{match.Groups[1].Value}.png{theme}\"";

            if (match.Groups[3].Success)
            {
                attributes[1] = $" width=\"{match.Groups[3].Value}\"";
            }

            if (match.Groups[4].Success)
            {
                attributes[2] = $" height=\"{match.Groups[4].Value}\"";
            }

            this.html = $"<a href=\"https://stackoverflow.com/users/{match.Groups[1].Value}\"><img {attributes[0]}{attributes[1]}{attributes[2]}></img></a>";
        }

        public override void Render(Context context, TextWriter result)
        {
            result.Write(this.html);
        }
    }
}