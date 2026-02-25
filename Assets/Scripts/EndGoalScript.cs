using UnityEngine;

public class EndGoalScript : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;              
    [SerializeField]private LevelLoaderScript levelLoaderScript;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            levelLoaderScript.TimeToGo = true;
        }
    }
}
