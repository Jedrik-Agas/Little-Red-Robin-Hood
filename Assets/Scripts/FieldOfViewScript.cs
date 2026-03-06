using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

public class FieldOfViewScript : MonoBehaviour
{
    private Mesh mesh;
    private Vector3 OriginPoint;
    private float StartingAngle;
    private float DetectionLevel;
    private GameObject Viewer;
    //private bool PlayerDetected = false;
    public float FOV;
    public float ViewDistance = 7f;
    private LevelLoaderScript LevelLoaderScript;
    [SerializeField] private LayerMask LayerMask;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        OriginPoint = Vector3.zero;
        LevelLoaderScript = GameObject.FindGameObjectWithTag("Respawn").GetComponent<LevelLoaderScript>();
    }

    public void SetViewer(GameObject viewer)
    {
        Viewer = viewer;
    }
    public void SetOrigin(Vector3 origin)
    {
       this.OriginPoint = origin;
    }

    public void SetAimDirection(Vector3 Aimdirection)
    {
        StartingAngle = GetAngleFromVectorFloat(Aimdirection) - FOV / 2f;
    }

    // turns vector3 into angle
    public static Vector3 GetVectorFromAngle(float angle)
    {
        //0 -> 360
        float AngleRadius = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(AngleRadius), Mathf.Sin(AngleRadius));
    }

    public static float GetAngleFromVectorFloat(Vector3 Dir)
    {
        Dir = Dir.normalized;
        float num = Mathf.Atan2(Dir.x, Dir.y) * Mathf.Rad2Deg;
        if (num < 0)
        {
            num += 360;
        }
        return num;
    }
    void Update()
    {

        FOV = 90;
        int RayCount = 20;
        float Angle = StartingAngle;
        float AngleIncrease = FOV / RayCount;

        // sets up look and shape of mesh (triangle in this case)
        Vector3[] Verticies = new Vector3[RayCount + 1 + 1];    //+1 for origin, +1 for array '0'
        Vector2[] UV = new Vector2[Verticies.Length];
        int[] Triangles = new int[RayCount*3];

        //Draws a triangle / sets points of triangle
        Verticies[0] = OriginPoint;
        Verticies[1] = new Vector3(7, 0);
        Verticies[2] = new Vector3(0, -7);

        //sets triangles to draw a cone
        int VertexIndex = 1;
        int TriangleIndex = 0;
        for (int i = 0; i <= RayCount; i++)
        {
            Vector3 Vertex;
            RaycastHit2D RaycastHit = Physics2D.Raycast(OriginPoint, GetVectorFromAngle(Angle), ViewDistance, LayerMask);
            if (RaycastHit.collider == null)
            {
                Vertex = OriginPoint + GetVectorFromAngle(Angle) * ViewDistance;
            }
            else
            {
                Vertex = RaycastHit.point;
                if (RaycastHit.collider.CompareTag("Player"))
                {
                    //PlayerDetected = true;
                    //print(PlayerDetected);
                    //LevelLoaderScript.NextLevelName = "Stealth Section";
                    //LevelLoaderScript.TimeToGo = true;
                    DetectionLevel++;
                    print(DetectionLevel);
                    return;
                }
            }
                
            Verticies[VertexIndex] = Vertex;

            if(i > 0)
            {

                Triangles[TriangleIndex + 0] = 0;
                Triangles[TriangleIndex + 1] = VertexIndex - 1;
                Triangles[TriangleIndex + 2] = VertexIndex;

                TriangleIndex += 3;
            }
            VertexIndex++;

            Angle -= AngleIncrease;
            if (Viewer == null)
            {
                Destroy(gameObject);
            }

            if (DetectionLevel >= 10)
            {
                LevelLoaderScript.NextLevelName = "Stealth Section";
                LevelLoaderScript.TimeToGo = true;
            }
        }

        //sets look for mesh
        mesh.vertices = Verticies;
        mesh.uv = UV;
        mesh.triangles = Triangles;
        mesh.RecalculateBounds();

    }

}
