using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveService : MonoBehaviour
{
    private string _filePath;

    public GameSaveData SaveData { get; private set; } = new GameSaveData();

    private void OnEnable()
    {
        _filePath = Application.persistentDataPath + "/Save.json";
        Load();
    }
    
    public void Save()
    {
        using (FileStream file = File.Create(_filePath))
        {
            new BinaryFormatter().Serialize(file, SaveData);
        }
    }
    
    public void Load()
    {
        using (FileStream file = File.Open(_filePath, FileMode.Open))
        {
            object loadedData = new BinaryFormatter().Deserialize(file);
            SaveData = (GameSaveData)loadedData;
        }
    }
}

[Serializable]
public class GameSaveData
{
    public Dictionary<string, SaveData> _data = new Dictionary<string, SaveData>();

    public void AddData(string id, SaveData data)
    {
        _data.TryAdd(id, data);
    }

    public bool TryGetData<T>(string id, out T data) where T : SaveData
    {
        foreach (SaveData dataList in _data.Values)
        {
            if (dataList.Id == id)
            {
                data = (T)dataList;
                return true;
            }
        }
        data = null;
        return false;
    }
    
    public GameSaveData()
    {

    }
}

[Serializable]
public class SaveData
{
    public string Id { get; private set; }
    public Type Type { get; private set; }
    

    public SaveData(string id, Type type)
    {
        Id = id;
        Type = type;
    }
}
