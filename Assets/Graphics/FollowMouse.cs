using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        // Get the current mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the Z coordinate to zero (2D space)
        worldPosition.z = 0f;

        // Update the position of the GameObject to be exactly at the mouse cursor
        transform.position = worldPosition;
    }
}
