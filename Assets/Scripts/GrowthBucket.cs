using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("UI")] 
    [SerializeField] private Image[] vegImages;
    [SerializeField] private Image[] waterLevelImages;
    public float[] waterTime = new float[2] {0f,0f};
    public float[] waterTotalTime = new float[2] {0f,0f};
    

    // Start is called before the first frame update
    void Awake()
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
        if (content.Count >= maxVegetablesNumber || !GetComponentInParent<LockedDirt>().unlocked) return;
        content.Add(veg);

        vegImages[content.Count-1].sprite = veg.sprite;
        print(vegImages[content.Count-1].sprite);
        vegImages[content.Count-1].enabled = true;

        var newPlant = Instantiate(veg.vegPrefab, pos, Quaternion.identity);
        newPlant.GetComponent<GrowingPlant>().conteneur = this;
        newPlant.SetActive(true);
        
        StartCoroutine(WaitOtherSeed(newPlant.GetComponent<GrowingPlant>(), content.Count-1));
    }

    IEnumerator WaitOtherSeed(GrowingPlant newPlant, int i)
    {
        _waterCollide.StartWater(i);
        newPlant.index = i;
        
     //   print("wait");
     //   print("water time = " + waterTime[i] + waterTotalTime[i]);
        while (waterTime[i] + waterTotalTime[i] < newPlant.data.vegWateringTime)
        {
        //    print("BEFORE : water time = " + waterTime[i] + "; total water time = " + waterTotalTime[i]);
            var rectTransformAnchorMax = waterLevelImages[i].rectTransform.anchorMax;
            rectTransformAnchorMax.y = (waterTime[i] + waterTotalTime[i]) / newPlant.data.vegWateringTime;
            waterLevelImages[i].rectTransform.anchorMax = rectTransformAnchorMax;
            yield return null;
        }
        
      //  print("AFTER : water time = " + waterTime[i] + "; total water time = " + waterTotalTime[i] + "; total time = " + Time.time);

        waterTime[i] = 0;
        waterTotalTime[i] = 0;
        _waterCollide.StopWater(i);

        newPlant.SetQuantity();
     //   print("setQuantity");
        newPlant.Grow();
     //   print("callGrow");
    }

    

    public void RemoveVeggie(VegData veg, int index)
    {
        vegImages[index].enabled = false;
        var rectTransformAnchorMax = waterLevelImages[index].rectTransform.anchorMax;
        rectTransformAnchorMax.y = 0;
        waterLevelImages[index].rectTransform.anchorMax = rectTransformAnchorMax;
        
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
    }


}
