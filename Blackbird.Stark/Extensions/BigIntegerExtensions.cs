using System.Numerics;

namespace Blackbird.Stark.Extensions
{
    public static class BigIntegerExtensions
    {
        public static BigInteger Sqrt(this BigInteger self)
        {
            if(self < 0)
                return BigInteger.MinusOne;
            if(self == 0)
                return BigInteger.Zero;
            if (self == 1)
                return BigInteger.One;

            var bottom = BigInteger.Zero;
            var top = self;
            while (top != bottom)
            {
                var mid = bottom + (top - bottom) / 2;
                var s = mid * mid;
                switch (s)
                {
                    case BigInteger sqrt when sqrt == self || (sqrt < self && ((mid+1)*(mid+1)) > self):
                        return mid;
                    case BigInteger sqrt when sqrt > self:
                        top = mid;
                        break;
                    case BigInteger sqrt when sqrt < self:
                        bottom = mid;
                        break;
                }
            }
            return BigInteger.MinusOne;
        }
    }
}