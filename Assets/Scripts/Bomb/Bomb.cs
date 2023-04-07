using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Bomb : MonoBehaviour
{
    private const float DELAY_DESTROY = 0.2f;
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.transform.CompareTag("Player"))
        {
            StartCoroutine(DestroyBomb());
        }
    }

    private IEnumerator DestroyBomb()
    {
        GetComponent<CircleCollider2D>().radius *= Random.Range(1f, 2f);
        GetComponent<SpriteRenderer>().enabled = false;
        
        yield return new WaitForSeconds(DELAY_DESTROY);
        Destroy(gameObject);
    }
}