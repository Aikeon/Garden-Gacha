using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtEvolution : MonoBehaviour
{

    [SerializeField] private Material solidDirt;
    [SerializeField] private Material soiledDirt;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        
        if (other.TryGetComponent<Shovel>(out Shovel shovel))
        {
            Debug.Log("pelle");
            GetComponent<MeshRenderer>().sharedMaterial = soiledDirt;
        }
    }
}
