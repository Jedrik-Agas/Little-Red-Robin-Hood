using UnityEngine;
using UnityEngine.VFX;

public class EnemyBehaviourScript : MonoBehaviour
{

    private Rigidbody2D RB;
    private GameObject Target;
    [SerializeField] private LevelLoaderScript LevelLoaderScript;
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
        FieldOfViewScript.SetAimDirection(RB.transform.up);
        FieldOfViewScript.SetViewer(gameObject);
        Vector2 TargetDirection = Target.transform.position - RB.transform.position;
        if (Vector3.Distance(RB.transform.position, Target.transform.position) < FieldOfViewScript.ViewDistance)
        {
            float angle = Vector2.Angle(-RB.transform.up, TargetDirection);
            if (angle <= FieldOfViewScript.FOV/2)
            {
                LevelLoaderScript.NextLevelName = "Stealth Section";
                LevelLoaderScript.TimeToGo = true;
                print("DETECTED");
            }
        }
    }
}
