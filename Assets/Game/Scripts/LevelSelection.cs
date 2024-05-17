using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : Singleton<LevelSelection>
{ 
    public int currentLevel = 1;
    private GameObject curentMap;
       
    private void Awake ()
    {
        //DataManager.Instance.Load();
        //currentLevel = DataManager.Instance.useData.level;
        LoadLevel(currentLevel);
    }
    public void LoadLevel(int level)
    {
        if (curentMap != null)
        {
            Destroy(curentMap);
           
        }
        currentLevel = level;
        GameObject MapPrifab = Resources.Load<GameObject>($"{Level.Map}{level}");
        GameObject map = Instantiate(MapPrifab);       
        
        curentMap = map;       
        CameraFollower.Instance.ResetCamera();
    }
    public void SaveLevel()
    {
        DataManager.Instance.useData.level = currentLevel;
        DataManager.Instance.Save();
    }
}
