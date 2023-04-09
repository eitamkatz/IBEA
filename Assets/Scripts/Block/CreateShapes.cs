using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateShapes : MonoBehaviour
{
    private int radius;
    private int minGap = 3;
    private int maxGap = 5;
    private float timer;

    private void Start()
    {
        timer = Time.time;
    }

    private void Update()
    {
        if (Random.Range(minGap,maxGap) < Time.time - timer)
        {
            ShapePool.Shared.Get();
            timer = Time.time;
        }
    }
}
