using UnityEngine;

public enum TestMode
{
    Empty,
    Texture2D,
    Texture2DSeparateResult,
    Texture2DSplit2SeparateResult,
    Texture2DSplit3SeparateResult,
    StructuredBuffer,
    StructuredBufferSplit2,
    StructuredBufferSplit3,
    StructuredBufferNoPadding,
    StructuredBufferWithPadding,
    StructuredBufferSeperateRead,
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
                case TestMode.StructuredBufferSeperateRead:
                    Calculator = new StructuredBufferSeperateReadCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.StructuredBufferSplit2:
                    Calculator = new StructuredBufferSplit2Calculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.StructuredBufferSplit3:
                    Calculator = new StructuredBufferSplit3Calculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.StructuredBufferNoPadding:
                    Calculator = new StructuredBufferNoPaddingCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.StructuredBufferWithPadding:
                    Calculator = new StructuredBufferWithPaddingCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.Texture2D:
                    Calculator = new Texture2DCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.Texture2DSeparateResult:
                    Calculator = new Texture2DSeparateResultCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.Texture2DSplit2SeparateResult:
                    Calculator = new Texture2DSplit2SeparateResultCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
                    break;
                case TestMode.Texture2DSplit3SeparateResult:
                    Calculator = new Texture2DSplit3SeparateResultCalculator(TEXTURE_SIZE, THREADS_X, THREADS_Y, Renderer);
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
