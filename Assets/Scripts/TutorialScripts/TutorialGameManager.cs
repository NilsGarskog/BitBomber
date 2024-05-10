using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (player.GetComponent<Player>().currentHealth <= 0)
        {
            Debug.Log("Player is dead");
            SceneManager.LoadScene("Tutorial");
        }
    }
}
