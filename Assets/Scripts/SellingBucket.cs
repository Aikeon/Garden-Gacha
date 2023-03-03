using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBucket : MonoBehaviour
{
    [SerializeField] private Transform explosionTransform;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Vegetable>(out var veg))
        {
            ParticleSystem explosion = Instantiate(veg.soldExplosion, explosionTransform.position, explosionTransform.rotation);
            explosion.Play();
            GameManager.Instance.money += veg.origin.data.basePrice;
            Destroy(veg.gameObject);
        }
    }
}
