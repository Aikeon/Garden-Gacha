using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousMeshMaker : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] int[] indices;
    [SerializeField] Transform referenceYL;
    [SerializeField] Transform referenceYR;
    [SerializeField] Transform refTop;
    [SerializeField] Transform refBot;
    private float distX;
    private float distY;
    public float multX = 1f;
    public float multZ = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one;
        distY = Vector3.Distance(referenceYL.position, referenceYR.position);
        distX = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vert = new Vector3[points.Length];
        Vector2[] uvs = new Vector2[points.Length];
        Vector3[] normals = new Vector3[points.Length];
        var distanceY = Vector3.Distance(referenceYL.position, referenceYR.position);
        var distanceRY = Vector3.Distance(referenceYR.position, transform.position);
        for(int i=0; i < points.Length; i++)
        {
            vert[i] = points[i].localPosition;
            var angleOffset = Mathf.Atan((refTop.localPosition.x + refBot.localPosition.x) / (refTop.localPosition.y - refBot.localPosition.y)) * 180 / Mathf.PI;
            uvs[i] = new Vector2((vert[i].x * multX) + 0.5f + angleOffset / 180, vert[i].y * multZ * distY / distanceY - distanceRY / distanceY);
            normals[i] = Vector3.Cross(points[indices[3*i]].position-points[indices[3*i+1]].position, points[indices[3*i]].position-points[indices[3*i+2]].position).normalized;
            if (normals[i].y < 0) normals[i] = -normals[i];
        }
        Mesh mesh = new Mesh();
        
        mesh.vertices = vert;
        mesh.triangles = indices;
        mesh.normals = normals;
        mesh.uv = uvs;
        
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
