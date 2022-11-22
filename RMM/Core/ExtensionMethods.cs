using RMM.Data;
using System.Text.RegularExpressions;

namespace RMM.Core
{
    public static class ExtensionMethods
    {
        public static List<string> GetEnumNamesCorrected<TEnum>(bool withNull = false) where TEnum : Enum
        {
            var correctedNames = new List<string>();
            foreach (var enumName in Enum.GetNames(typeof(TEnum)))
            {
                var wordsInType =
                    Regex.Matches(enumName.ToString(), @"([A-Z][a-z]+)")
                        .Select(m => m.Value);

                var withSpaces = string.Join(" ", wordsInType);
                if (!withNull && withSpaces == "Null") continue;
                correctedNames.Add(withSpaces);
            }
            return correctedNames;
        }
    }
}
