using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuestionEnable : MonoBehaviour
{
    public Button QuestButton1;
    public Button QuestButton2;
    public Button QuestButton3;

    public GameObject questtext1;
    public GameObject questtext2;
    public GameObject questtext3;
    bool quest1 = false;
    bool quest2 = false;
    bool quest3 = false;
    // Start is called before the first frame update
    void Start()
    {
        QuestButton1.onClick.AddListener(Quest1);
        QuestButton2.onClick.AddListener(Quest2);
        QuestButton3.onClick.AddListener(Quest3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Quest1()
    {
        if(quest1 == false)
        {
            questtext1.SetActive(true);
            quest1 = true;
        }
        else
        {
            questtext1.SetActive(false);
            quest1 = false;
        }
   

    }
    void Quest2()
    {
        if (quest2 == false)
        {
            quest2 = true;
            questtext2.SetActive(true);
        }
        else
        {
            quest2 = false;
            questtext2.SetActive(false);
        }
      

    }
    void Quest3()
    {
        if (quest3 == false)
        {
            quest3 = true;
            questtext3.SetActive(true);
        }
        else
        {
            quest3 = false;
            questtext3.SetActive(false);
        }
       
    }

}

