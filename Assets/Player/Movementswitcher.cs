using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Movementswitcher : MonoBehaviour
{


    public PlayerMovementKeyboard playerMovementKeyboard;
    public PlayerMovementMouse playerMovementMouse;


    private void Start()
    {


        playerMovementKeyboard.enabled = false;
        playerMovementMouse.enabled = false;
    }
    public void PlayerMovementKeyboard()
    {
        playerMovementKeyboard.enabled = true;
        playerMovementMouse.enabled = false;

    }

    public void PlayerMovementMouse()
    {
        playerMovementKeyboard.enabled = false;
        playerMovementMouse.enabled = true;

    }
}
