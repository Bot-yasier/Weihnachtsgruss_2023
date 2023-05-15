using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            SceneManager.LoadScene(nextSceneName);
            Debug.Log("hallo");







        }
    }
}
