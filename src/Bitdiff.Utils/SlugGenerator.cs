using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Bitdiff.Utils
{
    public class SlugGenerator
    {
        public string GenerateSlug(string input)
        {
            if (!input.HasValue())
                return String.Empty;

            string output = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(input)).ToLowerInvariant();

            output = Regex.Replace(output, @"[^a-z0-9\s-]", "");
            output = Regex.Replace(output, @"\s+", " ").Trim(); 
            output = Regex.Replace(output, @"\s", "-");
            output = Regex.Replace(output, @"[-]{2,}", "-");

            return output;
        }
    }
}