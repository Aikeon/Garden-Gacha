using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelHead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GrowthBucket bucket))
        {
            Debug.Log("oui");
            bucket.ShovelCollision();
        }
    }
}
