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
    private Vector2 direction = Vector2.zero;
    // private bool flag = true;
    private float timer;
    [SerializeField] float speed = 0.05f;
    // [SerializeField] private float speed = 1f;

    private void Start()
    {
        timer = Time.time;
    }

    private void Update()
    {
        CheckInput();
        RotationPlayer();
    }

    private void FixedUpdate()
    {

        UpdateDirection();
        // StartCoroutine(UpdateDirection());
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
        // else 
        //     direction = Vector2.zero;
    }

    private void UpdateDirection()
    {
        if (Time.time - timer > speed)
        {
            Vector3 position = new Vector3(
                Mathf.Round(transform.position.x) + direction.x,
                Mathf.Round(transform.position.y) + direction.y, 0.0f);
            transform.position = position;
            timer = Time.time;
        }
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
