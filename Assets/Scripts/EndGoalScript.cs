using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using static DialougeSystemScript;

public class EndGoalScript : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;              
    [SerializeField]private LevelLoaderScript levelLoaderScript;
    [SerializeField ]private CinemachineCamera virtualCamera;
    [SerializeField] private PlayerControlsScript playerControlsScript;

    void Start()
    {
        playerControlsScript.CanPlay = false;
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(ShowGoal());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelLoaderScript.TimeToGo = true;
        }
    }

    private IEnumerator ShowGoal()
    {
        yield return new WaitForSeconds(0.1f);
        virtualCamera.Priority = 2;
        yield return new WaitForSeconds(3f);
        virtualCamera.Priority = 0;
        yield return new WaitForSeconds(2f);
        playerControlsScript.CanPlay = true;
    }
}
