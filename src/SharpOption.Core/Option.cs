using System.Collections;

namespace SharpOption.Core;

/// <summary>
/// Contains operations for working with options.
/// </summary>
/// <remarks>
/// - <see href="https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html">F# option documentation</see>.
/// <para />
/// - <see href="https://github.com/dotnet/fsharp/blob/main/src/FSharp.Core/option.fs">F# option source code</see>.
/// </remarks>
public static class Option
{
	/// <summary>
	/// Create an option value that is a 'Some' value.
	/// </summary>
	/// <typeparam name="T">Returned option generic type.</typeparam>
	/// <param name="value">The input value</param>
	/// <returns>An option representing the <paramref name="value"/>.</returns>
	public static Option<T> Some<T>(T value) => new(value);

	/// <summary>
	/// Create an option value that is a 'None' value.
	/// </summary>
	/// <typeparam name="T">Returned option generic type.</typeparam>
	public static Option<T> None<T>() => Option<T>.None;

	/// <summary>
	/// <b><c>Option.Bind(option, binder)</c></b> evaluates to <b><c>option.IsSome ? binder(option.Value) : Option.None&#60;T&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="binder"/>'s return option generic type.</typeparam>
	/// <param name="binder">
	/// A function that takes the value of type <typeparamref name="T"/> from an <paramref name="option"/> and transforms it into
	/// an option containing a value of type <typeparamref name="U"/>.
	/// </param>
	/// <param name="option">The input option.</param>
	/// <returns>An option of the output type of the binder.</returns>
	public static Option<U> Bind<T, U>(Option<T> option, Func<T, Option<U>> binder) => option.IsSome
		? binder(option.Value!)
		: None<U>();

	/// <summary>
	/// Evaluates to <see langword="true"/> if <paramref name="option"/> is <c>Some</c>
	/// and its value is equal to <paramref name="value"/>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="value">The value to test for equality.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// <see langword="true"/> if the <paramref name="option"/> is <c>Some</c>
	/// and contains a value equal to <paramref name="value"/>, otherwise <see langword="false"/>.
	/// </returns>
	public static bool Contains<T>(Option<T> option, T value) => option.IsSome && EqualityComparer<T>.Default.Equals(option.Value, value);

	/// <summary>
	/// <b><c>Option.Count(option)</c></b> evaluates to <b><c>option.IsSome ? 1 : 0</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>A zero if the <paramref name="option"/> is None, a one otherwise.</returns>
	public static int Count<T>(Option<T> option) => option.IsSome ? 1 : 0;

	/// <summary>
	/// Gets the value of the <paramref name="option"/> if the <paramref name="option"/> is <c>Some</c>,
	/// otherwise returns the specified <paramref name="defaultValue"/>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="defaultValue">The specified default value.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// The <paramref name="option"/> if the <paramref name="option"/> is Some,
	/// else the <paramref name="defaultValue"/>.
	/// </returns>
	public static T DefaultValue<T>(Option<T> option, T defaultValue) => option.IsSome
		? option.Value!
		: defaultValue;

	/// <summary>
	/// Gets the value of the <paramref name="option"/> if the <paramref name="option"/> is <c>Some</c>, 
	/// otherwise evaluates <paramref name="defThunk"/> and returns the result.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="defThunk">A thunk that provides a default value when evaluated.</param>
	/// <param name="option">The input option.</param>
	/// <returns>The <paramref name="option"/> if the <paramref name="option"/> is Some,
	/// else the result of evaluating <paramref name="defThunk"/>.</returns>
	/// <remarks><paramref name="defThunk"/> is not evaluated unless <paramref name="option"/> is <c>None</c>.</remarks>
	public static T DefaultWith<T>(Option<T> option, Func<T> defThunk) => option.IsSome
		? option.Value!
		: defThunk();

	/// <summary>
	/// <b><c>Option.Exists(option, predicate)</c></b> evaluates to <b><c>option.IsSome ? predicate(option.Value) : false</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="predicate">A function that evaluates to a boolean when given a value from the option type.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// <see langword="false"/> if the <paramref name="option"/> is None,
	/// otherwise it returns the result of applying the <paramref name="predicate"/> to the <paramref name="option"/> value.
	/// </returns>
	public static bool Exists<T>(Option<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!);

	/// <summary>
	/// <b><c>Option.Filter(option, predicate)</c></b> evaluates to <b><c>option.IsSome &#38;&#38; predicate(option.Value) ? option : Option.None&#60;T&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="predicate">A function that evaluates whether the value contained in the option should remain, or be filtered out.</param>
	/// <param name="option">The input option.</param>
	/// <returns>The input if the <paramref name="predicate"/> evaluates to <see langword="true"/>; otherwise, None.</returns>
	public static Option<T> Filter<T>(Option<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!)
		? option
		: None<T>();

