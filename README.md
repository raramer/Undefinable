# Undefinable
Represent a value as *undefined* instead of reserving a specific *assigned* value as "this is undefined".

## Where do I get it

Undefinable can be installed using the Nuget package manager 

```
PM> Install-Package Undefinable
```

or the dotnet CLI.

```
dotnet add package Undefinable
```

## Why?

We use the term "undefinable" because the value might be defined, similar to how a Nullable&lt;T&gt; might have a non-null value.  But unlike nullable, T is not restricted to "non-nullable value types", and allows for null as a defined value.

## How do I use it

*Some syntaxes may or may not be supported by your language version*

### Define an *Undefinable&lt;T&gt;*

* **As a variable with an undefined value**

    ```csharp
    Undefinable<string> myString;
    myString = default;
    myString = new Undefinable<string>();
    myString = default(Undefinable<string>);
    myString = Undefinable<string>.Undefined;
    myString = Undefined.String; // available for some well-known types
    ```

* **As a variable with an assigned value**

    ```csharp
    Undefinable<string> myString;
    myString = "my string";
    myString = null;
    ```

* **As a method parameter**

    ```csharp
    void DoStuff(Undefinable<int> myBool) 
    {
    }

    // undefined
    DoStuff(default);
    DoStuff(new Undefinable<int>());
    DoStuff(new());
    DoStuff(Undefinable<int>.Undefined);
    DoStuff(Undefined.Int);

    // defined
    DoStuff(123);
    ```

* **As an optional method parameter (various syntaxes)**
   
    If not specified by the caller, then defaults to undefined

    ```csharp
    void DoStuff(Undefinable<int> myInt = default)
    {
    }

    void DoStuff(Undefinable<int> myInt = new())
    {
    }

    void DoStuff(Undefinable<int> myInt = new Undefinable<int>())
    {
    }
    ```

### *IsDefined* Property

* returns true when a value is assigned

    ```csharp
    Undefinable<string> myString = "my string";
    var isDefined = myString.IsDefined; // true
    ```

* returns false when no value is assigned

    ```csharp
    Undefinable<string> myString = default;
    var isDefined = myString.IsDefined; // false
    ```

### *Value* Property / Implicit Conversion

* **When *IsDefined* = true**, the assigned value will be returned.

    ```csharp
    Undefinable<string> myString = "my string";
    var value = myString.Value; // "my string"
    string implicitValue = myString; // "my string"
    ``` 

* **When *IsDefined* = false**, an `InvalidOperationException` will be thrown.

    ```csharp
    Undefinable<string> myString = default;
    var value = myString.Value; // throws an InvalidOperationException 
    string implicitValue = myString; // throws an InvalidOperationException
    ``` 

### *GetValueOrDefault* Method
Retrieves the Value of the current _Undefinable&lt;T&gt;_ object, or the default value.

* **When *IsDefined* = true**, the assigned value will be returned.

    ```csharp
    Undefinable<string> myString = "my string";
    myString.GetValueOrDefault(); // "my string"
    myString.GetValueOrDefault("my default"); // "my string"
    myString.GetValueOrDefault(() => "my default"); // "my string"
    await myString.GetValueOrDefaultAsync(async () => Task.FromResult("my default")); // "my string"
    ``` 

* **When *IsDefined* = false**, the (specified) default value will be returned. 

    ```csharp
    Undefinable<string> myString = default;
    myString.GetValueOrDefault(); // null
    myString.GetValueOrDefault("my default"); // "my default"
    myString.GetValueOrDefault(() => "my default"); // "my default"
    await myString.GetValueOrDefaultAsync(async () => Task.FromResult("my default")); // "my default"
    ``` 

### *ToString* Method

* **When *IsDefined* = true**, the assigned value's ToString() result will be returned.  If null, then null will be returned.

    ```csharp
    Undefinable<string> myString = "my string";
    myString.ToString(); // "my string"
    ``` 

* **When *IsDefined* = false**, the *ToString()* method will return "&lt;undefined&gt;".  
    
    ```csharp
    Undefinable<string> myString = default;
    myString.ToString(); // "<undefined>"
    ``` 

## Practical Example

This example tests a SetPassword api method.  Because only the test data varies, each test calls a helper method with the necessary parameters.  Undefined parameters are populated with a sensible value.

   ```csharp
   public class SetPasswordTests
   {
       [Fact]
       public void Password_IsNull() => Test(password: null, expectedError: "password is required");

       [Fact]
       public void Password_IsEmpty() => Test(password: string.Empty, expectedError: "password is invalid");

       [Fact]
       public void Password_IsTooShort() => Test(password: "Sh0rt!", expectedError: "password is too short");

       [Fact]
       public void Password_IsTooWeak() => Test(password: "weakpassword", expectedError: "password is too weak");

       [Fact]
       public void Password_IsStrong() => Test(password: RandomStrongPassword);

       [Fact]
       public void UserId_IsNull() => Test(userId: null, expectedError: "userId is required");

       [Fact]
       public void UserId_IsEmpty() => Test(userId: string.Empty, expectedError: "userId is invalid");

       [Fact]
       public void UserId_DoesNotExist() => Test(userId: "does not exist", expectedError: "userId not found");

       private IApi _api = new Api();
       private string RandomUsername => "user" + DateTime.Now.Ticks;
       private string RandomStrongPassword => Path.GetRandomFileName() + "A1a";

       private void Test(
           Undefinable<string> userId = default, 
           Undefinable<string> password = default,
           Undefinable<string> expectedError = default)
       {
           /* ARRANGE */
           // if userId is not defined, create a new user
           userId = userId.GetValueOrDefault(() => _api.CreateUser(RandomUsername, RandomStrongPassword).UserId);

           // if password is not defined, define a strong one
           password = password.GetValueOrDefault(RandomStrongPassword);

           /* ACT */
           var setPasswordResult = _api.SetPassword(userId, password);

           /* ASSERT */
           if (expectedError.IsDefined)
           {
               // check for error
               Assert.False(setPasswordResult.Success);
               Assert.Equal(expectedError, setPasswordResult.Error);
           }
           else
           {
               // check for success
               Assert.True(setPasswordResult.Success);
           }
       }
   }
   ```

## Serialization

There currently isn't a great story for serialization without adding dependencies on third-party nuget packages. For now, avoid using an Undefinable&lt;T&gt; in situations where it might be serialized.