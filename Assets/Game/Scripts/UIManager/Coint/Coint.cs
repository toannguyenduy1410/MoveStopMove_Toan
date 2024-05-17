using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coint : Singleton<Coint>
{
    private int coints;
    public int Coints { get { return coints; } }
    private void Awake()
    {
        //DataManager.Instance.useData.Coint = coints;
        //DataManager.Instance.Save();
        DataManager.Instance.Load();
        coints = DataManager.Instance.useData.Coint;
        //LoadCoint();
    }    
    public void LoadCoint()
    {        
        UICoint.instance.OnInit(coints);
    }
    public void InCoint(int coint)
    {
        this.coints += coint;
        DataManager.Instance.useData.Coint = coints;
        DataManager.Instance.Save();
    }
    public void DesCoint(int coint)
    {
        this.coints -= coint;
        DataManager.Instance.useData.Coint = coints;
        DataManager.Instance.Save(); 
    }
}
