using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthHttpClient.Helpers
{
    internal static class HttpUtility
    {
        private const char QueryStartSymbol = '?';
        private const char QuerySeporatorSymbol = '&';
        private const char KeyValueSeporatorSymbol = '=';

        public static string BuildQueryString(Dictionary<string, string> queryParameters)
        {
            return string.Join(QuerySeporatorSymbol.ToString(),
                queryParameters.Select(e => $"{e.Key}{KeyValueSeporatorSymbol}{Uri.EscapeDataString(e.Value)}"));
        }

        public static Dictionary<string, string> ParseQueryString(string query)
        {
            var startIndex = query.LastIndexOf(QueryStartSymbol);
            if (startIndex >= 0 && query.Length > startIndex)
                query = query.Substring(startIndex + 1);
            
            string[] pairs = query.Split(QuerySeporatorSymbol);

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (string piece in pairs)
            {
                string[] pair = piece.Split(KeyValueSeporatorSymbol);
                if (pair.Length == 2)
                    result[pair[0]] = Uri.UnescapeDataString(pair[1]);
            }

            return result;
        }
    }
}