	/// <summary>
	/// <b><c>Option.Flatten(option)</c></b> evaluates to <b><c>option.IsSome &#38;&#38; option.Value.IsSome ? option.Value : Option.None&#60;T&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The input value if the value is Some; otherwise, None.</returns>
	public static Option<T> Flatten<T>(Option<Option<T>> option) => option.IsSome && option.Value.IsSome
		? option.Value
		: None<T>();

	/// <summary>
	/// <b><c>Option.Fold(option, state, folder)</c></b> evaluates to <b><c>option.IsSome ? folder(state, option.Value) : state</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="TState">The type of <paramref name="state"/>.</typeparam>
	/// <param name="folder">A function to update the state data when given a value from an option.</param>
	/// <param name="state">The initial state.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// The original <paramref name="state"/> if the <paramref name="option"/> is None,
	/// otherwise it returns the updated <paramref name="state"/> with the <paramref name="folder"/> and the <paramref name="option"/> value.
	/// </returns>
	public static TState Fold<T, TState>(Option<T> option, TState state, Func<TState, T, TState> folder) => option.IsSome
		? folder(state, option.Value!)
		: state;

	/// <summary>
	/// <b><c>Option.FoldBack(option, state, folder)</c></b> evaluates to <b><c>option.IsSome ? folder(option.Value, state) : state</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="TState">The type of <paramref name="state"/>.</typeparam>
	/// <param name="folder">A function to update the state data when given a value from an option.</param>
	/// <param name="state">The initial state.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// The original <paramref name="state"/> if the <paramref name="option"/> is None,
	/// otherwise it returns the updated <paramref name="state"/> with the <paramref name="folder"/> and the <paramref name="option"/> value.
	/// </returns>
	public static TState FoldBack<T, TState>(Option<T> option, TState state, Func<T, TState, TState> folder) => option.IsSome
		? folder(option.Value!, state)
		: state;

	/// <summary>
	/// <b><c>Option.ForAll(option, predicate)</c></b> evaluates to <b><c>option.IsNone || predicate(option.Value)</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="predicate">A function that evaluates to a boolean when given a value from the option type.</param>
	/// <param name="option">The input option.</param>
	/// <returns><see langword="true"/> if the <paramref name="option"/> is None,
	/// otherwise it returns the result of applying the <paramref name="predicate"/> to the <paramref name="option"/> value.
	/// </returns>
	public static bool ForAll<T>(Option<T> option, Func<T, bool> predicate) => option.IsNone || predicate(option.Value!);

	/// <summary>
	/// Gets the value associated with the <paramref name="option"/>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The value within the <paramref name="option"/>.</returns>
	/// <exception cref="ArgumentException">Thrown when the <paramref name="option"/> is None.</exception>
	public static T Get<T>(Option<T> option) => option.IsSome
		? option.Value!
		: throw new ArgumentException("The option value was None", nameof(option));

	/// <summary>
	/// Returns <see langword="true"/> if the <paramref name="option"/> is None.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns><see langword="true"/> if the option is None.</returns>
	public static bool IsNone<T>(Option<T> option) => option.IsNone;

	/// <summary>
	/// Returns <see langword="true"/> if the <paramref name="option"/> is not None.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns><see langword="true"/> if the option is not None.</returns>
	public static bool IsSome<T>(Option<T> option) => option.IsSome;

	/// <summary>
	/// <b><c>Option.Iter(option, action)</c></b> executes <b><c>if (option.IsSome) action(option.Value)</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="action">A function to apply to the <paramref name="option"/> value.</param>
	/// <param name="option">The input option.</param>
	public static void Iter<T>(Option<T> option, Action<T> action)
	{
		if (option.IsSome)
		{
			action(option.Value!);
		}
	}

	/// <summary>
	/// <b><c>Option.Map(option, mapping)</c></b> evaluates to <b><c>option.IsSome ? Option.Some(mapping(option.Value)) : Option.None&#60;U&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="mapping"/>'s return type.</typeparam>
	/// <param name="mapping">A function to apply to the <paramref name="option"/> value.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// An option of the <paramref name="option"/> value after applying the <paramref name="mapping"/> function,
	/// or None if the <paramref name="option"/> is None.
	/// </returns>
	public static Option<U> Map<T, U>(Option<T> option, Func<T, U> mapping) => option.IsSome
		? Some(mapping(option.Value))
		: None<U>();

