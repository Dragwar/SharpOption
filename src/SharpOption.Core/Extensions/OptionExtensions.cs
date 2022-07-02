namespace SharpOption.Core.Extensions;

/// <inheritdoc cref="Option" />
public static class OptionExtensions
{
	/// <inheritdoc cref="Option.Some{T}(T)" />
	public static Option<T> Some<T>(this T value) => Option.Some(value);

	/// <inheritdoc cref="Option.None{T}()" />
	public static Option<T> None<T>() => Option.None<T>();

	/// <inheritdoc cref="Option.Bind{T, U}(Option{T}, Func{T, Option{U}})" />
	public static Option<U> Bind<T, U>(this Option<T> option, Func<T, Option<U>> binder) => Option.Bind(option, binder);

	/// <inheritdoc cref="Option.Contains{T}(Option{T}, T)" />
	public static bool Contains<T>(this Option<T> option, T value) => Option.Contains(option, value);

	/// <inheritdoc cref="Option.Count{T}(Option{T})" />
	public static int Count<T>(this Option<T> option) => Option.Count(option);

	/// <inheritdoc cref="Option.DefaultValue{T}(Option{T}, T)" />
	public static T DefaultValue<T>(this Option<T> option, T defaultValue) => Option.DefaultValue(option, defaultValue);

	/// <inheritdoc cref="Option.DefaultWith{T}(Option{T}, Func{T})" />
	public static T DefaultWith<T>(this Option<T> option, Func<T> defThunk) => Option.DefaultWith(option, defThunk);

	/// <inheritdoc cref="Option.Exists{T}(Option{T}, Func{T, bool})" />
	public static bool Exists<T>(this Option<T> option, Func<T, bool> predicate) => Option.Exists(option, predicate);

	/// <inheritdoc cref="Option.Filter{T}(Option{T}, Func{T, bool})" />
	public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate) => Option.Filter(option, predicate);

	/// <inheritdoc cref="Option.Flatten{T}(Option{Option{T}})" />
	public static Option<T> Flatten<T>(this Option<Option<T>> option) => Option.Flatten(option);

	/// <inheritdoc cref="Option.Fold{T, TState}(Option{T}, TState, Func{TState, T, TState})" />
	public static TState Fold<T, TState>(this Option<T> option, TState state, Func<TState, T, TState> folder) => Option.Fold(option, state, folder);

	/// <inheritdoc cref="Option.FoldBack{T, TState}(Option{T}, TState, Func{T, TState, TState})" />
	public static TState FoldBack<T, TState>(this Option<T> option, TState state, Func<T, TState, TState> folder) => Option.FoldBack(option, state, folder);

	/// <inheritdoc cref="Option.ForAll{T}(Option{T}, Func{T, bool})" />
	public static bool ForAll<T>(this Option<T> option, Func<T, bool> predicate) => Option.ForAll(option, predicate);

	/// <inheritdoc cref="Option.Get{T}(Option{T})" />
	public static T Get<T>(this Option<T> option) => Option.Get(option);

	/// <inheritdoc cref="Option.IsNone{T}(Option{T})" />
	public static bool IsNone<T>(this Option<T> option) => Option.IsNone(option);

	/// <inheritdoc cref="Option.IsSome{T}(Option{T})" />
	public static bool IsSome<T>(this Option<T> option) => Option.IsSome(option);

	/// <inheritdoc cref="Option.Iter{T}(Option{T}, Action{T})" />
	public static void Iter<T>(this Option<T> option, Action<T> action) => Option.Iter(option, action);

	/// <inheritdoc cref="Option.Map{T, U}(Option{T}, Func{T, U})" />
	public static Option<U> Map<T, U>(this Option<T> option, Func<T, U> mapping) => Option.Map(option, mapping);

	/// <inheritdoc cref="Option.Map{T1, T2, U}(Option{T1}, Option{T2}, Func{T1, T2, U})" />
	public static Option<U> Map<T1, T2, U>(this Option<T1> option1, Option<T2> option2, Func<T1, T2, U> mapping) => Option.Map(option1, option2, mapping);

	/// <inheritdoc cref="Option.Map{T1, T2, T3, U}(Option{T1}, Option{T2}, Option{T3}, Func{T1, T2, T3, U})" />
	public static Option<U> Map<T1, T2, T3, U>(this Option<T1> option1, Option<T2> option2, Option<T3> option3, Func<T1, T2, T3, U> mapping) => Option.Map(option1, option2, option3, mapping);

	/// <inheritdoc cref="Option.OfNullable{T}(T?)" />
	public static Option<T> OfNullable<T>(this T? value) where T : struct => Option.OfNullable(value);

	/// <inheritdoc cref="Option.OfObj{T}(T)" />
	public static Option<T> OfObj<T>(this T? value) where T : class => Option.OfObj(value);

	/// <inheritdoc cref="Option.OrElse{T}(Option{T}, Option{T})" />
	public static Option<T> OrElse<T>(this Option<T> option, Option<T> ifNone) where T : class => Option.OrElse(option, ifNone);

	/// <inheritdoc cref="Option.OrElseWith{T}(Option{T}, Func{Option{T}})" />
	public static Option<T> OrElseWith<T>(this Option<T> option, Func<Option<T>> ifNoneThunk) where T : class => Option.OrElseWith(option, ifNoneThunk);

	/// <inheritdoc cref="Option.ToArray{T}(Option{T})" />
	public static T[] ToArray<T>(this Option<T> option) => Option.ToArray(option);

	/// <inheritdoc cref="Option.ToList{T}(Option{T})" />
	public static List<T> ToList<T>(this Option<T> option) => Option.ToList(option);

	/// <inheritdoc cref="Option.ToNullable{T}(Option{T})" />
	public static T? ToNullable<T>(this Option<T> option) where T : struct => Option.ToNullable(option);

	/// <inheritdoc cref="Option.ToObj{T}(Option{T})" />
	public static T? ToObj<T>(this Option<T> option) where T : class => Option.ToObj(option);

	/// <inheritdoc cref="Option.Match{T, U}(Option{T}, Func{T, U}, Func{U})" />
	public static U Match<T, U>(this Option<T> option, Func<T, U> some, Func<U> none) => Option.Match(option, some, none);

	/// <inheritdoc cref="Option.Match{T}(Option{T}, Action{T}, Action)" />
	public static void Match<T>(this Option<T> option, Action<T> some, Action none) => Option.Match(option, some, none);
}
