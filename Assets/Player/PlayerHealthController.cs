using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public Animator animator;
    public PlayerMovementMouse playerMovementMouse;
    public Rigidbody2D playerrigid;
    public GameObject PlayerSprite;
    public int startingHealth = 6;
    public int currentHealth;
    public GameObject Player;
    public GameObject Menu;

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
    private void Update()
    {
        if (currentHealth == 6) { hard1.SetActive(true);  hard2.SetActive(true); hard3.SetActive(true); hard4.SetActive(true); hard5.SetActive(true); hard6.SetActive(true); }
        if (currentHealth == 5) { hard6.SetActive(false); hard1.SetActive(true); hard2.SetActive(true); hard3.SetActive(true); hard4.SetActive(true); hard5.SetActive(true); }
        if (currentHealth == 4) { hard5.SetActive(false); hard6.SetActive(false); hard1.SetActive(true); hard2.SetActive(true); hard3.SetActive(true); hard4.SetActive(true); }
        if (currentHealth == 3) { hard4.SetActive(false); hard5.SetActive(false); hard6.SetActive(false); hard1.SetActive(true); hard2.SetActive(true); hard3.SetActive(true); }
        if (currentHealth == 2) { hard3.SetActive(false); hard4.SetActive(false); hard5.SetActive(false); hard6.SetActive(false); hard1.SetActive(true); hard2.SetActive(true); }
        if (currentHealth == 1) { hard2.SetActive(false); hard3.SetActive(false); hard4.SetActive(false); hard5.SetActive(false); hard6.SetActive(false); hard1.SetActive(true); }
        if (currentHealth == 0) { hard1.SetActive(false); hard2.SetActive(false); hard3.SetActive(false); hard4.SetActive(false); hard5.SetActive(false); hard6.SetActive(false); Menu.SetActive(true);

            animator.enabled = false;
            playerMovementMouse.enabled = false;
            playerrigid.constraints = RigidbodyConstraints2D.FreezePosition;
            playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            GameObject[] enemyAmmoObjects = GameObject.FindGameObjectsWithTag("EnemyAmmo");
            PlayerSprite.SetActive(false);

            foreach (GameObject enemyAmmoObject in enemyAmmoObjects)
            {
                Destroy(enemyAmmoObject);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount;
      

    }

}
