using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] private Button ButtonNext;
    [SerializeField] private TextMeshProUGUI txCoint;
    private LevelSelection levelSelection;
    private void OnEnable()
    {
        int coint = Player.instance.CointDesEnemy;
        txCoint.text = coint.ToString();       
        Coint.Instance.InCoint(coint);
    }
    private void Awake()
    {
        levelSelection = LevelSelection.Instance;
    }
    private void Start()
    {
        ButtonNext.onClick.AddListener(Next);
    }
    public void Next()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Instance.CloseUIAll();
        UIManager.Instance.OpenUI<CanvasMenu>();

        levelSelection.currentLevel++;
        levelSelection.LoadLevel(levelSelection.currentLevel);
        //levelSelection.SaveLevel();

    }
}
