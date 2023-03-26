using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementPageLayout : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI[] titles;
   [SerializeField] private TextMeshProUGUI[] descriptions;

   public void UpdateTitle(int index, string title, bool isUnlocked)
   {
      titles[index].text = title;
      titles[index].color = isUnlocked ? Color.green : Color.black;
   }

   public void UpdateDescription(int index, string description, bool isUnlocked)
   {
      descriptions[index].text = description;
      descriptions[index].color = isUnlocked ? Color.green : Color.black;
   }
}
