using System.Collections;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Level1Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite connectTutorial;
    [SerializeField] private Sprite rotationTutorial;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private Player player;
    private int _tutorialStage = 0;

    private void Start()
    {
        StartCoroutine(ConnectTutorial());
    }

    private void Update()
    {
        if (_tutorialStage == 1 && player.PlayerShape.Count > 1)
        {
            StartCoroutine(RotationTutorial());
        }

        if (_tutorialStage == 2 && Input.GetKey(KeyCode.Space))
        {
            tutorialImage.color = Color.clear;
        }
    }

    IEnumerator ConnectTutorial()
    {
        tutorialImage.sprite = connectTutorial;
        tutorialImage.color = Color.white;
        _tutorialStage++;
        yield return 0;
    }

    IEnumerator RotationTutorial()
    {
        tutorialImage.sprite = rotationTutorial;
        tutorialImage.color = Color.white;
        _tutorialStage++;
        yield return 0;
    }
}