using UnityEngine;

public class LoadSceneOnCollision : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Application.LoadLevel(nextSceneName);
        }
    }
}
