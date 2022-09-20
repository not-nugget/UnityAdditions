namespace UnityAdditions.Enum
{
    using System;

    public class EnumData<T> where T : Enum
    {



    }

    public struct EnumTypeObjectPair<T> where T : Type
    {
        public Type Key { get; }
        public object Value { get; set; }
    }
}