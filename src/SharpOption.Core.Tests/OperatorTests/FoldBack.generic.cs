namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(FoldBack))]
public class FoldBack
{
	[Fact]
	public void Some_FoldBack_FolderExecuted_ShouldReturn_NewState()
	{
		// Arrange
		var op = "llo".Some();
		var expected = "Hello";

		// Act
		var actual = op.FoldBack("He", (str, state) => state + str);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_FoldBack_FolderNotExecuted_ShouldReturn_InitialState()
	{
		// Arrange
		var op = None<string>();
		var expected = "He";

		// Act
		var actual = op.FoldBack("He", (str, state) => state + str);

		// Assert
		Assert.Equal(expected, actual);
	}
}