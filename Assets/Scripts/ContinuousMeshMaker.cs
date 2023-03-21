using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMeshMaker : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] int[] indices;
    [SerializeField] Transform referenceX;
    [SerializeField] Transform referenceY;
    private float distX;
    private float distY;
    public float multX = 1f;
    public float multZ = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one;
        distX = Vector3.Distance(transform.position, referenceX.position);
        distY = Vector3.Distance(transform.position, referenceY.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vert = new Vector3[points.Length];
        Vector2[] uvs = new Vector2[points.Length];
        Vector3[] normals = new Vector3[points.Length];
        var distanceX = Vector3.Distance(transform.position, referenceX.position);
        var distanceY = Vector3.Distance(transform.position, referenceY.position);
        for(int i=0; i < points.Length; i++)
        {
            vert[i] = points[i].localPosition;
            uvs[i] = new Vector2(vert[i].x * multX * distX / distanceX + 0.5f, vert[i].y * multZ * distY / distanceY + 0.5f);
            // normals[i] = -Vector3.Cross(vert[indices[3*i]]-vert[indices[3*i+1]], vert[indices[3*i]]-vert[indices[3*i+2]]).normalized;
        }
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vert;
        mesh.triangles = indices;
        // mesh.normals = normals;
        mesh.uv = uvs;
    }
}
