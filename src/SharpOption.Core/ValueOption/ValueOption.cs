using SharpOption.Core.Delegates;
using System.Collections;

namespace SharpOption.Core.ValueOption;

/// <summary>
/// Contains operations for working with options.
/// </summary>
/// <remarks>
/// - <see href="https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html">F# option documentation</see>.
/// <para />
/// - <see href="https://github.com/dotnet/fsharp/blob/main/src/FSharp.Core/option.fs">F# option source code</see>.
/// </remarks>
public static class ValueOption
{
	/// <summary>
	/// Create an option value that is a 'Some' value.
	/// </summary>
	/// <typeparam name="T">Returned option generic type.</typeparam>
	/// <param name="value">The input value</param>
	/// <returns>An option representing the <paramref name="value"/>.</returns>
	public static ValueOption<T> Some<T>(T value) => new(value);

	/// <summary>
	/// Create an option value that is a 'None' value.
	/// </summary>
	/// <typeparam name="T">Returned option generic type.</typeparam>
	public static ValueOption<T> None<T>() => ValueOption<T>.None;

	/// <summary>
	/// <b><c>ValueOption.Bind(option, binder)</c></b> evaluates to <b><c>option.IsSome ? binder(option.Value) : ValueOption.None&#60;T&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="binder"/>'s return option generic type.</typeparam>
	/// <param name="binder">
	/// A function that takes the value of type <typeparamref name="T"/> from an <paramref name="option"/> and transforms it into
	/// an option containing a value of type <typeparamref name="U"/>.
	/// </param>
	/// <param name="option">The input option.</param>
	/// <returns>An option of the output type of the binder.</returns>
	public static ValueOption<U> Bind<T, U>(ValueOption<T> option, Func<T, ValueOption<U>> binder) => option.IsSome
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
	public static bool Contains<T>(ValueOption<T> option, T value) => option.IsSome && EqualityComparer<T>.Default.Equals(option.Value, value);

	/// <summary>
	/// <b><c>ValueOption.Count(option)</c></b> evaluates to <b><c>option.IsSome ? 1 : 0</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>A zero if the <paramref name="option"/> is None, a one otherwise.</returns>
	public static int Count<T>(ValueOption<T> option) => option.IsSome ? 1 : 0;

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
	public static T DefaultValue<T>(ValueOption<T> option, T defaultValue) => option.IsSome
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
	public static T DefaultWith<T>(ValueOption<T> option, Func<T> defThunk) => option.IsSome
		? option.Value!
		: defThunk();

	/// <summary>
	/// <b><c>ValueOption.Exists(option, predicate)</c></b> evaluates to <b><c>option.IsSome ? predicate(option.Value) : false</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="predicate">A function that evaluates to a boolean when given a value from the option type.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// <see langword="false"/> if the <paramref name="option"/> is None,
	/// otherwise it returns the result of applying the <paramref name="predicate"/> to the <paramref name="option"/> value.
	/// </returns>
	public static bool Exists<T>(ValueOption<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!);

	/// <summary>
	/// <b><c>ValueOption.Filter(option, predicate)</c></b> evaluates to <b><c>option.IsSome &#38;&#38; predicate(option.Value) ? option : ValueOption.None&#60;T&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="predicate">A function that evaluates whether the value contained in the option should remain, or be filtered out.</param>
	/// <param name="option">The input option.</param>
	/// <returns>The input if the <paramref name="predicate"/> evaluates to <see langword="true"/>; otherwise, None.</returns>
	public static ValueOption<T> Filter<T>(ValueOption<T> option, Func<T, bool> predicate) => option.IsSome && predicate(option.Value!)
		? option
		: None<T>();

	/// <summary>
	/// <b><c>ValueOption.Flatten(option)</c></b> evaluates to <b><c>option.IsSome &#38;&#38; option.Value.IsSome ? option.Value : ValueOption.None&#60;T&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The input value if the value is Some; otherwise, None.</returns>
	public static ValueOption<T> Flatten<T>(ValueOption<ValueOption<T>> option) => option.IsSome && option.Value.IsSome
		? option.Value
		: None<T>();