	/// <summary>
	/// <b><c>Option.Map(option1, option2, mapping)</c></b>
	/// evaluates to
	/// <b><c>option1.IsSome &#38;&#38; option2.IsSome ? Option.Some(mapping(option1.Value, option2.Value)) : Option.None&#60;U&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T1"><paramref name="option1"/>'s generic type.</typeparam>
	/// <typeparam name="T2"><paramref name="option2"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="mapping"/>'s return type.</typeparam>
	/// <param name="mapping">A function to apply to the option values.</param>
	/// <param name="option1">The first option.</param>
	/// <param name="option2">The second option.</param>
	/// <returns>
	/// An option of the input option values after applying the <paramref name="mapping"/> function,
	/// or None if either input options is None.
	/// </returns>
	public static Option<U> Map<T1, T2, U>(Option<T1> option1, Option<T2> option2, Func<T1, T2, U> mapping) => option1.IsSome && option2.IsSome
		? Some(mapping(option1.Value, option2.Value))
		: None<U>();

	/// <summary>
	/// <b><c>Option.Map(option1, option2, option3, mapping)</c></b>
	/// evaluates to
	/// <b><c>option1.IsSome &#38;&#38; option2.IsSome &#38;&#38; option3.IsSome ? Option.Some(mapping(option1.Value, option2.Value, option3.Value)) : Option.None&#60;U&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T1"><paramref name="option1"/>'s generic type.</typeparam>
	/// <typeparam name="T2"><paramref name="option2"/>'s generic type.</typeparam>
	/// <typeparam name="T3"><paramref name="option3"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="mapping"/>'s return type.</typeparam>
	/// <param name="mapping">A function to apply to the option values.</param>
	/// <param name="option1">The first option.</param>
	/// <param name="option2">The second option.</param>
	/// <param name="option3">The third option.</param>
	/// <returns>
	/// An option of the input option values after applying the <paramref name="mapping"/> function,
	/// or None if any input options is None.
	/// </returns>
	public static Option<U> Map<T1, T2, T3, U>(Option<T1> option1, Option<T2> option2, Option<T3> option3, Func<T1, T2, T3, U> mapping) => option1.IsSome && option2.IsSome && option3.IsSome
		? Some(mapping(option1.Value, option2.Value, option3.Value))
		: None<U>();

	/// <summary>
	/// Convert a Nullable <paramref name="value"/> to an option.
	/// </summary>
	/// <typeparam name="T"><paramref name="value"/>'s generic type.</typeparam>
	/// <param name="value">The input nullable value.</param>
	/// <returns>The result option.</returns>
	public static Option<T> OfNullable<T>(T? value) where T : struct => value.HasValue
		? Some(value.Value)
		: None<T>();

	/// <summary>
	/// Convert a potentially <see langword="null"/> <paramref name="value"/> to an option.
	/// </summary>
	/// <typeparam name="T"><paramref name="value"/>'s generic type.</typeparam>
	/// <param name="value">The input value.</param>
	/// <returns>The result option.</returns>
	public static Option<T> OfObj<T>(T? value) where T : class => value is not null
		? Some(value)
		: None<T>();

	/// <summary>
	/// Returns <paramref name="option"/> if it is <c>Some</c>, otherwise returns <paramref name="ifNone"/>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="ifNone">The value to use if <paramref name="option"/> is <c>None</c>.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// The <paramref name="option"/> if the <paramref name="option"/> is Some,
	/// else the alternate <paramref name="option"/>.
	/// </returns>
	public static Option<T> OrElse<T>(Option<T> option, Option<T> ifNone) where T : class => option.IsNone
		? ifNone
		: option;

	/// <summary>
	/// Returns <paramref name="option"/> if it is <c>Some</c>, otherwise evaluates <paramref name="ifNoneThunk"/> and returns the result.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="ifNoneThunk">A thunk that provides an alternate option when evaluated.</param>
	/// <param name="option">The input option.</param>
	/// <returns>The option if the option is Some, else the result of evaluating <paramref name="ifNoneThunk"/>.</returns>
	/// <remarks><paramref name="ifNoneThunk"/> is not evaluated unless <paramref name="option"/> is <c>None</c>.</remarks>
	public static Option<T> OrElseWith<T>(Option<T> option, Func<Option<T>> ifNoneThunk) where T : class => option.IsNone
		? ifNoneThunk()
		: option;

	/// <summary>
	/// Convert the <paramref name="option"/> to a array of length 0 or 1.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The result array.</returns>
	public static T[] ToArray<T>(Option<T> option) => option.IsSome
		? new T[] { option.Value! }
		: Array.Empty<T>();

