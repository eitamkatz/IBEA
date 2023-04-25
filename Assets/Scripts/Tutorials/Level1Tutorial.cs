using System.Collections;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Level1Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite connectTutorial;
    [SerializeField] private Sprite bombTutorial;
    [SerializeField] private Sprite rotationTutorial;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private Player player;
    [SerializeField] private GameObject bomb;
    private int tutorialStage = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ConnectTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialStage == 1 && player.PlayerShape.Count > 1)
        {
            StartCoroutine(RotationTutorial());
        }
        // if (tutorialStage == 2 && player.PlayerShape.Count == 1)
        // {
        //     StartCoroutine(RotationTutorial());
        // }
        if (tutorialStage == 2 && Input.GetKey(KeyCode.Space))
        {
            tutorialImage.color = Color.clear;
        }
    }
    
    IEnumerator ConnectTutorial()
    {
        tutorialImage.sprite = connectTutorial;
        tutorialImage.color = Color.white;
        tutorialStage++;
        yield return 0;
    }
    
    // IEnumerator BombTutorial()
    // {
    //     tutorialImage.sprite = bombTutorial;
    //     tutorialImage.color = Color.white;
    //     bomb.active = true;
    //     tutorialStage++;
    //     yield return 0;
    // }
    
    IEnumerator RotationTutorial()
    {
        tutorialImage.sprite = rotationTutorial;
        tutorialImage.color = Color.white;
        tutorialStage++;
        yield return 0;
    }
}
