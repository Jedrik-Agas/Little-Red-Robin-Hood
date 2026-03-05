using UnityEngine;

public class TargetCheckerScript : MonoBehaviour
{
    [SerializeField] LevelLoaderScript LevelLoaderScript;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            LevelLoaderScript.TimeToGo = true;
        }
    }
}
