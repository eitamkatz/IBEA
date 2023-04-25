using UnityEngine;

public class CreateBombs : MonoBehaviour
{
    private int minGap = 5;
    private int maxGap = 11;
    private float timer;

    private void Start()
    {
        timer = Time.time;
    }

    private void Update()
    {
        if (Random.Range(minGap,maxGap) < Time.time - timer)
        {
            BombPool.Shared.Get();
            timer = Time.time;
        }
    }
}
