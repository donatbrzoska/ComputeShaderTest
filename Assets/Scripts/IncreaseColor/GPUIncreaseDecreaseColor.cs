using UnityEngine;

public class GPUIncreaseDecreaseColor
{
    private int TextureHeight;
    private int TextureWidth;
    private ComputeShader IncreaseShader;
    private ComputeShader DecreaseShader;
    private RenderTexture RenderTexture;

    private ComputeBuffer GPUBuffer;

    private ComputeBuffer DebugBuffer;
    public int[] DebugValue;

    public GPUIncreaseDecreaseColor(int texture_height, int texture_width)
    {
        TextureHeight = texture_height;
        TextureWidth = texture_width;

        RenderTexture = new RenderTexture(TextureWidth, TextureHeight, 1);
        RenderTexture.filterMode = FilterMode.Point;
        RenderTexture.enableRandomWrite = true;
        RenderTexture.Create();


        GPUBuffer = new ComputeBuffer(TextureWidth * TextureHeight * 4, sizeof(float));
        Color[] initial = new Color[TextureWidth * TextureHeight];
        GPUBuffer.SetData(initial);

        DebugBuffer = new ComputeBuffer(TextureWidth * TextureHeight, sizeof(int));
        DebugValue = new int[TextureWidth * TextureHeight];
        DebugBuffer.SetData(DebugValue);


        IncreaseShader = (ComputeShader)Resources.Load("IncreaseShader");
        IncreaseShader.SetBuffer(0, "DebugValue", DebugBuffer);
        IncreaseShader.SetTexture(0, "Result", RenderTexture);
        IncreaseShader.SetInt("canvas_width", TextureWidth);
        IncreaseShader.SetInt("canvas_height", TextureHeight);
        IncreaseShader.SetBuffer(0, "Colors", GPUBuffer);

        DecreaseShader = (ComputeShader)Resources.Load("DecreaseShader");
        DecreaseShader.SetBuffer(0, "DebugValue", DebugBuffer);
        DecreaseShader.SetTexture(0, "Result", RenderTexture);
        DecreaseShader.SetInt("canvas_width", TextureWidth);
        DecreaseShader.SetInt("canvas_height", TextureHeight);
        DecreaseShader.SetBuffer(0, "Colors", GPUBuffer);
    }

    public Texture GetTexture()
    {
        return RenderTexture;
    }

    public void Increase()
    {
        IncreaseShader.Dispatch(0, RenderTexture.width / 8, RenderTexture.height, 1);
        //DebugBuffer.GetData(DebugValue);
    }

    public void Decrease()
    {
        DecreaseShader.Dispatch(0, RenderTexture.width / 8, RenderTexture.height, 1);
        //DebugBuffer.GetData(DebugValue);
    }

    public void Dispose()
    {
        GPUBuffer.Dispose();
        DebugBuffer.Dispose();
    }
}