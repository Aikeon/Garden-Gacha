using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TestButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject vegPrefab;
    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.TryGetComponent<ActionBasedController>(out var hand))
        {
            Instantiate(vegPrefab, transform.position + Vector3.right + Vector3.up, Quaternion.identity);
        }
    }
}
