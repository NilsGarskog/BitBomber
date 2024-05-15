using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndGameTiles : MonoBehaviour
{
    public Tilemap endGameTiles; // Reference to the EndGameTiles Tilemap
    public GameObject blockPreFab;
    private float timeBetween = 0.4f;

    /*
        void Start()
        {
            StartCoroutine(CreateBlocks());

        }
    */

    public void startEndGame()
    {
        StartCoroutine(CreateBlocks());
    }

    IEnumerator CreateBlocks()
    {
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

    // Update is called once per frame
    void Update()
    {

    }
}
