namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(IsNone))]
public class IsNone
{
	[Fact]
	public void Some_IsNone_ShouldReturn_False()
	{
		// Arrange
		var op = 10.Some();

		// Act
#if OPTION_GENERIC_TESTS
		var actual = Option.IsNone(op);
#elif VALUEOPTION_GENERIC_TESTS
		var actual = ValueOption.ValueOption.IsNone(op);
#endif

		// Assert
		Assert.False(actual);
	}

	[Fact]
	public void None_IsNone_ShouldReturn_True()
	{
		// Arrange
		var op = None<int>();

		// Act
#if OPTION_GENERIC_TESTS
		var actual = Option.IsNone(op);
#elif VALUEOPTION_GENERIC_TESTS
		var actual = ValueOption.ValueOption.IsNone(op);
#endif

		// Assert
		Assert.True(actual);
	}
}
