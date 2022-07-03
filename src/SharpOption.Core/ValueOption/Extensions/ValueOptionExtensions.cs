using SharpOption.Core.Delegates;

namespace SharpOption.Core.ValueOption.Extensions;

/// <inheritdoc cref="ValueOption" />
public static class ValueOptionExtensions
{
	/// <inheritdoc cref="ValueOption.Some{T}(T)" />
	public static ValueOption<T> Some<T>(this T value) => ValueOption.Some(value);

	/// <inheritdoc cref="ValueOption.None{T}()" />
	public static ValueOption<T> None<T>() => ValueOption.None<T>();

	/// <inheritdoc cref="ValueOption.Bind{T, U}(ValueOption{T}, Func{T, ValueOption{U}})" />
	public static ValueOption<U> Bind<T, U>(this ValueOption<T> option, Func<T, ValueOption<U>> binder) => ValueOption.Bind(option, binder);

	/// <inheritdoc cref="ValueOption.Contains{T}(ValueOption{T}, T)" />
	public static bool Contains<T>(this ValueOption<T> option, T value) => ValueOption.Contains(option, value);

	/// <inheritdoc cref="ValueOption.Count{T}(ValueOption{T})" />
	public static int Count<T>(this ValueOption<T> option) => ValueOption.Count(option);

	/// <inheritdoc cref="ValueOption.DefaultValue{T}(ValueOption{T}, T)" />
	public static T DefaultValue<T>(this ValueOption<T> option, T defaultValue) => ValueOption.DefaultValue(option, defaultValue);

	/// <inheritdoc cref="ValueOption.DefaultWith{T}(ValueOption{T}, Func{T})" />
	public static T DefaultWith<T>(this ValueOption<T> option, Func<T> defThunk) => ValueOption.DefaultWith(option, defThunk);

	/// <inheritdoc cref="ValueOption.Exists{T}(ValueOption{T}, Func{T, bool})" />
	public static bool Exists<T>(this ValueOption<T> option, Func<T, bool> predicate) => ValueOption.Exists(option, predicate);

	/// <inheritdoc cref="ValueOption.Filter{T}(ValueOption{T}, Func{T, bool})" />
	public static ValueOption<T> Filter<T>(this ValueOption<T> option, Func<T, bool> predicate) => ValueOption.Filter(option, predicate);

	/// <inheritdoc cref="ValueOption.Flatten{T}(ValueOption{ValueOption{T}})" />
	public static ValueOption<T> Flatten<T>(this ValueOption<ValueOption<T>> option) => ValueOption.Flatten(option);

	/// <inheritdoc cref="ValueOption.Fold{T, TState}(ValueOption{T}, TState, Func{TState, T, TState})" />
	public static TState Fold<T, TState>(this ValueOption<T> option, TState state, Func<TState, T, TState> folder) => ValueOption.Fold(option, state, folder);

	/// <inheritdoc cref="ValueOption.FoldBack{T, TState}(ValueOption{T}, TState, Func{T, TState, TState})" />
	public static TState FoldBack<T, TState>(this ValueOption<T> option, TState state, Func<T, TState, TState> folder) => ValueOption.FoldBack(option, state, folder);

	/// <inheritdoc cref="ValueOption.ForAll{T}(ValueOption{T}, Func{T, bool})" />
	public static bool ForAll<T>(this ValueOption<T> option, Func<T, bool> predicate) => ValueOption.ForAll(option, predicate);

	/// <inheritdoc cref="ValueOption.Get{T}(ValueOption{T})" />
	public static T Get<T>(this ValueOption<T> option) => ValueOption.Get(option);

	/// <inheritdoc cref="ValueOption.IsNone{T}(ValueOption{T})" />
	public static bool IsNone<T>(this ValueOption<T> option) => ValueOption.IsNone(option);

	/// <inheritdoc cref="ValueOption.IsSome{T}(ValueOption{T})" />
	public static bool IsSome<T>(this ValueOption<T> option) => ValueOption.IsSome(option);

	/// <inheritdoc cref="ValueOption.Iter{T}(ValueOption{T}, Action{T})" />
	public static void Iter<T>(this ValueOption<T> option, Action<T> action) => ValueOption.Iter(option, action);

