using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{

    public float moveSpeed;

    private Vector3 moveTarget;

    public NavMeshAgent meshAgent;
    private bool isMoving;

    public bool isEnemy;

    public float moveRange = 3.5f, runRange = 8f;

    public float meleeRange = 1.5f;
    [HideInInspector]
    public List<CharacterController> meleeTargets = new List<CharacterController>();
    [HideInInspector]
    public int currentMeleeTarget;

    // Start is called before the first frame update
    void Start()
    {
        moveTarget = transform.position;
        meshAgent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

       // if(transform.position != moveTarget)//was for the capsule
       if(isMoving == true)
       {
            //transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);

            if(GameManager.instance.activePlayer == this)
            {
                CameraController.instance.SetMoveTarget(transform.position);
                if (Vector3.Distance(transform.position, moveTarget) < .2f)
                {
                    isMoving = false;
                    GameManager.instance.FinishMovement();
                }
            }
       }

    }

    public void MoveToPoint(Vector3 target)
    {
        moveTarget = target;

        meshAgent.SetDestination(moveTarget);
        isMoving = true;
    }

    public void GetMeleeTargets()
    {
        meleeTargets.Clear();
        if(isEnemy == false)
        {
            foreach(CharacterController cc in GameManager.instance.enemyTeam)
            {
                if(Vector3.Distance(transform.position, cc.transform.position) < meleeRange)
                {
                    meleeTargets.Add(cc);
                }
            }
        } else
        {
            foreach (CharacterController cc in GameManager.instance.playerTeam)
            {
                if (Vector3.Distance(transform.position, cc.transform.position) < meleeRange)
                {
                    meleeTargets.Add(cc);
                }
            }
        }

        //ensure current target i never null
        if(currentMeleeTarget >= meleeTargets.Count)
        {
            currentMeleeTarget = 0;
        }
    }

    public void DoMelee()
    {
        meleeTargets[currentMeleeTarget].gameObject.SetActive(false);

    }
}
