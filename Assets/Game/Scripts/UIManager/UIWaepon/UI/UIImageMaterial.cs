using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageMaterial : MonoBehaviour
{
    [SerializeField] public Image imMaterialKh;
    [SerializeField] private Image imMaterial;    
    [SerializeField] private Button btMaterial;
   
    public void SetColor (Color32 color32)
    {
        imMaterial.color = color32;
    }
    public void OnInit(string index,Color32 color, Action<int,Image> onclick)
    {      
        imMaterial.color = color;        
        btMaterial.onClick.AddListener(() =>
        {
            onclick?.Invoke(int.Parse(index), imMaterialKh);
        });
    }
}
