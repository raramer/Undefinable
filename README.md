# Undefinable
Represent a value as *undefined* instead of reserving a specific *assigned* value as "this is undefined".

We use the term "undefinable" because the value *might* be defined, similar to how a Nullable&lt;T&gt; might have a non-null value.  But unlike nullable, T is not restricted to "non-nullable value types".

Why not just use null?  In many cases, null is a valid assignable value.

## Where do I get it

Undefinable can be installed using the Nuget package manager 

```
PM> Install-Package Undefinable
```

or the dotnet CLI.

```
dotnet add package Undefinable
```

## How do I use it

### Define an *Undefinable&lt;T&gt;*

* **As a variable with an undefined value**

    *Some syntaxes may or may not be supported by your language version*

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
    void AsParameter(Undefinable<bool> myBool) 
    {
    }
    ```

* **As an optional method parameter**

    *Some syntaxes may or may not be supported by your language version*

    ```csharp
    void DoStuff(Undefinable<bool> myBool = default)
    { 
        // if not specified by caller, then defaults to an undefined value
    }

    void DoStuff(Undefinable<bool> myBool = new()) 
    {
        // if not specified by caller, then defaults to an undefined value
    }

    void DoStuff(Undefinable<bool> myBool = new Undefinable<bool>()) 
    { 
        // if not specified by caller, then defaults to an undefined value
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

TO DO