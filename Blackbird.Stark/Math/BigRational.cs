using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.Serialization;
using Blackbird.Stark.Extensions;

namespace Blackbird.Stark.Math
{
    public struct BigRational: IEquatable<BigRational>, IComparable, IComparable<BigRational>, ISerializable, IDeserializationCallback
    {
        #region Instance Fields
        
        private BigInteger _numerator;
        private BigInteger _denominator;
        public int Sign => _numerator.Sign;
        
        #endregion
        
        #region Static Properties
        
        public static BigRational Zero { get; } = new BigRational(BigInteger.Zero);
        public static BigRational One { get; } = new BigRational(BigInteger.One);
        public static BigRational MinusOne { get; } = new BigRational(BigInteger.MinusOne);
        
        #endregion
        
        #region Constructors
        
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

            if (numerator.Sign == 0) {
                // 0/n = 0/1
                _numerator   = BigInteger.Zero;
                _denominator = BigInteger.One;
            }
            else if (_denominator.Sign < 0) {
                _numerator = BigInteger.Negate(numerator);
                _denominator = BigInteger.Negate(denominator);
            }
            else {
                _numerator = numerator;
                _denominator = denominator;
            }
            this = Reduce(this); 
        }
        
        #endregion

        #region Static Methods
        
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
                // get count of signs after delimiter
                // xxx.zzz = xxxzzz/1000 
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
                res = Parse(val);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static BigRational Abs(BigRational r) {
            return (r._numerator.Sign < 0 ? new BigRational(BigInteger.Abs(r._numerator), r._denominator) : r);
        }
        
        public static BigRational Negate(BigRational r) {
            return new BigRational(BigInteger.Negate(r._numerator), r._denominator);
        }

        public static BigRational Invert(BigRational r) {
            return new BigRational(r._denominator, r._numerator);
        }

        public static BigRational Add(BigRational x, BigRational y) {
            return x + y;
        }

        public static BigRational Subtract(BigRational x, BigRational y) {
            return x - y;
        }


        public static BigRational Multiply(BigRational x, BigRational y) {
            return x * y;
        }

        public static BigRational Divide(BigRational dividend, BigRational divisor) {
            return dividend / divisor;
        }

        public static BigRational Remainder(BigRational dividend, BigRational divisor) {
            return dividend % divisor;
        }
        
        public static BigRational Pow(BigRational baseValue, BigInteger exponent) {
            if (exponent.Sign == 0) {
                // 0^0 = 1
                // n^0 = 1
                return One;
            }

            if (exponent.Sign < 0) {
                if (baseValue == Zero) {
                    throw new ArgumentException("cannot raise zero to a negative power", "baseValue");
                }
                // n^(-e) = (1/n)^e
                baseValue = Invert(baseValue);
                exponent  = BigInteger.Negate(exponent);
            }

            BigRational result = baseValue;
            while (exponent > BigInteger.One) {
                result *= baseValue;
                exponent--;
            }

            return result;
        }
        
        public static BigInteger LeastCommonDenominator(BigRational x, BigRational y) {
            // LCD( a/b, c/d ) == (bd) / gcd(b,d)
            return (x._denominator * y._denominator) / BigInteger.GreatestCommonDivisor(x._denominator, y._denominator);
        }
        
