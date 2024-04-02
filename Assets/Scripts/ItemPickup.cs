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
        Extraskull, // 1/3
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
            case ItemType.Extraskull: // 2/3
                player.GetComponent<SkullController>().AddSkull();
                break;

            // MAKE SURE TO ALSO ADD SPAWNABLE ITEMS UNDER PREFAB DESTRUCTIBLE TO REINTRODUCE THE HAUNTED SKULL
            // AS A DROPPED ITEM // 3/3
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D triggered with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player tag confirmed, calling onItemPickup");
            onItemPickup(other.gameObject);
        }
    }
}
