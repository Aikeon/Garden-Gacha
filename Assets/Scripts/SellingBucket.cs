using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBucket : MonoBehaviour
{
    [SerializeField] private Transform explosionTransform;
    [SerializeField] private AudioClip sfx;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Vegetable>(out var veg))
        {
            ParticleSystem explosion = Instantiate(veg.soldExplosion, explosionTransform.position, explosionTransform.rotation);
            explosion.Play();
            // audioSource.pitch = 1 + Random.Range(-0.2f,0.2f);
            // audioSource.PlayOneShot(sfx);
            foreach (var sc in GameManager.Instance.soldVegCount)
            {
                if (sc.obj == veg.origin.data.mapType) sc.count++; 
            }
           
            GameManager.Instance.money += veg.origin.data.basePrice;
            GameManager.Instance.totalObtainedMoney += veg.origin.data.basePrice;
            Destroy(veg.gameObject);
        }
    }
}
