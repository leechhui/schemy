using System;
using System.Collections.Generic;

namespace cscheme
{
    public enum ValueType
    {
        Empty = 0,
        Object = 1,
        DBNull = 2,
        Boolean = 3,
        Char = 4,
        SByte = 5,
        Byte = 6,
        Int16 = 7,
        UInt16 = 8,
        Int32 = 9,
        UInt32 = 10,
        Int64 = 11,
        UInt64 = 12,
        Single = 13,
        Double = 14,
        Decimal = 15,
        DateTime = 16,
        String = 18,
       // Cell = 19, // lisp cell type
       // Sym = 20 // lisp symbol type
    }

    public abstract class SchObj
    {
        public virtual bool isString => false;
        public virtual bool isNumber => false;
        public virtual bool isObject => false;
    }

    public abstract class SchNum : SchObj
    {
        public override bool isNumber => true;

        public virtual bool isZero => true;
        public virtual bool isOne => false;
        public virtual ValueType valueType => ValueType.Empty;

        public virtual int AsInt() => 0;

        public virtual uint AsUInt() => 0;

        public virtual long AsLong() => 0L;

        public virtual ulong AsULong() => 0L;

        public virtual float AsFloat() => 0.0f;

        public virtual double AsDouble() => 0.0;

        /// <summary>
        ///   Add x to self, return self
        /// </summary>
        public abstract SchNum Add(SchNum x);

        /// <summary>
        ///   Subtract x from self, return self
        /// </summary>
        public abstract SchNum Subtract(SchNum x);

        /// <summary>
        ///   Multiply by x , return self
        /// </summary>
        public abstract SchNum Multiply(SchNum x);

        /// <summary>
        ///   Divide by x, return self
        /// </summary>
        // public abstract LispNumber Divide(LispNumber x);

        // public abstract int Compare(LispNumber x);

        public static SchNum Add(SchNum x, SchNum y)
        {
            if (object.ReferenceEquals(y, null) || y.isZero)
                return x;
            var xType = x.valueType;
            var yType = y.valueType;
            if (ValueType.Double == xType || ValueType.Double == yType)
            {
                return new SchDouble(x.AsDouble() + y.AsDouble());
            }
            else if (ValueType.Single == xType || ValueType.Single == yType)
            {
                return new SchFloat(x.AsFloat() + y.AsFloat());
            }
            else if (ValueType.UInt64 == xType || ValueType.UInt64 == yType)
            {
                return new SchUInt64(x.AsULong() + y.AsULong());
            }
            else if (ValueType.Int64 == xType || ValueType.Int64 == yType)
            {
                return new SchInt64(x.AsLong() + y.AsLong());
            }
            else if (ValueType.UInt32 == xType || ValueType.UInt32 == yType)
            {
                return new SchUInt(x.AsUInt() + y.AsUInt());
            }
            return new SchInt(x.AsInt() + y.AsInt());
        }

        public static SchNum Subtract(SchNum x, SchNum y)
        {
            if (object.ReferenceEquals(y, null) || x.isZero)
                return x;
            var xType = x.valueType;
            var yType = y.valueType;
            if (ValueType.Double == xType || ValueType.Double == yType)
            {
                return new SchDouble(x.AsDouble() - y.AsDouble());
            }
            else if (ValueType.Single == xType || ValueType.Single == yType)
            {
                return new SchFloat(x.AsFloat() - y.AsFloat());
            }
            else if (ValueType.UInt64 == xType || ValueType.UInt64 == yType)
            {
                return new SchUInt64(x.AsULong() - y.AsULong());
            }
            else if (ValueType.Int64 == xType || ValueType.Int64 == yType)
            {
                return new SchInt64(x.AsLong() - y.AsLong());
            }
            else if (ValueType.UInt32 == xType || ValueType.UInt32 == yType)
            {
                return new SchUInt(x.AsUInt() - y.AsUInt());
            }
            return new SchInt(x.AsInt() - y.AsInt());
        }

        public static SchNum Multiply(SchNum x, SchNum y)
        {
            if (object.ReferenceEquals(y, null) || y.isZero)
            {
                return new SchInt(0);
            }
            if (y.isOne)
                return x;
            var xType = x.valueType;
            var yType = y.valueType;
            if (ValueType.Double == xType || ValueType.Double == yType)
            {
                return new SchDouble(x.AsDouble() * y.AsDouble());
            }
            else if (ValueType.Single == xType || ValueType.Single == yType)
            {
                return new SchFloat(x.AsFloat() * y.AsFloat());
            }
            else if (ValueType.UInt64 == xType || ValueType.UInt64 == yType)
            {
                return new SchUInt64(x.AsULong() * y.AsULong());
            }
            else if (ValueType.Int64 == xType || ValueType.Int64 == yType)
            {
                return new SchInt64(x.AsLong() * y.AsLong());
            }
            else if (ValueType.UInt32 == xType || ValueType.UInt32 == yType)
            {
                return new SchUInt(x.AsUInt() * y.AsUInt());
            }
            return new SchInt(x.AsInt() * y.AsInt());
        }