	/// <summary>
	/// <b><c>ValueOption.Fold(option, state, folder)</c></b> evaluates to <b><c>option.IsSome ? folder(state, option.Value) : state</c></b>.
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
	public static TState Fold<T, TState>(ValueOption<T> option, TState state, Func<TState, T, TState> folder) => option.IsSome
		? folder(state, option.Value!)
		: state;

	/// <summary>
	/// <b><c>ValueOption.FoldBack(option, state, folder)</c></b> evaluates to <b><c>option.IsSome ? folder(option.Value, state) : state</c></b>.
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
	public static TState FoldBack<T, TState>(ValueOption<T> option, TState state, Func<T, TState, TState> folder) => option.IsSome
		? folder(option.Value!, state)
		: state;

	/// <summary>
	/// <b><c>ValueOption.ForAll(option, predicate)</c></b> evaluates to <b><c>option.IsNone || predicate(option.Value)</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="predicate">A function that evaluates to a boolean when given a value from the option type.</param>
	/// <param name="option">The input option.</param>
	/// <returns><see langword="true"/> if the <paramref name="option"/> is None,
	/// otherwise it returns the result of applying the <paramref name="predicate"/> to the <paramref name="option"/> value.
	/// </returns>
	public static bool ForAll<T>(ValueOption<T> option, Func<T, bool> predicate) => option.IsNone || predicate(option.Value!);

	/// <summary>
	/// Gets the value associated with the <paramref name="option"/>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The value within the <paramref name="option"/>.</returns>
	/// <exception cref="ArgumentException">Thrown when the <paramref name="option"/> is None.</exception>
	public static T Get<T>(ValueOption<T> option) => option.IsSome
		? option.Value!
		: throw new ArgumentException("The option value was None", nameof(option));

	/// <summary>
	/// Returns <see langword="true"/> if the <paramref name="option"/> is None.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns><see langword="true"/> if the option is None.</returns>
	public static bool IsNone<T>(ValueOption<T> option) => option.IsNone;

	/// <summary>
	/// Returns <see langword="true"/> if the <paramref name="option"/> is not None.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns><see langword="true"/> if the option is not None.</returns>
	public static bool IsSome<T>(ValueOption<T> option) => option.IsSome;

	/// <summary>
	/// <b><c>ValueOption.Iter(option, action)</c></b> executes <b><c>if (option.IsSome) action(option.Value)</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="action">A function to apply to the <paramref name="option"/> value.</param>
	/// <param name="option">The input option.</param>
	public static void Iter<T>(ValueOption<T> option, Action<T> action)
	{
		if (option.IsSome)
		{
			action(option.Value!);
		}
	}

	/// <summary>
	/// <b><c>ValueOption.Map(option, mapping)</c></b> evaluates to <b><c>option.IsSome ? ValueOption.Some(mapping(option.Value)) : ValueOption.None&#60;U&#62;()</c></b>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="mapping"/>'s return type.</typeparam>
	/// <param name="mapping">A function to apply to the <paramref name="option"/> value.</param>
	/// <param name="option">The input option.</param>
	/// <returns>
	/// An option of the <paramref name="option"/> value after applying the <paramref name="mapping"/> function,
	/// or None if the <paramref name="option"/> is None.
	/// </returns>
	public static ValueOption<U> Map<T, U>(ValueOption<T> option, Func<T, U> mapping) => option.IsSome
		? Some(mapping(option.Value))
		: None<U>();

	/// <summary>
	/// <b><c>ValueOption.Map(option1, option2, mapping)</c></b>
	/// evaluates to
	/// <b><c>option1.IsSome &#38;&#38; option2.IsSome ? ValueOption.Some(mapping(option1.Value, option2.Value)) : ValueOption.None&#60;U&#62;()</c></b>.
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
	public static ValueOption<U> Map<T1, T2, U>(ValueOption<T1> option1, ValueOption<T2> option2, Func<T1, T2, U> mapping) => option1.IsSome && option2.IsSome
		? Some(mapping(option1.Value, option2.Value))
		: None<U>();

