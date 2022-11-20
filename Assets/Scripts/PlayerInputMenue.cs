using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInputMenue : MonoBehaviour
{

    public static PlayerInputMenue instance;

    public TMP_Text turnPointText;

    public void Awake()
    {
        instance = this;
    }

    public GameObject inputMenue, moveMenue;

    public void HideMenues()
    {
        inputMenue.SetActive(false);
        moveMenue.SetActive(false);
    }

    public void ShowInputMenues()
    {
        inputMenue.SetActive(true);
    }

    public void ShowMoveMenue()
    {
        HideMenues();
        moveMenue.SetActive(true);

        ShowMove();
    }

    public void HideMoveMenue()
    {
        HideMenues();
        MoveGrid.instance.HideMovePoints();
        ShowInputMenues();
    }

    public void ShowMove()
    {
        if(GameManager.instance.turnPointsRemaining >= 1)
        {
            MoveGrid.instance.ShowPointsInRange(GameManager.instance.activePlayer.moveRange, GameManager.instance.activePlayer.transform.position);
            GameManager.instance.currentActionCost = 1;
        }
    }

    public void ShowRun()
    {
        if (GameManager.instance.turnPointsRemaining >= 2)
        {
            MoveGrid.instance.ShowPointsInRange(GameManager.instance.activePlayer.runRange, GameManager.instance.activePlayer.transform.position);
            GameManager.instance.currentActionCost = 2;
        }
    }

    public void UpdateTurnPointText(int turnPoints)
    {
        turnPointText.text = "Turn Points Remaining: " + turnPoints;
    }

    public void SkipTurn()
    {
        GameManager.instance.EndTurn();
    }
}
