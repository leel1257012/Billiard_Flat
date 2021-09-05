using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class LevelClear : MonoBehaviour
{
    public static LevelClear instance;
    int stageCount = 4;
    int levelCount = 6;

    public int currentStage;
    public int currentLevel;

    public LevelLoad levelLoad;

    public bool admin;


    public LevelClearDataStruct LevelClearData;
    string DataPath;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);


        admin = false; // fixme

        DataPath = Path.Combine(Application.persistentDataPath + "/LevelClearData.json");
        FileInfo savefile = new FileInfo(DataPath);

        if (savefile.Exists)
        {
            LoadLevel();
        }
        else
        {
            LevelClearData.IsClear = new bool[stageCount, levelCount];
            for(int i=0; i<stageCount; i++)
            {
                for(int j=0; j<levelCount; j++)
                {
                    LevelClearData.IsClear[i, j] = false;
                }
            }
            //LevelClearData.IsClear[0, 0] = true;
            SaveLevel();
        }
    }

    private void Start()
    {
        

    }

    void SaveLevel()
    {
        string jsonData = JsonConvert.SerializeObject(LevelClearData);
        File.WriteAllText(DataPath, jsonData);
    }

    void LoadLevel()
    {
        string jsonData = File.ReadAllText(DataPath);
        LevelClearData = JsonConvert.DeserializeObject<LevelClearDataStruct>(jsonData);
    }

    public bool isCleared(int stage, int level)
    {
        return LevelClearData.IsClear[stage - 1, level - 1];
    }

    public void ClearLevel(int stage, int level)
    {
        LevelClearData.IsClear[stage - 1, level - 1] = true;
        SaveLevel();
    }
    
}

[System.Serializable]
public class LevelClearDataStruct
{
    public bool[,] IsClear;
    public LevelClearDataStruct()
    {
    }
}
