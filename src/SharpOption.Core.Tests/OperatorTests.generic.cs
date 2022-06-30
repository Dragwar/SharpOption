namespace SharpOption.Core.Tests;

public class OperatorTests
{
	public class Get
	{
		[Fact]
		public void Some_Get_ShouldReturn_Value()
		{
			// Arrange
			var op = 10.Some();
			var expected = 10;

			// Act
			var actual = op.Get();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_Get_ShouldThrow_ArgumentException()
		{
			// Arrange
			var op = None<int>();

			// Act
			// Assert
			Assert.Throws<ArgumentException>(() => op.Get());
		}
	}

	public class Bind
	{
		[Fact]
		public void Some_Bind_ShouldReturn_Some()
		{
			// Arrange
			var op = 10.Some();
			var expected = 11.Some();

			// Act
			var actual = op.Bind(n => (n + 1).Some());

			// Assert
			Assert.True(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Some_Bind_ShouldReturn_None()
		{
			// Arrange
			var op = 10.Some();
			var expected = None<int>();

			// Act
			var actual = op.Bind(_ => None<int>());

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}
	}

	public class Map
	{
		[Fact]
		public void Some_Map_ShouldReturn_Some()
		{
			// Arrange
			var op = 10.Some();
			var expected = 11.Some();

			// Act
			var actual = op.Map(n => n + 1);

			// Assert
			Assert.True(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_Map_ShouldReturn_None()
		{
			// Arrange
			var op = None<int>();
			var expected = None<int>();

			// Act
			var actual = op.Map(n => n + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Some_Some_Map2_ShouldReturn_Some()
		{
			// Arrange
			var op1 = 10.Some();
			var op2 = 10.Some();
			var expected = 21.Some();

			// Act
			var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

			// Assert
			Assert.True(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_Some_Map2_ShouldReturn_None()
		{
			// Arrange
			var op1 = None<int>();
			var op2 = 10.Some();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Some_None_Map2_ShouldReturn_None()
		{
			// Arrange
			var op1 = 10.Some();
			var op2 = None<int>();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_None_Map2_ShouldReturn_None()
		{
			// Arrange
			var op1 = None<int>();
			var op2 = None<int>();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, (n1, n2) => n1 + n2 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Some_Some_Some_Map3_ShouldReturn_Some()
		{
			// Arrange
			var op1 = 10.Some();
			var op2 = 10.Some();
			var op3 = 10.Some();
			var expected = 31.Some();

			// Act
			var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

			// Assert
			Assert.True(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Some_Some_None_Map3_ShouldReturn_None()
		{
			// Arrange
			var op1 = 10.Some();
			var op2 = 10.Some();
			var op3 = None<int>();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Some_None_Some_Map3_ShouldReturn_None()
		{
			// Arrange
			var op1 = 10.Some();
			var op2 = None<int>();
			var op3 = 10.Some();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_Some_Some_Map3_ShouldReturn_None()
		{
			// Arrange
			var op1 = None<int>();
			var op2 = 10.Some();
			var op3 = 10.Some();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_None_Some_Map3_ShouldReturn_None()
		{
			// Arrange
			var op1 = None<int>();
			var op2 = None<int>();
			var op3 = 10.Some();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_None_None_Map3_ShouldReturn_None()
		{
			// Arrange
			var op1 = None<int>();
			var op2 = None<int>();
			var op3 = None<int>();
			var expected = None<int>();

			// Act
			var actual = op1.Map(op2, op3, (n1, n2, n3) => n1 + n2 + n3 + 1);

			// Assert
			Assert.False(actual.IsSome);
			Assert.Equal(expected, actual);
		}
	}

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

	public class DefaultValue
	{
		[Fact]
		public void Some_DefaultValue_ShouldReturn_Value()
		{
			// Arrange
			var op = 10.Some();
			var expected = 10;

			// Act
			var actual = op.DefaultValue(-1);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_DefaultValue_ShouldReturn_Default()
		{
			// Arrange
			var op = None<int>();
			var expected = 10;

			// Act
			var actual = op.DefaultValue(10);

			// Assert
			Assert.Equal(expected, actual);
		}
	}

	public class DefaultWith
	{
		[Fact]
		public void Some_DefaultWith_ShouldReturn_Value()
		{
			// Arrange
			var op = 10.Some();
			var expected = 10;

			// Act
			var actual = op.DefaultWith(() => -1);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void None_DefaultWith_ShouldReturn_Default()
		{
			// Arrange
			var op = None<int>();
			var expected = 10;

			// Act
			var actual = op.DefaultWith(() => 10);

			// Assert
			Assert.Equal(expected, actual);
		}
	}

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
}
