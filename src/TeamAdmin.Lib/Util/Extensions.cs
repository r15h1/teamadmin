using System.Text.RegularExpressions;

namespace TeamAdmin.Lib.Util
{
    public static class Extensions
    {
        public static string MakeSEOFriendlyUrl(this string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return "-";

            string text = url.RemoveAccent().ToLower();
            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            text = Regex.Replace(text, @"\s+", " ").Trim();
            // cut and trim 
            text = text.Substring(0, text.Length <= 60 ? text.Length : 60).Trim();
            text = Regex.Replace(text, @"\s", "-"); // hyphens   
            text = Regex.Replace(text, @"\-+", "-"); // successive hyphens  
            return text;
        }

        private static string RemoveAccent(this string text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}