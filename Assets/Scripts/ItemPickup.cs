using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        Extrabomb,
        BlastRadius,
        SpeedIncrease,
        ExtraArrow,
        PowerUp,
    }

    public ItemType Type;

    private void onItemPickup(GameObject player)
    {
        switch (Type)
        {

            case ItemType.Extrabomb:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius += 1;
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<Mover>().moveSpeed += 1;
                break;
            case ItemType.ExtraArrow:
                player.GetComponent<BowController>().arrowsRemaining += 1;
                break;
            case ItemType.PowerUp:
                player.GetComponent<Player>().Heal(20);
                break;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onItemPickup(other.gameObject);
        }
    }
}
