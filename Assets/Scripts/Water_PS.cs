using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Water_PS : MonoBehaviour
{

    // Are my particles colliding with something?
    public bool particlesCurrentlyColliding = false;
     
    // When did particles start colliding ?
    public float debutCollisionTime;
     
    // How long do we go without particle collision before saying we are not colliding ?
    public float intervalleSansCollision = 0.2f;

    // Particles start colliding with something
    void OnParticleCollision(GameObject other)
    {
        // If the thing we collided with is tagged as "Arrosable"
        if (other.CompareTag("Arrosable") )
        {
            // Record / Overwrite the time the first particle collides with it
            debutCollisionTime = Time.time;
             
            // Set our particles colliding flag to true
            particlesCurrentlyColliding = true;
        }
 
    }
 
    // Every frame check if we have gone long enough without water particles colliding
    public void FixedUpdate()
    {
        // If it's been long enough
        if (particlesCurrentlyColliding && Time.time - debutCollisionTime >= intervalleSansCollision)
        {
            // Set our particles colliding flag to false
            particlesCurrentlyColliding = false;

        }
    }
}
