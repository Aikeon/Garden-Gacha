using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBucket : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Vegetable>(out var veg))
        {
            //TODO VFX of selling
            GameManager.Instance.money += veg.origin.data.basePrice;
            Destroy(veg.gameObject);
        }
    }
}
