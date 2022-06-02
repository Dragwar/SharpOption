namespace SharpOption.Core.Extensions;

/// <inheritdoc cref="Option" />
public static class OptionExtensions
{
	public static Option<T> Some<T>(this T value) => Option.Some(value);
	public static Option<T> None<T>() => Option.None<T>();
	public static Option<TOther> Bind<T, TOther>(this Option<T> option, Func<T, Option<TOther>> binder) => Option.Bind(option, binder);
	public static bool Contains<T>(this Option<T> option, T value) => Option.Contains(option, value);
	public static int Count<T>(this Option<T> option) => Option.Count(option);
	public static T DefaultValue<T>(this Option<T> option, T defaultValue) => Option.DefaultValue(option, defaultValue);
	public static T DefaultWith<T>(this Option<T> option, Func<T> defThunk) => Option.DefaultWith(option, defThunk);
	public static bool Exists<T>(this Option<T> option, Func<T, bool> predicate) => Option.Exists(option, predicate);
	public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate) => Option.Filter(option, predicate);
	public static Option<T> Flatten<T>(this Option<Option<T>> option) => Option.Flatten(option);
	public static TState Fold<T, TState>(this Option<T> option, TState state, Func<TState, T, TState> folder) => Option.Fold(option, state, folder);
	public static TState FoldBack<T, TState>(this Option<T> option, TState state, Func<T, TState, TState> folder) => Option.FoldBack(option, state, folder);
	public static bool ForAll<T>(this Option<T> option, Func<T, bool> predicate) => Option.ForAll(option, predicate);
	public static T Get<T>(this Option<T> option) => Option.Get(option);
	public static bool IsNone<T>(this Option<T> option) => Option.IsNone(option);
	public static bool IsSome<T>(this Option<T> option) => Option.IsSome(option);
	public static void Iter<T>(this Option<T> option, Action<T> action) => Option.Iter(option, action);
	public static Option<TOther> Map<T, TOther>(this Option<T> option, Func<T, TOther> mapping) => Option.Map(option, mapping);
	public static Option<TOther> Map<T1, T2, TOther>(this Option<T1> option1, Option<T2> option2, Func<T1, T2, TOther> mapping) => Option.Map(option1, option2, mapping);
	public static Option<TOther> Map<T1, T2, T3, TOther>(this Option<T1> option1, Option<T2> option2, Option<T3> option3, Func<T1, T2, T3, TOther> mapping) => Option.Map(option1, option2, option3, mapping);
	public static Option<T> OfNullable<T>(this T? value) where T : struct => Option.OfNullable(value);
	public static Option<T> OfObj<T>(this T value) where T : class => Option.OfObj(value);
	public static Option<T> OrElse<T>(this Option<T> option, Option<T> ifNone) where T : class => Option.OrElse(option, ifNone);
	public static Option<T> OrElseWith<T>(this Option<T> option, Func<Option<T>> ifNoneThunk) where T : class => Option.OrElseWith(option, ifNoneThunk);
	public static T[] ToArray<T>(this Option<T> option) => Option.ToArray(option);
	public static List<T> ToList<T>(this Option<T> option) => Option.ToList(option);
	public static T? ToNullable<T>(this Option<T> option) where T : struct => Option.ToNullable(option);
	public static T? ToObj<T>(this Option<T> option) => Option.ToObj(option);

	public static TResult Match<T, TResult>(this Option<T> option, Func<T, TResult> some, Func<TResult> none) => Option.Match(option, some, none);
	public static void Match<T>(this Option<T> option, Action<T> some, Action none) => Option.Match(option, some, none);
}
