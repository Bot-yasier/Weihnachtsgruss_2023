using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public HttpManager httpManager; // Reference to HttpManager

    string playerName = "Player 1";
    int jumpscore = 10;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(jumpscore);

            // Call SendScore method from HttpManager
            httpManager.SendScore(playerName, jumpscore);
        }
    }
}
