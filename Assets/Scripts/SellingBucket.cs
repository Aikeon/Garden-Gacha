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
            AudioManager.Instance.PlaySFX("moneyGain");
            switch (veg.origin.data.mapType)
            {
                case VegType.Tomate: GameManager.Instance.soldTomates++; break;
                case VegType.Oignon: GameManager.Instance.soldOignons++; break;
                case VegType.Haricot: GameManager.Instance.soldHaricots++; break;
                case VegType.Chou: GameManager.Instance.soldChoux++; break;
                case VegType.Ail: GameManager.Instance.soldAil++; break;
                case VegType.Poireau: GameManager.Instance.soldPoireaux++; break;
                case VegType.Carotte: GameManager.Instance.soldCarrottes++; break;
            }
           GameManager.Instance.totalVegCount++;

            GameManager.Instance.money += veg.origin.data.basePrice;
            GameManager.Instance.totalObtainedMoney += veg.origin.data.basePrice;
            Destroy(veg.gameObject);
        }
    }
}
