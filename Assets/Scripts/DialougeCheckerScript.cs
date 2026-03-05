using UnityEngine;

public class DialougeCheckerScript : MonoBehaviour
{
    [SerializeField] private DialougeSystemScript DialougeSystemScript;
    [SerializeField] private LevelLoaderScript LevelLoaderScript;
    [SerializeField] private PlayerControlsScript playerControlsScript;

    void Start()
    {
        DialougeSystemScript.BeginDialouge();
        if (playerControlsScript != null)
        {
            playerControlsScript.CanPlay = false;
        }
    }

    void Update()
    {
        if( DialougeSystemScript.EndDialouge == true)
        {
            if (playerControlsScript == null)
            {
                LevelLoaderScript.TimeToGo = true;
            }
            else
            {
                playerControlsScript.CanPlay = true;
            }
        }
    }
}
