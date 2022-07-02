namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Map))]
public class Map
{
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

	[Fact]
	public void Some_Some_Map2_ShouldReturn_Some()
	{
		// Arrange
		var op1 = 10.Some();
		var op2 = 10.Some();
		var expected = 21.Some();

		// Act
		var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

		// Assert
		Assert.True(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Some_Map2_ShouldReturn_None()
	{
		// Arrange
		var op1 = None<int>();
		var op2 = 10.Some();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Some_None_Map2_ShouldReturn_None()
	{
		// Arrange
		var op1 = 10.Some();
		var op2 = None<int>();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_None_Map2_ShouldReturn_None()
	{
		// Arrange
		var op1 = None<int>();
		var op2 = None<int>();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Some_Some_Some_Map3_ShouldReturn_Some()
	{
		// Arrange
		var op1 = 10.Some();
		var op2 = 10.Some();
		var op3 = 10.Some();
		var expected = 31.Some();

		// Act
		var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

		// Assert
		Assert.True(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Some_Some_None_Map3_ShouldReturn_None()
	{
		// Arrange
		var op1 = 10.Some();
		var op2 = 10.Some();
		var op3 = None<int>();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Some_None_Some_Map3_ShouldReturn_None()
	{
		// Arrange
		var op1 = 10.Some();
		var op2 = None<int>();
		var op3 = 10.Some();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Some_Some_Map3_ShouldReturn_None()
	{
		// Arrange
		var op1 = None<int>();
		var op2 = 10.Some();
		var op3 = 10.Some();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_None_Some_Map3_ShouldReturn_None()
	{
		// Arrange
		var op1 = None<int>();
		var op2 = None<int>();
		var op3 = 10.Some();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_None_None_Map3_ShouldReturn_None()
	{
		// Arrange
		var op1 = None<int>();
		var op2 = None<int>();
		var op3 = None<int>();
		var expected = None<int>();

		// Act
		var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

		// Assert
		Assert.False(actual.IsSome);
		Assert.Equal(expected, actual);
	}
}
