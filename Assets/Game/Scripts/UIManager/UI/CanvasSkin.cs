using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkin : UICanvas
{
    [SerializeField] private Button btCloss;
    private void OnEnable()
    {
       // Player.instance.ChangAnim(Anim.ANIM_DANCE);       
    }
    private void OnDisable()
    {
        //if(Player.instance != null)
        //{
        //    Player.instance.ChangAnim(Anim.ANIM_IDLE);
        //}        
    }
    void Start()
    {
        btCloss.onClick.AddListener(BTCloss);
    }

    private void BTCloss()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        Player.instance.UpdateAnim();
        Player.instance.SetSkin();
    }
}