        public static int Compare(BigRational r1, BigRational r2) {
            // a/b = c/d, if ad = bc
            return BigInteger.Compare(r1._numerator * r2._denominator, r2._numerator * r1._denominator);
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
        
        #endregion

        #region Instance Methods
        
        public bool Equals(BigRational other)
        {
            if (_denominator == other._denominator) {
                return _numerator == other._numerator;
            }

            return (_numerator * other._denominator) == (_denominator * other._numerator);
        }

        public int CompareTo(BigRational other)
        {
            return Compare(this, other);
        }

        public override bool Equals(Object obj) {
            if (obj == null)
                return false;

            if (!(obj is BigRational))
                return false;
            return Equals((BigRational)obj);
        }

        public override int GetHashCode() {
            return (_numerator / _denominator).GetHashCode();
        }
        
        public override string ToString()
        {
            var whole = BigInteger.Divide(_numerator, _denominator).ToString("R", CultureInfo.InvariantCulture);
            string fraction = default;
            var r = BigInteger.Remainder(_numerator, _denominator);
            r = r < 0 ? -r : r;
            var tmp = r;
            while (r > 0)
            {
                tmp *= 10;
                r = BigInteger.Remainder(tmp, _denominator);
                var w = BigInteger.Divide(tmp, _denominator);
                if (w > 0)
                    tmp = r;
                fraction +=  w.ToString("R", CultureInfo.InvariantCulture);
            }

            return $"{whole}.{fraction}";
        }

        public void OnDeserialization(object sender)
        {
            try {
                // verify that the deserialized number is well formed
                if (_denominator.Sign == 0 || _numerator.Sign == 0) {
                    // n/0 = 0/1
                    // 0/m = 0/1
                    _numerator = BigInteger.Zero;
                    _denominator = BigInteger.One;
                }
                else if (_denominator.Sign < 0) {
                    _numerator = BigInteger.Negate(_numerator);
                    _denominator = BigInteger.Negate(_denominator);
                }

                this = Reduce(this);
            }
            catch (ArgumentException e) {
                throw new SerializationException("invalid serialization data", e);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Numerator", _numerator);
            info.AddValue("Denominator", _denominator);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is BigRational))
                return 1;
            return Compare(this, (BigRational)obj);
        }

        #endregion

        #region Operators
        
        public static BigRational operator + (BigRational r1, BigRational r2) {
            // a/b + c/d  = (ad + bc)/bd
            return new BigRational((r1._numerator * r2._denominator) + (r1._denominator * r2._numerator), (r1._denominator * r2._denominator));
        }
        
        public static BigRational operator - (BigRational r1, BigRational r2) {
            // a/b - c/d  = (ad - bc)/bd
            return new BigRational((r1._numerator * r2._denominator) - (r1._denominator * r2._numerator), (r1._denominator * r2._denominator));
        }

        public static BigRational operator * (BigRational r1, BigRational r2) {
            // a/b * c/d  = (ac)/(bd)
            return new BigRational((r1._numerator * r2._numerator), (r1._denominator * r2._denominator));
        }

        public static BigRational operator / (BigRational r1, BigRational r2) {
            // a/b / c/d  = (ad)/(bc)
            return new BigRational((r1._numerator * r2._denominator), (r1._denominator * r2._numerator));
        }

        public static BigRational operator % (BigRational r1, BigRational r2) {
            // a/b % c/d  = (ad % bc)/bd
            return new BigRational((r1._numerator * r2._denominator) % (r1._denominator * r2._numerator), (r1._denominator * r2._denominator));
        }
        
        public static bool operator ==(BigRational x, BigRational y) {
            return Compare(x, y) == 0;
        }

        public static bool operator !=(BigRational x, BigRational y) {
            return Compare(x, y) != 0;
        }

        public static bool operator <(BigRational x, BigRational y) {
            return Compare(x, y) < 0;
        }

        public static bool operator <=(BigRational x, BigRational y) {
            return Compare(x, y) <= 0;
        }

        public static bool operator >(BigRational x, BigRational y) {
            return Compare(x, y) > 0;
        }

        public static bool operator >=(BigRational x, BigRational y) {
            return Compare(x, y) >= 0;
        }
 
        public static BigRational operator +(BigRational r) {
            return r;
        }

        public static BigRational operator -(BigRational r) {
            return new BigRational(-r._numerator, r._denominator);
        }

        public static BigRational operator ++ (BigRational r) {
            return r + One;
        }

        public static BigRational operator -- (BigRational r) {
            return r - One;
        }
        
        #endregion
    }
}