	/// <summary>
	/// <b><c>ValueOption.Map(option1, option2, option3, mapping)</c></b>
	/// evaluates to
	/// <b><c>option1.IsSome &#38;&#38; option2.IsSome &#38;&#38; option3.IsSome ? ValueOption.Some(mapping(option1.Value, option2.Value, option3.Value)) : ValueOption.None&#60;U&#62;()</c></b>.
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
	public static ValueOption<U> Map<T1, T2, T3, U>(ValueOption<T1> option1, ValueOption<T2> option2, ValueOption<T3> option3, Func<T1, T2, T3, U> mapping) => option1.IsSome && option2.IsSome && option3.IsSome
		? Some(mapping(option1.Value, option2.Value, option3.Value))
		: None<U>();

	/// <summary>
	/// Convert a Nullable <paramref name="value"/> to an option.
	/// </summary>
	/// <typeparam name="T"><paramref name="value"/>'s generic type.</typeparam>
	/// <param name="value">The input nullable value.</param>
	/// <returns>The result option.</returns>
	public static ValueOption<T> OfNullable<T>(T? value) where T : struct => value.HasValue
		? Some(value.Value)
		: None<T>();

	/// <summary>
	/// Convert a potentially <see langword="null"/> <paramref name="value"/> to an option.
	/// </summary>
	/// <typeparam name="T"><paramref name="value"/>'s generic type.</typeparam>
	/// <param name="value">The input value.</param>
	/// <returns>The result option.</returns>
	public static ValueOption<T> OfObj<T>(T? value) where T : class => value is not null
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
	public static ValueOption<T> OrElse<T>(ValueOption<T> option, ValueOption<T> ifNone) => option.IsNone
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
	public static ValueOption<T> OrElseWith<T>(ValueOption<T> option, Func<ValueOption<T>> ifNoneThunk) => option.IsNone
		? ifNoneThunk()
		: option;

	/// <summary>
	/// Convert the <paramref name="option"/> to a array of length 0 or 1.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The result array.</returns>
	public static T[] ToArray<T>(ValueOption<T> option) => option.IsSome
		? new T[] { option.Value! }
		: Array.Empty<T>();

	/// <summary>
	/// Convert the <paramref name="option"/> to a list of length 0 or 1.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The result list.</returns>
	public static List<T> ToList<T>(ValueOption<T> option) => option.IsSome
		? new List<T>() { option.Value! }
		: new List<T>();

	/// <summary>
	/// Convert the <paramref name="option"/> to a Nullable value.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input option.</param>
	/// <returns>The result value.</returns>
	public static T? ToNullable<T>(ValueOption<T> option) where T : struct => option.IsSome
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
	public static T? ToObj<T>(ValueOption<T> option) where T : class => option.IsSome ? option.Value : default;

	/// <summary>
	/// Execute and return the result of either <paramref name="some"/> or <paramref name="none"/>
	/// depending whether <paramref name="option"/> is <c>Some</c> or <c>None</c>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <typeparam name="U"><paramref name="some"/> and <paramref name="none"/>'s return type.</typeparam>
	/// <param name="option">The input value.</param>
	/// <param name="some">The function to execute when <paramref name="option"/> is <c>Some</c>.</param>
	/// <param name="none">The function to execute when <paramref name="option"/> is <c>None</c>.</param>
	public static U Match<T, U>(ValueOption<T> option, Func<T, U> some, Func<U> none) => option.IsSome
		? some(option.Value)
		: none();

	/// <summary>
	/// Execute either <paramref name="some"/> or <paramref name="none"/> depending whether <paramref name="option"/> is <c>Some</c> or <c>None</c>.
	/// </summary>
	/// <typeparam name="T"><paramref name="option"/>'s generic type.</typeparam>
	/// <param name="option">The input value.</param>
	/// <param name="some">The function to execute when <paramref name="option"/> is <c>Some</c>.</param>
	/// <param name="none">The function to execute when <paramref name="option"/> is <c>None</c>.</param>
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

