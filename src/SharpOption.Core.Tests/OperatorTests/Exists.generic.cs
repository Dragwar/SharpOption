namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Exists))]
public class Exists
{
	[Fact]
	public void Some_Exists_PredicateMatches_ShouldReturn_True()
	{
		// Arrange
		var op = 10.Some();

		// Act
		var actual = op.Exists(n => n > 5);

		// Assert
		Assert.True(actual);
	}

	[Fact]
	public void Some_Exists_PredicateNoMatch_ShouldReturn_False()
	{
		// Arrange
		var op = 10.Some();

		// Act
		var actual = op.Exists(n => n > 500);

		// Assert
		Assert.False(actual);
	}

	[Fact]
	public void None_Exists_PredicateNotExecuted_ShouldReturn_False()
	{
		// Arrange
		var op = None<int>();

		// Act
		var actual = op.Exists(n => n > 5);

		// Assert
		Assert.False(actual);
	}
}
