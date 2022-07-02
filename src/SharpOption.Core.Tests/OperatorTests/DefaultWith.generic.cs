namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(DefaultWith))]
public class DefaultWith
{
	[Fact]
	public void Some_DefaultWith_ShouldReturn_Value()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10;

		// Act
		var actual = op.DefaultWith(() => -1);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_DefaultWith_ShouldReturn_Default()
	{
		// Arrange
		var op = None<int>();
		var expected = 10;

		// Act
		var actual = op.DefaultWith(() => 10);

		// Assert
		Assert.Equal(expected, actual);
	}
}
