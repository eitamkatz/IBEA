using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;

    void Update()
    {
        CheckInput();
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
        else 
            direction = Vector2.zero;
        transform.Translate(direction);
    }
}
