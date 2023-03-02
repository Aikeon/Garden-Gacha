using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boosterPackPrefab;

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
        var b = Instantiate(boosterPackPrefab, transform.position + 0.25f * Vector3.up, transform.rotation);
        b.GetComponent<Booster>().origin = this;
    }
}
