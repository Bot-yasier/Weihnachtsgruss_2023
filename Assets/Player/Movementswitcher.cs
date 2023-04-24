using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Movementswitcher : MonoBehaviour
{


    public PlayerMovement playerMovement;
    public PlayerMovementMouse playerMovementMouse;

    private void Start()
    {
        playerMovement.enabled = false;
        playerMovementMouse.enabled = false;
    }
    public void PlayerMovementclick()
    {
        playerMovement.enabled = true;
        playerMovementMouse.enabled = false;

    }

    public void PlayerMovementMouseclick()
    {
        playerMovement.enabled = false;
        playerMovementMouse.enabled = true;

    }
}
