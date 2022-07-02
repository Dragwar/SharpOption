namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Iter))]
public class Iter
{
	[Fact]
	public void Some_Iter_ShouldExecute()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10;

		// Act
		var actual = 0;
		op.Iter(v => actual = v);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Iter_ShouldNotExecute()
	{
		// Arrange
		var op = None<int>();
		var expected = 0;

		// Act
		var actual = 0;
		op.Iter(v => actual = v);

		// Assert
		Assert.Equal(expected, actual);
	}
}
