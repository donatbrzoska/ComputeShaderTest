using UnityEngine;

public class GPUIncreaseDecreaseColor
{
    private int TextureHeight;
    private int TextureWidth;
    private ComputeShader IncreaseShader;
    private ComputeShader IncreaseShader2;
    private ComputeShader DecreaseShader;
    private ComputeShader DecreaseShader2;
    private RenderTexture RenderTexture_r;
    private RenderTexture RenderTexture_g;
    private RenderTexture RenderTexture_b;
    private RenderTexture RenderTexture;

    private ComputeBuffer GPUBuffer;
    // private ComputeBuffer GPUBuffer_r;
    // private ComputeBuffer GPUBuffer_g;
    // private ComputeBuffer GPUBuffer_b;
    // private ComputeBuffer GPUBuffer_a;

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

        RenderTexture_r = new RenderTexture(TextureWidth, TextureHeight, 1, RenderTextureFormat.RFloat);
        RenderTexture_r.filterMode = FilterMode.Point;
        RenderTexture_r.enableRandomWrite = true;
        RenderTexture_r.Create();

        RenderTexture_g = new RenderTexture(TextureWidth, TextureHeight, 1, RenderTextureFormat.RFloat);
        RenderTexture_g.filterMode = FilterMode.Point;
        RenderTexture_g.enableRandomWrite = true;
        RenderTexture_g.Create();

        RenderTexture_b = new RenderTexture(TextureWidth, TextureHeight, 1, RenderTextureFormat.RFloat);
        RenderTexture_b.filterMode = FilterMode.Point;
        RenderTexture_b.enableRandomWrite = true;
        RenderTexture_b.Create();


        GPUBuffer = new ComputeBuffer(TextureWidth * TextureHeight * 4, sizeof(float));
        Color[] initial = new Color[TextureWidth * TextureHeight];
        GPUBuffer.SetData(initial);

        // GPUBuffer_r = new ComputeBuffer(TextureWidth * TextureHeight, sizeof(float), ComputeBufferType.Raw);
        // float[] initial_r = new float[TextureWidth * TextureHeight];
        // GPUBuffer_r.SetData(initial_r);
        // GPUBuffer_g = new ComputeBuffer(TextureWidth * TextureHeight, sizeof(float), ComputeBufferType.Raw);
        // float[] initial_g = new float[TextureWidth * TextureHeight];
        // GPUBuffer_g.SetData(initial_g);
        // GPUBuffer_b = new ComputeBuffer(TextureWidth * TextureHeight, sizeof(float), ComputeBufferType.Raw);
        // float[] initial_b = new float[TextureWidth * TextureHeight];
        // GPUBuffer_b.SetData(initial_b);
        // GPUBuffer_a = new ComputeBuffer(TextureWidth * TextureHeight, sizeof(float), ComputeBufferType.Raw);
        // float[] initial_a = new float[TextureWidth * TextureHeight];
        // GPUBuffer_a.SetData(initial_a);


        DebugBuffer = new ComputeBuffer(TextureWidth * TextureHeight, sizeof(int));
        DebugValue = new int[TextureWidth * TextureHeight];
        DebugBuffer.SetData(DebugValue);


        IncreaseShader = (ComputeShader)Resources.Load("IncreaseShader");
        IncreaseShader.SetBuffer(0, "DebugValue", DebugBuffer);
        IncreaseShader.SetTexture(0, "r", RenderTexture_r);
        IncreaseShader.SetTexture(0, "g", RenderTexture_g);
        IncreaseShader.SetTexture(0, "b", RenderTexture_b);
        IncreaseShader.SetTexture(0, "Result", RenderTexture);
        IncreaseShader.SetInt("canvas_width", TextureWidth);
        IncreaseShader.SetInt("canvas_height", TextureHeight);
        IncreaseShader.SetBuffer(0, "Colors", GPUBuffer);

        // IncreaseShader2 = (ComputeShader)Resources.Load("IncreaseShader2");
        // IncreaseShader2.SetBuffer(0, "DebugValue", DebugBuffer);
        // IncreaseShader2.SetTexture(0, "Result", RenderTexture);
        // IncreaseShader2.SetInt("canvas_width", TextureWidth);
        // IncreaseShader2.SetInt("canvas_height", TextureHeight);
        // IncreaseShader2.SetBuffer(0, "Colors_r", GPUBuffer_r);
        // IncreaseShader2.SetBuffer(0, "Colors_g", GPUBuffer_g);
        // IncreaseShader2.SetBuffer(0, "Colors_b", GPUBuffer_b);
        // IncreaseShader2.SetBuffer(0, "Colors_a", GPUBuffer_a);

        DecreaseShader = (ComputeShader)Resources.Load("DecreaseShader");
        DecreaseShader.SetBuffer(0, "DebugValue", DebugBuffer);
        DecreaseShader.SetTexture(0, "Result", RenderTexture);
        DecreaseShader.SetInt("canvas_width", TextureWidth);
        DecreaseShader.SetInt("canvas_height", TextureHeight);
        DecreaseShader.SetBuffer(0, "Colors", GPUBuffer);

        // DecreaseShader2 = (ComputeShader)Resources.Load("DecreaseShader2");
        // DecreaseShader2.SetBuffer(0, "DebugValue", DebugBuffer);
        // DecreaseShader2.SetTexture(0, "Result", RenderTexture);
        // DecreaseShader2.SetInt("canvas_width", TextureWidth);
        // DecreaseShader2.SetInt("canvas_height", TextureHeight);
        // DecreaseShader2.SetBuffer(0, "Colors_r", GPUBuffer_r);
        // DecreaseShader2.SetBuffer(0, "Colors_g", GPUBuffer_g);
        // DecreaseShader2.SetBuffer(0, "Colors_b", GPUBuffer_b);
        // DecreaseShader2.SetBuffer(0, "Colors_a", GPUBuffer_a);
    }

    public Texture GetTexture()
    {
        return RenderTexture;
    }

    public void Increase()
    {
        IncreaseShader.Dispatch(0, RenderTexture.width / 8, RenderTexture.height, 1);
        // IncreaseShader2.Dispatch(0, RenderTexture.width / 8, RenderTexture.height, 1);
        //DebugBuffer.GetData(DebugValue);
    }

    public void Decrease()
    {
        DecreaseShader.Dispatch(0, RenderTexture.width / 8, RenderTexture.height, 1);
        // DecreaseShader2.Dispatch(0, RenderTexture.width / 8, RenderTexture.height, 1);
        //DebugBuffer.GetData(DebugValue);
    }

    public void Dispose()
    {
        // GPUBuffer.Dispose();
        // GPUBuffer_r.Dispose();
        // GPUBuffer_g.Dispose();
        // GPUBuffer_b.Dispose();
        // GPUBuffer_a.Dispose();

        DebugBuffer.Dispose();
    }
}