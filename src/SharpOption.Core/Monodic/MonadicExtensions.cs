using SharpOption.Core.Extensions;
using static SharpOption.Core.Option;

namespace SharpOption.Core.Monodic;
public static class MonadicExtensions
{
	public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate) => option.Filter(predicate);
	public static Option<T> Where<T>(this T option, Func<T, bool> predicate) where T : notnull => option.Some().Filter(predicate);
	public static Option<TOut> Select<T, TOut>(this Option<T> option, Func<T, Option<TOut>> selector) => option.Bind(selector);
	public static Option<TOut> Select<T, TOut>(this T option, Func<T, Option<TOut>> selector) where T : notnull => option is not null ? option.Some().Bind(selector) : None<TOut>();

	public static Option<TOut> SelectMany<TFirst, TSecond, TOut>(
		this Option<TFirst> first,
		Func<TFirst, Option<TSecond>> getSecond,
		Func<TFirst, TSecond, TOut> selector) => first.Bind(f => first.Map(getSecond(f), selector));

	public static async Task<Option<TOut>> SelectMany<TFirst, TSecond, TOut>(
		this Task<Option<TFirst>> first,
		Func<TFirst, Task<Option<TSecond>>> getSecond,
		Func<TFirst, TSecond, TOut> selector)
	{
		var firstOption = await first;
		var res = await firstOption.Match(
			async f =>
			{
				var secondOption = await getSecond(f);
				var r = secondOption.Bind(s => Some(selector(f, s)));
				return r;
			},
			() => Task.FromResult(None<TOut>())
		);
		return res;
	}
}
