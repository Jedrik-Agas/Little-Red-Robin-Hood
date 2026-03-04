using UnityEngine;
using UnityEngine.VFX;

public class EnemyBehaviourScript : MonoBehaviour
{

    private Rigidbody2D RB;
    private GameObject Target;
    [SerializeField] private Transform FieldOfViewPrefab;
    private FieldOfViewScript FieldOfViewScript;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player");
        FieldOfViewScript = Instantiate(FieldOfViewPrefab,null).GetComponent<FieldOfViewScript>();
    }

    void Update()
    {
        FieldOfViewScript.SetOrigin(RB.transform.position);
        FieldOfViewScript.SetAimDirection(RB.transform.forward);
        Vector2 TargetDirection = Target.transform.position - RB.transform.position;
        if (Vector3.Distance(RB.transform.position, Target.transform.position) < FieldOfViewScript.ViewDistance)
        {
            //if (Vector3.Angle(RB.transform.forward, TargetDirection) < FieldOfViewScript.FOV / 2f)
            {
                print("DETECTED");
            }
        }
    }

    private void FixedUpdate()
    {

    }
}
