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
            StartCoroutine(
                UpdatePosition(gameObject.transform.parent.gameObject));
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(transform.parent.gameObject);
            // StartCoroutine(DestroySquare());
        }
        else if (other.CompareTag("restart"))
        {
            GameManager.Shared.RestartLevel();
        }
    }
    
    private IEnumerator DestroySquare()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        // gameObject.SetActive(false);
        yield return new WaitForSeconds(PARTICLE_DELAY);
        // Player.Shared._numOfSquares--;
        if(transform.parent.gameObject)
            Destroy(transform.parent.gameObject);
    }
    
    private IEnumerator UpdatePosition(GameObject parent)
    {
        yield return new WaitForSeconds(0.03f);
        parent.transform.localPosition = new Vector3(
            Mathf.Round(parent.transform.localPosition.x),
            Mathf.Round(parent.transform.localPosition.y), 0f);
    }

    
}
