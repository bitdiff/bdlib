using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace Bitdiff.Utils
{
    public static class StringExtensions
    {
        private static readonly Regex NonAlphaNumericCharacters = new Regex(@"[^A-Za-z0-9]+", RegexOptions.Compiled);
        private static readonly Markdown Markdown = new Markdown();
        private static readonly SlugGenerator SlugGenerator = new SlugGenerator();

        public static string AsNullIfEmpty(this string input)
        {
            return input.DoesNotHaveValue() ? null : input;
        }

        public static string AsEmptyIfNull(this string input)
        {
            return input.DoesNotHaveValue() ? string.Empty : input;
        }

        public static string SafeTrim(this string input)
        {
            return input.DoesNotHaveValue() ? String.Empty : input.Trim();
        }

        public static bool DoesNotHaveValue(this string input)
        {
            return !input.HasValue();
        }

        public static string StripHtmlTags(this string input)
        {
            if (input.DoesNotHaveValue())
                return String.Empty;

            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(input, "");
        }

        public static bool HasValue(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static string ToTitleCase(this string input)
        {
            if (input.DoesNotHaveValue())
                return String.Empty;

            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(input.ToLower());
        }

        public static bool Contains(this string input, string value, StringComparison comparisonType)
        {
            if (input.DoesNotHaveValue())
                return false;

            return input.IndexOf(value, comparisonType) >= 0;
        }

        public static string Truncate(this string input, int maxLength)
        {
            if (input.DoesNotHaveValue() || maxLength <= 0)
                return String.Empty;

            if (input.Length <= maxLength)
                return input;

            const string suffix = "...";
            var suffixLength = suffix.Length;

            int index = input.Trim().LastIndexOf(" ", StringComparison.Ordinal);

            while ((index + suffixLength) > maxLength)
                index = input.Substring(0, index).Trim().LastIndexOf(" ", StringComparison.Ordinal);

            if (index > 0)
                return input.Substring(0, index) + suffix;

            return input.Substring(0, maxLength - suffixLength) + suffix;
        }

        public static string ToSlug(this string input)
        {
            return input.HasValue() ? SlugGenerator.GenerateSlug(input) : input;
        }

        public static string NormaliseForTracking(this string input)
        {
            return input.HasValue() ? NonAlphaNumericCharacters.Replace(input.ToLowerInvariant(), "") : input;
        }

        public static string NormaliseAlphaNumeric(this string input)
        {
            return input.HasValue() ? NonAlphaNumericCharacters.Replace(input.ToLowerInvariant(), "_") : input;
        }

        public static string ToHtmlFromMarkdown(this string markdown)
        {
            return Markdown.Transform(markdown);
        }

        public static string F(this string input, params object[] args)
        {
            return String.Format(input, args);
        }
    }
}