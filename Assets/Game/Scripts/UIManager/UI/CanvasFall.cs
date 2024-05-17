using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFall : UICanvas
{
    [SerializeField] private Button btMenu;
    [SerializeField] private TextMeshProUGUI txTopXH;
    [SerializeField] private TextMeshProUGUI txNameBot;
    [SerializeField] private TextMeshProUGUI txCoint;

    
    private LevelSelection levelSelection;
    private void Awake()
    {
        
        levelSelection = LevelSelection.Instance;
    }
    private void OnEnable()
    {
        int coint = Player.instance.CointDesEnemy;
        int top = Bot.Instance.aliveEnemy;
        string nameBot = Player.instance.NameBot;
        OffScreenIndicator.Instance.gameObject.SetActive(false);
        txCoint.text = coint.ToString();
        txNameBot.text = nameBot;
        txTopXH.text = "# " + top.ToString();
        Coint.Instance.InCoint(coint);
    }
    private void Start()
    {
        btMenu.onClick.AddListener(MenuGame);
    }
    public void MenuGame()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
        //Character.Instance.OnInit();
        levelSelection.LoadLevel(levelSelection.currentLevel);
        GameManager.ChangeState(GameState.MainMenu);

    }
}
