using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterCollide : MonoBehaviour
{
    // Time spent under water, refreshed in real time. If particles stop colliding, this value is added to totalTimeUnderWater and reset to 0
    public float[] timeUnderWater = new float[2];

    // Total time spent under water, refreshed only when particles stop colliding
    public float[] totalTimeUnderWater = new float[2];
    
    // Have particles collided ?
    public bool[] _particlesCollided = new bool[2] {false, false};
    
    // When did particles last collide ?
    public float[] _particlesLastCollisionTime = new float[] {0f,0f};
     
    // How long do we go without particle collision before saying we are not ?
    private const float IntervalleWithoutCollision = 0.3f;

    public GrowthBucket _bucket;
    public bool[] canReceiveWater = new bool[2] {false, false};

    private void Start()
    {
        _bucket = GetComponent<GrowthBucket>();
    }

    // we get hit with a particle
    void OnParticleCollision(GameObject other)
    {
        print("COLLISION");
        
        // Set our particles colliding flag to true
        if (!_particlesCollided[0] && canReceiveWater[0])
        {
            _particlesLastCollisionTime[0] += Time.deltaTime;
            _particlesCollided[0] = true;
        }
        else
        {
            // Record / Overwrite the time we just got hit with a water particle
            if (canReceiveWater[0])
            {
                _particlesLastCollisionTime[0] += Time.deltaTime;
            }
        }

        if (!_particlesCollided[1] && canReceiveWater[1])
        {
            _particlesLastCollisionTime[1] += Time.deltaTime;
            _particlesCollided[1] = true;
        }
        else
        {
            if (canReceiveWater[1])
            {
                _particlesLastCollisionTime[1] += Time.deltaTime;
            }
        }

        
    }
 
    // Every frame check if we have gone long enough without getting hit by water particles
    // If so, we calculate time spent under this "session d'arrosage" in timeUnderWater and add it to totalTimeUnderWater which counts all the "sessions d'arrosage".
    public void FixedUpdate()
    {
        if (_bucket.content.Count <= 0) return;
        for (int i = 0; i < _bucket.content.Count; i++)
        {
            if (canReceiveWater[i])
            {
               // print("can receive water : " + i);
                timeUnderWater[i] = _particlesLastCollisionTime[i] ;

             //   print("time under water = " + timeUnderWater[i]);
             //   print("last collision = " + _particlesLastCollisionTime);
                
                // If it's been long enough
                if (_particlesCollided[i] && (_particlesLastCollisionTime[i] > IntervalleWithoutCollision))
                {
                    // Set our particles colliding flag to false, check if vegetables can grow
                    _particlesCollided[i] = false;
                    totalTimeUnderWater[i] += timeUnderWater[i];
                //    print("total time under water = " + totalTimeUnderWater[i]);
                    
                _particlesLastCollisionTime[i] = 0;
                }

                _bucket.waterTime[i] = timeUnderWater[i];
                _bucket.waterTotalTime[i] = totalTimeUnderWater[i];
            }
        }

    }

    public void StopWater(int i)
    {
        canReceiveWater[i] = false;
        timeUnderWater[i] = 0;
        totalTimeUnderWater[i] = 0;
        _particlesLastCollisionTime[i] = 0;
    }

    public void StartWater(int i)
    {
        canReceiveWater[i] = true;
    }
}
