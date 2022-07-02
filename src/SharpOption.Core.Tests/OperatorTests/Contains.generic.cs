namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Contains))]
public class Contains
{
	[Fact]
	public void Some_Contains_ShouldReturn_True()
	{
		// Arrange
		var op = 10.Some();

		// Act
		var actual = op.Contains(10);

		// Assert
		Assert.True(actual);
	}

	[Fact]
	public void None_Contains_ShouldReturn_False()
	{
		// Arrange
		var op = None<int>();

		// Act
		var actual = op.Contains(10);

		// Assert
		Assert.False(actual);
	}
}