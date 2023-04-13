using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    public int squareCount = 1;
    [SerializeField] float speed = 0.05f;
    private bool isMooving;
    private Vector3 orignalPosition;
    private Vector3 targetPosition;
    private float timeToMove = 0.2f;
    private float timer;
    private Vector2[] corners;
    
    private void Awake()
    {
        Shared = this;
    }

    private void Update()
    {
        CheckInput();
        // RotationPlayer();
    }
    
    public void MergeShapes(GameObject toMerge)
    {
        // merge the new game object to the player
        for (int i = 0; i < toMerge.transform.childCount; i++)
        {
            toMerge.transform.GetChild(i).tag = "Player";
            squareCount++;
        }
        toMerge.tag = "Player";
        toMerge.transform.SetParent(transform);
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
            direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector2.left;
        // up and down movement for testing
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RotationPlayer(direction));
            direction = Vector2.zero;
        }
        if(!isMooving)
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
        if (Time.time - timer > speed)
            isMooving = true;
        float loopTime = 0f;

        orignalPosition = transform.position;
        targetPosition = orignalPosition + moveDirection;

        while (loopTime < timeToMove)
        {
            transform.position = Vector3.Lerp(orignalPosition, targetPosition,
                loopTime / timeToMove);
            loopTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isMooving = false;
    }



    //target- array of the target shape 
    //n- the length of the target shape
    //dstSquareCount- the number of squares on the target shape
    public bool FinalShape(int[,] target, int n, int dstSquareCount)
    {
        if (dstSquareCount != squareCount) return false;
        // initialize the indexes from the center to the top left corner
        int xIndex = - n / 2 + 1;
        int yIndex = - n / 2 + 1;
        //moving on all the connects shapes
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform currentChild = transform.GetChild(i);
            xIndex = (int)currentChild.localPosition.x;
            yIndex = (int)currentChild.localPosition.y;
            // moving on all the squares of the current shape
            for (int j = 0; j < currentChild.childCount; j++)
            {
                Transform currentSquare = currentChild.GetChild(j);
                Vector2 index = currentSquare.localPosition;
                xIndex += (int)index.x;
                yIndex += (int)index.y;
                
                if (!MatchCheck(xIndex, yIndex, n-1, target[xIndex, yIndex]))
                    return false;
            }
        }
        return true;
    }

    private bool MatchCheck(int xIndex, int yIndex, int n, int hasSquare)
    {    
        // return false if the currentSquare location doesnt match the target shape or the current square is off limits
        return !(xIndex < 0 || xIndex > n || yIndex < 0 || yIndex > n || hasSquare == 0);
    }
    
}
