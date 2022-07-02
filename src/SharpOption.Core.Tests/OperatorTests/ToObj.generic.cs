namespace SharpOption.Core.Tests.OperatorTests;

[Trait(CATEGORY, OPERATOR)]
[Trait(OPERATOR, nameof(ToObj))]
public class ToObj
{
	private record MyRecord(string FirstName, string LastName);

	[Fact]
	public void Some_ToObj_ShouldReturn_Obj()
	{
		// Arrange
		var op = new MyRecord("John", "Doe").Some();
		var expected = new MyRecord("John", "Doe");

		// Act
		var actual = op.ToObj();

		// Assert
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void None_ToObj_ShouldReturn_Null()
	{
		// Arrange
		var op = None<MyRecord>();
		var expected = default(MyRecord);

		// Act
		var actual = op.ToObj();

		// Assert
		Assert.Equal(expected, actual);
	}
}
