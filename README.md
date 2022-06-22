![Nuget](https://img.shields.io/nuget/v/SharpOption.Core?style=for-the-badge)

# SharpOption
C# representation of a optional value, heavily inspired by F#'s option module and type.


## Usage

add a using for `SharpOption.Core` to start using the `Option<T>` type.

or you can use `SharpOption.Core.ValueOption` to use `ValueOption<T>` value type instead.


### Simple usage
```C#
using SharpOption.Core;

//...

Option<int> myOption = Option.Some(31);
if (myOption.IsSome) // also has myOption.IsNone property
{
    Console.WriteLine($"my option has Some {myOption.Value} value.");
}
else
{
    Console.WriteLine("my option had a None value.");
}

// instead of if syntax, you can use the .Match(...) method
myOption.Match(
    value => Console.WriteLine($"my option has Some {value} value."),
    () => Console.WriteLine("my option had a None value."),
);

// .Match(...) also supports return
string message = myOption.Match(
    value => $"my option has Some {value} value.",
    () => "my option had a None value.",
);
Console.WriteLine(message);

Option<string> nameOption = Option.None<string>();
string name = Option.DefaultValue(nameOption, "System user");
```

### Simple usage cont.
You can also make use of the `using static` syntax to reduce the use of `Option.<method>` throughout your code.
```C#
using static SharpOption.Core.Option;

//...

Option<int> myOption1 = Some(31);
Option<int> myOption2 = None<int>();

Option<string> nameOption = None<string>();
Option<string> name = DefaultValue(nameOption, "System user");
```

### Extension method usage
You can start accessing the extension methods by adding a using to `SharpOption.Core.Extensions`.
```C#
using SharpOption.Core.Extensions;

//...

Option<int> myNum = 52.Some();
int? myNullableNum = myNum.ToNullable(); // 52
myNullableNum = null;
Option<int> myNumOfNullable = myNullableNum.OfNullable() // None
```


### F# like usage
See the static class called `Option`, this class contains F# like [option functions](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html).

```C#
using SharpOption.Core.Option;

//...

Option<int> myNum1 = Option.Some(52);
Option<int> myNum2 = Option.Some(3);
Option<int> myNumMap = Option.Map(myNum1, myNum2, (n1, n2) => n1 + n2);
Option<int> myNumBind = Option.Bind(myNum1, myNum2, (n1, n2) => Option.Some(n1 + n2));

// please see the Option static class for all methods...
```

### Query syntax
To start using the linq query syntax add a using for `SharpOption.Core.Monodic`.

```C#
using SharpOption.Core.Monodic;

Option<int> op1 =
    from first in Option.Some(43)
    where first > 14
    select first; // will return Some(43)

Option<int> op2 =
    from first in Option.Some(43)
    where first > 50
    select first; // will return None

Option<int> op3 =
    from first in Option.Some(10)
    from second in Option.Some(2)
    select first + second; // will return Some(12)

Option<int> op4 =
    from first in Option.Some(10)
    from second in Option.None<int>()
    select first + second; // will return None

// also supports Task
async Task<Option<int>> GetOptionFromDbAsync(int? value)
{
    await Task.Delay(0);
    Option<int> myOptionFromDb = Option.OfNullable(value);
    return myOptionFromDb;
}

Option<int> op5 = await (
    from first in GetOptionFromDbAsync(15)
    from second in GetOptionFromDbAsync(10)
    select first + second
); // will return Some(25)

Option<int> op6 = await (
    from first in GetOptionFromDbAsync(15)
    from second in GetOptionFromDbAsync(null)
    select first + second
); // will return None
```

## Todo
- XML documentation for all public APIs.
- Unit tests for static classes `Option` / `ValueOption`.
- Add more linq query operator/keyword support.
