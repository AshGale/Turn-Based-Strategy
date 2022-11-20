using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    public CharacterController activePlayer;

    public List<CharacterController> allChars = new List<CharacterController>();
    public List<CharacterController> playerTeam = new List<CharacterController>(), enemyTeam = new List<CharacterController>();

    private int currentChar;

    public int totalTurnPoints = 2;
    [HideInInspector]
    public int turnPointsRemaining;
    public int currentActionCost;

    // Start is called before the first frame update
    void Start()
    {
        List<CharacterController> tempList = new List<CharacterController>();

        tempList.AddRange(FindObjectsOfType<CharacterController>());

        int iterations = tempList.Count + 50;
        while(tempList.Count > 0 && iterations > 0)
        {
            int randomPick = Random.Range(0, tempList.Count);
            allChars.Add(tempList[randomPick]);
            
            tempList.RemoveAt(randomPick);
            iterations--;//ensure loop will run in case of bad logic
        }


        //allChars.AddRange(FindObjectsOfType<CharacterController>());
        foreach(CharacterController cc in allChars)
        {
            if(cc.isEnemy == true)
            {
                enemyTeam.Add(cc);
            } else
            {
                playerTeam.Add(cc);
            }
        }
        allChars.Clear();
        
        //50/50 chance of players or enemyies going first
        if(Random.value >= 0.5)
        {
            allChars.AddRange(playerTeam);
            allChars.AddRange(enemyTeam);
        } else
        {
            allChars.AddRange(enemyTeam);
            allChars.AddRange(playerTeam);
        }

        activePlayer = allChars[0];
        CameraController.instance.SetMoveTarget(activePlayer.transform.position);

        //setup first turn with normal flow of logic.
        currentChar = -1;
        EndTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishMovement()
    {
        SpendTurnPoints();
    }

    public void SpendTurnPoints()
    {
        //number of actions per turn
        turnPointsRemaining -= currentActionCost;

        if(turnPointsRemaining <= 0)
        {
            EndTurn();
        } else
        {
            if (activePlayer.isEnemy == true)
            {
                PlayerInputMenue.instance.HideMenues();
            }
            else
            {
                //MoveGrid.instance.ShowPointsInRange(activePlayer.moveRange, activePlayer.transform.position);
                PlayerInputMenue.instance.ShowInputMenues();
            }
        }
        PlayerInputMenue.instance.UpdateTurnPointText(turnPointsRemaining);
    }

    public void EndTurn()
    {
        currentChar++;
        if(currentChar >= allChars.Count)
        {
            currentChar = 0;
        }

        //start turn

        activePlayer = allChars[currentChar];

        CameraController.instance.SetMoveTarget(activePlayer.transform.position);

        turnPointsRemaining = totalTurnPoints;

        if(activePlayer.isEnemy == true)
        {
            PlayerInputMenue.instance.HideMenues();
            StartCoroutine(AiSkipCo());
            PlayerInputMenue.instance.turnPointText.gameObject.SetActive(false);
        }
        else
        {
            //MoveGrid.instance.ShowPointsInRange(activePlayer.moveRange, activePlayer.transform.position);
            PlayerInputMenue.instance.ShowInputMenues();
            PlayerInputMenue.instance.turnPointText.gameObject.SetActive(true);
        }

        currentActionCost = 1;//set default for first start

        PlayerInputMenue.instance.UpdateTurnPointText(turnPointsRemaining);
    }

    public IEnumerator AiSkipCo()
    {
        yield return new WaitForSeconds(1f);
        EndTurn();
    }
}
