using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSHandler : MonoBehaviour
{

    public GameObject FPSobj;
    public float targetFramerate = 15f;
    public Button Shut;
    bool Start1 = false;



    // Start is called before the first frame update
    void Start()
    {
        Shut.onClick.AddListener(Shut1);
        StartCoroutine(wait());
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Start1 == true)
        {
            float currentFramerate = 1.0f / Time.deltaTime;

            if (currentFramerate < targetFramerate)
            {
                FPSobj.SetActive(true);

            }
        }


    }

    void Shut1()
    {
        FPSobj.SetActive(false);
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
        Start1 = true;

    }
}
