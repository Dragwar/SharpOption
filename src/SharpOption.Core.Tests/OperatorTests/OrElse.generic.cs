namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(OrElse))]
public class OrElse
{
	[Fact]
	public void Some_OrElse_ShouldReturn_OriginalOption()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10.Some();

		// Act
		var actual = op.OrElse(2.Some());

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_OrElse_ShouldReturn_DefaultOption()
	{
		// Arrange
		var op = None<int>();
		var expected = 15.Some();

		// Act
		var actual = op.OrElse(15.Some());

		// Assert
		Assert.Equal(expected, actual);
	}
}
