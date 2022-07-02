namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Match))]
public class Match
{
	[Fact]
	public void Some_Match_ShouldReturn_SomeFuncValue()
	{
		// Arrange
		var op = 10.Some();
		var expected = 200;

		// Act
		var actual = op.Match(
				n => 200,
				() => -1
		);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Match_ShouldReturn_NoneFuncValue()
	{
		// Arrange
		var op = None<int>();
		var expected = -1;

		// Act
		var actual = op.Match(
				n => 200,
				() => -1
		);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Some_Match_ShouldExecute_SomeFunc()
	{
		// Arrange
		var op = 10.Some();
		var expected = 200;

		// Act
		var actual = 0;
		op.Match(
				n => { actual = 200; },
				() => { actual = -1; }
		);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Match_ShouldExecute_NoneFuncValue()
	{
		// Arrange
		var op = None<int>();
		var expected = -1;

		// Act
		var actual = 0;
		op.Match(
				n => { actual = 200; },
				() => { actual = -1; }
		);

		// Assert
		Assert.Equal(expected, actual);
	}
}
