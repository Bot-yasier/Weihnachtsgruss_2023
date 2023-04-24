using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 130f; // Die Geschwindigkeit des Spielers

    private Rigidbody2D rb; // Eine Referenz auf den Rigidbody2D des Spielers

   


    // Start wird einmal aufgerufen, wenn das Spiel startet
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Referenz auf den Rigidbody2D des Spielers holen
    }

    // FixedUpdate wird in regelm��igen Abst�nden aufgerufen, um Physik-Berechnungen durchzuf�hren
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Setzen Sie die maximale Geschwindigkeit des Charakters
        float maxSpeed = 130f;

        // Beschr�nken Sie die Geschwindigkeit des Charakters auf die maximale Geschwindigkeit
        movement = Vector2.ClampMagnitude(movement, maxSpeed);

        // F�gen Sie die Kraft hinzu, die den Charakter in die gew�nschte Richtung bewegt
        rb.AddForce(movement * speed);

        rb.velocity *= 0.5f;
       
    }
}
