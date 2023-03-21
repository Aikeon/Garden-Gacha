using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentOffset : MonoBehaviour
{
    [SerializeField] bool isMiddle;
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform parent;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (!isMiddle)
        offset = parent.transform.position - transform.position;
        else 
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMiddle) {transform.localPosition = new Vector3(offset.x + (down.localPosition.x - up.localPosition.x)/2, (down.localPosition.y + up.localPosition.y)/2, (down.localPosition.z + up.localPosition.z)/2); return;}
        transform.position = parent.position - transform.parent.rotation * parent.localRotation * Quaternion.AngleAxis(90,Vector3.right) * offset;
    }
}
