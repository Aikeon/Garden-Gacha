using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform parent;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.position + offset;
    }
}
