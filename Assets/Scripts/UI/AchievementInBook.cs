using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementInBook : MonoBehaviour
{
    private Book _book;
    private AutoFlip _autoFlip;
    [SerializeField] private AchievementPageLayout leftPage;
    [SerializeField] private AchievementPageLayout rightPage;
    [SerializeField] private GameObject achievementPages;
    private void Start()
    {
        _book = GetComponent<Book>();
        _autoFlip = GetComponent<AutoFlip>();
    }

    void Update()
    {
        if ((_book.currentPage is >= 16 and <= 20) && _autoFlip.isFlipping == false)
        {
            achievementPages.SetActive(true);
            int forBeginValue = (_book.currentPage - 16) * 4;
            for (int i = 0; i < 4; i++)
            {
                leftPage.UpdateTitle(i, Achievements.Instance.achievements[forBeginValue+i].name, Achievements.Instance.achievements[forBeginValue+i].done);
                leftPage.UpdateDescription(i, Achievements.Instance.achievements[forBeginValue+i].description, Achievements.Instance.achievements[forBeginValue+i].done);
            }

            forBeginValue += 4;
            for (int i = 0; i < (_book.currentPage == 20 ? 2 : 4); i++)
            {
                rightPage.UpdateTitle(i, Achievements.Instance.achievements[forBeginValue+i].name, Achievements.Instance.achievements[forBeginValue+i].done);
                rightPage.UpdateDescription(i, Achievements.Instance.achievements[forBeginValue+i].description, Achievements.Instance.achievements[forBeginValue+i].done);
            }

            if (_book.currentPage == 20)
            {
                rightPage.UpdateTitle(2, "", true);
                rightPage.UpdateDescription(2, "", true);
                rightPage.UpdateTitle(3, "", true);
                rightPage.UpdateDescription(3, "", true);
            }
        }
        else
        {
            achievementPages.SetActive(false);
        }
    }
}
