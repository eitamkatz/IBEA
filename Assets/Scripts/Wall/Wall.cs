using UnityEngine;

public class Wall : MonoBehaviour
{
    // private Player _player;
    // private Collider2D _collider2D;
    // void Start()
    // {
    //     _player = GameObject.Find("Player").GetComponent<Player>();
    //     _collider2D = GetComponent<Collider2D>();
    // }
    //
    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     Vector2 wallPos = col.GetContact(0).normal;
    //     wallPos.x = (float)Math.Round(wallPos.x);
    //     wallPos.y = (float)Math.Round(wallPos.y);
    //     print(wallPos);
    //     if (!_player.Walls.Contains(wallPos))
    //     {
    //         _player.Walls.Add(wallPos);
    //     }
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     _player.Walls.Clear();
    // }
}
