namespace System
{
    public struct Money : IEquatable<Money>, IComparable<Money>, IFormattable, IConvertible
    {
        public static implicit operator Money(Byte value)
        {
            return new Money(value);
        }

        public static implicit operator Money(SByte value)
        {
            return new Money(value);
        }

        public static implicit operator Money(Single value)
        {
            return new Money((Decimal)value);
        }

        public static implicit operator Money(Double value)
        {
            return new Money((Decimal)value);
        }

        public static implicit operator Money(Decimal value)
        {
            return new Money(value);
        }

        public static implicit operator Decimal(Money value)
        {
            return value.ComputeValue();
        }

        public static implicit operator Money(Int16 value)
        {
            return new Money(value);
        }

        public static implicit operator Money(Int32 value)
        {
            return new Money(value);
        }

        public static implicit operator Money(Int64 value)
        {
            return new Money(value);
        }

        public static implicit operator Money(UInt16 value)
        {
            return new Money(value);
        }

        public static implicit operator Money(UInt32 value)
        {
            return new Money(value);
        }

        public static implicit operator Money(UInt64 value)
        {
            return new Money(value);
        }

        public static Money operator -(Money value)
        {
            return new Money(-value._units, -value._decimalFraction);
        }

        public static Money operator +(Money left, Money right)
        {
            Int32 fractionSum = left._decimalFraction + right._decimalFraction;

            Int64 overflow = 0;
            Int32 fractionSign = Math.Sign(fractionSum);
            Int32 absFractionSum = Math.Abs(fractionSum);

            if (absFractionSum >= FractionScale)
            {
                overflow = fractionSign;
                absFractionSum -= (Int32)FractionScale;
                fractionSum = fractionSign * absFractionSum;
            }

            Int64 newUnits = left._units + right._units + overflow;

            if (fractionSign < 0 && Math.Sign(newUnits) > 0)
            {
                newUnits -= 1;
                fractionSum = (Int32)FractionScale - absFractionSum;
            }

            return new Money(newUnits,
                             fractionSum);
        }

        public static Money operator -(Money left, Money right)
        {
            return left + -right;
        }

        public static Money operator *(Money left, Decimal right)
        {
            return ((Decimal)left * right);
        }

        public static Money operator /(Money left, Decimal right)
        {
            return ((Decimal)left / right);
        }

        public static Boolean operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static Boolean operator !=(Money left, Money right)
        {
            return !left.Equals(right);
        }

        public static Boolean operator >(Money left, Money right)
        {
            return left.CompareTo(right) > 0;
        }

        public static Boolean operator <(Money left, Money right)
        {
            return left.CompareTo(right) < 0;
        }

        public static Boolean operator >=(Money left, Money right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static Boolean operator <=(Money left, Money right)
        {
            return left.CompareTo(right) <= 0;
        }

        private const Decimal FractionScale = 1E9M;
        private readonly Int64 _units;
        private readonly Int32 _decimalFraction;

        public Money(Decimal value)
        {
            CheckValue(value);

            _units = (Int64)value;
            _decimalFraction = (Int32)Decimal.Round((value - _units) * FractionScale);

            if (_decimalFraction >= FractionScale)
            {
                _units += 1;
                _decimalFraction = _decimalFraction - (Int32)FractionScale;
            }
        }

        private Money(Int64 units, Int32 fraction)
        {
            _units = units;
            _decimalFraction = fraction;
        }

        public override Int32 GetHashCode()
        {
            return 207501131 ^ _units.GetHashCode();
        }

        public override Boolean Equals(Object obj)
        {
            if (!(obj is Money))
            {
                return false;
            }

            var other = (Money)obj;
            return Equals(other);
        }

        public override String ToString()
        {
            return ComputeValue().ToString("C");
        }

        public String ToString(String format)
        {
            return ComputeValue().ToString(format);
        }

        #region Implementation of IEquatable<Money>

        public Boolean Equals(Money other)
        {
            return _units == other._units &&
                   _decimalFraction == other._decimalFraction;
        }

        #endregion

        #region Implementation of IComparable<Money>

        public Int32 CompareTo(Money other)
        {
            Int32 unitCompare = _units.CompareTo(other._units);

            return unitCompare == 0
                       ? _decimalFraction.CompareTo(other._decimalFraction)
                       : unitCompare;
        }

        #endregion

        #region Implementation of IFormattable

        public String ToString(String format, IFormatProvider formatProvider)
        {
            return ComputeValue().ToString(format, formatProvider);
        }

        #endregion

        #region Implementation of IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public Boolean ToBoolean(IFormatProvider provider)
        {
            return _units == 0 && _decimalFraction == 0;
        }

        public Char ToChar(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        public SByte ToSByte(IFormatProvider provider)
        {
            return (SByte)ComputeValue();
        }

        public Byte ToByte(IFormatProvider provider)
        {
            return (Byte)ComputeValue();
        }

        public Int16 ToInt16(IFormatProvider provider)
        {
            return (Int16)ComputeValue();
        }

        public UInt16 ToUInt16(IFormatProvider provider)
        {
            return (UInt16)ComputeValue();
        }

        public Int32 ToInt32(IFormatProvider provider)
        {
            return (Int32)ComputeValue();
        }

        public UInt32 ToUInt32(IFormatProvider provider)
        {
            return (UInt32)ComputeValue();
        }

        public Int64 ToInt64(IFormatProvider provider)
        {
            return (Int64)ComputeValue();
        }

        public UInt64 ToUInt64(IFormatProvider provider)
        {
            return (UInt64)ComputeValue();
        }

        public Single ToSingle(IFormatProvider provider)
        {
            return (Single)ComputeValue();
        }

        public Double ToDouble(IFormatProvider provider)
        {
            return (Double)ComputeValue();
        }

        public Decimal ToDecimal(IFormatProvider provider)
        {
            return ComputeValue();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        public String ToString(IFormatProvider provider)
        {
            return ((Decimal)this).ToString(provider);
        }

        public Object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        #endregion

        private Decimal ComputeValue()
        {
            return _units + _decimalFraction / FractionScale;
        }

        private static void CheckValue(Decimal value)
        {
            if (value < Int64.MinValue || value > Int64.MaxValue)
            {
                throw new ArgumentOutOfRangeException("value",
                                                      value,
                                                      "Money value must be between " +
                                                      Int64.MinValue + " and " +
                                                      Int64.MaxValue);
            }
        }
    }
}