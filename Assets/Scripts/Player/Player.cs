using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Numerics;
using System.Security.Cryptography;
using Cinemachine.Utility;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

public class Player : MonoBehaviour
{
    public static Player Shared { get; private set; }
    public List<Vector2> PlayerShape { get; private set; }
    public Vector2 direction;
    public List<Vector2> walls;
    [SerializeField] public Transform movePoint;
    [SerializeField] private float speed = 5f;
    private bool[] _canMove = { true, true, true, true };
    private bool _inRotation;
    private Vector4 _shapeLimits; // (minX, maxX, minY, maxY)


    private void Awake()
    {
        Shared = this;
    }

    private void Start()
    {
        walls = new List<Vector2>();
        _shapeLimits = new Vector4();
        PlayerShape = new List<Vector2>() { new Vector2(0f, 0f) };
        movePoint.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            movePoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <=
            0.05f && !_inRotation)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));
            if (_canMove[0] == false && input.x.Equals(1))
                input.x = 0;
            else if (_canMove[1] == false && input.x.Equals(-1))
                input.x = 0;
            else if (_canMove[2] == false && input.y.Equals(1))
                input.y = 0;
            else if (_canMove[3] == false && input.y.Equals(-1))
                input.y = 0;

            if (!Math.Abs(input.x).Equals(0))
                movePoint.position += new Vector3(input.x, 0f, 0f);
            else if (!Math.Abs(input.y).Equals(0))
                movePoint.position += new Vector3(0f, input.y, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_inRotation)
        {
            _inRotation = true;
            StartCoroutine(RotationPlayer(direction));
            direction = Vector2.zero;
        }
    }

    public List<Vector2> Get_Player_shape()
    {
        return PlayerShape;
    }

    public bool GetInRotation()
    {
        return _inRotation;
    }

    /*
     * merge the new game object to the player if the player isn't while rotation
     */
    public void MergeShapes(GameObject toMerge)
    {
        if (!_inRotation)
        {
            // merge the new game object to the player
            for (int i = 0; i < toMerge.transform.childCount; i++)
            {
                toMerge.transform.GetChild(i).tag = "Player";
                float posX = (float)Math.Round(
                    toMerge.transform.GetChild(i).position.x -
                    transform.position.x);
                float posY = (float)Math.Round(
                    toMerge.transform.GetChild(i).position.y -
                    transform.position.y);
                Merge(new Vector2(posX, posY));
            }

            toMerge.tag = "Player";
            toMerge.transform.SetParent(transform);
        }
    }

    /*
     * Rotate the player shape
     */
    private IEnumerator RotationPlayer(Vector2 prevDirection)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation =
            startRotation * Quaternion.Euler(0f, 0f, -90f);
        float rotateTime = 0.3f; // Time to complete the rotation (in seconds)
        float elapsedTime = 0f;

        while (elapsedTime < rotateTime)
        {
            float t = elapsedTime / rotateTime;
            transform.rotation =
                Quaternion.Lerp(startRotation, endRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        RotatePlayerShape();
        direction = prevDirection;
        transform.rotation = endRotation;
        _inRotation = false;
    }

    /*
     * Limit the movement of the player to the frame of the grid
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Right"))
            _canMove[0] = false;
        else if (other.CompareTag("Left"))
            _canMove[1] = false;
        else if (other.CompareTag("Up"))
            _canMove[2] = false;
        else if (other.CompareTag("Down"))
            _canMove[3] = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Right"))
            _canMove[0] = true;
        else if (other.CompareTag("Left"))
            _canMove[1] = true;
        else if (other.CompareTag("Up"))
            _canMove[2] = true;
        else if (other.CompareTag("Down"))
            _canMove[3] = true;
    }
    /*
     * add the coordinates of the added shape to the PlayerShape of the player
     */
    private void Merge(Vector2 position)
    {
        if (position.x > _shapeLimits.y) _shapeLimits.y = (int)position.x;
        if (position.x < _shapeLimits.x) _shapeLimits.x = (int)position.x;
        if (position.y > _shapeLimits.w) _shapeLimits.w = (int)position.y;
        if (position.y < _shapeLimits.z) _shapeLimits.z = (int)position.y;
        if (!PlayerShape.Contains(position)) PlayerShape.Add(position);
    }

    /*
     * update the coordinates of the PlayerShape of the player while rotation
     */
    private void RotatePlayerShape()
    {
        List<Vector2> newM = new List<Vector2>();
        foreach (var spot in PlayerShape)
        {
            newM.Add(new Vector2(spot.y, -spot.x));
        }

        Vector4 newLimits = new Vector4(_shapeLimits.z, _shapeLimits.w,
            -_shapeLimits.y, -_shapeLimits.x);
        _shapeLimits = newLimits;
        PlayerShape = newM;
    }

    /*
     * return the limits of the player shape
     */
    public Vector4 GetShapeLimits()
    {
        return _shapeLimits;
    }
}