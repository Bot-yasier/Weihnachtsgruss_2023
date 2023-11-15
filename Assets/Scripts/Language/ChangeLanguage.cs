using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChangeLanguage : MonoBehaviour
{

    public TextMeshProUGUI TMPButton;
    public Text NButton;

    public string German;
    public string French;

    public bool TMP = false;
    public bool Normal = false;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        int FrenchInt = PlayerPrefs.GetInt("French");
        if (TMP == true)
        {
            if(FrenchInt == 0)
            {
                TMPButton.text = German;
            }
            if (FrenchInt == 1)
            {
                TMPButton.text = French;
            }

        }
        if (Normal == true)
        {
            if (FrenchInt == 0)
            {
                NButton.text = German;
            }
            if (FrenchInt == 1)
            {
                NButton.text = French;
            }

        }
    }
}
