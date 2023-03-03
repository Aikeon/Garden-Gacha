using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthBucket : MonoBehaviour
{
    public int maxVegetablesNumber = 2;
    public List<VegData> content;
    public float growthMult;

    // Start is called before the first frame update
    void Start()
    {
        content = new List<VegData>();
    }

    private void Update()
    {
        print(content.Count);
    }

    public VegType FindOtherVeg(VegData vegData)
    {
        if (content.Count < 2)
        {
            print("No other veg");
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
        print("coroutine");
        yield return new WaitForSeconds(5f);
        newPlant.SetQuantity();
        newPlant.Grow();
    }
    
    public void RemoveVeggie(VegData veg)
    {
        content.Remove(veg);
    }
}
