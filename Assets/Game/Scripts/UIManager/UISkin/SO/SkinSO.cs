using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skin/new")]
public class SkinSO : ScriptableObject
{
    public List<SkinHair> listSkinHair;
    public List<SkinPant> listSkinPant;
    public List<SkinShield> listSkinShield;
    public List<SkinSet> listSkinSet;
}
[Serializable]
public class SkinHair
{
    public int coint;
    public Sprite sprite;
    public Material[] matPlayer;
    public List<ObjSkin> listObjSkin;
}
[Serializable]
public class SkinPant
{
    public int coint;
    public Sprite sprite;
    public Material[] matPlayer;
    public Material[] material;
}
[Serializable]
public class SkinShield
{
    public int coint;
    public Sprite sprite;
    public Material[] matPlayer;
    public ObjSkin objSkin;
}
[Serializable]
public class SkinSet
{
    public int coint;
    public Sprite sprite;
    public Material[] material;
    public List<ObjSkin> listObjSkin;
}
[Serializable]
public class ObjSkin
{
    public ParentSkin parent;
    public GameObject objSkin;
}

public enum PageSkinType
{
    SkinHair = 0,
    SkinPant = 1,
    SkinShield = 2,
    SkinSet = 3
}
public enum ParentSkin
{
    hair = 0,
    lefthand = 1,
    shield = 2,
    wing = 3,   
    tail = 4,
}
public enum HairType
{
    Arrow = 0,
    Crown = 1,
    Ear = 2,
    Flower = 3,
    Hair = 4,
    Hat = 5,
    Hat_Cap = 6,
    Horn = 7,
}
public enum PantsType
{
    Batman = 0,
    chambi = 1,
    comy = 2,
    dabao = 3,
    onion = 4,
    pokemon = 5,
    skull = 6,
    vantim = 7,
}