using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedDirt : MonoBehaviour
{
    [SerializeField] private bool isFirst;
    [SerializeField] private int price;
    [SerializeField] private Material unlockedMaterial;
    [SerializeField] private TMP_Text priceDisplay;
    [SerializeField] private Light topLight;
    public bool unlocked = false;
    private Renderer _renderer;
    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        priceDisplay.text = price.ToString() + " $";
        _renderer = GetComponent<Renderer>();
        topLight.gameObject.SetActive(false);
        if (isFirst)
        {
            Unlock();
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && test)Unlock();
        ;
    }

    public void Unlock()
    {
        if (unlocked || GameManager.Instance.money < price) return;

        unlocked = true;
        GameManager.Instance.money -= price;
        var newMaterials =  _renderer.materials;
        newMaterials[1] = unlockedMaterial;
        _renderer.materials = newMaterials;
        topLight.gameObject.SetActive(true);
        
        Destroy(priceDisplay.transform.parent.gameObject);
    }
}
