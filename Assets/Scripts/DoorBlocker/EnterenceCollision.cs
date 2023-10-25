using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterenceCollision : MonoBehaviour
{
    public bool finalroom = false;
    bool Minimapsymbol = false;
    public GameObject Symbol;
    public DoorBlockerHandler doorBlockerHandler;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            doorBlockerHandler.PlayerCollWithEnterence();

            if(finalroom == true)
            {
              
                StartCoroutine(wait());
            }
        }
    }

    private void Update()
    {
        if(finalroom == true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0 && Minimapsymbol)
            {
                Symbol.SetActive(true);
            }

        }


    }


    private IEnumerator wait()
    {
        yield return new WaitForSeconds(4f);
        Minimapsymbol = true;
    }
}
