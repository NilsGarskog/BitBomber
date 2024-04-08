using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINumberUpdate : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI arrowText;
    public Image AliveHeadImage;
    public Image DeadHeadImage;

    // Update is called once per frame
    void Update()
    {
        bombText.text = ("X" + player.GetComponent<BombController>().bombsRemaining.ToString());
        arrowText.text = ("X" + player.GetComponent<BowController>().arrowsRemaining.ToString());

        if (player.GetComponent<Player>().currentHealth <= 0)
        {
            AliveHeadImage.enabled = false;
            DeadHeadImage.enabled = true;
        }
        else
        {
            AliveHeadImage.enabled = true;
            DeadHeadImage.enabled = false;
        }
    }
}
