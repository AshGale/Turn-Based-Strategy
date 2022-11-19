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
}
