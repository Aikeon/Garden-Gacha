using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMeshMaker : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] int[] indices;
    [SerializeField] Transform right;
    private Vector3[] normals;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one;
        normals = new Vector3[indices.Length];
        dist = transform.position.z - right.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vert = new Vector3[points.Length];
        for(int i=0; i < points.Length; i++)
        {
            // vert[i] = new Vector3(points[i].position.x, points[i].position.z, points[i].position.y) - transform.parent.position;
            vert[i] = points[i].localPosition;
        }
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vert;
        mesh.triangles = indices;
    }
}
