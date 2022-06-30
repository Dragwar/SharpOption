namespace SharpOption.Core.Tests;

public class OptionInstanceTests
{
	[Fact]
	public void Some_And_IsSome_ShouldBe_True_And_IsNone_ShouldBe_False()
	{
		// Arrange
		var op = 10.Some();

		// Act
		// Assert
		Assert.True(op.IsSome);
		Assert.False(op.IsNone);
	}

	[Fact]
	public void None_And_IsSome_ShouldBe_False_And_IsNone_ShouldBe_True()
	{
		// Arrange
		var op = None<int>();

		// Act
		// Assert
		Assert.False(op.IsSome);
		Assert.True(op.IsNone);
	}

	[Fact]
	public void Some_And_ShouldReturn_Getter_Value()
	{
		// Arrange
		var op = 10.Some();
		var expected = 10;

		// Act
		var actual = op.Value;

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_And_Getter_Value_ShouldThrow_InvalidOperationException()
	{
		// Arrange
		var op = None<int>();

		// Act
		// Assert
		Assert.Throws<InvalidOperationException>(() => op.Value);
	}

	//TODO: move equality/comparison tests here
}
