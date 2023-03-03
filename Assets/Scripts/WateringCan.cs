using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem waterParticles;
    
    private bool _isWatering = false;
    private Transform _wateringCanTransform;

    private void Awake()
    {
        waterParticles.Stop();
    }

    void Start()
    {
        _wateringCanTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_wateringCanTransform.rotation.eulerAngles.z > 1f && _wateringCanTransform.rotation.eulerAngles.z < 180f )
        {
            if (!_isWatering)
            {
                waterParticles.Play();
                _isWatering = true;
            }
        }
        else
        {
            if (_isWatering)
            {
                waterParticles.Stop();
                _isWatering = false;
            }
        }
    }
}
