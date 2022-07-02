namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Get))]
public class Get
{
	[Fact]
	public void Some_Get_ShouldReturn_Value()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10;

		// Act
		var actual = op.Get();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Get_ShouldThrow_ArgumentException()
	{
		// Arrange
		var op = None<int>();

		// Act
		// Assert
		Assert.Throws<ArgumentException>(() => op.Get());
	}
}
