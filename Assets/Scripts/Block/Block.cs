using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float timer;
    private float dieTime = 10f;
    private void Start()
    {
        timer = Time.time;
    }

    private void Update()
    {
        if (Time.time - timer > dieTime && !transform.parent)
        {
            Destroy(gameObject);
        }
    }
    
}
