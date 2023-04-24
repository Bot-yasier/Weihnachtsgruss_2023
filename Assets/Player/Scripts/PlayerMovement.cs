using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Die Geschwindigkeit des Charakters
    public float stopDistance = 0.1f; // Der Schwellenwert, bei dem der Spieler gestoppt wird
    public float maxSpeed = 10f; // Die maximale Geschwindigkeit des Charakters

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private Vector2 lastMousePos;
    private float lastDistance;

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

        // Berechnen Sie die Entfernung und die Geschwindigkeit der Mausbewegung
        float distance = Vector2.Distance(lastMousePos, mousePos);
        float speedMultiplier = Mathf.Clamp(distance / lastDistance, 0f, 1f);

        // Aktualisieren Sie die Geschwindigkeit des Charakters
        speed = maxSpeed * speedMultiplier;

        // Speichern Sie die aktuelle Mausposition und Entfernung
        lastMousePos = mousePos;
        lastDistance = distance;
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
            rb.AddForce(movement.normalized * speed);
        }

        // Deaktivieren der Rotation des Spielers
        rb.freezeRotation = true;
    }
}
