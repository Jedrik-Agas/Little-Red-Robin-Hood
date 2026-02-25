using UnityEngine;

public class DialougeCheckerScript : MonoBehaviour
{
    [SerializeField] private DialougeSystemScript DialougeSystemScript;
    [SerializeField] private LevelLoaderScript LevelLoaderScript;

    // Update is called once per frame
    void Update()
    {
        if( DialougeSystemScript.EndDialouge == true)
        {
            LevelLoaderScript.TimeToGo = true;
        }
    }
}
