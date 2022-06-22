using SharpOption.Core.ValueOption.Extensions;
using static SharpOption.Core.ValueOption.ValueOption;

namespace SharpOption.Core.ValueOption.Monodic;
public static class ValueOptionMonadicExtensions
{
	public static ValueOption<T> Where<T>(this ValueOption<T> option, Func<T, bool> predicate) => option.Filter(predicate);
	public static ValueOption<T> Where<T>(this T option, Func<T, bool> predicate) where T : notnull => option.Some().Filter(predicate);
	public static ValueOption<TOut> Select<T, TOut>(this ValueOption<T> option, Func<T, ValueOption<TOut>> selector) => option.Bind(selector);
	public static ValueOption<TOut> Select<T, TOut>(this T option, Func<T, ValueOption<TOut>> selector) where T : notnull => option is not null ? option.Some().Bind(selector) : None<TOut>();

	public static ValueOption<TOut> SelectMany<TFirst, TSecond, TOut>(
		this ValueOption<TFirst> first,
		Func<TFirst, ValueOption<TSecond>> getSecond,
		Func<TFirst, TSecond, TOut> selector) => first.Bind(f => first.Map(getSecond(f), selector));

	public static async Task<ValueOption<TOut>> SelectMany<TFirst, TSecond, TOut>(
		this Task<ValueOption<TFirst>> first,
		Func<TFirst, Task<ValueOption<TSecond>>> getSecond,
		Func<TFirst, TSecond, TOut> selector)
	{
		var firstValueOption = await first;
		var res = await firstValueOption.Match(
			async f =>
			{
				var secondValueOption = await getSecond(f);
				var r = secondValueOption.Bind(s => Some(selector(f, s)));
				return r;
			},
			() => Task.FromResult(None<TOut>())
		);
		return res;
	}
}
