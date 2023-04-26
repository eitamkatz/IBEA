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
            { 1, 1, 1, 1 },
            { 1, 1, 0, 0 },
            { 1, 1, 1, 0 },
            { 0, 0, 0, 0},
        },
        new[,] 
        {
            { 1, 1, 1, 0, 0, },
            { 1, 1, 0, 0, 0, },
            { 1, 1, 1, 0, 0, },
            { 1, 1, 0, 0, 0, },
            { 1, 1, 1, 0, 0, }
        },
        new[,] 
        {
            { 1, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 0 },
            { 1, 1, 1, 0, 0 },
            { 1, 1, 1, 1, 0 },
            { 1, 1, 1, 1, 1 }
        },
        new[,] 
        {
            {1, 1, 1, 1, 1 },
            {1, 1, 1, 1, 1 },
            {1, 1, 1, 1, 1 },
            {1, 1, 1, 1, 1 },
            {1, 1, 1, 1, 1 }
        },
    };
    private int[] _levelNumOfSquares = { 1, 5, 9, 13, 15, 25 };

    public int[,] GetLevelShape(int level)
    {
        return _levels[level];
    }

    public int GetLevelNumOfSquares(int level)
    {
        return _levelNumOfSquares[level];
    }
}