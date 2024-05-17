using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPageSkin : MonoBehaviour
{
    [SerializeField] private Image imSkin;
    [SerializeField] private Button btSkin;
    public Image imageGR;
    
    public void OnInit(string index,Sprite spSkin,Action<int,Image> HandleClick)
    {
        imSkin.sprite = spSkin;
        btSkin.onClick.AddListener(() =>
        {
            HandleClick.Invoke(int.Parse(index), imageGR);
        });
    }
}
