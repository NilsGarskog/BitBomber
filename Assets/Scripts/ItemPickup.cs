using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private float shieldTime = 7f;
    public enum ItemType
    {
        Extrabomb,
        BlastRadius,
        SpeedIncrease,
        ColdShield,
    }
    private int coldShieldOrderInLayer = 6;
    public ItemType Type;

    private IEnumerator ActivateShield(GameObject player)
    {
       player.GetComponent<Mover>().isShielded = true;
       yield return new WaitForSeconds(shieldTime);
       player.GetComponent<Mover>().isShielded = false;
    }
    private void onItemPickup(GameObject player)
    {
        switch (Type)
        {

            case ItemType.Extrabomb:
                player.GetComponent<BombController>().AddBomb();
                Destroy(gameObject);
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius += 1;
                Destroy(gameObject);
                break;
            case ItemType.SpeedIncrease:
                player.GetComponent<Mover>().moveSpeed += 1;
                Destroy(gameObject);
                break;

            case ItemType.ColdShield:
                StartCoroutine(ActivateShield(player));
                StartCoroutine(DestroyAfterDelay(gameObject));
                Transform playerTransform = player.transform;
                transform.SetParent(playerTransform);
                SpriteRenderer itemRenderer = GetComponent<SpriteRenderer>();
                if(itemRenderer != null)
                {
                    itemRenderer.sortingOrder = coldShieldOrderInLayer;
                }
                break;
        }
        
    }
    private void Update()
    {
        if(Type == ItemType.ColdShield && transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(shieldTime);
        Destroy(obj);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onItemPickup(other.gameObject);
        }
    }
}
