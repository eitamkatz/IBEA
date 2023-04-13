using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _level;
    private int _gridSize;
    private int[,] _goalShape;
    private int _numOfSquares;
    private int _levelTime;
    [SerializeField] private ShapeDisplay _shapeDisplay;
    [SerializeField] private ShapeGenerator _shapeGenerator;
    [SerializeField] private Player _player;
    [SerializeField] private Timer _timer;

    private void Start()
    {
        _level = 1;
        _gridSize = 5;
        _levelTime = 60;
        _numOfSquares = 4 * _level + 1;
        _numOfSquares = 1;
        InitializeLevel(_level);
        // PrintShape(_goalShape);
    }

    private void Update()
    {
        if (Player.Shared.endOfLevel)
            EndOfLevel();
    }

    /*
     * Initializes the level 
     */
    void InitializeLevel(int level)
    {
        print("LEVEL " + level);
        if (level % 5 == 0)
        {
            _gridSize += 2;
            _shapeDisplay.UpdateGrid(_gridSize);
        }
        _goalShape = _shapeGenerator.GenerateShape(_level, _gridSize);
        // _shapeDisplay.DisplayShape(_goalShape);
        // _timer.StartTimer(_levelTime);
    }
    
    /*
     * checks if the win condition is met at the end of a level
     * if the shapes match, the player moves on to the next level.
     * if not - the game is over.
     */
    public void EndOfLevel()
    {
        if (_player.FinalShape(_goalShape, _numOfSquares))
        {
            print("LEVEL COMPLETE!");
            _level++;
            InitializeLevel(_level);
        }
        else
        {
            print("GAME OVER");
            SceneManager.LoadScene("GameOver");
        }
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
