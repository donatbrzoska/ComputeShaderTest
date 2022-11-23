using System;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class CPUColorFun: IColorFun
{
    private int TextureHeight;
    private int TextureWidth;
    private int ScreenHeight;
    private int ScreenWidth;
    private Texture2D Texture;
    private NativeArray<Color32> Texture_raw;

    public CPUColorFun(int texture_height, int texture_width, int screen_height, int screen_width)
    {
        TextureHeight = texture_height;
        TextureWidth = texture_width;
        ScreenHeight = screen_height;
        ScreenWidth = screen_width;

        Texture = new Texture2D(TextureWidth, TextureHeight, TextureFormat.RGBA32, false);
        Texture.filterMode = FilterMode.Point;

        Texture_raw = Texture.GetRawTextureData<Color32>();
    }

    public Texture GetTexture()
    {
        return Texture;
    }

    public Color[] Compute(int mouse_x, int mouse_y)
    {
        //for (int i = 0; i < TextureWidth; i++)
        Parallel.For(0, TextureWidth, (i, state) =>
        {
            for (int j = 0; j < TextureHeight; j++)
            {
                float mouse_comp_x = (float)mouse_x / ScreenWidth;
                float mouse_comp_y = (float)mouse_y / ScreenHeight;
                float plane_comp_x = (float)i / TextureWidth;
                float plane_comp_y = (float)j / TextureHeight;
                float r = (mouse_comp_x + plane_comp_x) / 2;
                float g = (mouse_comp_y + plane_comp_y) / 2;
                float b = 1 - g;
                // SetPixelFast(i, j, new Color(r * r * r * r * r / g / b / g / b * r * r * r * r / g / b / g / b * r * r * r * r / g / b / g / b, g * g * g * g * g / r / b / r / b * g * g * g * g / r / b / r / b * g * g * g * g / r / b / r / b, b * b * b * b * b / r / g / r / g * b * b * b * b / r / g / r / g * b * b * b * b / r / g / r / g));
                SetPixelFast(i, j, new Color(r, g, b));
            }
        });
        Texture.Apply();

        return Texture.GetPixels();
    }

    public Color[] Compute_Testable(int mouse_x, int mouse_y)
    {
        Compute(mouse_x, mouse_y);

        return Texture.GetPixels();
    }

    private void SetPixelFast(int x, int y, Color color)
    {
        int index_1D = y * TextureWidth + x;
        Texture_raw[index_1D] = color;
    }
}
