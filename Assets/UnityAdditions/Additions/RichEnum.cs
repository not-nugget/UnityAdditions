using System;
using UnityEngine;

public class RichEnum<T> where T : Enum
{
    public string[] Names { get { return Enum.GetNames(typeof(T)); } }

    
}

public struct EnumDataContainer
{

}
