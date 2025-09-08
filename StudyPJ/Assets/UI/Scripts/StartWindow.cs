using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartWindow : GenericWindow
{
    public bool canCountinue = true;

    public Button continueButton;
    public Button newGameButton;
    public Button optionButton;
    //public GameObject GameOverUi;


    protected void Awake()
    {
        if (continueButton != null)
            continueButton.onClick.AddListener(OnClickContinue);
        if (newGameButton != null)
            newGameButton.onClick.AddListener(OnClickNewGame);
        if (optionButton != null)
            optionButton.onClick.AddListener(OnClickOption);
        SaveLoadManager.Data = new SaveDataV4();
        SaveLoadManager.Load();
    }

    public override void Open()
    {
        continueButton.gameObject.SetActive(canCountinue);
        firstSelected = canCountinue ? continueButton.gameObject : newGameButton.gameObject;
        base.Open();
    }

    public void OnClickContinue()
    {
        Debug.Log("Continue Clicked");
        windowManager.windows[(int)Windows.PauseMenu].Open();
    }

    public void OnClickNewGame()
    {
       Debug.Log("New Game Clicked");
       windowManager.windows[(int)Windows.MainMenu].Open();

       //GameOverUi.SetActive(true);
    }
    public void OnClickOption()
    {
       Debug.Log("Option Clicked");
       windowManager.windows[(int)Windows.Settings].Open();
    }

}
