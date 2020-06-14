using System.Collections.Generic;
using System.Linq;
using Blackbird.Stark.Ranges;

namespace Blackbird.Stark.Extensions
{
    public static class RangeExtensions
    {
        public static IEnumerable<Range> MergeOverlappingRanges(this IEnumerable<Range> self)
        {
            var nonOverlapping = new List<Range>();
            var rngs = self.ToList();

            while (rngs.Any())
            {
                var r = rngs.First();
                rngs.Remove(r);
                var merged = r;
                if (rngs.Any(x => r.IsOverlapping(x)))
                {
                    var overlapping = rngs.FirstOrDefault(x => r.IsOverlapping(x));
                    rngs.Remove(overlapping);
                    merged = merged.Merge(overlapping);
                    rngs.Add(merged);
                }
                else
                    nonOverlapping.Add(merged);
            }

            return nonOverlapping;
        }
    }
}
