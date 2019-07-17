using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public TextMeshProUGUI PlayerMana, EnemyMana;
    public TextMeshProUGUI PlayerHP, EnemyHP;

    public GameObject ResultGO;
    public TextMeshProUGUI ResultTxt;

    public TextMeshProUGUI TurnTime;
    public Button EndTurnBtn;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        EndTurnBtn.interactable = true;
        ResultGO.SetActive(false);
        UpdateHPAndMana();
    }

    public void UpdateHPAndMana()
    {
        PlayerMana.text = GameManagerScr.Instance.CurrentGame.Player.Mana.ToString();
        EnemyMana.text = GameManagerScr.Instance.CurrentGame.Enemy.Mana.ToString();
        PlayerHP.text = GameManagerScr.Instance.CurrentGame.Player.HP.ToString();
        EnemyHP.text = GameManagerScr.Instance.CurrentGame.Enemy.HP.ToString();
    }

    public void ShowResult()
    {
        ResultGO.SetActive(true);

        if (GameManagerScr.Instance.CurrentGame.Enemy.HP == 0)
            ResultTxt.text = "WIN";
        else
            ResultTxt.text = "-25";
    }

    public void UpdateTurnTime(int time)
    {
        TurnTime.text = time.ToString();
    }

    public void DisableTurnBtn()
    {
        EndTurnBtn.interactable = GameManagerScr.Instance.IsPlayerTurn;
    }
}