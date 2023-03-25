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
    [SerializeField] private Light topLight;
    private bool _unlocked = false;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        dirt.enabled = false;
        priceDisplay.text = price.ToString() + " $";
        _renderer = GetComponent<Renderer>();
        topLight.gameObject.SetActive(false);
    }
    

    public void Unlock()
    {
        if (_unlocked || GameManager.Instance.money < price) return;

        GameManager.Instance.money -= price;
        dirt.enabled = true;
        var newMaterials =  _renderer.materials;
        newMaterials[1] = unlockedMaterial;
        _renderer.materials = newMaterials;
        topLight.gameObject.SetActive(true);
        
        Destroy(priceDisplay.transform.parent.gameObject);
    }
}
