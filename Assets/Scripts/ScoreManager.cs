using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI[] positionTextFields;

    public int maxPlayersToShow;

    public Sprite[] playerSpritesArray;
    public GameObject[] playerObjects;

    void Awake()
    {
        int[] scoreBoard = GameManager.instance.GetScoreBoard();
        int[] scoreBoardWithout10 = Array.FindAll(scoreBoard, score => score != 10);
        maxPlayersToShow = scoreBoardWithout10.Length;
        UpdateScoreboardVisibility();
        UpdateScoreboardText();
        UpdatePlayerSprites(scoreBoardWithout10);
    }


    void UpdateScoreboardVisibility()
    {
        for (int i = maxPlayersToShow; i < positionTextFields.Length; i++)
        {
            positionTextFields[i].gameObject.SetActive(false);
        }
        for (int i = maxPlayersToShow; i < playerObjects.Length; i++)
        {
            playerObjects[i].SetActive(false);
        }
    }
    void UpdateScoreboardText()
    {
        for (int i = 0; i < positionTextFields.Length; i++)
        {
            positionTextFields[i].text = $"{i + 1}";
        }
    }
    void UpdatePlayerSprites(int[] scoreBoardWithout10)
    {
        int[] reversedScoreBoard = scoreBoardWithout10.Reverse().ToArray();
        int maxPlayers = Mathf.Min(maxPlayersToShow, Mathf.Min(reversedScoreBoard.Length, playerObjects.Length));

        for (int i = 0; i < maxPlayers; i++)
        {
            SpriteRenderer spriteRenderer = playerObjects[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                int spriteIndex = Mathf.Clamp(reversedScoreBoard[i], 0, playerSpritesArray.Length - 1);
                spriteRenderer.sprite = playerSpritesArray[spriteIndex];
            }
            else
            {
                Debug.LogWarning("SpriteRenderer not found ");
            }
        }
    }
}
