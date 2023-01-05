using System;
using UnityEngine;

public abstract class Calculator
{
    protected const float STEPSIZE = 0.005f;

    protected int TextureSize;
    protected int ThreadsX;
    protected int ThreadsY;

    protected Renderer Renderer;

    protected float CurrentValue;
    protected bool Ascending;

    public Calculator(int textureSize, int threadsX, int threadsY, Renderer renderer)
    {
        TextureSize = textureSize;
        ThreadsX = threadsX;
        ThreadsY = threadsY;

        Renderer = renderer;
    }

    public void Update()
    {
        int STEPS = 1;
        for (int i=0; i<STEPS; i++)
        {
            Step();
        }
    }

    private void Step()
    {
        // determine direction
        if (CurrentValue - STEPSIZE < 0)
        {
            Ascending = true;
        }
        else if (CurrentValue + STEPSIZE > 1)
        {
            Ascending = false;
        }


        // get shader
        ComputeShader cs;
        string path = "PerformanceTests/" + GetPath();
        if (Ascending)
        {
            cs = (ComputeShader)Resources.Load(path + "/IncreaseColorShader");
            CurrentValue += STEPSIZE;
        }
        else
        {
            cs = (ComputeShader)Resources.Load(path + "/DecreaseColorShader");
            CurrentValue -= STEPSIZE;
        }

        // prepare shader
        cs.SetFloat("Stepsize", STEPSIZE);

        DoComputation(cs);
    }

    public void Dispose()
    {
        DoDispose();
    }

    protected abstract string GetPath();

    protected abstract void DoComputation(ComputeShader cs);

    protected abstract void DoDispose();

    protected RenderTexture CreateTexture(int textureSize)
    {
        RenderTexture r = new RenderTexture(textureSize, textureSize, 1);
        r.filterMode = FilterMode.Point;
        r.enableRandomWrite = true;
        r.Create();
        return r;
    }
}
