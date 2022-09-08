using System;

public struct PrimitiveValue
{
    class PrimitiveValueError : Exception {
        public PrimitiveValueError(string message) : base(message) { }
    }

    public enum Type {
        INT,
        FLOAT
    }
    public Type type { get; private set;  }
    int intValue;
    float floatValue;

    public static PrimitiveValue makeInt(int value) {
        PrimitiveValue p = new PrimitiveValue();
        p.type = Type.INT;
        p.intValue = value;
        return p;
    }

    public static PrimitiveValue makeFloat(float value)
    {
        PrimitiveValue p = new PrimitiveValue();
        p.type = Type.FLOAT;
        p.floatValue = value;
        return p;
    }

    public static implicit operator int(PrimitiveValue p)
    {
        if (p.type == Type.INT)
        {
            return p.intValue;
        }
        else {
            throw new PrimitiveValueError($"intにキャストできません: {p}");
        }
    }

    public static implicit operator float(PrimitiveValue p)
    {
        if (p.type == Type.FLOAT)
        {
            return p.floatValue;
        }
        else
        {
            throw new PrimitiveValueError($"floatにキャストできません: {p}");
        }
    }

    public static PrimitiveValue operator +(PrimitiveValue p1, PrimitiveValue p2) {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return makeInt(p1.intValue + p2.intValue);
            }
            else if (p1.type == Type.FLOAT)
            {
                return makeFloat(p1.floatValue + p2.floatValue);
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else {
            throw new PrimitiveValueError($"+演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static PrimitiveValue operator -(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return makeInt(p1.intValue - p2.intValue);
            }
            else if (p1.type == Type.FLOAT)
            {
                return makeFloat(p1.floatValue - p2.floatValue);
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"-演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static PrimitiveValue operator *(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return makeInt(p1.intValue * p2.intValue);
            }
            else if (p1.type == Type.FLOAT)
            {
                return makeFloat(p1.floatValue * p2.floatValue);
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"*演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static PrimitiveValue operator /(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return makeInt(p1.intValue / p2.intValue);
            }
            else if (p1.type == Type.FLOAT)
            {
                return makeFloat(p1.floatValue / p2.floatValue);
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"/演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static PrimitiveValue operator %(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return makeInt(p1.intValue % p2.intValue);
            }
            else if (p1.type == Type.FLOAT)
            {
                throw new PrimitiveValueError($"除算は整数のみ適用可能です");
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"%演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static bool operator ==(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return p1.intValue == p2.intValue;
            }
            else if (p1.type == Type.FLOAT)
            {
                return p1.floatValue == p2.floatValue;
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"==演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static bool operator !=(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return p1.intValue != p2.intValue;
            }
            else if (p1.type == Type.FLOAT)
            {
                return p1.floatValue != p2.floatValue;
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"!=演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static bool operator <(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return p1.intValue < p2.intValue;
            }
            else if (p1.type == Type.FLOAT)
            {
                return p1.floatValue < p2.floatValue;
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"<演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static bool operator >(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return p1.intValue > p2.intValue;
            }
            else if (p1.type == Type.FLOAT)
            {
                return p1.floatValue > p2.floatValue;
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($">演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static bool operator <=(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return p1.intValue <= p2.intValue;
            }
            else if (p1.type == Type.FLOAT)
            {
                return p1.floatValue <= p2.floatValue;
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($"<=演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }

    public static bool operator >=(PrimitiveValue p1, PrimitiveValue p2)
    {
        if (p1.type == p2.type)
        {
            if (p1.type == Type.INT)
            {
                return p1.intValue >= p2.intValue;
            }
            else if (p1.type == Type.FLOAT)
            {
                return p1.floatValue >= p2.floatValue;
            }
            else
            {
                throw new PrimitiveValueError($"無効な型です");
            }
        }
        else
        {
            throw new PrimitiveValueError($">=演算子は２項の型が一致しなければなりません: {p1.type} != {p2.type}");
        }
    }
}
