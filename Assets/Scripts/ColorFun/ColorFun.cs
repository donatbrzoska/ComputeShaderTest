using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ColorFun : MonoBehaviour
{
    public bool GPU_ENABLE;

    public int TextureHeight;
    public int TextureWidth;

    IColorFun colorFun;
    private Renderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        if (GPU_ENABLE)
        {
            colorFun = new GPUColorFun(TextureHeight, TextureHeight, Screen.height, Screen.width);
        }
        else
        {
            colorFun = new CPUColorFun(TextureHeight, TextureHeight, Screen.height, Screen.width);
        }

        Renderer = GameObject.Find("Plane").GetComponent<Renderer>();
        Renderer.material.SetTexture("_MainTex", colorFun.GetTexture());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            Vector3 mouse_pos = Input.mousePosition;
            colorFun.Compute((int)mouse_pos.x, (int)mouse_pos.y);

            double ms = 1000 * (double)sw.ElapsedTicks / Stopwatch.Frequency;
            //double us = 1000000.0 * (double)sw.ElapsedTicks / Stopwatch.Frequency;
            //double ns = 1000000000.0 * (double)sw.ElapsedTicks / Stopwatch.Frequency;
            //UnityEngine.Debug.Log(ns + "ns");
            UnityEngine.Debug.Log(ms + "ms");
        }
    }
}
