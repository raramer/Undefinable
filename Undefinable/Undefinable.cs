using System;

namespace Undefinable
{
    public struct Undefinable<T>
    {
        internal const string Undefined_ToString = "<undefined>";

        private readonly T _value;

        public bool IsDefined { get; }

        public T Value => IsDefined ? _value : throw new InvalidOperationException($"Undefinable<{typeof(T).FullName}> is {Undefined_ToString}.");

        public Undefinable(T value)
        {
            IsDefined = true;
            _value = value;
        }

        public static Undefinable<T> Undefined => new Undefinable<T>();

        public override bool Equals(object obj)
        {
            if (obj is Undefinable<T>)
            {
                var undefinedObj = (Undefinable<T>)obj;

                if (!this.IsDefined && !undefinedObj.IsDefined)
                    return true;

                if (this.IsDefined && undefinedObj.IsDefined)
                {
                    if (this.Value == null && undefinedObj.Value == null)
                        return true;

                    return this.Value != null && this.Value.Equals(undefinedObj.Value);
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(IsDefined, _value).GetHashCode();
        }

        public override string ToString()
        {
            return IsDefined ? Value?.ToString() : Undefined_ToString;
        }

        public static implicit operator Undefinable<T>(T value) => new Undefinable<T>(value);

        public static bool operator ==(Undefinable<T> a, Undefinable<T> b) => a.Equals(b);

        public static bool operator !=(Undefinable<T> a, Undefinable<T> b) => !a.Equals(b);
    }
}
