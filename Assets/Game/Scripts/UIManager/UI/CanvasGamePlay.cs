using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Button btSetting;
    [SerializeField] private TextMeshProUGUI txAtive;
      
    private void Start()
    {       
        btSetting.onClick.AddListener(Setting);
       // InvokeRepeating(nameof(UpdateQuantityEnemy),0f,0.5f);              
    }
    private void Update()
    {
        UpdateQuantityEnemy();
    }
    public override void SetUp()
    {
        base.SetUp();       
    }
    public void UpdateQuantityEnemy()
    {        
        txAtive.text = "Ative : " + Bot.Instance.aliveEnemy.ToString();
        if(Bot.Instance.aliveEnemy == 1)
        {
            Close(0);           
            UIManager.Instance.OpenUI<CanvasVictory>();
            GameManager.ChangeState(GameState.Victory);
            Player.instance.WinGame();
        }
    }
    public void Setting()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSetting>();
        GameManager.ChangeState(GameState.Setting);
    }
}
