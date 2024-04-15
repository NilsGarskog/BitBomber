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
    public TextMeshProUGUI EnergyText;
    public TextMeshProUGUI SkullText;
    public Image AliveHeadImage;
    public Image DeadHeadImage;
    public Slider slider;
    public Slider ShieldSlider;

    // Update is called once per frame
    void Update()
    {
        bombText.text = ("X" + player.GetComponent<BombController>().bombsRemaining.ToString());
        arrowText.text = ("X" + player.GetComponent<BowController>().arrowsRemaining.ToString());
        EnergyText.text = ("X" + player.GetComponent<EnergyBallController>().energyBallAmount.ToString());
        SkullText.text = ("X" + player.GetComponent<SkullController>().skullsRemaining.ToString());
        slider.value = player.GetComponent<Player>().currentHealth;
        ShieldSlider.value = (player.GetComponent<ShieldController>().currentShieldTime / player.GetComponent<ShieldController>().baseShieldTime);

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
