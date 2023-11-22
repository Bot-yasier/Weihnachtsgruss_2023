using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterenceCollision : MonoBehaviour
{
    public bool finalroom = false;
    bool Minimapsymbol = false;
    public GameObject Symbol;
    public DoorBlockerHandler doorBlockerHandler;
    public GameObject Haufen;
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
        if(doorBlockerHandler.HaufenSpawn == true)
        {
            Haufen.SetActive(true);

        }

        GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies1.Length == 0)
        {
            Haufen.SetActive(false);
        }

        if (finalroom == true)
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
        yield return new WaitForSeconds(0f);
        Minimapsymbol = true;
    }
}
