using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextLevelUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUpLevel;    
    public void OnInit(float textUpLevel)
    {
        this.textUpLevel.text = textUpLevel.ToString() + " M";
        Invoke(nameof(DesTroy), 3f);
    }
    private void DesTroy()
    {
        Destroy(gameObject);
    }
}
