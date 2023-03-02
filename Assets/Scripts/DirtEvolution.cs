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
        if (other.TryGetComponent<Shovel>(out Shovel shovel))
        {
            GetComponent<MeshRenderer>().material = soiledDirt;
        }
    }
}
