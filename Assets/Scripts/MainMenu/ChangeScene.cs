using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
  
   
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScenes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangeScenes()
    {
        SceneManager.LoadScene(sceneName);
    }
}
