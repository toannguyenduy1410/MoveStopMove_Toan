using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkin : MonoBehaviour
{
    [SerializeField] private Image imSkin;
    [SerializeField] private Image imLocket;
    [SerializeField] private Image imEnppui;
    [SerializeField] private Button btSkin;
    [SerializeField]  Image imageBorders;
    public void SetIsBprders(bool isBorders)
    {
        imageBorders.gameObject.SetActive(isBorders);
    }
    public void SetIsEnppui(bool isEnppui)
    {
        imEnppui.gameObject.SetActive(isEnppui);
    }
    public void SetIsLocket(bool locket)
    {
        imLocket.gameObject.SetActive(locket);
    }
    public void OnInit(string index, Sprite spSkin, Action<int> HandleClick)
    {
        imSkin.sprite = spSkin;
        btSkin.onClick.AddListener(() =>
        {
            HandleClick.Invoke(int.Parse(index));
        });
    }
}
