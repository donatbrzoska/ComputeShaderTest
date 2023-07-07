using System;
using UnityEngine;

public class StructuredBufferSeperateReadCalculator : StructuredBufferCalculator
{
    public StructuredBufferSeperateReadCalculator(int textureSize, int threadsX, int threadsY, Renderer renderer)
        : base(textureSize, threadsX, threadsY, renderer) { }

    protected override string GetPath()
    {
        return "StructuredBuffer/SeperateRead";
    }
}
