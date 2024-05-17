using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UseData
{     
    public int level;
    public int Coint;
    public string name;
    public List<WeaponData> WaeponList;
    public List<ColorWaepon> colorList;
    public WaeponType currentEquippedWeaponType;
    public int curentEquipping;    

}
[Serializable]
public class WeaponData
{
    public bool isBoght;
    public List<bool> isLocked = new List<bool>();
    public List<bool> isEnppui = new List<bool>();
}
[Serializable]
public class ColorWaepon
{
    public Color32[] colors;
    public Material[] materials;
}
[Serializable]
public class SkinData
{
    public PageSkinType DataCurrentPageSkin;
    public int dataCurrentEnppui;
    public bool isEnppui;
    public List<Skin> listSkinData = new List<Skin>();
}
[Serializable]
public class Skin
{
    public List<bool> isLocked = new List<bool>();
    public List<bool> isEnppui = new List<bool>();
}
