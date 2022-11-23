using System;
using UnityEngine;

public interface IColorFun
{
    Texture GetTexture();
    Color[] Compute(int mouse_x, int mouse_y);
    Color[] Compute_Testable(int mouse_x, int mouse_y);
}
