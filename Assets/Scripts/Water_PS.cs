using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_PS : MonoBehaviour
{

    private GameObject _currentOther;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnParticleCollision(GameObject other)
    {
        
        if (_currentOther != other)
        {
            _currentOther = other;
            Debug.Log("Fin");
            Debug.Log("Debut");
            Debug.Log("1 other " + other.name + " 2 this " + this.name);
            
        }
        else
        {
            Debug.Log("En cours");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
