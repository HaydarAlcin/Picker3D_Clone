using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public struct HorizontalInputParams
{
    //Instead of determining movement limits through physical interaction,
    //we calculate and determine them mathematically.
    public float2 ClampValues;
    public float horizontalValue;
}
