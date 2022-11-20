using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Debug.Log("Clicked");
        //FindObjectOfType<CharacterController>().MoveToPoint(transform.position);

        if(Input.mousePosition.y > Screen.height * .1)
        {
            GameManager.instance.activePlayer.MoveToPoint(transform.position);

            //ensure only one correct move set is active
            MoveGrid.instance.HideMovePoints();
            PlayerInputMenue.instance.HideMenues();
        }
    }
}
