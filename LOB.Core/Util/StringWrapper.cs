﻿namespace LOB.Core.Util
{
    public class StringWrapper
    {
        public StringWrapper(string s)
        {
            Value = s;
        }

        public string Value { get; set; }

        protected bool Equals(StringWrapper other)
        {
            return string.Equals(Value, other.Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(StringWrapper e)
        {
            return e.Value;
        }

        public static implicit operator StringWrapper(string value)
        {
            return new StringWrapper(value);
        }

        public override bool Equals(object obj)
        {
            return string.Equals(this.Value, obj.ToString());
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}