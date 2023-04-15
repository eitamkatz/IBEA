using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _level;
    private int[,] _goalShape;
    private int _numOfSquares;
    private int _levelTime;
    // [SerializeField] private ShapeDisplay _shapeDisplay;
    // [SerializeField] private ShapeGenerator _shapeGenerator;
    [SerializeField] private Levels _levels;
    [SerializeField] private Player _player;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameManager _gameManager;
    
    //     if (_gameManager == null)
    //     {
    //         _gameManager = this;
    //         DontDestroyOnLoad(this);
    //     }
    //     else if (_gameManager != this)
    //     {
    //         Destroy(gameObject);
    //     }
    //     InitializeLevel(_level);
    // } 

    private void Start()
    {
        _level = 0;
        DontDestroyOnLoad(this);
        InitializeLevel(_level);
        // PrintShape(_goalShape);
    }

    private void Update()
    {
        if (Player.Shared.endOfLevel)
           CheckWin();
    }

    /*
     * Initializes the level 
     */
    void InitializeLevel(int level)
    {
        print("LEVEL " + level);
        _goalShape = _levels.GetLevelShape(_level);
        _numOfSquares = _levels.GetLevelNumOfSquares(_level);
        // _shapeDisplay.UpdateGrid(_gridSize);
        // _shapeDisplay.DisplayShape(_goalShape);
        _player.NewLevel();
        _levelTime = _levels.GetLevelTime(_level);
        _timer.StartTimer(_levelTime);
    }
    
    /*
     * loads game over scene
     */
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void CheckWin()
    {
        if (_player.FinalShape(_goalShape, _numOfSquares))
        {
            print("LEVEL " + _level + " COMPLETE!");
            _level++;
            SceneManager.LoadScene("Level" + _level);
            InitializeLevel(_level);
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
