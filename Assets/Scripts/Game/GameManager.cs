using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _level = 1;
    private int[,] _goalShape;
    [SerializeField] private ShapeGenerator shapeGenerator;

    void Start()
    {
        InitializeLevel();
        PrintShape(_goalShape);
    }

    /*
     * Initializes the level 
     */
    void InitializeLevel()
    {
        _goalShape = shapeGenerator.GenerateShape(_level);
    }
    
    /*
     * prints a given shape (for testing!)
     */
    void PrintShape(int[,] shape)
    {
        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                print(shape[i, j]);
            }
        }
    }
}
