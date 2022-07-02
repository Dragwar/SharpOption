namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(OfNullable))]
public class OfNullable
{
	private record struct MyRecordStruct(string FirstName, string LastName);

	[Fact]
	public void Value_OfNullable_ShouldReturn_Some()
	{
		// Arrange
		var value = new MyRecordStruct("John", "Doe") as MyRecordStruct?;
		var expected = new MyRecordStruct("John", "Doe").Some();

		// Act
		var actual = value.OfNullable();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Null_OfNullable_ShouldReturn_None()
	{
		// Arrange
		var value = null as MyRecordStruct?;
		var expected = None<MyRecordStruct>();

		// Act
		var actual = value.OfNullable();

		// Assert
		Assert.Equal(expected, actual);
	}
}
