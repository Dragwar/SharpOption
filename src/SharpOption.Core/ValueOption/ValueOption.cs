using System.Collections;

namespace SharpOption.Core.ValueOption;

// see - https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html
// see - https://github.com/dotnet/fsharp/blob/main/src/FSharp.Core/option.fsi
public static class ValueOption
{
	public static ValueOption<T> Some<T>(T value) => new(value);

	public static ValueOption<T> None<T>() => ValueOption<T>.None;

	public static ValueOption<TOther> Bind<T, TOther>(ValueOption<T> option, Func<T, ValueOption<TOther>> binder) => option.IsSome
		? binder(option.Value!)
		: None<TOther>();

	public static bool Contains<T>(ValueOption<T> option, T value) => option.IsSome && EqualityComparer<T>.Default.Equals(option.Value, value);

	public static int Count<T>(ValueOption<T> option) => option.IsSome ? 1 : 0;

	public static T DefaultValue<T>(ValueOption<T> option, T defaultValue) => option.IsSome
		? option.Value!
		: defaultValue;

	public static T DefaultWith<T>(ValueOption<T> option, Func<T> defThunk) => option.IsSome
		? option.Value!
		: defThunk();

	public static bool Exists<T>(ValueOption<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!);

	public static ValueOption<T> Filter<T>(ValueOption<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!)
		? option
		: None<T>();

	public static ValueOption<T> Flatten<T>(ValueOption<ValueOption<T>> option) => option.IsSome
		? option.Value.IsSome
			? option.Value
			: None<T>()
		: None<T>();

	public static TState Fold<T, TState>(ValueOption<T> option, TState state, Func<TState, T, TState> folder) => option.IsSome
		? folder(state, option.Value!)
		: state;

	public static TState FoldBack<T, TState>(ValueOption<T> option, TState state, Func<T, TState, TState> folder) => option.IsSome
		? folder(option.Value!, state)
		: state;

	public static bool ForAll<T>(ValueOption<T> option, Func<T, bool> predicate) => option.IsNone || predicate(option.Value!);

	public static T Get<T>(ValueOption<T> option) => option.IsSome
		? option.Value!
		: throw new ArgumentException("The option value was None", nameof(option));

	public static bool IsNone<T>(ValueOption<T> option) => option.IsNone;

	public static bool IsSome<T>(ValueOption<T> option) => option.IsSome;

	public static void Iter<T>(ValueOption<T> option, Action<T> action)
	{
		if (option.IsSome)
		{
			action(option.Value!);
		}
	}

	public static ValueOption<TOut> Map<T, TOut>(ValueOption<T> option, Func<T, TOut> mapping) => option.IsSome
		? Some(mapping(option.Value))
		: None<TOut>();

	public static ValueOption<TOut> Map<T1, T2, TOut>(ValueOption<T1> option1, ValueOption<T2> option2, Func<T1, T2, TOut> mapping) => option1.IsSome && option2.IsSome
		? Some(mapping(option1.Value, option2.Value))
		: None<TOut>();

	public static ValueOption<TOut> Map<T1, T2, T3, TOut>(ValueOption<T1> option1, ValueOption<T2> option2, ValueOption<T3> option3, Func<T1, T2, T3, TOut> mapping) => option1.IsSome && option2.IsSome && option3.IsSome
		? Some(mapping(option1.Value, option2.Value, option3.Value))
		: None<TOut>();

	public static ValueOption<T> OfNullable<T>(T? value) where T : struct => value.HasValue
		? Some(value.Value)
		: None<T>();

	public static ValueOption<T> OfObj<T>(T value) where T : class => value is not null
		? Some(value)
		: None<T>();

	public static ValueOption<T> OrElse<T>(ValueOption<T> option, ValueOption<T> ifNone) where T : class => option.IsNone
		? ifNone
		: option;

	public static ValueOption<T> OrElseWith<T>(ValueOption<T> option, Func<ValueOption<T>> ifNoneThunk) where T : class => option.IsNone
		? ifNoneThunk()
		: option;

	public static T[] ToArray<T>(ValueOption<T> option) => option.IsSome
		? new T[] { option.Value! }
		: Array.Empty<T>();

	public static List<T> ToList<T>(ValueOption<T> option) => option.IsSome
		? new List<T>() { option.Value! }
		: new List<T>();

	public static T? ToNullable<T>(ValueOption<T> option) where T : struct => option.IsSome
		? new T?(option.Value)
		: new T?();

	public static T? ToObj<T>(ValueOption<T> option) => option.Value;


	public static TOut Match<T, TOut>(ValueOption<T> option, Func<T, TOut> some, Func<TOut> none) => option.IsSome
		? some(option.Value)
		: none();

	public static void Match<T>(ValueOption<T> option, Action<T> some, Action none)
	{
		if (option.IsSome)
		{
			some(option.Value);
		}
		else
		{
			none();
		}
	}
}

public readonly record struct ValueOption<T> : IStructuralEquatable, IStructuralComparable, IEquatable<T>, IEquatable<ValueOption<T>>, IComparable, IComparable<ValueOption<T>>
{
	internal static ValueOption<T> None { get; } = new(default, false);

	public readonly T Value { get; }
	private readonly bool HasValue { get; }

	public readonly bool IsSome => HasValue;
	public readonly bool IsNone => !HasValue;

	private ValueOption(T? value, bool hasValue)
	{
		Value = value!;
		HasValue = hasValue;
	}

	internal ValueOption(T value)
		: this(value, hasValue: true)
	{
	}

	public bool Equals(T? other) => EqualityComparer<T>.Default.Equals(Value, other);
	public bool Equals(object? other, IEqualityComparer comparer) => comparer.Equals(Value, other);
	public int GetHashCode(IEqualityComparer comparer) => comparer.GetHashCode(Value!);
	public int CompareTo(object? other, IComparer comparer) => comparer.Compare(Value, other);
	public int CompareTo(object? obj) => CompareTo(obj, Comparer<T>.Default);
	public int CompareTo(ValueOption<T> other) => CompareTo(other.Value, Comparer<T>.Default);


	public static bool operator <(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) < 0;
	public static bool operator <=(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) <= 0;
	public static bool operator >(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) > 0;
	public static bool operator >=(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) >= 0;
}
