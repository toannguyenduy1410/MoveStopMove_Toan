using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScale : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScale;

    public void OnInit(string tesScale)
    {
        this.textScale.text = tesScale;
    }
}
