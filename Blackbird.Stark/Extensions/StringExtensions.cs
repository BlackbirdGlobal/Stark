using System.Linq;

namespace Blackbird.Stark.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether string represents a valid number
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNumber(this string self)
        {
            if (string.IsNullOrWhiteSpace(self))
                return false;
            var trimmed = self.Trim();
            var hasDelimiter = false;
            for (var i = 0; i < trimmed.Length; i++)
            {
                if (i == 0 && trimmed[i] == '-')
                    continue;
                if (!hasDelimiter && (trimmed[i] == '.' || trimmed[i] == ','))
                {
                    hasDelimiter = true;
                    continue;
                }

                if (hasDelimiter && (trimmed[i] == '.' || trimmed[i] == ','))
                {
                    return false;
                }

                if (!char.IsNumber(trimmed[i]))
                    return false;
            }

            return true;
        }
    }
}
