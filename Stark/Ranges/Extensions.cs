using System.Collections.Generic;
using System.Linq;

namespace Stark.Ranges
{
    public static class Extensions
    {
        public static IEnumerable<Range> MergeOverlapping(this IEnumerable<Range> self)
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
                    var overlapping = rngs.Where(x => r.IsOverlapping(x)).ToList();

                    foreach (var o in overlapping)
                    {
                        merged = merged.Merge(o);
                        rngs.Remove(o);
                    }
                    rngs.Add(merged);
                }
                else
                    nonOverlapping.Add(merged);
            }

            return nonOverlapping;
        }
    }
}
