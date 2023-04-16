using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using Cinemachine.Utility;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    public static Player Shared { get; private set; }
    public Vector2 direction = Vector2.zero;
    // public int squareCount = 1;
    public bool winCheck = false;
    // public bool endOfLevel = false;
    [SerializeField] private float speed = 0.05f;
    private bool _isMooving;
    private Vector3 _orignalPosition;
    private Vector3 _targetPosition;
    private float _timeToMove = 0.2f;
    private float _timer;
    private bool isWalled;

    public int _numOfSquares;
    private int _maxX;
    private int _minX;
    private int _maxY;
    private int _minY;
    private List<Vector2> _playerShape;
    
    [SerializeField]
    private float rayLength = 0.8f;

    [SerializeField]
    private LayerMask wallLayer = default;
    [SerializeField]
    private LayerMask checkLayer = default;


    private void Awake()
    {
        Shared = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        _numOfSquares = 1;
        _maxX = 0;
        _minX = 0;
        _maxY = 0;
        _minY = 0;
        _playerShape = new List<Vector2>() {new Vector2(0f,0f)};
    }
    
    private void Update()
    {
        CheckInput();
        // RotationPlayer();
        isWalled = IsTouchingWall();
        winCheck = IsTouchingCheck();
    }

    public void MergeShapes(GameObject toMerge)
    {
        // merge the new game object to the player
        for (int i = 0; i < toMerge.transform.childCount; i++)
        {
            toMerge.transform.GetChild(i).tag = "Player";
            float posX = (float)Math.Round(toMerge.transform.GetChild(i).position.x - transform.position.x);
            float posY = (float)Math.Round(toMerge.transform.GetChild(i).position.y - transform.position.y);
            Merge(new Vector2(posX, posY));
            // squareCount++;
        }
        toMerge.tag = "Player";
        toMerge.transform.SetParent(transform);
    }

    private void CheckInput()
    {
        if (isWalled) 
            direction = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RotationPlayer(direction));
            direction = Vector2.zero;
        }
        if (!_isMooving)
            StartCoroutine(UpdateMovement(direction));
    }

    private IEnumerator RotationPlayer(Vector2 prevDirection)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, 0f, -90f);
        float rotateTime = 0.3f; // Time to complete the rotation (in seconds)
        float elapsedTime = 0f;

        while (elapsedTime < rotateTime)
        {
            float t = elapsedTime / rotateTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        direction = prevDirection;
        transform.rotation = endRotation;
    }

    private IEnumerator UpdateMovement(Vector3 moveDirection)
    {
        if (Time.time - _timer > speed)
            _isMooving = true;
        float loopTime = 0f;

        _orignalPosition = transform.position;
        _targetPosition = _orignalPosition + moveDirection;

        while (loopTime < _timeToMove)
        {
            transform.position = Vector3.Lerp(_orignalPosition, _targetPosition,
                loopTime / _timeToMove);
            loopTime += Time.deltaTime;
            yield return null;
        }
        transform.position = _targetPosition;
        _isMooving = false;
        // if (winCheck)
        //     endOfLevel = true;
    }



    //target- array of the target shape 
    //n- the length of the target shape
    //dstSquareCount- the number of squares on the target shape
    // public bool FinalShape(int[,] target, int dstSquareCount)
    // {
    //     int n = target.GetLength(0);
    //     if (dstSquareCount != squareCount) return false;
    //     // initialize the indexes from the center to the top left corner
    //     int xIndex = -n / 2 + 1;
    //     int yIndex = -n / 2 + 1;
    //     //moving on all the connects shapes
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         Transform currentChild = transform.GetChild(i);
    //         xIndex = (int)currentChild.localPosition.x;
    //         yIndex = (int)currentChild.localPosition.y;
    //         // moving on all the squares of the current shape
    //         for (int j = 0; j < currentChild.childCount; j++)
    //         {
    //             Transform currentSquare = currentChild.GetChild(j);
    //             Vector2 index = currentSquare.localPosition;
    //             xIndex += (int)index.x;
    //             yIndex += (int)index.y;
    //
    //             if (!MatchCheck(xIndex, yIndex, n - 1, target[xIndex, yIndex]))
    //                 return false;
    //         }
    //     }
    //
    //     return true;
    // }

    // private bool MatchCheck(int xIndex, int yIndex, int n, int hasSquare)
    // {
    //     // return false if the currentSquare location doesnt match the target shape or the current square is off limits
    //     return !(xIndex < 0 || xIndex > n || yIndex < 0 || yIndex > n || hasSquare == 0);
    // }

    public void NewLevel()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        transform.position = new Vector3(0f, 0f, 0f);
        print(transform.position);
    }
    
    private void Merge(Vector2 position)
    {
        if (position.x > _maxX) _maxX = (int)position.x;
        if (position.x < _minX) _minX = (int)position.x ;
        if (position.y > _maxY) _maxY = (int)position.y;
        if (position.y < _minY) _minY = (int)position.y;
        _playerShape.Add(position);
        _numOfSquares++;
    }
    
    public bool CheckShapeMatch(int[,] goalShape, int goalNumOfSquares)
    {
        print("start of check");
        if (_numOfSquares != goalNumOfSquares)
        {
            print("num of squares dont match" + _numOfSquares + " " + goalNumOfSquares);
            return false;
        }

        for (int row = 0; row < goalShape.GetLength(0); row++)
        {
            for (int col = 0; col < goalShape.GetLength(1); col++)
            {
                Vector2 vec = new Vector2(col + _minX, _maxY - row);
                if (goalShape[row, col] == 0 && _playerShape.Contains(vec))
                {
                    print("2 " + row + " " + col);
                    return false;
                }
                if (goalShape[row, col] == 1 && !_playerShape.Contains(new Vector2(col + _minX, _maxY - row)))
                {
                    print("3 " + row + " " + col );
                    return false;
                }
            }
        }
        NewLevel();
        return true;
    }
    

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, direction * rayLength, Color.magenta);
        Gizmos.color = winCheck  ? Color.yellow : Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

    private bool IsTouchingWall()
    {
        Vector3 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, wallLayer);
        return hit.collider != null;
    }
    
    private bool IsTouchingCheck()
    {
        Vector3 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, checkLayer);
        return hit.collider != null;
    }
}
