namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Flatten))]
public class Flatten
{
	[Fact]
	public void Some_Some_Flatten_ShouldReturn_Some()
	{
		// Arrange
		var op = 10.Some().Some();
		var expected = 10.Some();

		// Act
		var actual = op.Flatten();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_None_Flatten_ShouldReturn_None()
	{
		// Arrange
#if OPTION_GENERIC_TESTS
		var op = None<Option<int>>();
#elif VALUEOPTION_GENERIC_TESTS
		var op = None<ValueOption.ValueOption<int>>();
#endif
		var expected = None<int>();

		// Act
		var actual = op.Flatten();

		// Assert
		Assert.Equal(expected, actual);
	}
}
