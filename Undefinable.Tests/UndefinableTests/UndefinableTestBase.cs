namespace Undefinable.Tests.UndefinableTests
{
    public abstract class UndefinableTestBase<TValue> : UndefinableTestData
    {
        public static IEnumerable<object?[]> AsImplicitParameter_TestData => GetImplicitParameterTestData<TValue>();
        public static IEnumerable<object?[]> AsUndefinableParameter_TestData => GetUndefinableParameterTestData<TValue>();
        public static IEnumerable<object?[]> IsDefined_TestData => GetIsDefinedTestData<TValue>();

        [Theory]
        [InlineData(null)]
        public void NullObject(Undefinable<TValue> value)
        {
            Assert.False(value.IsDefined);
        }

        [Theory]
        [MemberData(nameof(AsImplicitParameter_TestData))]
        public void AsImplicitParameter(TValue value) => AsUndefinableParameter(value, true, value);

        [Theory]
        [MemberData(nameof(AsUndefinableParameter_TestData))]
        public void AsUndefinableParameter(Undefinable<TValue> value, bool expectedIsDefined, TValue expectedValue)
        {
            Assert.Equal(expectedIsDefined, value.IsDefined);

            if (value.IsDefined)
                Assert.Equal(expectedValue, value.Value);
        }

        [Theory]
        [MemberData(nameof(IsDefined_TestData))]
        public void IsDefined(TValue value, TValue unequalValue)
        {
            // arrange
            Undefinable<TValue> undefinable = new Undefinable<TValue>(value);
            Undefinable<TValue> equalUndefinable = value;
            Undefinable<TValue> unequalUndefinable = unequalValue;
            Undefinable<TValue> undefined = Undefinable<TValue>.Undefined;

            // assert IsDefined
            Assert.True(undefinable.IsDefined);

            // assert Value
            Assert.Equal(value, undefinable.Value);

            // assert Equals
            Assert.True(undefinable.Equals(equalUndefinable));
            Assert.False(undefinable.Equals(unequalUndefinable));
            Assert.False(undefinable.Equals(undefined));

            // assert ==
            Assert.True(undefinable == equalUndefinable);
            Assert.False(undefinable == unequalUndefinable);
            Assert.False(undefinable == undefined);

            // assert !=
            Assert.False(undefinable != equalUndefinable);
            Assert.True(undefinable != unequalUndefinable);
            Assert.True(undefinable != undefined);

            // assert GetHashCode
            Assert.Equal(undefinable.GetHashCode(), equalUndefinable.GetHashCode());
            //Assert.NotEqual(undefinable.GetHashCode(), unequalUndefinable.GetHashCode()); // this really depends on the value, sometimes they ARE equal
            Assert.NotEqual(undefinable.GetHashCode(), undefined.GetHashCode());

            // assert ToString
            Assert.Equal(value?.ToString(), undefinable.ToString());
        }

        [Fact]
        public void IsNotDefined()
        {
            // arrange
            Undefinable<TValue> undefinable = new Undefinable<TValue>();
            Undefinable<TValue> equalUndefinable = Undefinable<TValue>.Undefined;
            Undefinable<TValue> unequalUndefinable = default(TValue);

            // assert IsDefined
            Assert.False(undefinable.IsDefined);

            // assert Value
            var exception = Assert.Throws<InvalidOperationException>(() => undefinable.Value);
            Assert.Equal($"Undefinable<{typeof(TValue).FullName}> is {Undefinable<TValue>.Undefined_ToString}.", exception.Message);

            // assert Equals
            Assert.True(undefinable.Equals(equalUndefinable));
            Assert.False(undefinable.Equals(unequalUndefinable));

            // assert ==
            Assert.True(undefinable == equalUndefinable);
            Assert.False(undefinable == unequalUndefinable);

            // assert Equals
            Assert.False(undefinable != equalUndefinable);
            Assert.True(undefinable != unequalUndefinable);

            // assert GetHashCode
            Assert.Equal(undefinable.GetHashCode(), equalUndefinable.GetHashCode());
            Assert.NotEqual(undefinable.GetHashCode(), unequalUndefinable.GetHashCode());

            // assert ToString
            Assert.Equal(Undefinable<TValue>.Undefined_ToString, undefinable.ToString());
        }

        protected static IEnumerable<object?[]> GetImplicitParameterTestData<T>()
        {
            T[] items = (T[])TypeTestData[typeof(TValue)];

            foreach (var item in items)
            {
                yield return new object?[] { item };
            }
        }

        protected static IEnumerable<object?[]> GetIsDefinedTestData<T>()
        {
            T[] items = (T[])TypeTestData[typeof(TValue)];

            if (items.Length < 2)
                throw new ArgumentOutOfRangeException("Test data needs at least 2 unique items");

            for (var i = 0; i < items.Length; i++)
            {
                for (var j = 0; j < items.Length; j++)
                {
                    if (i == j)
                        continue;

                    yield return new object?[] { items[i], items[j] };
                }
            }
        }

        protected static IEnumerable<object?[]> GetUndefinableParameterTestData<T>()
        {
            T[] items = (T[])TypeTestData[typeof(TValue)];

            yield return new object?[] { default(Undefinable<T>), false, default(T) };
            yield return new object?[] { new Undefinable<T>(), false, default(T) };
            foreach (var item in items)
            {
                yield return new object?[] { new Undefinable<T>(item), true, item };
            }
        }
    }
}