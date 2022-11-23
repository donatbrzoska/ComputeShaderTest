using System;
using UnityEngine;

struct MousePosition
{
    public int x;
    public int y;
}

public class GPUColorFun : IColorFun
{
    private const int N_ThreadGroups = 8; // this has to match numthreads in the ComputeShader

    private int TextureHeight;
    private int TextureWidth;
    private int ScreenHeight;
    private int ScreenWidth;
    private ComputeShader ComputeShader;
    private RenderTexture RenderTexture;

    public GPUColorFun(int texture_height, int texture_width, int screen_height, int screen_width)
    {
        TextureHeight = texture_height;
        TextureWidth = texture_width;
        ScreenHeight = screen_height;
        ScreenWidth = screen_width;

        RenderTexture = new RenderTexture(TextureWidth, TextureHeight, 24);
        RenderTexture.filterMode = FilterMode.Point;
        RenderTexture.enableRandomWrite = true;
        RenderTexture.Create();

        ComputeShader = (ComputeShader)Resources.Load("ComputeShader");
        ComputeShader.SetTexture(0, "Result", RenderTexture);
        ComputeShader.SetInt("screen_width", ScreenWidth);
        ComputeShader.SetInt("screen_height", ScreenHeight);
        ComputeShader.SetInt("canvas_width", TextureWidth);
        ComputeShader.SetInt("canvas_height", TextureHeight);
    }

    public Texture GetTexture()
    {
        return RenderTexture;
    }

    public Color[] Compute(int mouse_x, int mouse_y)
    {
        // use array just for trying out ComputeBuffers
        MousePosition[] data = new MousePosition[1] {
            new MousePosition() { x = mouse_x, y = mouse_y }
        };

        ComputeBuffer cb_mouse = new ComputeBuffer(1, sizeof(int) + sizeof(int));
        cb_mouse.SetData(data);

        ComputeBuffer cb_texture = new ComputeBuffer(TextureHeight * TextureWidth * 4, sizeof(int) + sizeof(int));
        Color[] colors = new Color[TextureWidth * TextureHeight];
        cb_texture.SetData(colors);

        ComputeShader.SetBuffer(0, "mouse_position", cb_mouse);
        ComputeShader.SetBuffer(0, "texture_data", cb_texture);
        ComputeShader.Dispatch(0, RenderTexture.width / 1, RenderTexture.height / 1, 1);

        // OPTION
        cb_texture.GetData(colors);

        cb_mouse.Dispose();
        cb_texture.Dispose();

        return colors;
    }

    public Color[] Compute_Testable(int mouse_x, int mouse_y)
    {
        Compute(mouse_x, mouse_y);

        return getPixels(RenderTexture);
    }

    private Color[] getPixels(RenderTexture renderTexture)
    {
        Texture2D target = new Texture2D(TextureWidth, TextureHeight);

        //copy to Texture2D
        RenderTexture.active = renderTexture;
        target.ReadPixels(new Rect(0, 0, TextureWidth, TextureHeight), 0, 0);
        target.Apply();
        RenderTexture.active = null;

        return target.GetPixels();
    }
}
