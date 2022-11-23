using UnityEngine;
using NUnit.Framework;

public class TestGPUColorFun
{
    public static void AssertColorsAreEqual(Color[] expected, Color[] actual)
    {
        Assert.AreEqual(expected.GetLength(0), actual.GetLength(0));

        for (int i = 0; i < expected.GetLength(0); i++)
        {
            if (!ColorsAreEqual(expected[i], actual[i]))
            {
                // TODO this is hacky but works, just NOTE that the given error message
                //      might be misleading and you have to print your arrays for a better view
                Assert.AreEqual(expected, actual);
            }
        }
    }

    private static bool ColorsAreEqual(Color expected, Color actual)
    {
        bool equal = FloatsEqual(expected.a, actual.a)
            && FloatsEqual(expected.r, actual.r)
            && FloatsEqual(expected.g, actual.g)
            && FloatsEqual(expected.b, actual.b);
        return equal;
    }

    private static bool FloatsEqual(float a, float b, float precision = 0.001f)
    {
        return Mathf.Abs(a - b) < precision;
    }

    [Test]
    public void ResultsMatch()
    {
        CPUColorFun cc = new CPUColorFun(16, 16, 4, 4);
        Color[] cc_res = cc.Compute_Testable(2, 3);

        GPUColorFun gc = new GPUColorFun(16, 16, 4, 4);
        Color[] gc_res = gc.Compute_Testable(2, 3);

        //string res = "";
        //for (int i = 0; i < cc_res.Length; i++)
        //{
        //    res += cc_res[i] + "  ";
        //}
        //Debug.Log(res);

        //res = "";
        //for (int i = 0; i < gc_res.Length; i++)
        //{
        //    res += gc_res[i] + "  ";
        //}
        //Debug.Log(res);

        AssertColorsAreEqual(cc_res, gc_res);
    }
}
