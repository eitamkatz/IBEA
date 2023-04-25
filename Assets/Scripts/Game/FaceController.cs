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
        {
            faceImage.sprite = faceSprites[0];
            return;
        }
        else if (direction.x > 0 && direction.y == 0)
        {
            faceImage.sprite = faceSprites[1];
            return;
        }
        else if (direction.x < 0 && direction.y == 0)
        {
            faceImage.sprite = faceSprites[2];
            return;
        }
        else if (direction.y > 0 && direction.x == 0)
        {
            faceImage.sprite = faceSprites[3];
            return;
        }
        else if (direction.y < 0 && direction.x == 0)
        {
            faceImage.sprite = faceSprites[4];
            return;
        }
    }
}
