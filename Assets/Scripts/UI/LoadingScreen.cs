using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingScreenImage;
    [SerializeField] private Sprite[] loadingScreens;
    [SerializeField] private Image[] boxes;

    public IEnumerator LoadLoadingScreen()
    {
        int index = Random.Range(0, loadingScreens.Length);
        loadingScreenImage.sprite = loadingScreens[index];
        loadingScreenImage.enabled = true;
        loadingScreenImage.color = Color.white;
        foreach (var box in boxes)
        {
            box.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        foreach (var box in boxes)
        {
            box.color = Color.clear;
        }
        loadingScreenImage.color = Color.clear;
        loadingScreenImage.enabled = false;
    }
}
