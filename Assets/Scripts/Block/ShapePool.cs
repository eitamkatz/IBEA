using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShapePool : MonoBehaviour
{
    public static ShapePool Shared { get; private set; }
    [SerializeField] private Block[] template = default;
    private Transform _playerPosition;
    private ObjectPool<Block> _pool;
    private Vector3 _startPosition;
    private float minGapX = 3f;
    private float maxGapX = 13f;
    private float minGapY = 7f;
    private float maxGapY = 15f;
    private int count = 0;
    
    private void Awake()
    {
        Shared = this;
        _pool = new ObjectPool<Block>(Create, OnActivate,
            OnDeactivate);
    }

    private void Start()
    {
        _playerPosition = transform;
        _startPosition = transform.position;
        DontDestroyOnLoad(this);
    }

    public Block Get()
    {
        return _pool.Get();
    }

    public void Release(Block shape)
    {
        if (shape)
            _pool.Release(shape);
    }

    public void ReleaseAll()
    {
        _pool.Dispose();
    }

    private Block Create()
    {
        Block shape = Instantiate(template[count == template.Length ? Random.Range(0,template.Length) : count++]);
        shape.gameObject.SetActive(false);
        return shape;
    }

    private void OnActivate(Block shape)
    {
        shape.gameObject.SetActive(true);
       
        float a = Random.Range(minGapX, maxGapX);
        float b = Random.Range(minGapY, maxGapY);
        _startPosition = GetRandomPosition(a, b);
        shape.transform.position = _startPosition;
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
        return new Vector2(x, y);
    }


    private void OnDeactivate(Block shape)
    {
        shape.gameObject.SetActive(false);
    }
}
