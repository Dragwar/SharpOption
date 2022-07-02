namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(OrElseWith))]
public class OrElseWith
{
	[Fact]
	public void Some_OrElseWith_ShouldReturn_OriginalOption()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10.Some();

		// Act
		var actual = op.OrElseWith(() => 2.Some());

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_OrElseWith_ShouldReturn_DefaultOption()
	{
		// Arrange
		var op = None<int>();
		var expected = 15.Some();

		// Act
		var actual = op.OrElseWith(() => 15.Some());

		// Assert
		Assert.Equal(expected, actual);
	}
}
