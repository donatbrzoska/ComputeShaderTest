using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SequentialTest : MonoBehaviour
{
    // Inspector
    public int TEXTURE_SIZE = 1024;
    public int THREADS_X = 8;
    public int THREADS_Y = 1;
    public int ITERATIONS = 100;
    public bool INSTANT_FLUSH = false;

    ComputeBuffer ComputeBuffer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ComputeBuffer = new ComputeBuffer(TEXTURE_SIZE * TEXTURE_SIZE, sizeof(float));
        float[] data = new float[TEXTURE_SIZE * TEXTURE_SIZE];
        ComputeBuffer.SetData(data);

        // run sequential computation
        for (int i = 0; i < ITERATIONS; i++)
        {
            ComputeShader first = (ComputeShader)Resources.Load("SequentialTest/First");
            first.SetBuffer(0, "Buf", ComputeBuffer);
            first.SetInt("Width", TEXTURE_SIZE);
            first.Dispatch(0, TEXTURE_SIZE / THREADS_X, TEXTURE_SIZE / THREADS_Y, 1);
            if (INSTANT_FLUSH)
                GL.Flush();

            ComputeShader second = (ComputeShader)Resources.Load("SequentialTest/Second");
            second.SetBuffer(0, "Buf", ComputeBuffer);
            second.SetInt("Width", TEXTURE_SIZE);
            second.Dispatch(0, TEXTURE_SIZE / THREADS_X, TEXTURE_SIZE / THREADS_Y, 1);
            if (INSTANT_FLUSH)
                GL.Flush();
        }

        // retrieve result data
        ComputeBuffer.GetData(data);

        // dispose buffer
        ComputeBuffer.Dispose();

        // check for equality
        bool success = true;
        for (int i = 0; i < TEXTURE_SIZE; i++)
        {
            for (int j = 0; j < TEXTURE_SIZE; j++)
            {
                if (data[i * TEXTURE_SIZE + j] != data[0])
                {
                    success = false;
                }
            }
        }

        Debug.Log("Success: " + success);
        Debug.Log("Result: " + data[0]);
    }
}
