using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonthDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] monthList;
    private float _time = 0;
    private int _month = 0;
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > GameManager.Instance.monthDuration)
        {
            _month += 1;
            GameManager.Instance.month = _month;
            UpdateSprite();
            _time = 0;
        }
    }

    private void UpdateSprite()
    {
        GetComponentInChildren<Image>().sprite = monthList[(_month)%12];
    }
}
