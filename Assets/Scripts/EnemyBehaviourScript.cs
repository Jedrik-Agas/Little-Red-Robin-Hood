using UnityEngine;
using UnityEngine.VFX;

public class EnemyBehaviourScript : MonoBehaviour
{

    private Rigidbody2D RB;
    private GameObject Target;
    public float ESpeed;
    public float BeginingRotation; 
    public float[] ERotation;
    private int TargetPoint;
    [SerializeField] private Transform[] PatrolPoints;
    [SerializeField] private LevelLoaderScript LevelLoaderScript;
    [SerializeField] private Transform FieldOfViewPrefab;
    [SerializeField] private GameObject MainSprite;
    private FieldOfViewScript FieldOfViewScript;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player");
        //CurrentPoint = PointA.transform;
        TargetPoint = 0;
        RB.transform.Rotate(0, 0,BeginingRotation);
        FieldOfViewScript = Instantiate(FieldOfViewPrefab,null).GetComponent<FieldOfViewScript>();
    }

    void Update()
    {
        FieldOfViewScript.SetOrigin(RB.transform.position);
        FieldOfViewScript.SetAimDirection(RB.transform.up);
        FieldOfViewScript.SetViewer(gameObject);
        MainSprite.transform.position = RB.transform.position;
        MainSprite.transform.rotation = Quaternion.identity; ;

        if (RB.transform.position == PatrolPoints[TargetPoint].transform.position)
        {
            IncreaseTargetInt();
        }

        RB.transform.position = Vector2.MoveTowards(RB.transform.position, PatrolPoints[TargetPoint].position, ESpeed * Time.deltaTime);
    }

    private void IncreaseTargetInt()
    {
        TargetPoint++;
       
        if (TargetPoint >= PatrolPoints.Length)
        {
            TargetPoint = 0;
        }
        RB.transform.Rotate(0, 0, ERotation[TargetPoint]);
        
    }
}
