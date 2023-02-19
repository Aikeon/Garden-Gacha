using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    Camera cam;
    Rigidbody rb;
    CapsuleCollider cc;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        cc.center = cam.transform.localPosition.y/2 * Vector3.up + cam.transform.localPosition.x * Vector3.right + cam.transform.localPosition.z * Vector3.forward;
        cc.height = cam.transform.localPosition.y;
        var dirX = cam.transform.right.x * Vector3.right + cam.transform.right.z * Vector3.forward;
        dirX = dirX.normalized;
        var dirZ = cam.transform.forward.x * Vector3.right + cam.transform.forward.z * Vector3.forward;
        dirZ = dirZ.normalized;
        rb.velocity = speed * (Input.GetAxis("Horizontal") * dirX + Input.GetAxis("Vertical") * dirZ);
    }
}
