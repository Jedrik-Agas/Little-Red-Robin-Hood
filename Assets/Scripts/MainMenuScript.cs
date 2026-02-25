using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] LevelLoaderScript levelLoaderScript;
    public void StartGame()
    {
        levelLoaderScript.TimeToGo = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
