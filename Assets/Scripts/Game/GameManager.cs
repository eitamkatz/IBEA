using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Shared { get; private set; }
    [SerializeField] private int level;
    [SerializeField] private Levels levels;
    [SerializeField] private GameObject player;
    [SerializeField] private LoadingScreen loadingScreen;
    private int[,] _goalShape;
    private int _levelSquareCount;
    private int _levelTime;


    public bool WinCondition { get; set; }
    
    private void Awake()
    {
        Shared = this;
    }
    private void Start()
    {
        InitializeLevel();
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene("Level" + level);
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
    private void InitializeLevel()
    {
        StartCoroutine(loadingScreen.LoadLoadingScreen());
        _goalShape = levels.GetLevelShape(level);
        _levelSquareCount = levels.GetLevelNumOfSquares(level);
    }
    
    /*
     * loads game over scene
     */
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    /*
     * checks if the player has won the level
     */
    public void CheckWinCondition()
    {
        if (CheckShapeMatch())
        {
            if (level == 5)
                SceneManager.LoadScene("Winning");
            else
            {
                level++;
                SceneManager.LoadScene("Level" + level);
            }
            InitializeLevel();
        }
    }
    
    /*
     * check if the player's shape matches the goal shape
     */
    public bool CheckShapeMatch()
    {
        List<Vector2> playerShape = player.GetComponent<Player>().Get_Player_shape();
        if (playerShape.Count != _levelSquareCount) return false;
        for (int row = 0; row < _goalShape.GetLength(0); row++)
        {
            for (int col = 0; col < _goalShape.GetLength(1); col++)
            {
                Vector4 shapeLimits = player.GetComponent<Player>().GetShapeLimits();
                Vector2 relativePos = new Vector2(shapeLimits.x + col, shapeLimits.w - row);
                if (_goalShape[row, col] == 0 && playerShape.Contains(relativePos)) return false;
                if (_goalShape[row, col] == 1 && !playerShape.Contains(relativePos)) return false;
            }
        }
        return true;
    }
}
