using UnityEngine;

public class Texture2DPerformance : MonoBehaviour
{
    int DIM = 4096;
    bool done;

    RenderTexture InputTexture;
    RenderTexture OutputTexture;

    // Start is called before the first frame update
    void Start()
    {
        InputTexture = new RenderTexture(DIM, DIM, 1);
        InputTexture.filterMode = FilterMode.Point;
        InputTexture.enableRandomWrite = true;
        InputTexture.Create();

        OutputTexture = new RenderTexture(DIM, DIM, 1);
        OutputTexture.filterMode = FilterMode.Point;
        OutputTexture.enableRandomWrite = true;
        OutputTexture.Create();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            Debug.Log("Running Shader");


            int[] finishedValue = new int[1];

            ComputeBuffer finishedBuffer = new ComputeBuffer(1, sizeof(int));
            finishedBuffer.SetData(finishedValue);

            ComputeShader cs = (ComputeShader)Resources.Load("Texture2DPerformance");
            cs.SetTexture(0, "InputTexture", InputTexture);
            cs.SetTexture(0, "OutputTexture", OutputTexture);
            cs.SetBuffer(0, "FinishedMarker", finishedBuffer);

            cs.Dispatch(0, DIM / 8, DIM, 1);
            GL.Flush();

            finishedBuffer.GetData(finishedValue);
            finishedBuffer.Dispose();

            done = true;
        }
    }
}
