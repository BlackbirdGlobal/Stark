using System.Linq;

namespace Blackbird.Stark.Extensions
{
    public static class StringExtensions
    {
        private static readonly char[] Numbers = new []{'0','1','2','3','4','5','6','7','8','9'};
         
        /// <summary>
        /// Checks whether string represents a valid number
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNumber(this string self)
        {
            if (self.IsNullOrWhiteSpace())
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

                if (!Numbers.Contains(trimmed[i]))
                    return false;
            }
            return true;
        }
        
        public static bool IsNullOrWhiteSpace(this string self)
        {
            return string.IsNullOrWhiteSpace(self);
        }
        
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }
    }
}
