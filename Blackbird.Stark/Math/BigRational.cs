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
        private BigInteger _denominator ;
        
        private BigInteger Denominator
        {
            get => _denominator == default ? BigInteger.One : _denominator;
            set => _denominator = value;
        }
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
        
        internal BigRational(SerializationInfo info, StreamingContext context) {
            if (info == null) {
                throw new ArgumentNullException(nameof(info));
            }

            _numerator   = (BigInteger)info.GetValue("Numerator", typeof(BigInteger));
            _denominator = (BigInteger)info.GetValue("Denominator", typeof(BigInteger));
        }

        public BigRational(decimal value)
        {
            //only 29 digits max according to MSDN
            _denominator = BigInteger.One;;
            while (value % 1 > 0)
            {
                _denominator *= 10;
                value *= 10;
            }
            _numerator = new BigInteger(value);
            this = Reduce(this);
        }

        public BigRational(double value): this(new decimal(value))
        {
        }

        private BigRational(float value) : this(new decimal(value))
        {
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
                result.Denominator = BigInteger.One;
            }
            else
            {
                // get count of signs after delimiter
                // xxx.zzz = xxxzzz/1000 
                var numbersAfterDelimiter = val.Length - delimiterIndex -1;
                result.Denominator = BigInteger.Pow(10, numbersAfterDelimiter);
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
                res = default;
                return false;
            }
        }
        
        public static BigRational Abs(BigRational r) {
            return (r._numerator.Sign < 0 ? new BigRational(BigInteger.Abs(r._numerator), r.Denominator) : r);
        }
        
        public static BigRational Negate(BigRational r) {
            return new BigRational(BigInteger.Negate(r._numerator), r.Denominator);
        }

        public static BigRational Invert(BigRational r) {
            return new BigRational(r.Denominator, r._numerator);
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
                    throw new ArgumentException("cannot raise zero to a negative power", nameof(baseValue));
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
            return (x.Denominator * y.Denominator) / BigInteger.GreatestCommonDivisor(x.Denominator, y.Denominator);
        }
        
        public static int Compare(BigRational r1, BigRational r2) {
            // a/b = c/d, if ad = bc
            return BigInteger.Compare(r1._numerator * r2.Denominator, r2._numerator * r1.Denominator);
        }
        
        private static BigRational Reduce(BigRational val)
        {
            var reduced = new BigRational {Denominator = val.Denominator, _numerator = val._numerator};

            if (val._numerator == BigInteger.Zero)
            {
                reduced.Denominator = BigInteger.One;
            }

            var gcd = BigInteger.GreatestCommonDivisor(val._numerator, val.Denominator);
            
            if (gcd > BigInteger.One)
            {
                reduced._numerator = val._numerator / gcd;
                reduced.Denominator = val.Denominator / gcd;
            }

            return reduced;
        }
        
        #endregion

        #region Instance Methods
        
        public bool Equals(BigRational other)
        {
            if (Denominator == other.Denominator) {
                return _numerator == other._numerator;
            }

            return (_numerator * other.Denominator) == (Denominator * other._numerator);
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
            return (_numerator / Denominator).GetHashCode();
        }
        
        /// <summary>
        /// Converts to string representation of BigRational
        /// </summary>
        /// <param name="precision">value represents number of signs after the separator</param>
        /// <returns></returns>
        public string ToString(uint precision = 512)
        {
            if (precision <= 0) throw new ArgumentOutOfRangeException(nameof(precision));
            var whole = BigInteger.Divide(_numerator, Denominator).ToString("R", CultureInfo.InvariantCulture);
            string fraction = default;
            var r = BigInteger.Remainder(_numerator, Denominator);
            r = r < 0 ? -r : r;
            var tmp = r;
            var i = 0;
            while (r > 0 && i < precision)
            {
                tmp *= 10;
                r = BigInteger.Remainder(tmp, Denominator);
                var w = BigInteger.Divide(tmp, Denominator);
                if (w > 0)
                    tmp = r;
                fraction +=  w.ToString("R", CultureInfo.InvariantCulture);
                i++;
            }

            fraction = fraction ?? "0";
            return $"{whole}.{fraction}";
        }

        public override string ToString()
        {
            return this.ToString();
        }

        public void OnDeserialization(object sender)
        {
            try {
                // verify that the deserialized number is well formed
                if (Denominator.Sign == 0 || _numerator.Sign == 0) {
                    // n/0 = 0/1
                    // 0/m = 0/1
                    _numerator = BigInteger.Zero;
                    Denominator = BigInteger.One;
                }
                else if (_denominator.Sign < 0) {
                    _numerator = BigInteger.Negate(_numerator);
                    Denominator = BigInteger.Negate(Denominator);
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
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("Numerator", _numerator);
            info.AddValue("Denominator", Denominator);
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
            return new BigRational((r1._numerator * r2.Denominator) + (r1.Denominator * r2._numerator), (r1.Denominator * r2.Denominator));
        }
        
        public static BigRational operator - (BigRational r1, BigRational r2) {
            // a/b - c/d  = (ad - bc)/bd
            return new BigRational((r1._numerator * r2.Denominator) - (r1.Denominator * r2._numerator), (r1.Denominator * r2.Denominator));
        }

        public static BigRational operator * (BigRational r1, BigRational r2) {
            // a/b * c/d  = (ac)/(bd)
            return new BigRational((r1._numerator * r2._numerator), (r1.Denominator * r2.Denominator));
        }

        public static BigRational operator / (BigRational r1, BigRational r2) {
            // a/b / c/d  = (ad)/(bc)
            return new BigRational((r1._numerator * r2.Denominator), (r1.Denominator * r2._numerator));
        }

        public static BigRational operator % (BigRational r1, BigRational r2) {
            // a/b % c/d  = (ad % bc)/bd
            return new BigRational((r1._numerator * r2.Denominator) % (r1.Denominator * r2._numerator), (r1.Denominator * r2.Denominator));
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
        
        #region Implicit Casts
        
        public static implicit operator BigRational(sbyte value) {           
            return new BigRational((BigInteger)value);
        }
        
        public static implicit operator BigRational(ushort value) {           
            return new BigRational((BigInteger)value);
        }
        
        public static implicit operator BigRational(uint value) {           
            return new BigRational((BigInteger)value);
        }
        
        public static implicit operator BigRational(ulong value) {           
            return new BigRational((BigInteger)value);
        }

        public static implicit operator BigRational(byte value) {           
            return new BigRational((BigInteger)value);
        }

        public static implicit operator BigRational(short value) {           
            return new BigRational((BigInteger)value);
        }

        public static implicit operator BigRational(int value) {           
            return new BigRational((BigInteger)value);
        }

        public static implicit operator BigRational(long value) {           
            return new BigRational((BigInteger)value);
        }

        public static implicit operator BigRational(BigInteger value) {           
            return new BigRational(value);
        }

        public static implicit operator BigRational(float value) { 
            return new BigRational(value);
        }

        public static implicit operator BigRational(double value) {      
            return new BigRational(value);
        }

        public static implicit operator BigRational(decimal value) {      
            return new BigRational(value);
        }
        
        #endregion
    }
}