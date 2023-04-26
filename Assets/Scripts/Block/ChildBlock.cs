using System.Collections;
using UnityEngine;

public class ChildBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !Player.Shared.GetInRotation())
        {
            Player.Shared.MergeShapes(gameObject.transform.parent.gameObject);
            StartCoroutine(
                UpdatePosition(gameObject.transform.parent.gameObject));
        }
        else if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(transform.parent.gameObject);
        }
        else if (other.CompareTag("restart"))
        {
            GameManager.Shared.RestartLevel();
        }
    }

    /* 
     * Rounds the position of the parent object to the nearest integer after it merge to the player
     */
    private IEnumerator UpdatePosition(GameObject parent)
    {
        yield return new WaitForSeconds(0.03f);
        parent.transform.localPosition = new Vector3(
            Mathf.Round(parent.transform.localPosition.x),
            Mathf.Round(parent.transform.localPosition.y), 0f);
    }
}