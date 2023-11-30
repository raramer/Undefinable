namespace Undefinable.Tests.ReadmeExamples;

public class ReadmeExamples
{
    // As a variable with an undefined value
    private void VariableUndefinedValue()
    {
        Undefinable<string> myString;
        myString = default;
        myString = new Undefinable<string>();
        myString = default(Undefinable<string>);
        myString = Undefinable<string>.Undefined;
        myString = Undefined.String; // available for some well-known types
    }

    // As a variable with an assigned value
    private void VariableAssignedValue()
    {
        Undefinable<string> myString;
        myString = "my string";
        myString = null;
    }

    // As a method parameter
    private void DoStuff(Undefinable<int> myInt)
    {
    }

    private void Test_DoStuff_1()
    {
        // undefined
        DoStuff(default);
        DoStuff(new Undefinable<int>());
        DoStuff(new());
        DoStuff(Undefinable<int>.Undefined);
        DoStuff(Undefined.Int);

        // defined
        DoStuff(123);
    }

    // As an optional parameter
    private void DoStuff_2(Undefinable<int> myInt = default)
    {
    }

    private void DoStuff_3(Undefinable<int> myInt = new())
    {
    }

    private void DoStuff_4(Undefinable<int> myInt = new Undefinable<int>())
    {
    }

    private void Test_DoStuff_234()
    {
        // undefined
        DoStuff_2();
        DoStuff_2(default);
        DoStuff_2(new Undefinable<int>());
        DoStuff_2(new());
        DoStuff_2(Undefinable<int>.Undefined);
        DoStuff_2(Undefined.Int);

        // defined
        DoStuff_2(123);

        // undefined
        DoStuff_3();
        DoStuff_3(default);
        DoStuff_3(new Undefinable<int>());
        DoStuff_3(new());
        DoStuff_3(Undefinable<int>.Undefined);
        DoStuff_3(Undefined.Int);

        // defined
        DoStuff_3(123);

        // undefined
        DoStuff_4();
        DoStuff_4(default);
        DoStuff_4(new Undefinable<int>());
        DoStuff_4(new());
        DoStuff_4(Undefinable<int>.Undefined);
        DoStuff_4(Undefined.Int);

        // defined
        DoStuff_4(123);
    }
}