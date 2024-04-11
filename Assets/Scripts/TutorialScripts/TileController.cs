// TileController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public Tilemap tutorialConditionTiles; // Reference to the TutorialConditionTiles Tilemap

    void Update(){
        if(GetComponent<BombController>().destructibleCount >= 5)
        {
            tutorialConditionTiles.gameObject.SetActive(false); // Disable the TutorialConditionTiles Tilemap
        }
    }

}