	/// <inheritdoc cref="ValueOption.Map{T, U}(ValueOption{T}, Func{T, U})" />
	public static ValueOption<U> Map<T, U>(this ValueOption<T> option, Func<T, U> mapping) => ValueOption.Map(option, mapping);

	/// <inheritdoc cref="ValueOption.Map{T1, T2, U}(ValueOption{T1}, ValueOption{T2}, Func{T1, T2, U})" />
	public static ValueOption<U> Map<T1, T2, U>(this ValueOption<T1> option1, ValueOption<T2> option2, Func<T1, T2, U> mapping) => ValueOption.Map(option1, option2, mapping);

	/// <inheritdoc cref="ValueOption.Map{T1, T2, T3, U}(ValueOption{T1}, ValueOption{T2}, ValueOption{T3}, Func{T1, T2, T3, U})" />
	public static ValueOption<U> Map<T1, T2, T3, U>(this ValueOption<T1> option1, ValueOption<T2> option2, ValueOption<T3> option3, Func<T1, T2, T3, U> mapping) => ValueOption.Map(option1, option2, option3, mapping);

	/// <inheritdoc cref="ValueOption.OfNullable{T}(T?)" />
	public static ValueOption<T> OfNullable<T>(this T? value) where T : struct => ValueOption.OfNullable(value);

	/// <inheritdoc cref="ValueOption.OfObj{T}(T)" />
	public static ValueOption<T> OfObj<T>(this T? value) where T : class => ValueOption.OfObj(value);

	/// <inheritdoc cref="ValueOption.OrElse{T}(ValueOption{T}, ValueOption{T})" />
	public static ValueOption<T> OrElse<T>(this ValueOption<T> option, ValueOption<T> ifNone) => ValueOption.OrElse(option, ifNone);

	/// <inheritdoc cref="ValueOption.OrElseWith{T}(ValueOption{T}, Func{ValueOption{T}})" />
	public static ValueOption<T> OrElseWith<T>(this ValueOption<T> option, Func<ValueOption<T>> ifNoneThunk) => ValueOption.OrElseWith(option, ifNoneThunk);

	/// <inheritdoc cref="ValueOption.ToArray{T}(ValueOption{T})" />
	public static T[] ToArray<T>(this ValueOption<T> option) => ValueOption.ToArray(option);

	/// <inheritdoc cref="ValueOption.ToList{T}(ValueOption{T})" />
	public static List<T> ToList<T>(this ValueOption<T> option) => ValueOption.ToList(option);

	/// <inheritdoc cref="ValueOption.ToNullable{T}(ValueOption{T})" />
	public static T? ToNullable<T>(this ValueOption<T> option) where T : struct => ValueOption.ToNullable(option);

	/// <inheritdoc cref="ValueOption.ToObj{T}(ValueOption{T})" />
	public static T? ToObj<T>(this ValueOption<T> option) where T : class => ValueOption.ToObj(option);

	/// <inheritdoc cref="ValueOption.Match{T, U}(ValueOption{T}, Func{T, U}, Func{U})" />
	public static U Match<T, U>(this ValueOption<T> option, Func<T, U> some, Func<U> none) => ValueOption.Match(option, some, none);

	/// <inheritdoc cref="ValueOption.Match{T}(ValueOption{T}, Action{T}, Action)" />
	public static void Match<T>(this ValueOption<T> option, Action<T> some, Action none) => ValueOption.Match(option, some, none);

	/// <inheritdoc cref="ValueOption.OfTryParse{T}(string?, TryParse{T})" />
	public static ValueOption<T> OfTryParse<T>(this string? s, TryParse<T> tryParser) => ValueOption.OfTryParse(s, tryParser);

	/// <inheritdoc cref="ValueOption.OfTryParse{T}(ReadOnlySpan{char}, TryParseSpan{T})" />
	public static ValueOption<T> OfTryParse<T>(this ReadOnlySpan<char> s, TryParseSpan<T> tryParser) => ValueOption.OfTryParse(s, tryParser);
}
