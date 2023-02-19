using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graine : MonoBehaviour
{
    public VegData vegData;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.gameObject.TryGetComponent<GrowthBucket>(out var bucket))
        {
            bucket.AddVeggie(vegData, collisionInfo.GetContact(0).point);
            Destroy(gameObject);
        }
    }
}
