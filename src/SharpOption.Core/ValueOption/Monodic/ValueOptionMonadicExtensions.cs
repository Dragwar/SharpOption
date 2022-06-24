using SharpOption.Core.ValueOption.Extensions;
using static SharpOption.Core.ValueOption.ValueOption;

namespace SharpOption.Core.ValueOption.Monodic;
public static class ValueOptionMonadicExtensions
{
	public static ValueOption<T> Where<T>(this ValueOption<T> option, Func<T, bool> predicate) => option.Filter(predicate);
	public static ValueOption<T> Where<T>(this T value, Func<T, bool> predicate) where T : notnull => value.Some().Filter(predicate);
	public static ValueOption<U> Select<T, U>(this ValueOption<T> option, Func<T, ValueOption<U>> selector) => option.Bind(selector);
	public static ValueOption<U> Select<T, U>(this T value, Func<T, ValueOption<U>> selector) where T : notnull => value is not null ? value.Some().Bind(selector) : None<U>();

	public static ValueOption<U> SelectMany<T1, T2, U>(
		this ValueOption<T1> first,
		Func<T1, ValueOption<T2>> getSecond,
		Func<T1, T2, U> selector) => first.Bind(f => first.Map(getSecond(f), selector));

	public static async Task<ValueOption<U>> SelectMany<T1, T2, U>(
		this Task<ValueOption<T1>> first,
		Func<T1, Task<ValueOption<T2>>> getSecond,
		Func<T1, T2, U> selector)
	{
		var firstValueOption = await first;
		var res = await firstValueOption.Match(
			async f =>
			{
				var secondValueOption = await getSecond(f);
				var r = secondValueOption.Bind(s => Some(selector(f, s)));
				return r;
			},
			() => Task.FromResult(None<U>())
		);
		return res;
	}
}
