using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private int[][,] _levels =
    {
        new[,] { { 1 } },
        new[,]
        {
            { 1, 1, 0 },
            { 1, 1, 0 },
            { 1, 0, 0 }
        },
        new[,]
        {
            { 0, 0, 0, 1 },
            { 0, 1, 1, 1 },
            { 0, 1, 1, 1 },
            { 0, 1, 0, 1 },
        },
        new[,] 
        {
            { 0, 0, 1, 1, 1 },
            { 0, 0, 1, 1, 0 },
            { 0, 0, 1, 1, 1 },
            { 0, 0, 1, 1, 0 },
            { 0, 0, 1, 1, 1 }
        },
        new[,] 
        {
            { 0, 0, 1, 0, 0, 0 },
            { 1, 1, 1, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 1 },
            { 0, 0, 0, 1, 1, 1 },
            { 0, 0, 1, 1, 1, 1 },
            { 0, 1, 1, 0, 0, 0 }
        },
        new[,] 
        {
            { 1, 1, 1, 1, 0, 0, 0 },
            { 0, 0, 1, 1, 0, 0, 0 },
            { 0, 0, 1, 1, 1, 1, 1 },
            { 0, 0, 1, 1, 1, 1, 1 },
            { 0, 0, 1, 1, 1, 0, 1 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 }
        },
    };

    private int[] _levelTimes = { 10, 20, 30, 40, 50, 60 };
    
    private int[] _levelNumOfSquares = { 1, 5, 9, 13, 17, 21 };

    public int[,] GetLevelShape(int level)
    {
        return _levels[level];
    }

    public int GetLevelTime(int level)
    {
        return _levelTimes[level];
    }

    public int GetLevelNumOfSquares(int level)
    {
        return _levelNumOfSquares[level];
    }
}