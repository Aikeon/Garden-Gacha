using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GrowthBucket : MonoBehaviour
{
    public int maxVegetablesNumber = 2;
    public List<VegData> content;
    public float growthMult;

    public float intervallePousse = 7f;
    
    [SerializeField] private WaterCollide _waterCollide;
    public int soiledDirtUsesRemaining;

    [SerializeField] private int soiledDirtUsesPerSoil;
    private bool _soiled;
    private bool _watered;
    

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
        print("wait");
        while (_waterCollide.timeUnderWater + _waterCollide.totalTimeUnderWater < newPlant.data.vegWateringTime)
        {
            yield return null;
        }

        newPlant.SetQuantity();
        print("setQuantity");
        print(newPlant);
        newPlant.Grow();
        print("callGrow");
     //   Debug.Log(newPlant +"Pousse après " + (_waterCollide.timeUnderWater + _waterCollide.totalTimeUnderWater) + " secondes");
    }

    

    public void RemoveVeggie(VegData veg)
    {
        content.Remove(veg);
    }

    public void ShovelCollision()
    {
        _soiled = true;
        soiledDirtUsesRemaining = soiledDirtUsesPerSoil;

    }

    private void Update()
    {
        if ((soiledDirtUsesRemaining == 0) && _soiled)
        {
            _soiled = false;
        }

        bool resetWater = content.All(veg => veg.vegPrefab.GetComponent<GrowingPlant>().isMature);

        if (!resetWater) return;
        _waterCollide.timeUnderWater = 0;
        _waterCollide.totalTimeUnderWater = 0;
    }


}
