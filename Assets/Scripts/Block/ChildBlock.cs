using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBlock : MonoBehaviour
{
    private const float PARTICLE_DELAY = 1f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Player.Shared.MergeShapes(gameObject.transform.parent.gameObject);
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            StartCoroutine(DestroySquare());
        }
    }
    
    private IEnumerator DestroySquare()
    {
        print(transform.name);
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        // gameObject.SetActive(false);
        yield return new WaitForSeconds(PARTICLE_DELAY);
        Destroy(gameObject);
    }
}
