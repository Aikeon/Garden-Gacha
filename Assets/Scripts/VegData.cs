using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegetable", menuName = "Game/Vegetable")]
public class VegData : ScriptableObject
{
    public static readonly Dictionary<VegType, Dictionary<VegType, int>> VegMatrix =
        new Dictionary<VegType, Dictionary<VegType, int>>()
        {
            {
                VegType.Ail,
                new Dictionary<VegType, int>()
                {
                    {VegType.Ail,2},
                    { VegType.Carotte,3},
                    { VegType.Chou,2},
                    { VegType.Haricot,1},
                    { VegType.Oignon,2},
                    { VegType.Poireau,2},
                    { VegType.Tomate,3},
                    { VegType.None,2}
                }
            },
            {
                VegType.Carotte,
                new Dictionary<VegType, int>()
                {
                    {VegType.Ail,3},
                    { VegType.Carotte,2},
                    { VegType.Chou,2},
                    { VegType.Haricot,3},
                    { VegType.Oignon,3},
                    { VegType.Poireau,3},
                    { VegType.Tomate,3},
                    { VegType.None,2}
                }
            },
            {
                VegType.Chou,
                new Dictionary<VegType, int>()
                {
                    {VegType.Ail,2},
                    { VegType.Carotte,2},
                    { VegType.Chou,2},
                    { VegType.Haricot,3},
                    { VegType.Oignon,1},
                    { VegType.Poireau,2},
                    { VegType.Tomate,1},
                    { VegType.None,2}
                }
            },
            {
                VegType.Haricot,
                new Dictionary<VegType, int>()
                {
                    {VegType.Ail,1},
                    { VegType.Carotte,3},
                    { VegType.Chou,2},
                    { VegType.Haricot,2},
                    { VegType.Oignon,1},
                    { VegType.Poireau,1},
                    { VegType.Tomate,2},
                    { VegType.None,2}
                }
            },
            {
                VegType.Oignon,
                new Dictionary<VegType, int>()
                {
                    {VegType.Ail,3},
                    { VegType.Carotte,3},
                    { VegType.Chou,1},
                    { VegType.Haricot,1},
                    { VegType.Oignon,2},
                    { VegType.Poireau,1},
                    { VegType.Tomate,3},
                    { VegType.None,2}
                }
            },
            {
                VegType.Poireau,
                new Dictionary<VegType, int>()
                {
                    {VegType.Ail,2},
                    { VegType.Carotte,3},
                    { VegType.Chou,2},
                    { VegType.Haricot,1},
                    { VegType.Oignon,2},
                    { VegType.Poireau,2},
                    { VegType.Tomate,3},
                    { VegType.None,2}
                }
            },
            {
                VegType.Tomate,
                new Dictionary<VegType, int>()
                {
                    {VegType.Ail,2},
                    { VegType.Carotte,3},
                    { VegType.Chou,3},
                    { VegType.Haricot,2},
                    { VegType.Oignon,3},
                    { VegType.Poireau,2},
                    { VegType.Tomate,2},
                    { VegType.None,2}
                }
            }
        };
    
    public GameObject vegPrefab;
    public float vegGrowthTime;
    public VegType mapType;
    public int prodNumber;
    public float basePrice;
}

public enum VegType
{
    None,
    Tomate,
    Oignon,
    Haricot,
    Chou,
    Ail,
    Poireau,
    Carotte
}

public enum Quantity
{
    Good,
    Normal,
    Bad
}

