using static SharpOption.Core.Option;

namespace SharpOption.Core.Tests;

public class SimpleOptionComparisonTests
{
	[Theory]
	[InlineData(new object[] { 1, 1 })]
	[InlineData(new object[] { -18, -18 })]
	[InlineData(new object[] { 1, 2 })]
	[InlineData(new object[] { -4, 43 })]
	[InlineData(new object[] { -4, null! })]
	[InlineData(new object[] { null!, 30 })]
	[InlineData(new object[] { null!, null! })]
	public void Option_To_Option_Equality(int? op1Value, int? op2Value)
	{
		// Arrange
		var expectedToBeEqual = op1Value == op2Value;

		// Act
		var op1 = op1Value.HasValue ? Some(op1Value.Value) : None<int>();
		var op2 = op2Value.HasValue ? Some(op2Value.Value) : None<int>();

		// Assert
		if (expectedToBeEqual)
		{
			Assert.True(op1.Equals(op2));
		}
		else
		{
			Assert.False(op1.Equals(op2));
		}
	}

	[Theory]
	[InlineData(new object[] { 1, ">", 1, false })]
	[InlineData(new object[] { 1, ">=", 1, true })]
	[InlineData(new object[] { 1, "<", 1, false })]
	[InlineData(new object[] { 1, "<=", 1, true })]
	[InlineData(new object[] { 1, "==", 1, true })]
	[InlineData(new object[] { 1, "!=", 1, false })]
	[InlineData(new object[] { null!, ">", 1, false })]
	[InlineData(new object[] { null!, ">=", 1, false })]
	[InlineData(new object[] { null!, "<", 1, true })]
	[InlineData(new object[] { null!, "<=", 1, true })]
	[InlineData(new object[] { null!, "==", 1, false })]
	[InlineData(new object[] { null!, "!=", 1, true })]
	[InlineData(new object[] { 1, ">", null!, true })]
	[InlineData(new object[] { 1, ">=", null!, true })]
	[InlineData(new object[] { 1, "<", null!, false })]
	[InlineData(new object[] { 1, "<=", null!, false })]
	[InlineData(new object[] { 1, "==", null!, false })]
	[InlineData(new object[] { 1, "!=", null!, true })]
	public void Option_To_Option_Comparison(int? op1Value, string op, int? op2Value, bool expected)
	{
		// Arrange
		var op1 = op1Value.HasValue ? Some(op1Value.Value) : None<int>();
		var op2 = op2Value.HasValue ? Some(op2Value.Value) : None<int>();

		// Act
		var actual = op switch
		{
			">" => op1 > op2,
			">=" => op1 >= op2,
			"<" => op1 < op2,
			"<=" => op1 <= op2,
			"==" => op1 == op2,
			"!=" => op1 != op2,
			_ => throw new Exception("Invalid op in test data."),
		};

		// Assert
		if (expected)
		{
			Assert.True(actual);
		}
		else
		{
			Assert.False(actual);
		}
	}

	[Fact]
	public void Option_To_Option_ClassObject_Eqaulity()
	{
		var a = new MyClass() { Id = 1, Name = "test1" };
		var b = new MyClass() { Id = 2, Name = "test2" };
		var c = new MyClass() { Id = 1, Name = "test1" };

		var op1 = Some(a);
		var op2 = Some(a);

		Assert.True(op1 == op2);

		var op3 = Some(a);
		var op4 = Some(b);

		Assert.False(op3 == op4);

		var op5 = Some(a);
		var op6 = Some(c);

		Assert.False(op5 == op6);
	}

	[Fact]
	public void Option_To_Option_ClassRecord_Eqaulity()
	{
		var a = new MyClassRecord() { Id = 1, Name = "test1" };
		var b = new MyClassRecord() { Id = 2, Name = "test2" };
		var c = new MyClassRecord() { Id = 1, Name = "test1" };

		var op1 = Some(a);
		var op2 = Some(a);

		Assert.True(op1 == op2);

		var op3 = Some(a);
		var op4 = Some(b);

		Assert.False(op3 == op4);

		var op5 = Some(a);
		var op6 = Some(c);

		Assert.True(op5 == op6);
	}

	[Fact]
	public void Option_To_Option_MyStructValue_Eqaulity()
	{
		var a = new MyStruct() { Id = 1, Name = "test1" };
		var b = new MyStruct() { Id = 2, Name = "test2" };
		var c = new MyStruct() { Id = 1, Name = "test1" };

		var op1 = Some(a);
		var op2 = Some(a);

		Assert.True(op1 == op2);

		var op3 = Some(a);
		var op4 = Some(b);

		Assert.False(op3 == op4);

		var op5 = Some(a);
		var op6 = Some(c);

		Assert.True(op5 == op6);
	}

	[Fact]
	public void Option_To_Option_MyStructRecord_Eqaulity()
	{
		var a = new MyStructRecord() { Id = 1, Name = "test1" };
		var b = new MyStructRecord() { Id = 2, Name = "test2" };
		var c = new MyStructRecord() { Id = 1, Name = "test1" };

		var op1 = Some(a);
		var op2 = Some(a);

		Assert.True(op1 == op2);

		var op3 = Some(a);
		var op4 = Some(b);

		Assert.False(op3 == op4);

		var op5 = Some(a);
		var op6 = Some(c);

		Assert.True(op5 == op6);
	}

	private class MyClass
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}

	private record MyClassRecord
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}

	private struct MyStruct
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}

	private record struct MyStructRecord
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
