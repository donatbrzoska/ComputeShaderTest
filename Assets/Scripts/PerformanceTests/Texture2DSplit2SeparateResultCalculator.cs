using System;
using UnityEngine;

public class Texture2DSplit2SeparateResultCalculator : Calculator
{
    private RenderTexture Texture10;
    private RenderTexture Texture11;
    private RenderTexture Texture20;
    private RenderTexture Texture21;
    private RenderTexture CurrentInputTexture1;
    private RenderTexture CurrentInputTexture2;
    private RenderTexture CurrentOutputTexture1;
    private RenderTexture CurrentOutputTexture2;

    private RenderTexture Texture;

    // Use a third texture as result, so we only have one SetTexture call -> makes a difference
    public Texture2DSplit2SeparateResultCalculator(int textureSize, int threadsX, int threadsY, Renderer renderer)
        : base(textureSize, threadsX, threadsY, renderer)
    {
        Texture10 = CreateTexture(textureSize);
        Texture11 = CreateTexture(textureSize);
        Texture20 = CreateTexture(textureSize);
        Texture21 = CreateTexture(textureSize);
        CurrentInputTexture1 = Texture10;
        CurrentInputTexture2 = Texture11;
        CurrentOutputTexture1 = Texture20;
        CurrentOutputTexture2 = Texture21;

        Texture = CreateTexture(textureSize);

        Renderer.material.SetTexture("_MainTex", Texture);
    }

    protected override string GetPath()
    {
        return "Texture2DSplit2SeparateResult";
    }

    protected override void DoComputation(ComputeShader cs)
    {
        // Do computation
        //int[] finishedValue = new int[1];

        //ComputeBuffer finishedBuffer = new ComputeBuffer(1, sizeof(int));
        //finishedBuffer.SetData(finishedValue);

        cs.SetTexture(0, "InputTexture1", CurrentInputTexture1);
        cs.SetTexture(0, "InputTexture2", CurrentInputTexture2);
        cs.SetTexture(0, "OutputTexture1", CurrentOutputTexture1);
        cs.SetTexture(0, "OutputTexture2", CurrentOutputTexture2);
        cs.SetTexture(0, "Result", Texture);
        //cs.SetBuffer(0, "FinishedMarker", finishedBuffer);

        cs.Dispatch(0, TextureSize / ThreadsX, TextureSize / ThreadsY, 1);
        //GL.Flush();

        //finishedBuffer.GetData(finishedValue);
        //finishedBuffer.Dispose();


        // Swap texture pointers
        RenderTexture h1 = CurrentInputTexture1;
        RenderTexture h2 = CurrentInputTexture2;
        CurrentInputTexture1 = CurrentOutputTexture1;
        CurrentInputTexture2 = CurrentOutputTexture2;
        CurrentOutputTexture1 = h1;
        CurrentOutputTexture2 = h2;
    }

    protected override void DoDispose()
    {
        // nothing to dispose
    }
}
