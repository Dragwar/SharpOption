namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(ForAll))]
public class ForAll
{
	[Fact]
	public void Some_ForAll_PredicateMatches_ShouldReturn_True()
	{
		// Arrange
		var op = 10.Some();

		// Act
		var actual = op.ForAll(n => n > 5);

		// Assert
		Assert.True(actual);
	}

	[Fact]
	public void Some_ForAll_PredicateNoMatch_ShouldReturn_False()
	{
		// Arrange
		var op = 10.Some();

		// Act
		var actual = op.ForAll(n => n > 500);

		// Assert
		Assert.False(actual);
	}

	[Fact]
	public void None_ForAll_PredicateNotExecuted_ShouldReturn_True()
	{
		// Arrange
		var op = None<int>();

		// Act
		var actual = op.ForAll(n => n > 5);

		// Assert
		Assert.True(actual);
	}
}
