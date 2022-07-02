namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(ToList))]
public class ToList
{
	[Fact]
	public void Some_ToList_ShouldReturn_SingletonList()
	{
		// Arrange
		var op = 10.Some();
		var expected = new[] { 10 }.AsEnumerable();

		// Act
		var actual = op.ToList();

		// Assert
		Assert.IsType<List<int>>(actual);
		Assert.NotEmpty(actual);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_ToList_ShouldReturn_EmptyList()
	{
		// Arrange
		var op = None<int>();
		var expected = Array.Empty<int>().AsEnumerable();

		// Act
		var actual = op.ToList();

		// Assert
		Assert.IsType<List<int>>(actual);
		Assert.Empty(actual);
		Assert.Equal(expected, actual);
	}
}
