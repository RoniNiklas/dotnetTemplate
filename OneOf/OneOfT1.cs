using System.Text.Json.Serialization;

namespace OneOf;

public struct OneOf<T0, T1>
    where T0 : notnull
    where T1 : notnull
{
    public readonly T0 Value0 { get; init; }
    public readonly T1 Value1 { get; init; }
    public readonly int Index { get; init; }

    [JsonConstructor]
    public OneOf(int index, T0 value0 = default, T1 value1 = default)
    {
        Index = index;
        Value0 = value0;
        Value1 = value1;
    }

    public object Value =>
        Index switch
        {
            0 => Value0,
            1 => Value1,
            _ => throw new InvalidOperationException()
        };

    public bool IsT0 => Index == 0;
    public bool IsT1 => Index == 1;

    public static implicit operator OneOf<T0, T1>(T0 t) => new(0, value0: t);
    public static implicit operator OneOf<T0, T1>(T1 t) => new(1, value1: t);

    public void Switch(Action<T0> f0, Action<T1> f1)
    {
        if (Index == 0 && f0 != null)
        {
            f0(Value0);
            return;
        }
        if (Index == 1 && f1 != null)
        {
            f1(Value1);
            return;
        }
        throw new InvalidOperationException();
    }

    public TResult Match<TResult>(Func<T0, TResult> f0, Func<T1, TResult> f1)
    {
        if (Index == 0 && f0 != null)
        {
            return f0(Value0);
        }
        if (Index == 1 && f1 != null)
        {
            return f1(Value1);
        }
        throw new InvalidOperationException();
    }

    public static OneOf<T0, T1> FromT0(T0 input) => input;
    public static OneOf<T0, T1> FromT1(T1 input) => input;

    bool Equals(OneOf<T0, T1> other) =>
        Index == other.Index &&
        Index switch
        {
            0 => Equals(Value0, other.Value0),
            1 => Equals(Value1, other.Value1),
            _ => false
        };

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        return obj is OneOf<T0, T1> o && Equals(o);
    }

    public override string ToString() =>
        Index switch
        {
            0 => FormatValue(Value0),
            1 => FormatValue(Value1),
            _ => throw new InvalidOperationException("Unexpected index, which indicates a problem in the OneOf codegen.")
        };

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = Index switch
            {
                0 => Value0?.GetHashCode(),
                1 => Value1?.GetHashCode(),
                _ => 0
            } ?? 0;
            return (hashCode * 397) ^ Index;
        }
    }
}