	/// <summary>
	/// Converts a string representation of <typeparamref name="T"/> to using the passed in <paramref name="tryParser"/>.
	/// </summary>
	/// <typeparam name="T">The type to convert to.</typeparam>
	/// <param name="s">A string containing a value to convert.</param>
	/// <param name="tryParser">The try parse function (for example: <see cref="int.TryParse(string?, out int)"/>).</param>
	/// <returns>
	/// If <paramref name="tryParser"/> returns <see langword="true"/> then Some parsedValue from the out parameter of <paramref name="tryParser"/>.
	/// Otherwise, <paramref name="tryParser"/> returns <see langword="false"/> then None.
	/// </returns>
	public static ValueOption<T> OfTryParse<T>(string? s, TryParse<T> tryParser) => tryParser(s, out var value)
		? Some(value)
		: None<T>();

	/// <summary>
	/// Converts a string representation of <typeparamref name="T"/> to using the passed in <paramref name="tryParser"/>.
	/// </summary>
	/// <typeparam name="T">The type to convert to.</typeparam>
	/// <param name="s">A string containing a value to convert.</param>
	/// <param name="tryParser">The try parse function (for example: <see cref="int.TryParse(ReadOnlySpan{char}, out int)"/>).</param>
	/// <returns>
	/// If <paramref name="tryParser"/> returns <see langword="true"/> then Some parsedValue from the out parameter of <paramref name="tryParser"/>.
	/// Otherwise, <paramref name="tryParser"/> returns <see langword="false"/> then None.
	/// </returns>
	public static ValueOption<T> OfTryParse<T>(ReadOnlySpan<char> s, TryParseSpan<T> tryParser) => tryParser(s, out var value)
		? Some(value)
		: None<T>();
}

/// <summary>
/// The type of optional values. represented as structs.
/// </summary>
/// <remarks>
/// Use the constructor functions <b><c>ValueOption.Some&#60;T&#62;(...)</c></b> and <b><c>ValueOption.None&#60;T&#62;()</c></b> to create values of this type.
/// Use the functions in the <see langword="static"/> <see langword="class"/> <c>ValueOption</c> to manipulate values of this type.
/// </remarks>
public record struct ValueOption<T> : IStructuralEquatable, IStructuralComparable, IEquatable<T>, IEquatable<ValueOption<T>>, IComparable, IComparable<ValueOption<T>>
{
	internal static ValueOption<T> None { get; } = new(default, false);

	private readonly T _value;
	private readonly bool _hasValue;

	/// <summary>
	/// Get the value of a 'Some' option. A <see cref="InvalidOperationException"/> is thrown if this option is 'None'.
	/// </summary>
	public T Value => IsNone ? throw new InvalidOperationException("ValueOption.Value") : _value;


	/// <summary>
	/// Return <see langword="true"/> if this option is a 'Some' value.
	/// </summary>
	public bool IsSome => _hasValue;

	/// <summary>
	/// Return <see langword="true"/> if this option is a 'None' value.
	/// </summary>
	public bool IsNone => !_hasValue;

	private ValueOption(T? value, bool hasValue)
	{
		_value = value!;
		_hasValue = hasValue;
	}

	internal ValueOption(T value)
		: this(value, hasValue: true)
	{
	}


	public bool Equals(T? other) => EqualityComparer<T>.Default.Equals(_value, other);
	public bool Equals(object? other, IEqualityComparer comparer) => comparer.Equals(_value, other);
	public int GetHashCode(IEqualityComparer comparer) => comparer.GetHashCode(_value!);
	public override int GetHashCode() => GetHashCode(EqualityComparer<T>.Default);
	public int CompareTo(object? other, IComparer comparer) => comparer.Compare(_value, other);
	public int CompareTo(object? obj) => CompareTo(obj, Comparer<T>.Default);
	public int CompareTo(ValueOption<T> other) => CompareTo(other._value, Comparer<T>.Default);


	public static bool operator <(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) < 0;
	public static bool operator <=(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) <= 0;
	public static bool operator >(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) > 0;
	public static bool operator >=(ValueOption<T> left, ValueOption<T> right) => left.CompareTo(right) >= 0;
}
