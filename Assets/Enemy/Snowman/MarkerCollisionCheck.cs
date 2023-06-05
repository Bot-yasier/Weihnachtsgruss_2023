using UnityEngine;
using System.Collections;

public class MarkerCollisionCheck : MonoBehaviour
{
    private bool isWaiting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isWaiting)
        {
            // Get the RandomWalkablePosition script attached to the same GameObject as the marker
            RandomWalkablePosition randomWalkablePosition = GetComponent<RandomWalkablePosition>();

            if (randomWalkablePosition != null)
            {
                StartCoroutine(WaitBeforeReposition(randomWalkablePosition));
            }
            else
            {
                Debug.LogError("RandomWalkablePosition component not found.");
            }
        }
    }

    private IEnumerator WaitBeforeReposition(RandomWalkablePosition randomWalkablePosition)
    {
        isWaiting = true;
        yield return new WaitForSeconds(2f);
        randomWalkablePosition.RepositionMarker();
        isWaiting = false;
    }
}
