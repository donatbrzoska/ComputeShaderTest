using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class IncreaseDecreaseColor : MonoBehaviour
{
    public int TextureHeight;
    public int TextureWidth;

    GPUIncreaseDecreaseColor GpuIncreaseColor;
    private Renderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        GpuIncreaseColor = new GPUIncreaseDecreaseColor(TextureHeight, TextureHeight);

        Renderer = GameObject.Find("Plane").GetComponent<Renderer>();
        Renderer.material.SetTexture("_MainTex", GpuIncreaseColor.GetTexture());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // NOTE: measure render time through FPS
            GpuIncreaseColor.Increase();
        }
        if (Input.GetKey(KeyCode.S))
        {
            GpuIncreaseColor.Decrease();
        }
    }

    private void OnDestroy()
    {
        if (GpuIncreaseColor != null)
            GpuIncreaseColor.Dispose();
    }
}
