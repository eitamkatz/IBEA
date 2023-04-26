using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class FaceController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Sprite[] faceSprites; // [idle, right, left, up, down]
    [SerializeField] private Image faceImage;

    private void Update()
    {
        Vector2 direction = player.movePoint.position - player.transform.position;
        if (direction == Vector2.zero)
            faceImage.sprite = faceSprites[0];
        else if (direction.x > 0 && direction.y == 0)
            faceImage.sprite = faceSprites[1];
        else if (direction.x < 0 && direction.y == 0)
            faceImage.sprite = faceSprites[2];
        else if (direction.y > 0 && direction.x == 0)
            faceImage.sprite = faceSprites[3];
        else if (direction.y < 0 && direction.x == 0)
            faceImage.sprite = faceSprites[4];
        if (Math.Abs(player.transform.position.x - 0.5f) < 0.5f ||
            Math.Abs(player.transform.position.y - 0.5f) < 0.5f)
            faceImage.sprite = faceSprites[0];
    }
}
