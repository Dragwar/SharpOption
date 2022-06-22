namespace SharpOption.Core.ValueOption.Extensions;

/// <inheritdoc cref="ValueOption" />
public static class ValueOptionExtensions
{
	public static ValueOption<T> Some<T>(this T value) => ValueOption.Some(value);
	public static ValueOption<T> None<T>() => ValueOption.None<T>();
	public static ValueOption<TOther> Bind<T, TOther>(this ValueOption<T> option, Func<T, ValueOption<TOther>> binder) => ValueOption.Bind(option, binder);
	public static bool Contains<T>(this ValueOption<T> option, T value) => ValueOption.Contains(option, value);
	public static int Count<T>(this ValueOption<T> option) => ValueOption.Count(option);
	public static T DefaultValue<T>(this ValueOption<T> option, T defaultValue) => ValueOption.DefaultValue(option, defaultValue);
	public static T DefaultWith<T>(this ValueOption<T> option, Func<T> defThunk) => ValueOption.DefaultWith(option, defThunk);
	public static bool Exists<T>(this ValueOption<T> option, Func<T, bool> predicate) => ValueOption.Exists(option, predicate);
	public static ValueOption<T> Filter<T>(this ValueOption<T> option, Func<T, bool> predicate) => ValueOption.Filter(option, predicate);
	public static ValueOption<T> Flatten<T>(this ValueOption<ValueOption<T>> option) => ValueOption.Flatten(option);
	public static TState Fold<T, TState>(this ValueOption<T> option, TState state, Func<TState, T, TState> folder) => ValueOption.Fold(option, state, folder);
	public static TState FoldBack<T, TState>(this ValueOption<T> option, TState state, Func<T, TState, TState> folder) => ValueOption.FoldBack(option, state, folder);
	public static bool ForAll<T>(this ValueOption<T> option, Func<T, bool> predicate) => ValueOption.ForAll(option, predicate);
	public static T Get<T>(this ValueOption<T> option) => ValueOption.Get(option);
	public static bool IsNone<T>(this ValueOption<T> option) => ValueOption.IsNone(option);
	public static bool IsSome<T>(this ValueOption<T> option) => ValueOption.IsSome(option);
	public static void Iter<T>(this ValueOption<T> option, Action<T> action) => ValueOption.Iter(option, action);
	public static ValueOption<TOther> Map<T, TOther>(this ValueOption<T> option, Func<T, TOther> mapping) => ValueOption.Map(option, mapping);
	public static ValueOption<TOther> Map<T1, T2, TOther>(this ValueOption<T1> option1, ValueOption<T2> option2, Func<T1, T2, TOther> mapping) => ValueOption.Map(option1, option2, mapping);
	public static ValueOption<TOther> Map<T1, T2, T3, TOther>(this ValueOption<T1> option1, ValueOption<T2> option2, ValueOption<T3> option3, Func<T1, T2, T3, TOther> mapping) => ValueOption.Map(option1, option2, option3, mapping);
	public static ValueOption<T> OfNullable<T>(this T? value) where T : struct => ValueOption.OfNullable(value);
	public static ValueOption<T> OfObj<T>(this T value) where T : class => ValueOption.OfObj(value);
	public static ValueOption<T> OrElse<T>(this ValueOption<T> option, ValueOption<T> ifNone) where T : class => ValueOption.OrElse(option, ifNone);
	public static ValueOption<T> OrElseWith<T>(this ValueOption<T> option, Func<ValueOption<T>> ifNoneThunk) where T : class => ValueOption.OrElseWith(option, ifNoneThunk);
	public static T[] ToArray<T>(this ValueOption<T> option) => ValueOption.ToArray(option);
	public static List<T> ToList<T>(this ValueOption<T> option) => ValueOption.ToList(option);
	public static T? ToNullable<T>(this ValueOption<T> option) where T : struct => ValueOption.ToNullable(option);
	public static T? ToObj<T>(this ValueOption<T> option) => ValueOption.ToObj(option);

	public static TResult Match<T, TResult>(this ValueOption<T> option, Func<T, TResult> some, Func<TResult> none) => ValueOption.Match(option, some, none);
	public static void Match<T>(this ValueOption<T> option, Action<T> some, Action none) => ValueOption.Match(option, some, none);
}
