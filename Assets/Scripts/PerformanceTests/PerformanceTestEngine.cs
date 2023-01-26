using UnityEngine;

public enum TestMode
{
    Empty,
    Texture2D,
    TripleTexture2D,
    StructuredBuffer,
    StructuredBufferNoPadding,
    StructuredBufferWithPadding,
}

public class PerformanceTestEngine : MonoBehaviour
{
    // Inspector
    public int TEXTURE_SIZE = 4096;
    public int THREADS_X = 8;
    public int THREADS_Y = 1;

    // UI
    public TestMode TestMode { get; private set; } = TestMode.StructuredBuffer;

    // Private
    private Renderer Renderer;
    private Calculator Calculator;
    private bool TestModeChanged = true;

    void Start()
    {
        Renderer = GameObject.Find("Plane").GetComponent<Renderer>();
    }

    void Update()
    {
        if (TestModeChanged)
        {
            TestModeChanged = false;

            OnDestroy();

            switch (TestMode)
            {
                case TestMode.StructuredBuffer:
                    Calculator = new StructuredBufferCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.StructuredBufferNoPadding:
                    Calculator = new StructuredBufferNoPaddingCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.StructuredBufferWithPadding:
                    Calculator = new StructuredBufferNoPaddingCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.Texture2D:
                    Calculator = new Texture2DCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.TripleTexture2D:
                    Calculator = new TripleTexture2DCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.Empty:
                    break;
                default:
                    Calculator = new Texture2DCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
            }
        }
        Calculator.Update();
    }

    private void OnDestroy()
    {
        if (Calculator != null)
            Calculator.Dispose();
    }

    public void OnTestModeChanged(int testMode)
    {
        TestMode = (TestMode)testMode;
        TestModeChanged = true;
    }

    /*
     * Empty
     * Standard Up and Down
     * - StructuredBuffer
     * - StructuredBuffer mit Padding (braucht aber noch besseres Beispiel)
     * - Texture2D
     * Multiple Shaders per Frame
     * - No synchronization
     * - Buffer synchronization
     * - Flush synchronization?
     * - Shader throughput loss when doing one per frame
     * MultiPassOperation
     * - 2x on Texture vs 1x on StructuredBuffer
     */
}
