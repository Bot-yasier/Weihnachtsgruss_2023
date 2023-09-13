using UnityEngine;

public class FollowYRotation : MonoBehaviour
{
    [Header("Minimap rotation")]
    public Transform playerReference;
    public float playerOffset = 10f;



    private void Update()
    {
        if(playerReference != null)
        {
            transform.position = new Vector3(playerReference.position.x, playerReference.position.y, playerReference.position.z + playerOffset);
        }
    }

}
