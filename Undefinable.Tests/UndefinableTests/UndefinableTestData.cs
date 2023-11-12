namespace Undefinable.Tests.UndefinableTests;

public abstract partial class UndefinableTestData
{
    protected static readonly Dictionary<Type, object> TypeTestData = new();
    private static void AddTestData<T>(params T[] data) => TypeTestData[typeof(T)] = data;
}
