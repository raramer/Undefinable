namespace Undefinable.Tests.UndefinableTests;

partial class UndefinableTestData
{
    static UndefinableTestData()
    {
        AddTestData<bool>(false, true);
        AddTestData<byte>(0, 1);
        AddTestData<char>('x', 'y');
        AddTestData<DateTime>(DateTime.MinValue, DateTime.Now, DateTime.MaxValue);
        AddTestData<DateTimeOffset>(DateTimeOffset.MinValue, DateTimeOffset.Now, DateTimeOffset.MaxValue);
        AddTestData<decimal>(-1, 0, 1);
        AddTestData<double>(-1, 0, 1);
        AddTestData<UriKind>(UriKind.Absolute, UriKind.Relative);
        AddTestData<float>(-1, 0, 1);
        AddTestData<Guid>(Guid.Empty, Guid.NewGuid());
        AddTestData<int>(-1, 0, 1);
        AddTestData<long>(-1, 0, 1);
        AddTestData<object>(null, true, -1, 0ul, 1.1, "string", new object());
        AddTestData<sbyte>(-1, 0, 1);
        AddTestData<short>(-1, 0, 1);
        AddTestData<string>(null, "", " ", "x", "y");
        AddTestData<TimeSpan>(TimeSpan.MinValue, TimeSpan.Zero, TimeSpan.MaxValue);
        AddTestData<uint>(0, 1);
        AddTestData<ulong>(0, 1);
        AddTestData<ushort>(0, 1);

        AddTestData<Nullable<bool>>(null, false, true);
        AddTestData<Nullable<byte>>(null, 0, 1);
        AddTestData<Nullable<char>>(null, 'x', 'y');
        AddTestData<Nullable<DateTime>>(null, DateTime.MinValue, DateTime.Now, DateTime.MaxValue);
        AddTestData<Nullable<DateTimeOffset>>(null, DateTimeOffset.MinValue, DateTimeOffset.Now, DateTimeOffset.MaxValue);
        AddTestData<Nullable<decimal>>(null, -1, 0, 1);
        AddTestData<Nullable<double>>(null, -1, 0, 1);
        AddTestData<Nullable<UriKind>>(null, UriKind.Absolute, UriKind.Relative);
        AddTestData<Nullable<float>>(null, -1, 0, 1);
        AddTestData<Nullable<Guid>>(null, Guid.Empty, Guid.NewGuid());
        AddTestData<Nullable<int>>(null, -1, 0, 1);
        AddTestData<Nullable<long>>(null, -1, 0, 1);
        AddTestData<Nullable<sbyte>>(null, -1, 0, 1);
        AddTestData<Nullable<short>>(null, -1, 0, 1);
        AddTestData<Nullable<TimeSpan>>(null, TimeSpan.MinValue, TimeSpan.Zero, TimeSpan.MaxValue);
        AddTestData<Nullable<uint>>(null, 0, 1);
        AddTestData<Nullable<ulong>>(null, 0, 1);
        AddTestData<Nullable<ushort>>(null, 0, 1);
    }
}

public class Undefinable_Bool_Tests : UndefinableTestBase<bool> { }
public class Undefinable_Byte_Tests : UndefinableTestBase<byte> { }
public class Undefinable_Char_Tests : UndefinableTestBase<char> { }
public class Undefinable_DateTime_Tests : UndefinableTestBase<DateTime> { }
public class Undefinable_DateTimeOffset_Tests : UndefinableTestBase<DateTimeOffset> { }
public class Undefinable_Decimal_Tests : UndefinableTestBase<decimal> { }
public class Undefinable_Double_Tests : UndefinableTestBase<double> { }
public class Undefinable_Dynamic_Tests : UndefinableTestBase<dynamic> { }
public class Undefinable_Enum_Tests : UndefinableTestBase<UriKind> { }
public class Undefinable_Float_Tests : UndefinableTestBase<float> { }
public class Undefinable_Guid_Tests : UndefinableTestBase<Guid> { }
public class Undefinable_Int_Tests : UndefinableTestBase<int> { }
public class Undefinable_Long_Tests : UndefinableTestBase<long> { }
public class Undefinable_Object_Tests : UndefinableTestBase<object> { }
public class Undefinable_Sbyte_Tests : UndefinableTestBase<sbyte> { }
public class Undefinable_Short_Tests : UndefinableTestBase<short> { }
public class Undefinable_String_Tests : UndefinableTestBase<string> { }
public class Undefinable_TimeSpan_Tests : UndefinableTestBase<TimeSpan> { }
public class Undefinable_Uint_Tests : UndefinableTestBase<uint> { }
public class Undefinable_Ulong_Tests : UndefinableTestBase<ulong> { }
public class Undefinable_UShort_Tests : UndefinableTestBase<ushort> { }

public class Undefinable_Nullable_Bool_Tests : UndefinableTestBase<bool?> { }
public class Undefinable_Nullable_Byte_Tests : UndefinableTestBase<byte?> { }
public class Undefinable_Nullable_Char_Tests : UndefinableTestBase<char?> { }
public class Undefinable_Nullable_DateTime_Tests : UndefinableTestBase<DateTime?> { }
public class Undefinable_Nullable_DateTimeOffset_Tests : UndefinableTestBase<DateTimeOffset?> { }
public class Undefinable_Nullable_Decimal_Tests : UndefinableTestBase<decimal?> { }
public class Undefinable_Nullable_Double_Tests : UndefinableTestBase<double?> { }
public class Undefinable_Nullable_Enum_Tests : UndefinableTestBase<UriKind?> { }
public class Undefinable_Nullable_Float_Tests : UndefinableTestBase<float?> { }
public class Undefinable_Nullable_Guid_Tests : UndefinableTestBase<Guid?> { }
public class Undefinable_Nullable_Int_Tests : UndefinableTestBase<int?> { }
public class Undefinable_Nullable_Long_Tests : UndefinableTestBase<long?> { }
public class Undefinable_Nullable_Sbyte_Tests : UndefinableTestBase<sbyte?> { }
public class Undefinable_Nullable_Short_Tests : UndefinableTestBase<short?> { }
public class Undefinable_Nullable_TimeSpan_Tests : UndefinableTestBase<TimeSpan?> { }
public class Undefinable_Nullable_Uint_Tests : UndefinableTestBase<uint?> { }
public class Undefinable_Nullable_Ulong_Tests : UndefinableTestBase<ulong?> { }
public class Undefinable_Nullable_UShort_Tests : UndefinableTestBase<ushort?> { }
