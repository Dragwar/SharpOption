using SharpOption.Core.Extensions;
using static SharpOption.Core.Option;

namespace SharpOption.Core.Monodic;
public static class OptionMonadicExtensions
{
	public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate) => option.Filter(predicate);
	public static Option<T> Where<T>(this T option, Func<T, bool> predicate) where T : notnull => option.Some().Filter(predicate);
	public static Option<U> Select<T, U>(this Option<T> option, Func<T, Option<U>> selector) => option.Bind(selector);
	public static Option<U> Select<T, U>(this T option, Func<T, Option<U>> selector) where T : notnull => option is not null ? option.Some().Bind(selector) : None<U>();

	public static Option<U> SelectMany<T1, T2, U>(
		this Option<T1> first,
		Func<T1, Option<T2>> getSecond,
		Func<T1, T2, U> selector) => first.Bind(f => first.Map(getSecond(f), selector));

	public static async Task<Option<U>> SelectMany<T1, T2, U>(
		this Task<Option<T1>> first,
		Func<T1, Task<Option<T2>>> getSecond,
		Func<T1, T2, U> selector)
	{
		var firstOption = await first;
		var res = await firstOption.Match(
			async f =>
			{
				var secondOption = await getSecond(f);
				var r = secondOption.Bind(s => Some(selector(f, s)));
				return r;
			},
			() => Task.FromResult(None<U>())
		);
		return res;
	}
}
