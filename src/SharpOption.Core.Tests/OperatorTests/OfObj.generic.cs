namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(OfObj))]
public class OfObj
{
	private record MyRecord(string FirstName, string LastName);

	[Fact]
	public void Obj_OfObj_ShouldReturn_Some()
	{
		// Arrange
		var obj = new MyRecord("John", "Doe");
		var expected = new MyRecord("John", "Doe").Some();

		// Act
		var actual = obj.OfObj();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void Null_OfObj_ShouldReturn_None()
	{
		// Arrange
		var expected = None<MyRecord>();
		var obj = null as MyRecord;

		// Act
		var actual = obj.OfObj();

		// Assert
		Assert.Equal(expected, actual);
	}
}
