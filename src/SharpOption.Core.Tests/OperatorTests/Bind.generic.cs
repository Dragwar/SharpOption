namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Bind))]
public class Bind
{
	[Fact]
	public void Some_Bind_ShouldReturn_Some()
	{
		// Arrange
		var op = 10.Some();
		var expected = 11.Some();

		// Act
		var actual = op.Bind(n => (n + 1).Some());

		// Assert
		Assert.True(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Some_Bind_ShouldReturn_None()
	{
		// Arrange
		var op = 10.Some();
		var expected = None<int>();

		// Act
		var actual = op.Bind(_ => None<int>());

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}
}
