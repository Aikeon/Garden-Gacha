using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjCount<T>
{
    public T obj;
    public int count;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float money = 0;
    public int month = 0;
    public float monthDuration;
    [Header("Statistiques")]
    public float totalObtainedMoney = 0;
    public List<ObjCount<VegType>> soldVegCount;
    public float timeInGame = 0f;
    public List<ObjCount<Booster.Rarity>> boughtBoosterCount;
    public int Nsecs;
    private List<float> moneyGainLatestNSecs;
    public float moneyGainInNSecs;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getSecondMoneyGain());
    }

    void Update()
    {
        timeInGame += Time.deltaTime;
    }

    IEnumerator getSecondMoneyGain()
    {
        var latestTotalMoney = totalObtainedMoney;
        yield return new WaitForSeconds(1);
        moneyGainLatestNSecs.Add(totalObtainedMoney - latestTotalMoney);
        moneyGainInNSecs += totalObtainedMoney - latestTotalMoney;
        if (moneyGainLatestNSecs.Count > Nsecs)
        {
            moneyGainInNSecs -= moneyGainLatestNSecs[0];
            moneyGainLatestNSecs.RemoveAt(0);
        }
    }
    
}
