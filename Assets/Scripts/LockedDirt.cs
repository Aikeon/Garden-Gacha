using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockedDirt : MonoBehaviour
{
    [SerializeField] private bool isFirst;
    [SerializeField] private int price;
    [SerializeField] private Material unlockedMaterial;
    [SerializeField] private TMP_Text priceDisplay;
    [SerializeField] private Light topLight;
    public bool unlocked = false;
    [SerializeField] private Renderer _renderer;
    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        priceDisplay.text = price.ToString() + " $";
        topLight.gameObject.SetActive(false);
        if (isFirst)
        {
            Unlock(true);
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && test)Unlock();
        ;
    }

    public void Unlock(bool save = false)
    {
        if ((unlocked || GameManager.Instance.money < price) && !save) return;

        unlocked = true;
        if (!save)
        {
            GameManager.Instance.money -= price;
        }
        var newMaterials =  _renderer.materials;
        newMaterials[1] = unlockedMaterial;
        _renderer.materials = newMaterials;
        topLight.gameObject.SetActive(true);
        
        Destroy(priceDisplay.transform.parent.parent.gameObject);

    }
}
