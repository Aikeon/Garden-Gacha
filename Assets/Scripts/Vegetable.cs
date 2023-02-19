using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Vegetable : MonoBehaviour
{
    public GrowingPlant origin;
    private XRGrabInteractable grabComponent;
    private Rigidbody rb;
    private bool detached = false;
    // Start is called before the first frame update
    void Start()
    {
        grabComponent = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        grabComponent.enabled = origin.isMature;
    }

    public void Collect()
    {
        if (detached) return;
        transform.SetParent(null);
        detached = true;
        origin.CollectedChild();
    }

    public void LoseGrip()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
