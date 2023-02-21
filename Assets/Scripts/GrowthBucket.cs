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

    public void AddVeggie(VegData veg, Vector3 pos)
    {
        if (content.Count >= maxVegetablesNumber) return;
        content.Add(veg);
        var newPlant = Instantiate(veg.vegPrefab, pos, Quaternion.identity);
        newPlant.GetComponent<GrowingPlant>().conteneur = this;
        newPlant.SetActive(true);
    }

    public void RemoveVeggie(VegData veg)
    {
        content.Remove(veg);
    }
}
