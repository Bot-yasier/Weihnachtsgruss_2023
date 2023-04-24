using UnityEngine;

public class PlayerMovementMouse : MonoBehaviour
{
    public float speed = 130f; // Die Geschwindigkeit des Charakters
    public float stopDistance = 0.1f; // Der Schwellenwert, bei dem der Spieler gestoppt wird

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Deaktivieren der Rotation des Spielers
    }

    void Update()
    {
        // Verfolgen Sie die Mausposition
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = mousePos;
    }

    void FixedUpdate()
    {
        // Bewegen Sie den Spieler in Richtung der Mausposition
        Vector2 movement = targetPosition - rb.position;

        // Wenn die Entfernung kleiner als der Schwellenwert ist, wird der Spieler gestoppt
        if (movement.magnitude < stopDistance)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            // Überprüfen, ob movement nicht Null ist, bevor AddForce aufgerufen wird
            if (movement != Vector2.zero)
            {
                rb.AddForce(movement.normalized * speed);
            }
            else
            {
                Debug.Log("Movement vector is null!");
            }
        }

        // Deaktivieren der Rotation des Spielers
        rb.freezeRotation = true;

        rb.velocity *= 0.5f;
    }
}
