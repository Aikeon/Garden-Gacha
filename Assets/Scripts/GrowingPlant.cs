using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

//using UnityEngine.Formats.Alembic.Importer;

public class GrowingPlant : MonoBehaviour
{
    public GrowthBucket conteneur; //TODO utiliser la liste du conteneur pour donner le multiplicateur
    private float growthIndicator = 0f;
    public VegData data;
    public bool isMature = false;
    public int veggiesToCollect;

    [SerializeField] private Animator _anim;
    [SerializeField] Renderer renderer;
    public GameObject vegetable;
    
    public List<Vegetable> potentialsVegetables;

    public int index;


    private void Start()
    {
        //_anim = GetComponentInChildren<Animator>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = Mathf.Min(1,(growthIndicator/data.vegGrowthTime)) * Vector3.one;
        growthIndicator += Time.deltaTime; // * multiplicateur du conteneur associé au vegetable
        if (growthIndicator > data.vegGrowthTime)
        {
            //for (int i = 0; i < data.prodNumber; i++)
            //{
                isMature = true;
            //}
        }
    }
    
    public void SetQuantity()
    {
        veggiesToCollect = VegData.VegMatrix[data.mapType][conteneur.FindOtherVeg(data)];
    }
    
    public void Grow()
    {
        renderer.enabled = true;
        StartCoroutine(GrowthCoroutine());
    }

    IEnumerator GrowthCoroutine()
    {
        _anim.CrossFade("Growth", 0.01f);
        _anim.speed = 1 / data.vegGrowthTime;
        yield return new WaitForSeconds(data.vegGrowthTime);
        vegetable.SetActive(true);
        
        AudioManager.Instance.PlaySFX("plantFinishedGrowing");
        ShowCollectablesVeggies();
        
        _anim.gameObject.SetActive(false);
    }

    private void ShowCollectablesVeggies()
    {
        
        switch (veggiesToCollect)
        {
            case 3:
                return;
            
            case 2:
                int i = Random.Range(0, 3);
                potentialsVegetables[i].gameObject.SetActive(false);
                break;
            
            //veggiesToCollect == 1
            default:
                foreach (var veg in potentialsVegetables)
                {
                    veg.gameObject.SetActive(false);
                }
                int j = Random.Range(0, 3);
                potentialsVegetables[j].gameObject.SetActive(true);
                break;
        }
    }
    
    void OnDestroy()
    {
        conteneur.RemoveVeggie(data, index);
    }

    public void CollectedChild()
    {
        veggiesToCollect--;
        if (veggiesToCollect == 0)
        {
            //TODO dying plant animation
            Destroy(gameObject);
        }
    }

    public void SetNumberOfChildren(int nb)
    {
        veggiesToCollect = nb;
    }
}
