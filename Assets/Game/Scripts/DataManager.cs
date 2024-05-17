using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public UseData useData;
    public SkinData skinData;
   
    public void Load()
    {
        // Load dữ liệu từ PlayerPrefs và chuyển đổi thành đối tượng UseData
        string jsonData = PlayerPrefs.GetString("useData");
        if (!string.IsNullOrEmpty(jsonData))
        {
            useData = JsonUtility.FromJson<UseData>(jsonData);           
        }
    }

    public void Save()
    {
        // Chuyển đổi đối tượng UseData thành chuỗi JSON và lưu vào PlayerPrefs
        string jsonData = JsonUtility.ToJson(useData);
        PlayerPrefs.SetString("useData", jsonData);
    }
    public void LoadSkin()
    {
        // Load dữ liệu từ PlayerPrefs và chuyển đổi thành đối tượng UseData
        string jsonData = PlayerPrefs.GetString("skinData");
        if (!string.IsNullOrEmpty(jsonData))
        {
            skinData = JsonUtility.FromJson<SkinData>(jsonData);
        }
    }
    public void SaveSkin()
    {
        // Chuyển đổi đối tượng UseData thành chuỗi JSON và lưu vào PlayerPrefs
        string jsonData = JsonUtility.ToJson(skinData);
        PlayerPrefs.SetString("skinData", jsonData);
    }
}
