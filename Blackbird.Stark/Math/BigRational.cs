using System;
using System.Numerics;
using Blackbird.Stark.Extensions;

namespace Blackbird.Stark.Math
{
    public struct BigRational: IEquatable<BigRational>
    {
        private BigInteger _numerator;
        private BigInteger _denominator;
        public int Sign => _numerator.Sign;
        
        public static BigRational Zero { get; } = new BigRational(BigInteger.Zero);
        public static BigRational One { get; } = new BigRational(BigInteger.One);
        public static BigRational MinusOne { get; } = new BigRational(BigInteger.MinusOne);
        
        public BigRational(BigInteger val)
        {
            _numerator = val;
            _denominator = BigInteger.One;
        }
        
        public BigRational(BigInteger numerator, BigInteger denominator)
        {
            if (denominator.Sign == 0) {
                throw new DivideByZeroException();
            }
            else if (numerator.Sign == 0) {
                // 0/m -> 0/1
                _numerator   = BigInteger.Zero;
                _denominator = BigInteger.One;
            }
            else if (denominator.Sign < 0) {
                _numerator = BigInteger.Negate(numerator);
                _denominator = BigInteger.Negate(denominator);
            }
            else {
                _numerator = numerator;
                _denominator = denominator;
            }
            this = Reduce(this); 
        }

        public static BigRational Parse(string val)
        {
            if(!val.IsNumber())
                throw new ArgumentException($"Value is not a number: {val}");

            var delimiterIndex = val.IndexOf('.');
            if (delimiterIndex == -1)
                delimiterIndex = val.IndexOf(',');

            var result = new BigRational();
            
            if (delimiterIndex == -1)
            {
                result._numerator = BigInteger.Parse(val);
                result._denominator = BigInteger.One;
            }
            else
            {
                //TODO: get count of signs after delimiter
                //TODO: xxx.zzz = xxxzzz/1000 
                var numbersAfterDelimiter = val.Length - delimiterIndex -1;
                result._denominator = BigInteger.Pow(10, numbersAfterDelimiter);
                result._numerator = BigInteger.Parse(val.Remove(delimiterIndex, 1));
                result = Reduce(result);   
            }

            return result;
        }

        public static bool TryParse(string val, out BigRational res)
        {
            try
            {
                res = BigRational.Parse(val);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Equals(BigRational other)
        {
            if (this._denominator == other._denominator) {
                return _numerator == other._numerator;
            }
            else {
                return (_numerator * other._denominator) == (_denominator * other._numerator);
            }
        }
        
        public static BigRational operator + (BigRational r1, BigRational r2) {
            // a/b + c/d  == (ad + bc)/bd
            return new BigRational((r1._numerator * r2._denominator) + (r1._denominator * r2._numerator), (r1._denominator * r2._denominator));
        }

        private static BigRational Reduce(BigRational val)
        {
            var reduced = new BigRational {_denominator = val._denominator, _numerator = val._numerator};

            if (val._numerator == BigInteger.Zero)
            {
                reduced._denominator = BigInteger.One;
            }

            var gcd = BigInteger.GreatestCommonDivisor(val._numerator, val._denominator);
            
            if (gcd > BigInteger.One)
            {
                reduced._numerator = val._numerator / gcd;
                reduced._denominator = val._denominator / gcd;
            }

            return reduced;
        }
    }
}