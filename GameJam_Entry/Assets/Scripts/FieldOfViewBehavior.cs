using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfViewBehavior : MonoBehaviour
{
    [SerializeField] private LayerMask layerM;
    private List<agentMovement> detectedEnemies = new List<agentMovement>();
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

        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    private void LateUpdate()
    {
        int rayCount = 30;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float vDistance = 30f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        detectedEnemies.Clear();

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

                agentMovement enemy = rCastHit.collider.GetComponent<agentMovement>();
                if (enemy != null)
                {
                    if (!detectedEnemies.Contains(enemy))
                    {
                        detectedEnemies.Add(enemy);
                        enemy.StopAgent(); // Stop the agent when detected
                    }
                }
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

        foreach (agentMovement enemy in FindObjectsOfType<agentMovement>())
        {
            if (!detectedEnemies.Contains(enemy))
            {
                enemy.ResumeAgent();
            }
        }
    }

    public void SetOrigin(Vector3 origin)
    {
        //Vector3 offset = UtilsClass.GetVectorFromAngle(startingAngle) * 1.5f;
        this.origin = origin;
        Debug.Log("FOV Origin:" + origin);
    }

    public void SetAimDirection(Vector3 aim)
    {
        startingAngle = UtilsClass.GetAngleFromVector(aim) + (fov / 2f); 
    }
}

