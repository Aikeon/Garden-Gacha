using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPlant : MonoBehaviour
{
    public GrowthBucket conteneur; //TODO utiliser la liste du conteneur pour donner le multiplicateur
    private float growthIndicator = 0f;
    public VegData data;
    public bool isMature = false;
    public int VeggiesToCollect;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Mathf.Min(1,(growthIndicator/data.vegGrowthTime)) * Vector3.one;
        growthIndicator += Time.deltaTime; // * multiplicateur du conteneur associé au vegetable
        if (growthIndicator > data.vegGrowthTime)
        {
            for (int i = 0; i < data.prodNumber; i++)
            {
                isMature = true;
            }
        }
    }

    void OnDestroy()
    {
        conteneur.RemoveVeggie(data);
    }

    public void CollectedChild()
    {
        VeggiesToCollect--;
        if (VeggiesToCollect == 0)
        {
            //TODO dying plant animation
            Destroy(gameObject);
        }
    }

    public void SetNumberOfChildren(int nb)
    {
        VeggiesToCollect = nb;
    }
}
