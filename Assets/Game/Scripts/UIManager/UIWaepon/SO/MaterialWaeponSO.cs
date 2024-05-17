using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MaterrialWaepon/new")]
public class MaterialWaeponSO : ScriptableObject
{
    public List<Waepon> listmateriaWaepon;
}
[Serializable]
public class Waepon
{
    public List<taMaterialWeapons> listmaterials;
    public int price;
    public int AmountMaterial;
    public Sprite sprite;
}
[Serializable]
public class taMaterialWeapons
{
    public int index;
    public List<Material> materials;
    public GameObject waepon;
    public RenderTexture textures;
}
