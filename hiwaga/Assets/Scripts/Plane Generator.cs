using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class PlaneGenerator : MonoBehaviour
{
    Mesh myMesh;
    MeshFilter meshFilter;

    [SerializeField] Vector2 planeSize = new Vector2(1, 1);
    [SerializeField, Range(1, 50)] int planeResolution = 1;

    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        myMesh = new Mesh();
        myMesh.name = "GeneratedPlane";
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = myMesh;
    }

    void OnValidate()
    {
        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();

        if (myMesh == null && meshFilter != null)
        {
            myMesh = new Mesh();
            myMesh.name = "GeneratedPlane";
            meshFilter.mesh = myMesh;
        }

        GeneratePlane(planeSize, planeResolution);
        AssignMesh();
    }

    void OnEnable()
    {
        // Regenerate when object is enabled in Edit mode
        OnValidate();
    }

    void GeneratePlane(Vector2 size, int resolution)
    {
        vertices = new List<Vector3>();
        float xPerStep = size.x / resolution;
        float yPerStep = size.y / resolution;
        for (int y = 0; y < resolution + 1; y++)
        {
            for (int x = 0; x < resolution + 1; x++)
            {
                vertices.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
            }
        }

        triangles = new List<int>();
        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                int i = (row * resolution) + row + column;
                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);

                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);
            }
        }
    }

    void AssignMesh()
    {
        myMesh.Clear();
        myMesh.vertices = vertices.ToArray();
        myMesh.triangles = triangles.ToArray();

        // Generate UVs
        Vector2[] uvs = new Vector2[vertices.Count];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / planeSize.x, vertices[i].z / planeSize.y);
        }
        myMesh.uv = uvs;

        myMesh.RecalculateNormals();
    }
}