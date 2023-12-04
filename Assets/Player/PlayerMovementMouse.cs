using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovementMouse : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public List<AudioClip> dash; // Die Liste der AudioClips
    private AudioSource audioSource;
    private Playerstats playerStats;
    public float stopDistance = 0.1f, dashDistance = 2f, dashDuration = 0.5f, dashCooldown = 2f;
    private Rigidbody2D rb;
    //public int speed;
    private Vector2 targetPosition, dashDirection;
    private bool isDashing, isMoving, isStanding;
    private float dashTimer; //dashCooldownTimer;
    private Vector2 prevPosition;
    public Animator animator;
    private bool tackling = false;
    private bool canPlaySound = true; // Eine Variable, um die Wiedergabe von Klängen zu steuern

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerStats = FindObjectOfType<Playerstats>();
    }

    void Update()
    {
        if (!isDashing) targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && playerStats.dashCooldown <= 0f && isMoving)
        {
            StartCoroutine(Dash());
            animator.SetTrigger("Roll"); // Set the animation trigger here
        }
        // Check if the player is standing
        isStanding = rb.velocity.magnitude <= 0f;
        animator.SetBool("isStanding", isStanding);

        if (!isStanding)
        {
            if (canPlaySound)
            {
                StartCoroutine(PlayRandomSoundWithDelay());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (tackling == true && playerStats.tackle == true)
            {
                enemy.TakeDamage(playerStats.tackleDamage);
            }
        }
    }

    IEnumerator PlayRandomSoundWithDelay()
    {
        canPlaySound = false; // Setze die Variable, um die Wiedergabe zu verhindern
        int randomIndex = Random.Range(0, audioClips.Count);
        AudioClip randomClip = audioClips[randomIndex];

        // Spiele den zufälligen AudioClip ab
        audioSource.PlayOneShot(randomClip);

        // Warte für eine Sekunde, bevor der nächste Sound gespielt werden kann
        yield return new WaitForSeconds(0.3f);

        canPlaySound = true; // Erlaube die Wiedergabe des nächsten Sounds
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 movement = targetPosition - rb.position;
            rb.velocity = movement.magnitude < stopDistance ? Vector2.zero : movement.normalized * playerStats.movementSpeed;
        }
        else
        {
            rb.velocity = dashDirection * dashDistance / dashDuration;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                playerStats.dashCooldown = dashCooldown;
            }
        }

        // Check if the player is moving
        isMoving = (rb.position - prevPosition).magnitude > 0.01f;
        prevPosition = rb.position;

        rb.freezeRotation = true;
        rb.velocity *= 0.5f;
        playerStats.dashCooldown -= playerStats.dashCooldown > 0f ? Time.deltaTime : 0f;
    }

    IEnumerator Dash()
    {
        if (dash.Count > 0)
        {
            int randomIndex = Random.Range(0, dash.Count);
            AudioClip randomClip = dash[randomIndex];

            // Spiele den zufälligen AudioClip ab
            audioSource.PlayOneShot(randomClip);
        }
        // Start the dash
        isDashing = true;
        tackling = true;
        dashTimer = dashDuration;
        dashDirection = (targetPosition - rb.position).normalized;

        // Wait for the dash to finish
        yield return new WaitForSeconds(dashDuration);

        // Finish the dash
        tackling = false;
        isDashing = false;
        playerStats.dashCooldown = dashCooldown;
    }
}