        public static SchNum Divide(SchNum x, SchNum y)
        {
            if (object.ReferenceEquals(y, null) || y.isZero)
            {
                return new SchInt(0);
            }
            var xType = x.valueType;
            var yType = y.valueType;
            if (ValueType.Double == xType || ValueType.Double == yType)
            {
                return new SchDouble(x.AsDouble() / y.AsDouble());
            }
            else if (ValueType.Single == xType || ValueType.Single == yType)
            {
                return new SchFloat(x.AsFloat() / y.AsFloat());
            }
            else if (ValueType.UInt64 == xType || ValueType.UInt64 == yType)
            {
                return new SchUInt64(x.AsULong() / y.AsULong());
            }
            else if (ValueType.Int64 == xType || ValueType.Int64 == yType)
            {
                return new SchInt64(x.AsLong() / y.AsLong());
            }
            else if (ValueType.UInt32 == xType || ValueType.UInt32 == yType)
            {
                return new SchUInt(x.AsUInt() / y.AsUInt());
            }
            return new SchInt(x.AsInt() / y.AsInt());
        }

        public static SchNum Remainder(SchNum x, SchNum y)
        {
            if (object.ReferenceEquals(y, null) || x.isZero)
            {
                return new SchInt(0);
            }
            var xType = x.valueType;
            var yType = y.valueType;
            if (ValueType.Double == xType || ValueType.Double == yType)
            {
                return new SchDouble(x.AsDouble() % y.AsDouble());
            }
            else if (ValueType.Single == xType || ValueType.Single == yType)
            {
                return new SchFloat(x.AsFloat() % y.AsFloat());
            }
            else if (ValueType.UInt64 == xType || ValueType.UInt64 == yType)
            {
                return new SchUInt64(x.AsULong() % y.AsULong());
            }
            else if (ValueType.Int64 == xType || ValueType.Int64 == yType)
            {
                return new SchInt64(x.AsLong() % y.AsLong());
            }
            else if (ValueType.UInt32 == xType || ValueType.UInt32 == yType)
            {
                return new SchUInt(x.AsUInt() % y.AsUInt());
            }
            return new SchInt(x.AsInt() % y.AsInt());
        }

        public static SchNum Mod(SchNum x, SchNum y)
        {
            if (object.ReferenceEquals(y, null) || x.isZero)
            {
                return new SchInt(0);
            }
            var xType = x.valueType;
            var yType = y.valueType;
            if (ValueType.Double == xType || ValueType.Double == yType)
            {
                return new SchDouble(x.AsDouble() % y.AsDouble());
            }
            else if (ValueType.Single == xType || ValueType.Single == yType)
            {
                return new SchFloat(x.AsFloat() % y.AsFloat());
            }
            else if (ValueType.UInt64 == xType || ValueType.UInt64 == yType)
            {
                return new SchUInt64(x.AsULong() % y.AsULong());
            }
            else if (ValueType.Int64 == xType || ValueType.Int64 == yType)
            {
                return new SchInt64(x.AsLong() % y.AsLong());
            }
            else if (ValueType.UInt32 == xType || ValueType.UInt32 == yType)
            {
                return new SchUInt(x.AsUInt() % y.AsUInt());
            }
            return new SchInt(x.AsInt() % y.AsInt());
        }

        public static SchNum Ceiling(SchNum x)
        {
            return x;
        }

        public static SchNum Floor(SchNum x)
        {
            return x;
        }

        public static int Compare(SchNum x, SchNum y)
        {
            return System.Math.Sign(x.AsDouble() - y.AsDouble());
        }

        public static bool TryParse(string s, out SchObj number)
        {
            // TODO: add BigInteger Support
            // TODO: Performanc optimise, check string patern first
            if (long.TryParse(s, out long valL))
            {
                number = new SchInt64(valL);
                return true;
            }
            else if (ulong.TryParse(s, out ulong valUl))
            {
                number = new SchUInt64(valUl);
                return true;
            }
            else if (double.TryParse(s, out double valDbl))
            {
                if (!double.IsNaN(valDbl))
                {
                    if (double.IsPositiveInfinity(valDbl))
                    {
                        valDbl = double.MaxValue;
                    }
                    else if (double.IsNegativeInfinity(valDbl))
                    {
                        valDbl = double.MinValue;
                    }
                    number = new SchDouble(valDbl);
                    return true;
                }
            }
            number = null;
            return false;
        }
    }

