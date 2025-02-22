using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfViewBehavior : MonoBehaviour
{
    [SerializeField] private LayerMask layerM;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float fov;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 50f;
        origin = Vector3.zero;

        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void LateUpdate()
    {
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float vDistance = 60f;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D rCastHit = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), vDistance, layerM);
            if (rCastHit.collider == null)
            {
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * vDistance;
            }
            else
            {
                vertex = rCastHit.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aim)
    {
        startingAngle = UtilsClass.GetAngleFromVector(aim) - fov / 2f; 
    }
}

