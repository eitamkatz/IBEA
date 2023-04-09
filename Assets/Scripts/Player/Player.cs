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
    private float timer;
    [SerializeField] float speed = 0.05f;
    private bool isMooving;
    private Vector3 orignalPosition;
    private Vector3 targetPosition;
    private float timeToMove = 0.2f;

    private void Awake()
    {
        Shared = this;
    }

    private void Update()
    {
        CheckInput();
        RotationPlayer();
    }
    
    public void MergeShapes(GameObject toMerge)
    {
        // merge the new game object to the player
        for (int i = 0; i < toMerge.transform.childCount; i++)
        {
            toMerge.transform.GetChild(i).tag = "Player";
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
        
        if(!isMooving)
            StartCoroutine(UpdateMovement(direction));
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

    private void RotationPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float newZ = transform.rotation.eulerAngles.z - 90f;
            transform.rotation = Quaternion.Euler(0f, 0f, newZ);
        }
    }
}
