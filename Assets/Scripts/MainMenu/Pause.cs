using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button Button1;
    public PlayerMovementMouse playerMovementMouse;
    public Rigidbody2D playerrigid;
    public GameObject PauseScreen;
    bool check = false;
    bool doubleEscape = false;

    private void Start()
    {
        Button1.onClick.AddListener(Button1clicked);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(doubleEscape == false)
            {
                PauseScreen.SetActive(true);

                if (playerMovementMouse.enabled == false)
                {

                    check = true;
                    
                }
                doubleEscape = true;
                playerMovementMouse.enabled = false;
                playerrigid.constraints = RigidbodyConstraints2D.FreezePosition;
                playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;

            }


        }
    }
    void Button1clicked()
    {

        if (check == false)
        {
            playerMovementMouse.enabled = true;
            playerrigid.constraints = RigidbodyConstraints2D.None;
            playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
           
        }
        check = false;
        doubleEscape = false;
        PauseScreen.SetActive(false);


    }
}
