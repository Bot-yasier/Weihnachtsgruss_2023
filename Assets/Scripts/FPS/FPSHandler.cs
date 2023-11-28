using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHandler : MonoBehaviour
{

    public GameObject FPSobj;
    public float targetFramerate = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentFramerate = 1.0f / Time.deltaTime;

        if(currentFramerate < targetFramerate)
        {
            FPSobj.SetActive(true);

        }
        else
        {
            FPSobj.SetActive(false);
        }
    }
}
