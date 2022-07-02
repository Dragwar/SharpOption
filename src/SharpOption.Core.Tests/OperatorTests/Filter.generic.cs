namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Filter))]
public class Filter
{
	[Fact]
	public void Some_Filter_PredicateMatches_ShouldReturn_Some()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10.Some();

		// Act
		var actual = op.Filter(n => n > 5);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Some_Filter_PredicateNoMatch_ShouldReturn_None()
	{
		// Arrange
		var op = 10.Some();
		var expected = None<int>();

		// Act
		var actual = op.Filter(n => n > 500);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Filter_PredicateNotExecuted_ShouldReturn_None()
	{
		// Arrange
		var op = None<int>();
		var expected = None<int>();

		// Act
		var actual = op.Filter(n => n > 5);

		// Assert
		Assert.Equal(expected, actual);
	}
}
