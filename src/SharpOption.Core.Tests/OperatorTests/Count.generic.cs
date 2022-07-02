namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Count))]
public class Count
{
	[Fact]
	public void Some_Count_ShouldReturn_1()
	{
		// Arrange
		var op = "Hello".Some();
		var expected = 1;

		// Act
		var actual = op.Count();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Count_ShouldReturn_0()
	{
		// Arrange
		var op = None<string>();
		var expected = 0;

		// Act
		var actual = op.Count();

		// Assert
		Assert.Equal(expected, actual);
	}
}
