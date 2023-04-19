using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BombPool : MonoBehaviour
{
    public static BombPool Shared { get; private set; }
    [SerializeField] private Bomb template = default;
    private Transform _playerPosition;
    private ObjectPool<Bomb> _pool;
    private Vector3 _startPosition;
    private float minGapX = 0f;
    private float maxGapX = 7f;
    private float minGapY = 0f;
    private float maxGapY = 10f;
    
    private void Awake()
    {
        Shared = this;
        _pool = new ObjectPool<Bomb>(Create, OnActivate,
            OnDeactivate);
    }

    private void Start()
    {
        _playerPosition = transform;
        _startPosition = transform.position;
        DontDestroyOnLoad(this);
    }

    public Bomb Get()
    {
        return _pool.Get();
    }

    public void Release(Bomb bomb)
    {
        if (bomb)
            _pool.Release(bomb);
    }

    public void ReleaseAll()
    {
        _pool.Dispose();
    }

    private Bomb Create()
    {
        Bomb bomb = Instantiate(template);
        bomb.gameObject.SetActive(false);
        return bomb;
    }

    private void OnActivate(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
       
        float a = Random.Range(minGapX, maxGapX);
        float b = Random.Range(minGapY, maxGapY);
        _startPosition = GetRandomPosition(a, b);
        bomb.transform.position = _startPosition;
    }

    public Vector3 GetRandomPosition(float a, float b)
    {
        float xPosition = _playerPosition.position.x;
        float yPosition = _playerPosition.position.y;
        float angle = Random.Range(0f, 360f);
        float x = xPosition + a * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = yPosition + b * Mathf.Sin(angle * Mathf.Deg2Rad);
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        return new Vector3(x, y, 0);
    }


    private void OnDeactivate(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
    }
}
