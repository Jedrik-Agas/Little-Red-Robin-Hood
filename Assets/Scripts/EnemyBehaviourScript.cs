using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{

    private float Speed = 2f;
    private bool SeesPlayer = false; 
    private GameObject Target;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (SeesPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D Ray = Physics2D.Raycast(transform.position, Target.transform.position - transform.position);
        if (Ray.collider != null)
        {
            SeesPlayer = Ray.collider.gameObject.CompareTag("Player");
            if(SeesPlayer)
            {
                Debug.DrawRay(transform.position, Target.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, Target.transform.position - transform.position, Color.red);
            }
        }
    }
}
