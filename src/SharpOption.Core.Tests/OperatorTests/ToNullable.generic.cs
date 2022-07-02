namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(ToNullable))]
public class ToNullable
{
	private record struct MyRecordStruct(string FirstName, string LastName);

	[Fact]
	public void Some_ToNullable_ShouldReturn_Value()
	{
		// Arrange
		var op = new MyRecordStruct("John", "Doe").Some();
		var expected = new MyRecordStruct("John", "Doe") as MyRecordStruct?;

		// Act
		var actual = op.ToNullable();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_ToNullable_ShouldReturn_Null()
	{
		// Arrange
		var op = None<MyRecordStruct>();
		var expected = null as MyRecordStruct?;

		// Act
		var actual = op.ToNullable();

		// Assert
		Assert.Equal(expected, actual);
	}
}
