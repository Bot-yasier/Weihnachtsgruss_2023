using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Vector3 GetRandomPosition(Collider2D groundCollider)
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(groundCollider.bounds.min.x, groundCollider.bounds.max.x),
            Random.Range(groundCollider.bounds.min.y, groundCollider.bounds.max.y),
            0f
        );

        return randomPosition;
    }
}
