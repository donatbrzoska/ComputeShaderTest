﻿using System;
using NUnit.Framework;
using UnityEngine;

public class TestInterlockedAdd
{
    public void Log1DArray(int[] arr, int cols)
    {
        string result = "";

        int rows = arr.GetLength(0) / cols;
        for (int i=0; i<rows; i++)
        {
            for (int j=0; j<cols; j++)
            {
                result += arr[i * cols + j] + ", ";
            }
            result += "\n";
        }
        Debug.Log(result);
    }

    [Test]
    public void Test()
    {
        int RUNS = 512;
        for (int i=0; i<RUNS; i++)
        {
            int DIM = 32;

            ComputeBuffer cb = new ComputeBuffer(DIM * DIM, sizeof(int));
            int[] buf = new int[DIM * DIM];
            cb.SetData(buf);

            ComputeBuffer debugBuffer = new ComputeBuffer(DIM * DIM, sizeof(int));
            int[] debugData = new int[DIM * DIM];
            cb.SetData(debugData);


            ComputeShader cs = (ComputeShader)Resources.Load("InterlockedAddTest");
            cs.SetBuffer(0, "Buf", cb);
            cs.SetBuffer(0, "Active", debugBuffer);
            cs.SetInt("Dimensions", DIM);
            cs.Dispatch(0, DIM / 8, DIM, 1);


            cb.GetData(buf);
            cb.Dispose();

            debugBuffer.GetData(debugData);
            debugBuffer.Dispose();

            //Log1DArray(debugData, DIM);
            //Log1DArray(buf, DIM);

            //Assert.AreEqual(
            //    new int[] {
            //        1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 1,
            //        2, 4, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 2,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
            //        2, 4, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 2,
            //        1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 1,
            //    },
            //    buf
            //);

            Assert.AreEqual(
                new int[] {
                1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 1,
                2, 4, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 2,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                3, 6, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 6, 3,
                2, 4, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 4, 2,
                1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 1,
                },
                buf
            );
        }
    }
}
