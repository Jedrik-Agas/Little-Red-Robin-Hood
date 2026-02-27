using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Collider2D boxcollider;

    [SerializeField] DialougeSystemScript dialougeSystemScript;
    [SerializeField] PlayerControlsScript playerControlsScript;
    [SerializeField] LevelLoaderScript levelLoaderScript;

    void Start()
    {
        boxcollider = GetComponent<Collider2D>();
        playerControlsScript.CanPlay = false;
        dialougeSystemScript.BeginDialouge();
    }

    // Update is called once per frame
    void Update()
    {
      if (dialougeSystemScript.EndDialouge == true)
      {
            playerControlsScript.CanPlay = true;
      }
      else
      {
            playerControlsScript.CanPlay = false;
      }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
          Destroy(gameObject);
          levelLoaderScript.TimeToGo = true;
        }
    }
}
