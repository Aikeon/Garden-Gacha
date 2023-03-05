using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GrowthBucket : MonoBehaviour
{
    public int maxVegetablesNumber = 2;
    public List<VegData> content;
    public float growthMult;

    public float intervallePousse = 2f;
    
    [SerializeField] private WaterCollide _waterCollide;

    // Start is called before the first frame update
    void Start()
    {
        content = new List<VegData>();
    }

    public VegType FindOtherVeg(VegData vegData)
    {
        if (content.Count < 2)
        {
            return VegType.None;
        }

        return content[0] == vegData ? content[1].mapType : content[0].mapType;
    }

    public void AddVeggie(VegData veg, Vector3 pos)
    {
        if (content.Count >= maxVegetablesNumber) return;
        content.Add(veg);
        
        var newPlant = Instantiate(veg.vegPrefab, pos, Quaternion.identity);
        newPlant.GetComponent<GrowingPlant>().conteneur = this;
        newPlant.SetActive(true);

        StartCoroutine(WaitOtherSeed(newPlant.GetComponent<GrowingPlant>()));
    }

    IEnumerator WaitOtherSeed(GrowingPlant newPlant)
    {
        while (_waterCollide.timeUnderWater < intervallePousse)
        {
            yield return null;
        }

        newPlant.SetQuantity();
        newPlant.Grow();
        Debug.Log("Pousse");
    }

    

    public void RemoveVeggie(VegData veg)
    {
        content.Remove(veg);
    }
}
