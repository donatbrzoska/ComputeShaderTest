using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSize : MonoBehaviour
{
    public int CANVAS_HEIGHT;
    public int CANVAS_WIDTH;
    public int PAINT_SIZE;

    private void PrintInfo(string name, long maxBytes)
    {
        Debug.Log(name + " supports " + maxBytes + " bytes");
        Debug.Log("= " + maxBytes / 1_000_000 + " MB");
        Debug.Log("= " + maxBytes / sizeof(float) + " floats");
        Debug.Log("= " + maxBytes / sizeof(float) / PAINT_SIZE / (CANVAS_HEIGHT * CANVAS_WIDTH) + " layers of 1500x100 pixels of paint");
        Debug.Log("= " + maxBytes / sizeof(float) / PAINT_SIZE / (CANVAS_HEIGHT * CANVAS_WIDTH) / 2 + " duplicated layers of 1500x100 pixels of paint");
    }

    // Start is called before the first frame update
    void Start()
    {
        PrintInfo("Texture", (long) SystemInfo.maxTextureSize * SystemInfo.maxTextureSize * 4 * sizeof(float));
        Debug.Log("");
        PrintInfo("Buffer", SystemInfo.maxGraphicsBufferSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
