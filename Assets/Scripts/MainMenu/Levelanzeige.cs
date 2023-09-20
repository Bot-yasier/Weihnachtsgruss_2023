using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Levelanzeige : MonoBehaviour
{
    [HideInInspector]  public LevelCounter levelCounter;
    public TextMeshProUGUI zahl;
    public GameObject Levels;
    public PlayerMovementMouse playerMovementMouse;
    public Rigidbody2D playerrigid;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        levelCounter = FindObjectOfType<LevelCounter>();
        Visibel();
    }

    // Update is called once per frame
    void Update()
    {
        if(levelCounter.Levelint == 1)
        {
            zahl.text = levelCounter.Levelint.ToString();
        }
        else
        {
            int level = levelCounter.Levelint;
            zahl.text = level.ToString();
        }
    
    }

    public void Visibel()
    {
        StartCoroutine(LoadLevel());

    }

    IEnumerator LoadLevel()
    {
        Levels.SetActive(true);
        animator.enabled = false;
        playerMovementMouse.enabled = false;
        playerrigid.constraints = RigidbodyConstraints2D.FreezePosition;
        playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(3);

        Levels.SetActive(false);
        playerMovementMouse.enabled = true;
        playerrigid.constraints = RigidbodyConstraints2D.None;
        playerrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.enabled = true;
    }
}
