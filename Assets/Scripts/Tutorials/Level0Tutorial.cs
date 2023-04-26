using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class Level0Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite welcomeTutorial;
    [SerializeField] private Sprite arrowsTutorial;
    [SerializeField] private Image tutorialImage;
    private int _tutorialStage = 0;

    private void Start()
    {
        StartCoroutine(ArrowsTutorial());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (_tutorialStage == 2)
                tutorialImage.color = Color.clear;
        }
    }

    private IEnumerator ArrowsTutorial()
    {
        tutorialImage.sprite = welcomeTutorial;
        tutorialImage.color = Color.white;
        _tutorialStage++;
        yield return new WaitForSeconds(4f);
        tutorialImage.sprite = arrowsTutorial;
        _tutorialStage++;
    }
}