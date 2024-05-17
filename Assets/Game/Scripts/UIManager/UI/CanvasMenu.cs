using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CanvasMenu : UICanvas
{
    [SerializeField] private Button btPlay;
    [SerializeField] private Button btWaepon;
    [SerializeField] private Button btSkin;
    [SerializeField] private TMP_InputField inputField;
    private const int maxNameLength = 8; // Số ký tự tối đa cho phép nhập
    private void OnEnable()
    {
        UIManager.Instance.OpenUI<UICoint>();
        Coint.Instance.LoadCoint();
        OffScreenIndicator.Instance.gameObject.SetActive(false);
        LoadName();
    }
    private void Start()
    {
        btPlay.onClick.AddListener(BTPlayGame);
        btWaepon.onClick.AddListener(BTWaepon);
        btSkin.onClick.AddListener(BTSkin);
        inputField.characterLimit = maxNameLength;
        inputField.onEndEdit.AddListener(OnInputEndEdit);
    }



    public void BTPlayGame()
    {
        GameManager.ChangeState(GameState.GamePlay);
        Close(0);
        UIManager.Instance.CloseUI<UICoint>(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        OffScreenIndicator.Instance.gameObject.SetActive(true);
        //on uilevel
        CharactorManager.Instance.CharactorActiveLevel();

    }
    public void BTWaepon()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasWaepon>();
        GameManager.ChangeState(GameState.SetGun);

        Player.instance.gameObject.SetActive(false);
    }
    public void BTSkin()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSkin>();
        GameManager.ChangeState(GameState.SetSkin);
        Player.instance.UpdateAnim();
    }
    private void OnInputEndEdit(string name)
    {       
        DataManager.Instance.useData.name = name;
        DataManager.Instance.Save();
        Player.instance.UISetName();
    }
    private void LoadName()
    {
        DataManager.Instance.Load();
        string name = DataManager.Instance.useData.name;
        inputField.text = name;
       
    }
}
