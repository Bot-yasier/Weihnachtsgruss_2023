using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour
{
    public int maxEnemys;
    public int minEnemys;
    public int doorcount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(doorcount <= 2)
        {
            maxEnemys = 1;
            minEnemys = 1;
        }
        if (doorcount <= 5 && doorcount > 2)
        {
            maxEnemys = 2;
            minEnemys = 1;
        }
        if (doorcount <= 10 && doorcount > 5)
        {
            maxEnemys = 3;
            minEnemys = 1;
        }
        if (doorcount >= 10)
        {
            maxEnemys = 5;
            minEnemys = 1;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enterence"))
        {
            doorcount++;
        }
    }

}
