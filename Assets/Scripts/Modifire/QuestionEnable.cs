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
        questtext1.SetActive(true);

    }
    void Quest2()
    {
        questtext2.SetActive(true);

    }
    void Quest3()
    {

        questtext3.SetActive(true);
    }

}

