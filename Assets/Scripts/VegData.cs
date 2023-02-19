using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegetable", menuName = "Game/Vegetable")]
public class VegData : ScriptableObject
{
    public GameObject vegPrefab;
    public float vegGrowthTime;
    public VegType mapType;
    public int prodNumber;
    public float basePrice;
}

public enum VegType
{
    Tomate,
    Oignon,
    Haricot,
    Chou,
    Ail,
    Poireau,
    Carotte
}
