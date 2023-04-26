using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class Level0Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite welcomeTutorial;
    [SerializeField] private Sprite arrowsTutorial; 
    [SerializeField] private Image tutorialImage;
    private int tutorialStage = 0;
    


    void Start()
    {
        StartCoroutine(ArrowsTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (tutorialStage == 2)
                tutorialImage.color = Color.clear;
        }
    }

    IEnumerator ArrowsTutorial()
    {
        tutorialImage.sprite = welcomeTutorial;
        tutorialImage.color = Color.white;
        tutorialStage++;
        yield return new WaitForSeconds(8f);
        tutorialImage.sprite = arrowsTutorial; 
        tutorialStage++;
    }
}
