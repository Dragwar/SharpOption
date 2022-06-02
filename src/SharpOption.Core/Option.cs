using System.Collections;

namespace SharpOption.Core;

// see - https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html
// see - https://github.com/dotnet/fsharp/blob/main/src/FSharp.Core/option.fsi
public static class Option
{
	public static Option<T> Some<T>(T value) => new(value);

	public static Option<T> None<T>() => Option<T>.None;

	public static Option<TOther> Bind<T, TOther>(Option<T> option, Func<T, Option<TOther>> binder) => option.IsSome
		? binder(option.Value!)
		: None<TOther>();

	public static bool Contains<T>(Option<T> option, T value) => option.IsSome && EqualityComparer<T>.Default.Equals(option.Value, value);

	public static int Count<T>(Option<T> option) => option.IsSome ? 1 : 0;

	public static T DefaultValue<T>(Option<T> option, T defaultValue) => option.IsSome
		? option.Value!
		: defaultValue;

	public static T DefaultWith<T>(Option<T> option, Func<T> defThunk) => option.IsSome
		? option.Value!
		: defThunk();

	public static bool Exists<T>(Option<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!);

	public static Option<T> Filter<T>(Option<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!)
		? option
		: None<T>();

	public static Option<T> Flatten<T>(Option<Option<T>> option) => option.IsSome
		? option.Value.IsSome
			? option.Value
			: None<T>()
		: None<T>();

	public static TState Fold<T, TState>(Option<T> option, TState state, Func<TState, T, TState> folder) => option.IsSome
		? folder(state, option.Value!)
		: state;

	public static TState FoldBack<T, TState>(Option<T> option, TState state, Func<T, TState, TState> folder) => option.IsSome
		? folder(option.Value!, state)
		: state;

	public static bool ForAll<T>(Option<T> option, Func<T, bool> predicate) => option.IsNone || predicate(option.Value!);

	public static T Get<T>(Option<T> option) => option.IsSome
		? option.Value!
		: throw new ArgumentException("The option value was None", nameof(option));

	public static bool IsNone<T>(Option<T> option) => option.IsNone;

	public static bool IsSome<T>(Option<T> option) => option.IsSome;

	public static void Iter<T>(Option<T> option, Action<T> action)
	{
		if (option.IsSome)
		{
			action(option.Value!);
		}
	}

	public static Option<TOther> Map<T, TOther>(Option<T> option, Func<T, TOther> mapping) => option.IsSome
		? Some(mapping(option.Value))
		: None<TOther>();

	public static Option<TOther> Map<T1, T2, TOther>(Option<T1> option1, Option<T2> option2, Func<T1, T2, TOther> mapping) => option1.IsSome && option2.IsSome
		? Some(mapping(option1.Value, option2.Value))
		: None<TOther>();

	public static Option<TOther> Map<T1, T2, T3, TOther>(Option<T1> option1, Option<T2> option2, Option<T3> option3, Func<T1, T2, T3, TOther> mapping) => option1.IsSome && option2.IsSome && option3.IsSome
		? Some(mapping(option1.Value, option2.Value, option3.Value))
		: None<TOther>();

	public static Option<T> OfNullable<T>(T? value) where T : struct => value.HasValue
		? Some(value.Value)
		: None<T>();

	public static Option<T> OfObj<T>(T value) where T : class => value is not null
		? Some(value)
		: None<T>();

	public static Option<T> OrElse<T>(Option<T> option, Option<T> ifNone) where T : class => option.IsNone
		? ifNone
		: option;

	public static Option<T> OrElseWith<T>(Option<T> option, Func<Option<T>> ifNoneThunk) where T : class => option.IsNone
		? ifNoneThunk()
		: option;

	public static T[] ToArray<T>(Option<T> option) => option.IsSome
		? new T[] { option.Value! }
		: Array.Empty<T>();

	public static List<T> ToList<T>(Option<T> option) => option.IsSome
		? new List<T>() { option.Value! }
		: new List<T>();

	public static T? ToNullable<T>(Option<T> option) where T : struct => option.IsSome
		? new T?(option.Value)
		: new T?();

	public static T? ToObj<T>(Option<T> option) => option.Value;
}

public readonly record struct Option<T> : IStructuralEquatable, IStructuralComparable, IEquatable<T>, IEquatable<Option<T>>, IComparable, IComparable<Option<T>>
{
	internal static Option<T> None { get; } = default;

	public readonly T Value { get; }
	private readonly bool HasValue { get; }

	public readonly bool IsSome => HasValue;
	public readonly bool IsNone => !HasValue;

	private Option(T? value, bool hasValue)
	{
		Value = value!;
		HasValue = hasValue;
	}

	internal Option(T value)
		: this(value, hasValue: true)
	{
	}

	public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) => IsSome ? some(Value) : none();
	public void Match<TResult>(Action<T> some, Action none) { if (IsSome) some(Value); else none(); }

	public bool Equals(T? other) => EqualityComparer<T>.Default.Equals(Value, other);
	public bool Equals(object? other, IEqualityComparer comparer) => comparer.Equals(Value, other);
	public int GetHashCode(IEqualityComparer comparer) => comparer.GetHashCode(Value!);
	public int CompareTo(object? other, IComparer comparer) => comparer.Compare(Value, other);
	public int CompareTo(object? obj) => CompareTo(obj, Comparer<T>.Default);
	public int CompareTo(Option<T> other) => CompareTo(other.Value, Comparer<T>.Default);


	public static bool operator <(Option<T> left, Option<T> right) => left.CompareTo(right) < 0;
	public static bool operator <=(Option<T> left, Option<T> right) => left.CompareTo(right) <= 0;
	public static bool operator >(Option<T> left, Option<T> right) => left.CompareTo(right) > 0;
	public static bool operator >=(Option<T> left, Option<T> right) => left.CompareTo(right) >= 0;
}
