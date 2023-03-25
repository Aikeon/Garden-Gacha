using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedDirt : MonoBehaviour
{
    [SerializeField] private GrowthBucket dirt;
    [SerializeField] private int price;
    [SerializeField] private Material unlockedMaterial;
    [SerializeField] private TMP_Text priceDisplay;
    private bool _unlocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        dirt.enabled = false;
        priceDisplay.text = price.ToString() + " $";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        if (_unlocked || GameManager.Instance.money < price) return;

        GameManager.Instance.money -= price;
        dirt.enabled = true;
        GetComponent<Renderer>().materials[1] = unlockedMaterial;
        Destroy(priceDisplay.transform.parent);
    }
}
