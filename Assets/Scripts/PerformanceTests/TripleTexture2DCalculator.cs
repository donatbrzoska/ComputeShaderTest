using System;
using UnityEngine;

public class TripleTexture2DCalculator : Calculator
{
    private RenderTexture Texture1;
    private RenderTexture Texture2;
    private RenderTexture CurrentInputTexture;
    private RenderTexture CurrentOutputTexture;

    private RenderTexture Texture;

    public TripleTexture2DCalculator(int textureSize, int threadsX, int threadsY, Renderer renderer)
        : base(textureSize, threadsX, threadsY, renderer)
    {
        Texture1 = CreateTexture(textureSize);
        Texture2 = CreateTexture(textureSize);
        CurrentInputTexture = Texture1;
        CurrentOutputTexture = Texture2;

        Texture = CreateTexture(textureSize);

        Renderer.material.SetTexture("_MainTex", Texture);
    }

    protected override string GetPath()
    {
        return "TripleTexture2D";
    }

    protected override void DoComputation(ComputeShader cs)
    {
        // Do computation
        //int[] finishedValue = new int[1];

        //ComputeBuffer finishedBuffer = new ComputeBuffer(1, sizeof(int));
        //finishedBuffer.SetData(finishedValue);

        cs.SetTexture(0, "InputTexture", CurrentInputTexture);
        cs.SetTexture(0, "OutputTexture", CurrentOutputTexture);
        cs.SetTexture(0, "Result", Texture);
        //cs.SetBuffer(0, "FinishedMarker", finishedBuffer);

        cs.Dispatch(0, TextureSize / ThreadsX, TextureSize / ThreadsY, 1);
        //GL.Flush();

        //finishedBuffer.GetData(finishedValue);
        //finishedBuffer.Dispose();


        // Swap texture pointers
        RenderTexture h = CurrentInputTexture;
        CurrentInputTexture = CurrentOutputTexture;
        CurrentOutputTexture = h;
    }

    protected override void DoDispose()
    {
        // nothing to dispose
    }
}
