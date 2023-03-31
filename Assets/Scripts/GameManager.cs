using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float money = 0;
    public int month = 0;
    public Transform Tables;
    public float monthDuration;
    [Header("Statistiques")]
    public float totalObtainedMoney = 0;
    public float soldAil = 0;
    public float soldCarrottes = 0;
    public float soldChoux = 0;
    public float soldHaricots = 0;
    public float soldOignons = 0;
    public float soldPoireaux = 0;
    public float soldTomates = 0;
    public float totalVegCount = 0;
    public float timeInGame = 0f;
    public float boughtBronze = 0;
    public float boughtSilver = 0;
    public float boughtGold = 0;
    public float totalBoosterCount = 0;
    public int Nsecs;
    private List<float> moneyGainLatestNSecs = new List<float>();
    public float moneyGainInNSecs;
    public float moneyGainIn30Secs;
    public float moneyGainIn60Secs;
    public bool autosaveOn;

    private int destroySaveCount;

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
        if (Input.GetKeyDown(KeyCode.M)) DestroySave();
    }

    void OnApplicationQuit()
    {
        if (autosaveOn)
        SaveGame();
    }

    IEnumerator getSecondMoneyGain()
    {
        var latestTotalMoney = totalObtainedMoney;
        yield return new WaitForSeconds(1);
        moneyGainLatestNSecs.Add(totalObtainedMoney - latestTotalMoney);
        if (moneyGainLatestNSecs.Count > 30)
        {
            moneyGainIn30Secs -= moneyGainLatestNSecs[moneyGainLatestNSecs.Count-30];
        }
        if (moneyGainLatestNSecs.Count > 60)
        {
            moneyGainIn60Secs -= moneyGainLatestNSecs[moneyGainLatestNSecs.Count-60];
        }
        if (moneyGainLatestNSecs.Count > Nsecs)
        {
            moneyGainInNSecs -= moneyGainLatestNSecs[0];
            moneyGainLatestNSecs.RemoveAt(0);
        }
        moneyGainInNSecs += totalObtainedMoney - latestTotalMoney;
        moneyGainIn30Secs += totalObtainedMoney - latestTotalMoney;
        moneyGainIn60Secs += totalObtainedMoney - latestTotalMoney;
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

            soldAil = save.soldAil;
            soldCarrottes = save.soldCarrottes;
            soldChoux = save.soldChoux;
            soldHaricots = save.soldHaricots;
            soldOignons = save.soldOignons;
            soldPoireaux = save.soldPoireaux;
            soldTomates = save.soldTomates;
            totalVegCount = soldAil + soldCarrottes + soldChoux + soldHaricots + soldOignons + soldPoireaux + soldTomates;

            boughtBronze = save.boughtBronze;
            boughtSilver = save.boughtSilver;
            boughtGold = save.boughtGold;
            totalBoosterCount = boughtBronze + boughtSilver + boughtGold;

            foreach (Achievement a in Achievements.Instance.achievements)
            {
                if (save.achievementsDone.Contains(a.name))
                {
                    a.done = true;
                }
            }
            foreach (int i in save.unlockedDirts)
            {
                Tables.GetChild(i).GetComponent<LockedDirt>().Unlock(true);
            }

            // Resets menu display

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

    public void DestroySave()
    {
        destroySaveCount++;
        if (destroySaveCount >= 3)
        {
            if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
            {
                File.Delete(Application.persistentDataPath + "/gamesave.save");
            }
            autosaveOn = false;
        }
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.money = money;
        save.totalObtainedMoney = totalObtainedMoney;
        save.month = month;
        save.timeInGame = timeInGame;

        save.soldAil = soldAil;
        save.soldCarrottes = soldCarrottes;
        save.soldChoux = soldChoux;
        save.soldHaricots = soldHaricots;
        save.soldOignons = soldOignons;
        save.soldPoireaux = soldPoireaux;
        save.soldTomates = soldTomates;

        save.boughtBronze = boughtBronze;
        save.boughtSilver = boughtSilver;
        save.boughtGold = boughtGold;

        save.unlockedDirts = new List<int>();
        var numTables = Tables.childCount;
        for (int i = 0; i < numTables; i++)
        {
            if (Tables.GetChild(i).TryGetComponent<LockedDirt>(out var possiblyLockedDirt))
            {
                if (possiblyLockedDirt.unlocked) save.unlockedDirts.Add(i);
            }
        }

        save.achievementsDone = new List<string>();
        foreach (Achievement a in Achievements.Instance.achievements)
        {
            if (a.done) save.achievementsDone.Add(a.name);
        }

        return save;
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}

[System.Serializable]
public class Save
{
    public float money;
    public float totalObtainedMoney;
    public int month;
    public float timeInGame;
    public float soldAil;
    public float soldCarrottes;
    public float soldChoux;
    public float soldHaricots;
    public float soldOignons;
    public float soldPoireaux;
    public float soldTomates;
    public float boughtBronze;
    public float boughtSilver;
    public float boughtGold;
    public List<string> achievementsDone;
    public List<int> unlockedDirts;
}

