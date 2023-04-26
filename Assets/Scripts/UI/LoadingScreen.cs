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
    [SerializeField] private AudioSource audioManager;
    [SerializeField] private AudioSource music;

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this);
    // }

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    public IEnumerator LoadLoadingScreen()
    {
        // audioManager.volume = 0.5f;
        // music.Pause();
        int index = Random.Range(0, loadingScreens.Length);
        loadingScreenImage.sprite = loadingScreens[index];
        loadingScreenImage.color = Color.white;
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].color = Color.clear;
        }
        loadingScreenImage.color = Color.clear;
        // audioManager.volume = 0.34f;
        // music.Play();
    }
}
