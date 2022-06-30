namespace SharpOption.Core.Tests;

public class OperatorTests
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

	[Fact]
	public void Some_Map_ShouldReturn_Some()
	{
		// Arrange
		var op = 10.Some();
		var expected = 11.Some();

		// Act
		var actual = op.Map(n => n + 1);

		// Assert
		Assert.True(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Map_ShouldReturn_None()
	{
		// Arrange
		var op = None<int>();
		var expected = None<int>();

		// Act
		var actual = op.Map(n => n + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}
}
