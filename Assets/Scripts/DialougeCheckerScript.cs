using UnityEngine;

public class DialougeCheckerScript : MonoBehaviour
{
    [SerializeField] private DialougeSystemScript DialougeSystemScript;
    [SerializeField] private LevelLoaderScript LevelLoaderScript;

    void Start()
    {
        DialougeSystemScript.BeginDialouge();
    }

    void Update()
    {
        if( DialougeSystemScript.EndDialouge == true)
        {
            LevelLoaderScript.TimeToGo = true;
        }
    }
}
