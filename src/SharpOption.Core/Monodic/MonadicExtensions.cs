using SharpOption.Core.Extensions;
using static SharpOption.Core.Option;

namespace SharpOption.Core.Monodic;
public static class MonadicExtensions
{
	public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate) => option.Filter(predicate);
	public static Option<T> Where<T>(this T option, Func<T, bool> predicate) where T : notnull => option.Some().Filter(predicate);
	public static Option<TResult> Select<T, TResult>(this Option<T> option, Func<T, Option<TResult>> selector) => option.Bind(selector);
	public static Option<TResult> Select<T, TResult>(this T option, Func<T, Option<TResult>> selector) where T : notnull => option is not null ? option.Some().Bind(selector) : None<TResult>();

	public static Option<TResult> SelectMany<TFirst, TSecond, TResult>(
		this Option<TFirst> first,
		Func<TFirst, Option<TSecond>> getSecond,
		Func<TFirst, TSecond, TResult> selector) => first.Bind(f => first.Map(getSecond(f), selector));

	public static async Task<Option<TResult>> SelectMany<TFirst, TSecond, TResult>(
		this Task<Option<TFirst>> first,
		Func<TFirst, Task<Option<TSecond>>> getSecond,
		Func<TFirst, TSecond, TResult> selector)
	{
		var firstOption = await first;
		var res = await firstOption.Match(
			async f =>
			{
				var secondOption = await getSecond(f);
				var r = secondOption.Bind(s => Some(selector(f, s)));
				return r;
			},
			() => Task.FromResult(None<TResult>())
		);
		return res;
	}
}
