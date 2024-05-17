using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIscores : Singleton<UIscores>
{
    [SerializeField] private TextMeshProUGUI textScores;    
    [SerializeField] private Image imScores;    
    [SerializeField] private TextMeshProUGUI txName;    
    private void LateUpdate()
    {
        transform.LookAt(transform.position + CameraFollower.Instance.transform.forward);
    }

    public void TextLevel(int tetextScoresxtS, Color colName)
    {
        this.textScores.text = tetextScoresxtS.ToString();
        imScores.color = colName;
    }
    public void TextName(string tXName, Color colName)
    {
        txName.text = tXName;
        txName.color = colName;
    }
}
