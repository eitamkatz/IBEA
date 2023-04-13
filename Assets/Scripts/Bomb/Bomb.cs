using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Bomb : MonoBehaviour
{
    private const float DELAY_DESTROY = 0.2f;
    private float _timer;
    private float _dieTime = 10f;
    private void Start()
    {
        _timer = Time.time;
    }

    private void Update()
    {
        if (Time.time - _timer > _dieTime && !transform.parent)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Player"))
        {
            StartCoroutine(DestroyBomb());
        }
        else if (col.CompareTag("Block"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBomb()
    {
        // GetComponent<CircleCollider2D>().radius = Random.Range(1f, 1f);
        GetComponent<SpriteRenderer>().enabled = false;
        
        yield return new WaitForSeconds(DELAY_DESTROY);
        Destroy(gameObject);
    }
}