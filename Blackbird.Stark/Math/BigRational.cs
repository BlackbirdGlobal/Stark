using System.Numerics;

namespace Blackbird.Stark.Math
{
    public struct BigRational
    {
        private BigInteger _numerator;
        private BigInteger _denumerator;
        
        
        public BigRational(BigInteger val)
        {
            _numerator = val;
            _denumerator = BigInteger.One;
        }


        public static BigRational Zero { get; } = new BigRational(BigInteger.Zero);
        public static BigRational One { get; } = new BigRational(BigInteger.One);
        public static BigRational MinusOne { get; } = new BigRational(BigInteger.MinusOne);
    }
}