using SharpOption.Core.Monodic;
using static SharpOption.Core.Option;

namespace SharpOption.Core.Tests;
public class MonadicExtensionTests
{
	[Fact]
	public void Select_Operator()
	{
		// Arrange
		var op1 = Some(5);

		// Act
		var op =
			from first in op1
			select first;

		// Assert
		Assert.True(op.IsSome);
		Assert.Equal(5, op.Value);
	}

	[Fact]
	public void Select_Operator_Return_Another_Option()
	{
		// Arrange
		var op1 = Some(1);

		// Act
		var op =
			from first in op1
			select Some(first + 2);

		// Assert
		Assert.Equal(3, op.Value);
	}

	[Fact]
	public void Select_Into_Operator()
	{
		// Arrange
		var op1 = Some(5);
		var op2 = Some(1);

		// Act
		var op =
			from first in op1
			select first into newFirst  // Can be used to separate variables into scopes (only newFirst is visible to the clauses below)
			from second in op2          // Also if there were any variables above this line, they won't be in the next clause' scope
			select second + newFirst;

		// Assert
		Assert.True(op.IsSome);
		Assert.Equal(6, op.Value);
	}

	[Fact]
	public void Where_Operator_True_Condition()
	{
		// Arrange
		var op1 = Some(5);

		// Act
		var op =
			from first in op1
			where first > 1
			select first;

		// Assert
		Assert.True(op.IsSome);
		Assert.Equal(5, op.Value);
	}

	[Fact]
	public void Where_Operator_False_Condition()
	{
		// Arrange
		var op1 = Some(1);

		// Act
		var op =
			from first in op1
			where first > 1
			select first;

		// Assert
		Assert.True(op.IsNone);
	}

	[Fact]
	public void Where_Operator_None()
	{
		// Arrange
		var op1 = None<int>();

		// Act
		var op =
			from first in op1
			where first > 1
			select first;

		// Assert
		Assert.True(op.IsNone);
	}

	[Fact]
	public void SelectMany_Operator_Two_Levels()
	{
		// Arrange
		var op1 = Some(1);
		var op2 = Some(2);

		// Act
		var op =
			from first in op1
			from second in op2
			select first + second;

		// Assert
		Assert.Equal(3, op.Value);
	}

	[Fact]
	public void SelectMany_Into_Operator()
	{
		// Arrange
		var op1 = Some(5);
		var op2 = Some(1);
		var op3 = Some(2);

		// Act
		var op =
			from first in op1
			from second in op2
			select first + second into firstResult  // Can be used to separate variables into scopes (only newFirst is visible to the clauses below)
			from first in op3                       // Also if there were any variables above this line, they won't be in the next clause' scope
			select first + firstResult;

		// Assert
		Assert.True(op.IsSome);
		Assert.Equal(8, op.Value);
	}
}
