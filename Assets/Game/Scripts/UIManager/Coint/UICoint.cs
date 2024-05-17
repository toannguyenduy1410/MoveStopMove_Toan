using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICoint : UICanvas
{
    public static UICoint instance;
    [SerializeField] private TextMeshProUGUI textCoint;
    private void Awake()
    {
        instance = this;
    }
   
    public void OnInit(int coint)
    {
        textCoint.text = coint.ToString();
    }
}