	/// <summary>
	/// Convert the <paramref name="option"/> to a list of length 0 or 1.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The result list.</returns>
	public static List<T> ToList<T>(Option<T> option) => option.IsSome
		? new List<T>() { option.Value! }
		: new List<T>();

	/// <summary>
	/// Convert the <paramref name="option"/> to a Nullable value.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The result value.</returns>
	public static T? ToNullable<T>(Option<T> option) where T : struct => option.IsSome
		? new T?(option.Value)
		: new T?();

	/// <summary>
	/// Convert an <paramref name="option"/> to a potentially <see langword="null"/> value.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input value.</param>
	/// <returns>
	/// The result value, which is <see langword="null"/> if the <paramref name="option"/> was None.
	/// </returns>
	public static T? ToObj<T>(Option<T> option) where T : class => option.IsSome ? option.Value : default;

	/// <summary>
	/// Execute and return the result of either <paramref name="some"/> or <paramref name="none"/>
	/// depending whether <paramref name="option"/> is <c>Some</c> or <c>None</c>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="some"/> and <paramref name="none"/>'s return type.</typeparam>
	/// <param name="option">The input value.</param>
	/// <param name="some">The function to execute when <paramref name="option"/> is <c>Some</c>.</param>
	/// <param name="none">The function to execute when <paramref name="option"/> is <c>None</c>.</param>
	public static U Match<T, U>(Option<T> option, Func<T, U> some, Func<U> none) => option.IsSome
		? some(option.Value)
		: none();

	/// <summary>
	/// Execute either <paramref name="some"/> or <paramref name="none"/> depending whether <paramref name="option"/> is <c>Some</c> or <c>None</c>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input value.</param>
	/// <param name="some">The function to execute when <paramref name="option"/> is <c>Some</c>.</param>
	/// <param name="none">The function to execute when <paramref name="option"/> is <c>None</c>.</param>
	public static void Match<T>(Option<T> option, Action<T> some, Action none)
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

/// <summary>
/// The type of optional values.
/// </summary>
/// <remarks>
/// Use the constructor functions <b><c>Option.Some&#60;T&#62;(...)</c></b> and <b><c>Option.None&#60;T&#62;()</c></b> to create values of this type.
/// Use the functions in the <see langword="static"/> <see langword="class"/> <c>Option</c> to manipulate values of this type.
/// </remarks>
public record Option<T> : IStructuralEquatable, IStructuralComparable, IEquatable<T>, IEquatable<Option<T>>, IComparable, IComparable<Option<T>>
{
	internal static Option<T> None { get; } = new(default, false);

	internal readonly T _value;
	private readonly bool _hasValue;

	/// <summary>
	/// Get the value of a 'Some' option. A <see cref="InvalidOperationException"/> is thrown if this option is 'None'.
	/// </summary>
	public T Value => IsNone ? throw new InvalidOperationException("Option.Value") : _value;


	/// <summary>
	/// Return <see langword="true"/> if this option is a 'Some' value.
	/// </summary>
	public bool IsSome => _hasValue;

	/// <summary>
	/// Return <see langword="true"/> if this option is a 'None' value.
	/// </summary>
	public bool IsNone => !_hasValue;

	private Option(T? value, bool hasValue)
	{
		_value = value!;
		_hasValue = hasValue;
	}

	internal Option(T value)
		: this(value, hasValue: true)
	{
	}


	public bool Equals(T? other) => EqualityComparer<T>.Default.Equals(_value, other);
	public bool Equals(object? other, IEqualityComparer comparer) => comparer.Equals(_value, other);
	public int GetHashCode(IEqualityComparer comparer) => comparer.GetHashCode(_value!);
	public override int GetHashCode() => GetHashCode(EqualityComparer<T>.Default);
	public int CompareTo(object? other, IComparer comparer) => comparer.Compare(_value, other);
	public int CompareTo(object? obj) => CompareTo(obj, Comparer<T>.Default);
	public int CompareTo(Option<T>? other) => CompareTo(other is null ? default : other._value, Comparer<T>.Default);


	public static bool operator <(Option<T> left, Option<T> right) => left.CompareTo(right) < 0;
	public static bool operator <=(Option<T> left, Option<T> right) => left.CompareTo(right) <= 0;
	public static bool operator >(Option<T> left, Option<T> right) => left.CompareTo(right) > 0;
	public static bool operator >=(Option<T> left, Option<T> right) => left.CompareTo(right) >= 0;
}
