using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float moveSpeed;

    private Vector3 moveTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

        transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
    }

    public void MoveToPoint(Vector3 target)
    {
        moveTarget = target;
    }
}
