using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Debug.Log("Clicked");
        //FindObjectOfType<CharacterController>().MoveToPoint(transform.position);

        GameManager.instance.activePlayer.MoveToPoint(transform.position);
    }
}
