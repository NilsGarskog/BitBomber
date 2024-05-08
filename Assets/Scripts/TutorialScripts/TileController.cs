// TileController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public Tilemap tutorialConditionTiles; // Reference to the TutorialConditionTiles Tilemap
    public Tilemap tutorialConditionTilesExit; // Reference to the TutorialConditionTiles2 Tilemap
    private bool exitOpened = false;
    public TMPro.TextMeshProUGUI tutorialText;

    void Update()
    {
        if (gameObject.tag != "TutorialNPC")
        {
            if (GetComponent<BombController>().destructibleCount >= 5)
            {
                tutorialConditionTiles.gameObject.SetActive(false); // Disable the TutorialConditionTiles Tilemap
            }
        }
    }

    public void ExitTutorial()
    {
        if (exitOpened == false)
        {
            tutorialConditionTilesExit.gameObject.SetActive(false); // Enable the TutorialConditionTiles2 Tilemap
            exitOpened = true;
            tutorialText.gameObject.SetActive(true);
        }
    }

}