using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    public Animator animator;

    public float TransitionTime = 1f;

    public string NextLevelName;

    public bool TimeToGo = false;


    void Update()
    {
        if (TimeToGo == true)
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(NextLevelName);
    }

}
