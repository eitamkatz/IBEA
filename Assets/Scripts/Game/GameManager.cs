using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Shared { get; private set; }
    [SerializeField] private int _level;
    private int[,] _goalShape;
    private int _LevelSquareCount;
    private int _levelTime;
    // [SerializeField] private ShapeDisplay _shapeDisplay;
    // [SerializeField] private ShapeGenerator _shapeGenerator;
    [SerializeField] private Levels _levels;
    [SerializeField] private GameObject _player;
    [SerializeField] private LoadingScreen loadingScreen;
    // [SerializeField] private Timer _timer;
    // [SerializeField] private GameObject _playerPrefab;
     
    public bool WinCondition { get; set; }
    
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
    private void Awake()
    {
        Shared = this;
    }
    private void Start()
    {
        InitializeLevel(_level);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene("level" + _level);
    }

    private void Update()
    {
        if (WinCondition)
            CheckWinCondition();
        else if (Input.GetKey(KeyCode.Escape))
            GameOver();
    }
    
    /*
     * Initializes the level 
     */
    private void InitializeLevel(int level)
    {
        print("LEVEL " + level);
        StartCoroutine(loadingScreen.LoadLoadingScreen());
        _goalShape = _levels.GetLevelShape(_level);
        _LevelSquareCount = _levels.GetLevelNumOfSquares(_level);
        // _shapeDisplay.UpdateGrid(_gridSize);
        // _shapeDisplay.DisplayShape(_goalShape);
        // _levelTime = _levels.GetLevelTime(_level);
        // _timer.StartTimer(_levelTime);
        // _player = null;
        // _player = GameObject.Find("Player");
        
    }
    
    /*
     * loads game over scene
     */
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    /*
     * check
     */
    public void CheckWinCondition()
    {
        print("check win");
        if (CheckShapeMatch())
        {
            print("LEVEL " + _level + " COMPLETE!");
            
            if (_level == 5)
                SceneManager.LoadScene("Winning");
            else
            {
                _level++;
                SceneManager.LoadScene("Level" + _level);
            }
            InitializeLevel(_level);
        }
    }
    
    public bool CheckShapeMatch()
    {
        List<Vector2> playerShape = _player.GetComponent<Player>().Get_Player_shape();
        if (playerShape.Count != _LevelSquareCount) return false;
        for (int row = 0; row < _goalShape.GetLength(0); row++)
        {
            for (int col = 0; col < _goalShape.GetLength(1); col++)
            {
                Vector4 _shapeLimits = _player.GetComponent<Player>().getShapeLimits();
                Vector2 relativePos = new Vector2(_shapeLimits.x + col, _shapeLimits.w - row);
                if (_goalShape[row, col] == 0 && playerShape.Contains(relativePos)) return false;
                if (_goalShape[row, col] == 1 && !playerShape.Contains(relativePos)) return false;
            }
        }
        return true;
    }
    
    /*
     * prints a given shape (for testing!)
     */
    // void PrintShape(int[,] shape)
    // {
    //     for (int i = 0; i < shape.GetLength(0); i++)
    //     {
    //         for (int j = 0; j < shape.GetLength(1); j++)
    //         {
    //             print(shape[i, j]);
    //         }
    //     }
    // }
    
}
