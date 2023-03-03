using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonthDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] monthList;
    private int _month;
    private void Update()
    {
        var gameManagerMonth = GameManager.Instance.month;
        if (gameManagerMonth != _month)
        {
            _month = gameManagerMonth;
            UpdateSprite(); 
        }
    }

    private void UpdateSprite()
    {
        GetComponentInChildren<Image>().sprite = monthList[_month-1];
    }
}
