using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
//using UnityEngine.Formats.Alembic.Importer;

public class GrowingPlant : MonoBehaviour
{
    public GrowthBucket conteneur; //TODO utiliser la liste du conteneur pour donner le multiplicateur
    private float growthIndicator = 0f;
    public VegData data;
    public bool isMature = false;
    public int VeggiesToCollect;

    private Animator _anim;
    public GameObject vegetable;

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
    
    private void OnEnable()
    {
        _anim = GetComponentInChildren<Animator>();
        StartCoroutine(GrowthCoroutine());
    }

    IEnumerator GrowthCoroutine()
    {
        _anim.CrossFade("Growth", 0.01f);
        _anim.speed = 1 / data.vegGrowthTime;
        yield return new WaitForSeconds(data.vegGrowthTime);
        vegetable.SetActive(true);
        _anim.gameObject.SetActive(false);
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
