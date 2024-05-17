using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Materrials/new")]

public class ListMaterialSO : ScriptableObject
{
    public List<Materialss> listmaterials;   
}
[Serializable]
public class Materialss
{
    public Material Material;
    public Color32 ColorMaterial;
}