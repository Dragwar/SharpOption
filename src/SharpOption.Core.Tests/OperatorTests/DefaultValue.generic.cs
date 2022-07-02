namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(DefaultValue))]
public class DefaultValue
{
	[Fact]
	public void Some_DefaultValue_ShouldReturn_Value()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10;

		// Act
		var actual = op.DefaultValue(-1);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_DefaultValue_ShouldReturn_Default()
	{
		// Arrange
		var op = None<int>();
		var expected = 10;

		// Act
		var actual = op.DefaultValue(10);

		// Assert
		Assert.Equal(expected, actual);
	}
}
