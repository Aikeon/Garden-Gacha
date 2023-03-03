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
        waterParticles.gameObject.SetActive(false);
    }

    void Start()
    {
        _wateringCanTransform = GetComponent<Transform>();
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name + this.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (_wateringCanTransform.rotation.eulerAngles.z > 1f && _wateringCanTransform.rotation.eulerAngles.z < 180f )
        {
            if (!_isWatering)
            {
                waterParticles.gameObject.SetActive(true);
                waterParticles.Play();
                _isWatering = true;
            }
        }
        else
        {
            if (_isWatering)
            {
                waterParticles.Stop();
                waterParticles.gameObject.SetActive(false);
                _isWatering = false;
            }
        }
    }
}
