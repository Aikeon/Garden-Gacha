using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] private Booster boosterPackPrefab;
    [SerializeField] private Booster.Rarity rarity;

    void Start()
    {
        pickedBooster();
    }

    public void pickedBooster()
    {
        StartCoroutine(addNewBooster());
    }

    IEnumerator addNewBooster()
    {
        //TODO fancy animation or VFX
        yield return new WaitForSeconds(2f);
        var b = Instantiate(boosterPackPrefab, transform.position + 0.01f * Vector3.up, transform.rotation);
        b.origin = this;
        b.StartMaterial(rarity);
    }
}
