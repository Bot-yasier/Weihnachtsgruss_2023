using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int startingHealth = 6;
    public int currentHealth;
    public GameObject Player;

    public GameObject hard1;
    public GameObject hard2;
    public GameObject hard3;
    public GameObject hard4;
    public GameObject hard5;
    public GameObject hard6;
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;
        if (currentHealth == 5) {hard6.SetActive(false); }
        if(currentHealth == 4) {hard5.SetActive(false); }
        if (currentHealth == 3) { hard4.SetActive(false); }
        if (currentHealth == 2) { hard3.SetActive(false); }
        if (currentHealth == 1) { hard2.SetActive(false); }
        if (currentHealth == 0) { hard1.SetActive(false); Destroy(Player); }

    }

}
