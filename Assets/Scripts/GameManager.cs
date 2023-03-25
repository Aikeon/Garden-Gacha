using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
    public Transform Tables;
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
        LoadGame();
        StartCoroutine(getSecondMoneyGain());
    }

    void Update()
    {
        timeInGame += Time.deltaTime;
    }

    void OnApplicationQuit()
    {
        SaveGame();
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

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    { 
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            money = save.money;
            totalObtainedMoney = save.totalObtainedMoney;
            month = save.month;
            timeInGame = save.timeInGame;
            soldVegCount = save.soldVegCount;
            boughtBoosterCount = save.boughtBoosterCount;
            foreach (Achievement a in Achievements.Instance.achievements)
            {
                if (save.achievementsDone.Contains(a.name))
                {
                    a.done = true;
                }
            }
            foreach (int i in save.unlockedDirts)
            {
                Tables.GetChild(i).GetComponent<LockedDirt>().dirt.enabled = true;
            }

            // Resets menu display

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.money = money;
        save.totalObtainedMoney = totalObtainedMoney;
        save.month = month;
        save.timeInGame = timeInGame;
        save.soldVegCount = soldVegCount;
        save.boughtBoosterCount = boughtBoosterCount;

        save.unlockedDirts = new List<int>();
        var numTables = Tables.childCount;
        for (int i = 0; i < numTables; i++)
        {
            if (Tables.GetChild(i).TryGetComponent<LockedDirt>(out var possiblyLockedDirt))
            {
                if (possiblyLockedDirt.dirt.enabled) save.unlockedDirts.Add(i);
            }
        }

        save.achievementsDone = new List<string>();
        foreach (Achievement a in Achievements.Instance.achievements)
        {
            if (a.done) save.achievementsDone.Add(a.name);
        }

        return save;
    }
    
}

[System.Serializable]
public class Save
{
    public float money;
    public float totalObtainedMoney;
    public int month;
    public float timeInGame;
    public List<ObjCount<VegType>> soldVegCount;
    public List<ObjCount<Booster.Rarity>> boughtBoosterCount;
    public List<string> achievementsDone;
    public List<int> unlockedDirts;
}

