using UnityEngine;

public class Check : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.WinCondition = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("Check")) _gameManager.WinCondition = true;
        if (CompareTag("InvisibleCheck")) CheckEnter();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CompareTag("Check")) _gameManager.WinCondition = false;
    }

    /*
     * check if the player has entered the invisible check
     */
    private void CheckEnter()
    {
        if (_gameManager.CheckShapeMatch()) GetComponent<Collider2D>().enabled = false;
    }
}
