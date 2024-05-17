using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIButtonMaterial : MonoBehaviour
{
    [SerializeField] private Image imMaterial;
    [SerializeField] private Button btMaterial;    
    public Material material;  
    public void OnInit( Color32 ColorMaterial, Material material, Action<Material, Color32> onclick)
    {              
        imMaterial.color = ColorMaterial;
        this.material = material;
        btMaterial.onClick.AddListener(() =>
        {
            onclick?.Invoke(this.material, ColorMaterial);
        });
    }
}
