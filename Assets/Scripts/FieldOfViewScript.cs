using UnityEngine;
using UnityEngine.Diagnostics;

public class FieldOfViewScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int RayCount = 15;
        float FOV = 90f;
        float ViewDistance = 10f;
        float Angle = 0f;
        float AngleIncrease = FOV / RayCount;
        Vector3 OriginPoint = Vector3.zero;
        

        // sets up look and shape of mesh (triangle in this case)
        Vector3[] Verticies = new Vector3[RayCount + 1 + 1];    //+1 for origin, +1 for array '0'
        Vector2[] UV = new Vector2[Verticies.Length];
        int[] Triangles = new int[RayCount*3];

        //Draws a triangle / sets points of triangle
        Verticies[0] = OriginPoint;
        Verticies[1] = new Vector3(10, 0);
        Verticies[2] = new Vector3(0, -10);

        //sets triangles to draw a cone
        int VertexIndex = 1;
        int TriangleIndex = 0;
        for (int i = 0; i <= RayCount; i++)
        {
          Vector3 Vertex = OriginPoint + GetVectorFromAngle(Angle) * ViewDistance;
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
        }

        //sets look for mesh
        mesh.vertices = Verticies;
        mesh.uv = UV;
        mesh.triangles = Triangles;

    }

    // turns vector3 into angle
    public static Vector3 GetVectorFromAngle (float angle)
    {
        //0 -> 360
        float AngleRadius = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(AngleRadius), Mathf.Sin(AngleRadius));
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
