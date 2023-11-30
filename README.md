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

### Check *IsDefined* Property

* returns true when a value is assigned

    ```csharp
    Undefinable<string> myString = "my string";
    var isDefined = myString.IsDefined;

    // isDefined => true
    ```

* returns false when no value is assigned

    ```csharp
    Undefinable<string> myString = default;
    var isDefined = myString.IsDefined;

    // isDefined => false 
    ```

### Get *Value* Property

* **When *IsDefined* = true**, the assigned value will be returned.

    ```csharp
    Undefinable<string> myString = "my string";
    var value = myString.Value;
    
    // value => "my string"
    ``` 

* **When *IsDefined* = false**, an `InvalidOperationException` will be thrown.

    ```csharp
    Undefinable<string> myString = default;
    var value = myString.Value; // throws an InvalidOperationException 
    ``` 

### Calling ToString() Method

* **When *IsDefined* = true**, the assigned value's ToString() result will be returned.  If null, then null will be returned.

    ```csharp
    Undefinable<string> myString = "my string";
    var toString = myString.ToString();
        
    // toString => "my string"
    ``` 

* **When *IsDefined* = false**, the *ToString()* method will return "{undefined}".  
    
    ```csharp
    Undefinable<string> myString = default;
    var toString = myString.ToString();

    // toString => "<undefined>"
    ``` 

## Practical Example

This example tests a SetPassword api method.  Because only the test data varies, each test calls a helper method with the necessary parameters.  Undefined parameters are populated with a sensible value.

   ```csharp
   public class SetPasswordTests
   {
       [Fact]
       public void PasswordIsNull() => Test(password: null, expectedError: "password is required");
       [Fact]
       public void PasswordIsEmpty() => Test(password: string.Empty, expectedError: "password is invalid");
       [Fact]
       public void PasswordIsTooShort() => Test(password: "sh0rt.", expectedError: "password is too short");
       [Fact]
       public void PasswordIsTooWeak() => Test(password: "weakpassword", expectedError: "password is too weak");
       [Fact]
       public void PasswordIsStrong() => Test(password: RandomStrongPassword);
       [Fact]
       public void UserIdIsNull() => Test(userId: null, expectedError: "userId is required");
       [Fact]
       public void UserIdIsEmpty() => Test(userId: string.Empty, expectedError: "userId is invalid");
       [Fact]
       public void UserIdDoesNotExist() => Test(userId: Guid.NewGuid().ToString("n"), expectedError: "userId not found");

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
           if (!userId.IsDefined)
               userId = _api.CreateUser(RandomUsername, RandomStrongPassword).UserId;

           // if password is not defined, define a strong one
           if (!password.IsDefined)
               password = RandomStrongPassword;

           /* ACT */
           var setPasswordResult = _api.SetPassword(userId.Value, password.Value);

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