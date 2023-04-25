using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Level0");
    }
}
