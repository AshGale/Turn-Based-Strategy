using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInputMenue : MonoBehaviour
{

    public static PlayerInputMenue instance;


    public void Awake()
    {
        instance = this;
    }

    public GameObject inputMenue, moveMenue, meleeMenue;
    public TMP_Text turnPointText, errorText;

    public float errorDispalyTime = 2f;
    private float errorCounter;

    public void HideMenues()
    {
        inputMenue.SetActive(false);
        moveMenue.SetActive(false);
        meleeMenue.SetActive(false);
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

    public void ShowMeleeMenue()
    {
        HideMenues();
        meleeMenue.SetActive(true);
    }

    public void HideMeleeMenue()
    {
        HideMenues();
        ShowInputMenues();
        GameManager.instance.targetDisplay.gameObject.SetActive(false);
    }

    public void CheckMelee()
    {
        GameManager.instance.activePlayer.GetMeleeTargets();

        if(GameManager.instance.activePlayer.meleeTargets.Count > 0)
        {
            ShowMeleeMenue();
            GameManager.instance.targetDisplay.SetActive(true);
            GameManager.instance.targetDisplay.transform.position = GameManager.instance.activePlayer.meleeTargets[GameManager.instance.activePlayer.currentMeleeTarget].transform.position;
        } else
        {
            ShorErrorText("No Enemies in melee range!");
        }
    }

    public void MeleeHit()
    {
        GameManager.instance.activePlayer.DoMelee();
        GameManager.instance.currentActionCost = 1;

        HideMenues();
        StartCoroutine(WaitToEndActionCo(1f));
    }

    public IEnumerator WaitToEndActionCo(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        GameManager.instance.SpendTurnPoints();
    }

    public void NextMeleeTarget()
    {
        GameManager.instance.activePlayer.currentMeleeTarget++;
        if(GameManager.instance.activePlayer.currentMeleeTarget >= GameManager.instance.activePlayer.meleeTargets.Count)
        {
            GameManager.instance.activePlayer.currentMeleeTarget = 0;
        }

        //update indicator
        GameManager.instance.targetDisplay.transform.position = GameManager.instance.activePlayer.meleeTargets[GameManager.instance.activePlayer.currentMeleeTarget].transform.position;
    }

    public void ShorErrorText(string messageToShow)
    {
        errorText.text = messageToShow;
        errorText.gameObject.SetActive(true);

        errorCounter = errorDispalyTime;
    }

    private void Update()
    {
        if(errorCounter > 0)
        {
            errorCounter -= Time.deltaTime;

            if(errorCounter <= 0)
            {
                errorText.gameObject.SetActive(false);
            }
        }
    }
}
