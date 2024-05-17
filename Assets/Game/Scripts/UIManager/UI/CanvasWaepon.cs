using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWaepon : UICanvas
{
    [SerializeField] private Button btCloss;
    private void OnDisable()
    {
        if (Player.instance != null)
        {
            Player.instance.gameObject.SetActive(true);
        }
    }
    void Start()
    {
        btCloss.onClick.AddListener(BTCloss);
    }

    private void BTCloss()
    {
        Player.instance.gameObject.SetActive(true);
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();               
        GameManager.ChangeState(GameState.MainMenu);
        Player.instance.UpdateAnim();
    }    
}