    public class SchBool : SchObj
    {
        public bool value;

        public SchBool(bool value)
        {
            this.value = value;
        }
    }

    // TODO: 添加数学运算
    public class SchInt : SchNum
    {
        public int value;

        public SchInt(int _int)
        {
            value = _int;
        }

        public static readonly SchInt zero = new SchInt(0);
        public static readonly SchInt one = new SchInt(1);

        public override ValueType valueType => ValueType.Int32;

        public override bool isZero => 0 == value;

        public override bool isOne => 1 == value;

        public override int AsInt() => value;

        public override uint AsUInt() => (uint)value;

        public override long AsLong() => value;

        public override ulong AsULong() => (ulong)value;

        public override float AsFloat() => value;

        public override double AsDouble() => value;

        public override string ToString()
        {
            return value.ToString();
        }

        public override SchNum Add(SchNum x)
        {
            return null;
        }

        public override SchNum Subtract(SchNum x)
        {
            return null;
        }

        public override SchNum Multiply(SchNum x)
        {
            return null;
        }

    }

    public class SchUInt : SchNum
    {
        public uint value;

        public SchUInt(uint _uint)
        {
            value = _uint;
        }

        public override ValueType valueType => ValueType.UInt32;

        public override int AsInt() => (int)value;

        public override uint AsUInt() => value;

        public override long AsLong() => value;

        public override ulong AsULong() => (ulong)value;

        public override float AsFloat() => value;

        public override double AsDouble() => value;

        public override bool isZero => 0 == value;

        public override bool isOne => 1 == value;

        public override string ToString()
        {
            return value.ToString();
        }

        public override SchNum Add(SchNum x)
        {
            return null;
        }

        public override SchNum Subtract(SchNum x)
        {
            return null;
        }

        public override SchNum Multiply(SchNum x)
        {
            return null;
        }

    }

    public class SchInt64 : SchNum
    {
        public long value;

        public SchInt64(long _long)
        {
            value = _long;
        }

        public override ValueType valueType => ValueType.Int64;

        public override int AsInt() => (int)value;

        public override uint AsUInt() => (uint)value;

        public override long AsLong() => value;

        public override ulong AsULong() => (ulong)value;

        public override float AsFloat() => value;

        public override double AsDouble() => value;

        public override bool isZero => 0L == value;

        public override bool isOne => 1L == value;

        public override string ToString()
        {
            return value.ToString();
        }

        public override SchNum Add(SchNum x)
        {
            return null;
        }

        public override SchNum Subtract(SchNum x)
        {
            return null;
        }

        public override SchNum Multiply(SchNum x)
        {
            return null;
        }

    }

    public class SchUInt64 : SchNum
    {
        public ulong value;

        public SchUInt64(ulong _ulong)
        {
            value = _ulong;
        }

        public override ValueType valueType => ValueType.UInt64;

        public override int AsInt() => (int)value;

        public override uint AsUInt() => (uint)value;

        public override long AsLong() => (long)value;

        public override ulong AsULong() => value;

        public override float AsFloat() => value;

        public override double AsDouble() => value;

        public override bool isZero => 0L == value;

        public override bool isOne => 1L == value;

        public override string ToString()
        {
            return value.ToString();
        }

        public override SchNum Add(SchNum x)
        {
            return null;
        }

        public override SchNum Subtract(SchNum x)
        {
            return null;
        }

        public override SchNum Multiply(SchNum x)
        {
            return null;
        }

    }

    public class SchFloat : SchNum
    {
        public float value;

        public SchFloat(float _float)
        {
            value = _float;
        }

        public override ValueType valueType => ValueType.Single;

        public override int AsInt() => (int)value;

        public override uint AsUInt() => (uint)value;

        public override long AsLong() => (long)value;

        public override ulong AsULong() => (ulong)value;

        public override float AsFloat() => value;

        public override double AsDouble() => value;

        public override bool isZero => 0.0f == value;

        public override bool isOne => 1.0f == value;

        public override string ToString()
        {
            return value.ToString();
        }

        public override SchNum Add(SchNum x)
        {
            return null;
        }

        public override SchNum Subtract(SchNum x)
        {
            return null;
        }

        public override SchNum Multiply(SchNum x)
        {
            return null;
        }
    }

    public class SchDouble : SchNum
    {
        public double value;

        public SchDouble(double _double)
        {
            value = _double;
        }

        public SchDouble(float _float)
        {
            value = _float;
        }

        public override bool isZero => 0.0 == value;

        public override bool isOne => 1.0 == value;

        public override ValueType valueType => ValueType.Double;

        public override int AsInt() => (int)value;

        public override uint AsUInt() => (uint)value;

        public override long AsLong() => (long)value;

        public override ulong AsULong() => (ulong)value;

