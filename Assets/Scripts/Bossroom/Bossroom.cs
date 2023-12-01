using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bossroom : MonoBehaviour
{
    public DungeonBuilder dungeonBuilder;
    public GameManager gameManager;
    [HideInInspector] public GameState gameState;
    public LevelCounter levelCounter;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        dungeonBuilder = FindObjectOfType<DungeonBuilder>();
        gameState = GameState.gameStarted;
        gameManager = FindObjectOfType<GameManager>();
        levelCounter = FindObjectOfType<LevelCounter>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                i++;
                if(i == 1)
                {
                    TextMeshProUGUI scoreTextMesh = GameObject.FindGameObjectWithTag("Zahl").GetComponent<TextMeshProUGUI>();
                    int score = 500;
                    int currentScore = 0;
                    if (!string.IsNullOrEmpty(scoreTextMesh.text))
                    {
                        currentScore = int.Parse(scoreTextMesh.text);
                    }

                    currentScore += score;
                    scoreTextMesh.text = currentScore.ToString();
                    gameManager.newDungeon = true;
                }
                GameObject playerObject = GameObject.Find("Player");

                if (playerObject != null)
                {
                    levelCounter.Levelint++;



                    // Finde alle GameObjects mit "Present" im Namen
                    GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("Package");

                    // Lösche jedes gefundene GameObject
                    foreach (GameObject obj in objectsToDelete)
                    {
                        Destroy(obj);
                    }



                    // Access the Transform component of the "Player" GameObject
                    Transform playerTransform = playerObject.transform;

                    // Set the position of the player to (0, 0, currentZ) to keep the current z-coordinate
                    Vector3 newPosition = new Vector3(0, 0, playerTransform.position.z);
                    playerTransform.position = newPosition;

                   
                }
            }
        }
    }
}
