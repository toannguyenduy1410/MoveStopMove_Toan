using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMaterialWaepon : MonoBehaviour
{
    [SerializeField] private Camera camerawaepon;
    [SerializeField] private Button buttonWaepon;
    [SerializeField] private RawImage RImageWaepon;    
    [SerializeField] private Image Imagelocked;
    [SerializeField] private Transform parentWaepon;

    public Image ImageKhung;
    public RenderTexture renderTexture;
    public GameObject obj;
    public void Instan(GameObject waepon)
    {
        obj = Instantiate(waepon, parentWaepon);
    }
    public void SetLocket(bool newLocket)
    {             
        Imagelocked.gameObject.SetActive(newLocket); 
    }  
    public void OnInit(string textIndex,RenderTexture texture,Action<int, Image, RenderTexture> LevelButtonClick)
    {        
        camerawaepon.targetTexture = texture;
        RImageWaepon.texture = texture;
        renderTexture = texture;
        buttonWaepon.onClick.AddListener(() =>
        {
            LevelButtonClick?.Invoke(int.Parse(textIndex), ImageKhung, renderTexture);
        });

    }   
}