        public override float AsFloat() => (float)value;

        public override double AsDouble() => value;

        public override string ToString()
        {
            return value.ToString();
        }

        public override SchNum Add(SchNum x)
        {
            return null;
        }

        public override SchNum Subtract(SchNum x)
        {
            return null;
        }

        public override SchNum Multiply(SchNum x)
        {
            return null;
        }
    }

    public class SchStr : SchObj
    {
        public string value;

        public SchStr(string _str)
        {
            value = _str;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is SchStr target)
            {
                return target.value == value;
            }
            return false;
        }
    }

    /// <summary>Schem Symbol</summary>
    public class SchSym : SchObj, IEquatable<SchSym>
    {
        /// <summary>The symbol's name</summary>
        public readonly string Name;

        /// <summary>Construct a symbol that is not interned.</summary>
        public SchSym(string name)
        {
            this.Name = name;
            // TODO: case insensitive handle
        }

        /// <summary>Return the symbol's name</summary>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>Return the hashcode of the symbol's name</summary>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object x)
        {
            if (x is SchSym xSym)
            {
                return xSym.Name == Name;
            }
            return false;
        }

        public bool Equals(SchSym x)
        {
            if (object.ReferenceEquals(x, null))
                return false;
            return x.Name == Name;
        }

        public static readonly SchSym IF = new SchSym("if");
        public static readonly SchSym QUOTE = new SchSym("quote");
        public static readonly SchSym SET = new SchSym("set!");
        public static readonly SchSym DEFINE = new SchSym("define");
        public static readonly SchSym LAMBDA = new SchSym("lambda");
        public static readonly SchSym BEGIN = new SchSym("begin");
        public static readonly SchSym DEFINE_MACRO = new SchSym("define-macro");
        public static readonly SchSym QUASIQUOTE = new SchSym("quasiquote");
        public static readonly SchSym UNQUOTE = new SchSym("unquote");
        public static readonly SchSym UNQUOTE_SPLICING = new SchSym("unquote-splicing");
        public static readonly SchSym EOF = new SchSym("#<eof>");
        public static readonly SchSym APPEND = new SchSym("append");
        public static readonly SchSym CONS = new SchSym("cons");

        public static readonly Dictionary<string, SchSym> QuotesMap = new Dictionary<string, SchSym>(){
            { "'", SchSym.QUOTE },
            { "`", SchSym.QUASIQUOTE},
            { ",", SchSym.UNQUOTE},
            { ",@", SchSym.UNQUOTE_SPLICING},
        };
     
    }

    // public class SchMutableArr : SchObj
    // {
    //     public List<SchObj> value;

    //     public SchMutableArr(int capacity)
    //     {
    //         value = new List<SchObj>(capacity);
    //     }

    //     public SchMutableArr(List<SchObj> _list)
    //     {
    //         // TODO: value can't be null
    //         value = _list;
    //     }

    //     public override string ToString()
    //     {
    //         return value.ToString();
    //     }
    // }

    // public class SchArr : SchObj
    // {
    //     public SchObj[] value;

    //     public SchArr(uint size)
    //     {
    //         value = new SchObj[size];
    //     }

    //     public SchArr(SchObj[] _arr)
    //     {
    //         // TODO: value can't be null
    //         value = _arr;
    //     }

    //     public override string ToString()
    //     {
    //         return value.ToString();
    //     }
    // }

    // public class SchMutableBytes : SchObj
    // {
    //     public List<byte> value;

    //     public SchMutableBytes(byte[] _bytes)
    //     {
    //         value = new List<byte>(_bytes);
    //     }

    //     public SchMutableBytes(List<byte> _bytes)
    //     {
    //         value = _bytes;
    //     }

    //     public SchMutableBytes(int capacity)
    //     {
    //         value = new List<byte>(capacity);
    //     }
    // }

    // public class SchBytes : SchObj
    // {
    //     public byte[] value;

    //     public SchBytes(byte[] _bytes)
    //     {
    //         value = _bytes;
    //     }

    //     public SchBytes(int size)
    //     {
    //         value = new byte[size];
    //     }
    // }

    public class SchWildArr<T> : SchObj
    {
        public T[] arr;
    }

    public class SchMutableWildArr<T> : SchObj
    {
        public List<T> arr;
    }

    // public class SchDict : SchObj
    // {
    //     public Dictionary<string, SchObj> value;

    //     public SchDict()
    //     {
    //         value = new Dictionary<string, SchObj>();
    //     }

    //     public SchDict(Dictionary<string, SchObj> _dict)
    //     {
    //         // TODO: value can't be null
    //         value = _dict;
    //     }
    // }

    public class SchWildDict<TKey, TValue> : SchObj
    {
        public Dictionary<TKey, TValue> dict;
    }
}
