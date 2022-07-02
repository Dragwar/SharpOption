namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(Fold))]
public class Fold
{
	[Fact]
	public void Some_Fold_FolderExecuted_ShouldReturn_NewState()
	{
		// Arrange
		var op = "llo".Some();
		var expected = "Hello";

		// Act
		var actual = op.Fold("He", (state, str) => state + str);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_Fold_FolderNotExecuted_ShouldReturn_InitialState()
	{
		// Arrange
		var op = None<string>();
		var expected = "He";

		// Act
		var actual = op.Fold("He", (state, str) => state + str);

		// Assert
		Assert.Equal(expected, actual);
	}
}
