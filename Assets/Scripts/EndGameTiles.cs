using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndGameTiles : MonoBehaviour
{
    public Tilemap endGameTiles; // Reference to the EndGameTiles Tilemap
    public GameObject blockPreFab;
    private float timeBetween = 0.4f;
    public AudioManagerMainScene audioManager;
    public GameObject endGameText;
    public GameObject[] players;
    private GameManager gameManager;
    private SpawnItemTimer spawnItemTimer;
    private bool EndGameStarted = false;
    private int playerDead;


    void Start()
    {
        playerDead = 0;
        spawnItemTimer = FindAnyObjectByType<SpawnItemTimer>();
        //gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (playerDead >= 2 && !EndGameStarted && spawnItemTimer.timer)
        {
            EndGameStarted = true;
            StartCoroutine(startEndGame());
        }
    }

    public void addDeath()
    {
        playerDead++;
    }


    IEnumerator startEndGame()
    {
        yield return new WaitForSeconds(5f);
        endGameText.SetActive(true);

        audioManager.suddenDeath();
        for (int i = -5; i < 6; i++)
        {
            Instantiate(blockPreFab, new Vector2(-6, i), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = -5; i < 7; i++)
        {
            Instantiate(blockPreFab, new Vector2(i, 5), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = 5; i > -6; i--)
        {
            Instantiate(blockPreFab, new Vector2(6, i), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = 6; i > -6; i--)
        {
            Instantiate(blockPreFab, new Vector2(i, -5), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        // Second layer
        for (int i = -4; i < 5; i++)
        {
            Instantiate(blockPreFab, new Vector2(-5, i), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = -4; i < 6; i++)
        {
            Instantiate(blockPreFab, new Vector2(i, 4), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = 4; i > -5; i--)
        {
            Instantiate(blockPreFab, new Vector2(5, i), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = 5; i > -5; i--)
        {
            Instantiate(blockPreFab, new Vector2(i, -4), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        // Third layer
        for (int i = -3; i < 4; i++)
        {
            Instantiate(blockPreFab, new Vector2(-4, i), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = -3; i < 5; i++)
        {
            Instantiate(blockPreFab, new Vector2(i, 3), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = 3; i > -4; i--)
        {
            Instantiate(blockPreFab, new Vector2(4, i), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
        for (int i = 4; i > -4; i--)
        {
            Instantiate(blockPreFab, new Vector2(i, -3), Quaternion.identity);
            yield return new WaitForSeconds(timeBetween);
        }
    }
}
