namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(OfTryParse))]
public class OfTryParse
{
	[Theory]
	[InlineData("-1", true)]
	[InlineData("-320", true)]
	[InlineData("0", true)]
	[InlineData("32", true)]
	[InlineData("10029", true)]
	[InlineData("a13av", false)]
	[InlineData("1__da1", false)]
	[InlineData("$1", false)]
	[InlineData(null, false)]
	public void String_OfTryParse_ShouldReturn_SomeOrNone(string? str, bool isValid)
	{
		// Arrange
		var expected = isValid ? int.Parse(str!).Some() : None<int>();

		// Act
		var actual = str.OfTryParse<int>(int.TryParse);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData("-1", true)]
	[InlineData("-320", true)]
	[InlineData("0", true)]
	[InlineData("32", true)]
	[InlineData("10029", true)]
	[InlineData("a13av", false)]
	[InlineData("1__da1", false)]
	[InlineData("$1", false)]
	[InlineData(null, false)]
	public void Span_OfTryParse_ShouldReturn_SomeOrNone(string? str, bool isValid)
	{
		// Arrange
		var span = str is not null ? str.AsSpan() : default;
		var expected = isValid ? int.Parse(span).Some() : None<int>();

		// Act
		var actual = span.OfTryParse<int>(int.TryParse);

		// Assert
		Assert.Equal(expected, actual);
	}
}
