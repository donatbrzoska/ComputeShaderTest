using System;
using UnityEngine;

public class StructuredBufferCalculator : Calculator
{
    private RenderTexture Texture;
    private ComputeBuffer ColorBuffer;

    public StructuredBufferCalculator(int textureSize, int threadsX, int threadsY, Renderer renderer)
        : base(textureSize, threadsX, threadsY, renderer)
    {
        Texture = CreateTexture(textureSize);

        Renderer.material.SetTexture("_MainTex", Texture);

        ColorBuffer = new ComputeBuffer(textureSize * textureSize, 4 * sizeof(float));
        Color[] initial = new Color[textureSize * textureSize];
        ColorBuffer.SetData(initial);
    }

    protected override string GetPath()
    {
        return "StructuredBuffer/Usual";
    }

    protected override void DoComputation(ComputeShader cs)
    {
        cs.SetBuffer(0, "Colors", ColorBuffer);
        cs.SetTexture(0, "OutputTexture", Texture);
        cs.SetInt("CanvasWidth", TextureSize);
        cs.Dispatch(0, TextureSize / ThreadsX, TextureSize / ThreadsY, 1);
    }

    protected override void DoDispose()
    {
        ColorBuffer.Dispose();
    }
}
