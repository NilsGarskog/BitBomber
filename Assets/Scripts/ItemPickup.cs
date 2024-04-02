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
        EnergyBall,
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
            case ItemType.EnergyBall:
                player.GetComponent<EnergyBallController>().AddEnergyBall();
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
