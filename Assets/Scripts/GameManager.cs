using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    private int[] scoreBoard = new int[4] { 10, 10, 10, 10 };
    private int scoreBoardIndex = 0;
    private List<int> activePlayerIndices = new List<int>();
    public static GameManager instance = null;
    private int numberOfPlayers;
    public GameObject endGameText;
    private void Start()
    {
        ResetActivePlayers();
    }
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void ResetActivePlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = players.Length;
        activePlayerIndices.Clear();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            activePlayerIndices.Add(i);
        }
    }
    public void HandlePlayerDeath(int playerIndex)
    {
        activePlayerIndices.Remove(playerIndex);
        setDeathIndex(playerIndex);
    }

    private void NewRound()
    {
        SceneManager.LoadScene("MainScene");
        StartCoroutine(WaitForSceneLoadAndResetPlayers());
        ResetScoreBoard();
    }
    private IEnumerator WaitForSceneLoadAndResetPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        ResetActivePlayers();
    }
    public void EndGame()
    {
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
        Invoke(nameof(NewRound), 5f);
    }
    public void setDeathIndex(int index)
    {
        if (scoreBoardIndex < scoreBoard.Length - 1)
        {
            scoreBoard[scoreBoardIndex] = index;
            scoreBoardIndex++;
        }
        if (activePlayerIndices.Count <= 1)
        {
            if (activePlayerIndices.Count == 1)
            {
                int winningPlayerIndex = activePlayerIndices[0];
                scoreBoard[scoreBoardIndex] = winningPlayerIndex;

            }
            Invoke(nameof(EndGame), 2f);
        }
        if (activePlayerIndices.Count == 2)
        {
            StartCoroutine(startEndGame());
        }
    }

    public void ResetScoreBoard()
    {
        for (int i = 0; i < scoreBoard.Length; i++)
        {
            scoreBoard[i] = 10;
        }
        scoreBoardIndex = 0;
    }
    IEnumerator startEndGame()
    {
        yield return new WaitForSeconds(5f);
        // Find the GameObject
        GameObject endGameSmallerController = GameObject.Find("EndGameSmallerController");

        // Get the script
        EndGameTiles endGameTilesScript = endGameSmallerController.GetComponent<EndGameTiles>();

        //Activate EndgameText
        endGameText.SetActive(true);

        // Call the function
        endGameTilesScript.startEndGame();
    }

    public int[] GetScoreBoard()
    {
        return scoreBoard;
    }


}
