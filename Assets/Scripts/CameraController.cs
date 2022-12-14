using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
        moveTarget = transform.position;
    }

    public float moveSpeed, manualMoveSpeed = 5f;
    private Vector3 moveTarget;
    private Vector2 moveInput;

    private float targetRotation;
    public float rotateSpeed;
    public int currentAngle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTarget != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        //if arrows keys are pressed
        if(moveInput != Vector2.zero)
        {
            //transform.position += new Vector3(moveInput.x * manualMoveSpeed, 0f, moveInput.y * manualMoveSpeed) * Time.deltaTime;
            transform.position += ((transform.forward * (moveInput.y * manualMoveSpeed)) + (transform.right * (moveInput.x * manualMoveSpeed))) * Time.deltaTime;
            //keep camera at current position
            moveTarget = transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SetMoveTarget(GameManager.instance.activePlayer.transform.position);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            currentAngle++;
            if(currentAngle >= 4)
            {
                currentAngle = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentAngle--;
            if (currentAngle < 0)
            {
                currentAngle = 3;
            }
        }

        targetRotation = (90f * currentAngle) + 45f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, targetRotation, 0f), rotateSpeed * Time.deltaTime);
    }

    public void SetMoveTarget(Vector3 newTarget)
    {
        moveTarget = newTarget;
    }
}
