﻿using System;
using UnityEngine;

public class StructuredBufferWithPaddingCalculator : Calculator
{
    private struct ColorInfo
    {
        public Color Rg;
        public float B;
        public Vector3 pad;
    }

    private RenderTexture Texture;
    private ComputeBuffer ColorInfoBuffer;

    public StructuredBufferWithPaddingCalculator(int textureSize, int threadsX, int threadsY, Renderer renderer)
        : base(textureSize, threadsX, threadsY, renderer)
    {
        Texture = CreateTexture(textureSize);

        Renderer.material.SetTexture("_MainTex", Texture);

        ColorInfoBuffer = new ComputeBuffer(textureSize * textureSize, 4 * sizeof(float) + 1 * sizeof(float) + 3 * sizeof(float));
        ColorInfo[] initial = new ColorInfo[textureSize * textureSize];
        ColorInfoBuffer.SetData(initial);
    }

    protected override string GetPath()
    {
        return "StructuredBuffer/WithPadding";
    }

    protected override void DoComputation(ComputeShader cs)
    {
        cs.SetBuffer(0, "Colors", ColorInfoBuffer);
        cs.SetTexture(0, "OutputTexture", Texture);
        cs.SetInt("CanvasWidth", TextureSize);
        cs.Dispatch(0, TextureSize / ThreadsX, TextureSize / ThreadsY, 1);
    }

    protected override void DoDispose()
    {
        ColorInfoBuffer.Dispose();
    }
}
