using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGeschenke : MonoBehaviour
{
    public TutorialHandler tutorialHandler;
    public GameObject present;
    public bool i = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(i == true)
        {
            present.SetActive(true);
        }
        else
        {
            tutorialHandler.presentscollected = true;
        }
 
        Destroy(gameObject);

    }
}