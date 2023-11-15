using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translation : MonoBehaviour
{
    
    public Button GermanLanguage;
    public Button FrenchLanguage;

    public GameObject Einleitung;
    public GameObject EinleitungFR;

    // Start is called before the first frame update
    void Start()
    {
        GermanLanguage.onClick.AddListener(GermanL);
        FrenchLanguage.onClick.AddListener(FrenchL);
    }

    // Update is called once per frame
    void Update()
    {
        int FrenchInt = PlayerPrefs.GetInt("French");

        if(FrenchInt == 0)
        {
            Einleitung.SetActive(true);
            EinleitungFR.SetActive(false);

        }
        if (FrenchInt == 1)
        {
            Einleitung.SetActive(false);
            EinleitungFR.SetActive(true);

        }
    }
    void GermanL()
    {
        PlayerPrefs.SetInt("French", 0);
    }
    void FrenchL()
    {
        PlayerPrefs.SetInt("French", 1);
    }
}
