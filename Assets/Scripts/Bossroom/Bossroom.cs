using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    gameManager.newDungeon = true;
                }
                GameObject playerObject = GameObject.Find("Player");

                if (playerObject != null)
                {
                    levelCounter.Levelint++;
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
