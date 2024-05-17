using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UICanvas
{
    [SerializeField] private Button buttonMenu;
    [SerializeField] private Button buttonConteniu;


    private void Start()
    {
        buttonMenu.onClick.AddListener(MenuGame);
        buttonConteniu.onClick.AddListener(Conteniu);
    }
   
    public void MenuGame()
    {
        UIManager.Instance.CloseUIAll();
        UIManager.Instance.OpenUI<CanvasMenu>();        
        LevelSelection.Instance.LoadLevel(LevelSelection.Instance.currentLevel);
        GameManager.ChangeState(GameState.MainMenu);
    }
    private void Conteniu()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.ChangeState(GameState.GamePlay);
    }

}
