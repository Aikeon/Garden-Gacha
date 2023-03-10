using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterCollide : MonoBehaviour
{
    public float timeUnderWater;
    public float totalTimeUnderWater = 0f;
    
    // Have particles collided ?
    private bool _particlesCollided = false;
     
    // When did particles first collide ?
    private float _particlesFirstCollisionTime = 10000f;
    
    // When did particles last collide ?
    private float _particlesLastCollisionTime = 10000f;
     
    // How long do we go without particle collision before saying we are not ?
    private const float IntervalleWithoutCollision = 0.3f;

    
    

    // we get hit with a particle
    void OnParticleCollision(GameObject other)
    {
        if (_particlesCollided == false)
        {
            _particlesFirstCollisionTime = Time.time;
            _particlesLastCollisionTime = Time.time;
            Debug.Log("début collision à " + _particlesFirstCollisionTime);
            
            // Set our particles colliding flag to true
            _particlesCollided = true;
        }
        else
        {
            // Record / Overwrite the time we just got hit with a water particle
            _particlesLastCollisionTime = Time.time;
        }

        
    }
 
    // Every frame check if we have gone long enough without getting hit by water particles
    // If so, we calculate time spent under water and decide if vegetables can grow
    public void FixedUpdate()
    {
        timeUnderWater = _particlesLastCollisionTime - _particlesFirstCollisionTime;

        // If it's been long enough
        if (_particlesCollided && (Time.time - _particlesLastCollisionTime > IntervalleWithoutCollision))
        {
            // Set our particles colliding flag to false, check if vegetables can grow
            _particlesCollided = false;
            totalTimeUnderWater += timeUnderWater;
            Debug.Log("fin collision ! Nouveau temps total : " + totalTimeUnderWater + " ajout temps : " + timeUnderWater);

        }
        
       
    }
}
