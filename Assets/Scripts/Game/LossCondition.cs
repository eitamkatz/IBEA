using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossCondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Player.Shared.winCheck)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
