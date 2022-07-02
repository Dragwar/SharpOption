namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(IsSome))]
public class IsSome
{
	[Fact]
	public void Some_IsSome_ShouldReturn_True()
	{
		// Arrange
		var op = 10.Some();

		// Act
#if OPTION_GENERIC_TESTS
		var actual = Option.IsSome(op);
#elif VALUEOPTION_GENERIC_TESTS
		var actual = ValueOption.ValueOption.IsSome(op);
#endif
		// Assert
		Assert.True(actual);
	}

	[Fact]
	public void None_IsSome_ShouldReturn_False()
	{
		// Arrange
		var op = None<int>();

		// Act
#if OPTION_GENERIC_TESTS
		var actual = Option.IsSome(op);
#elif VALUEOPTION_GENERIC_TESTS
		var actual = ValueOption.ValueOption.IsSome(op);
#endif

		// Assert
		Assert.False(actual);
	}
}
